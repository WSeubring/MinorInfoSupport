using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HAZ.FeBestellingen.Api.Data;
using HAZ.FeBestellingen.Api.Models;
using HAZ.FeBestellingen.Api.Services;
using Swashbuckle.Swagger.Model;
using Serilog;
using HAZ.FeBestellingen.Domain.Services;
using HAZ.FeBestellingen.Domain.Infrastructure.Repositories;
using HAZ.FeBestellingen.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using HAZ.FeBestellingen.Domain.Infrastructure.Agents;
using HAZ.FeBestellingen.Infrastructure.Agents;

namespace HAZ.FeBestellingen.Api
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            //Dependency Injection
            services.AddSingleton(BusOptions.CreateFromEnvironment());
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<IBestellingService, BestellingService>();
            services.AddScoped<IBestellingRepository, BestellingRepository>();
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<IMagazijnBeheerAgent, MagazijnBeheerAgent>();

            services.AddDbContext<BestellingenContext>
            (
                options => options.UseMySql(Configuration.GetConnectionString("DataAccessMysqlProvider"))
            );

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // Add swagger
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Kantilever BackOffice Bestellingen Service",
                    Description = "Kantilever BackOffice Bestellingen Service",
                    TermsOfService = "None"
                });
            });
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "Intranet/{controller=Intranet}/{action=Index}/{id?}");
            });

            // Use swagger
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
