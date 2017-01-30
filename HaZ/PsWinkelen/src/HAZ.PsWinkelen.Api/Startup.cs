using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HAZ.PsWinkelen.Api.Data;
using HAZ.PsWinkelen.Api.Models;
using HAZ.PsWinkelen.Api.Services;
using Swashbuckle.Swagger.Model;
using Serilog;
using HAZ.PsWinkelen.Infrastructure.Repositories;
using HAZ.PsWinkelen.Domain.Services;
using HAZ.PsWinkelen.Domain.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HAZ.PsWinkelen.Domain.Infrastructure.Agents;
using HAZ.PsWinkelen.Infrastructure.Agents;

namespace HAZ.PsWinkelen.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSwaggerGen();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //swagger
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Winkelen process service",
                    Description = "De service die het winkelproces beheert.",
                    TermsOfService = "None"
                });
            });

            BestellingenBeheerAgentConfig bestellingenBeheerServiceAgent = new BestellingenBeheerAgentConfig()
            {
                BestellingenBeheerServiceUri = new Uri(Environment.GetEnvironmentVariable("ds-bestellingen-beheer-service"))
            };
            services.AddSingleton(bestellingenBeheerServiceAgent);
            services.AddScoped<IBestellingenBeheerServiceAgent, BestellingenBeheerServiceAgent>();

            services.AddScoped<IArtikelRepository, ArtikelRepository>();
            services.AddScoped<IArtikelService, ArtikelService>();

            services.AddScoped<IPsWinkelenContext, PsWinkelenContext>();
            services.AddDbContext<PsWinkelenContext>
            (
                options => options.UseMySql(Configuration.GetConnectionString("DataAccessMysqlProvider"))
            );

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.ConfigurationSection(Configuration.GetSection("SerilogDebug"))
                .ReadFrom.ConfigurationSection(Configuration.GetSection("SerilogError"))
                .CreateLogger();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            loggerFactory.AddSerilog();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
