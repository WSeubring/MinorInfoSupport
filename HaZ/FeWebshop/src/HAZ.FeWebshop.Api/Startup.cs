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
using HAZ.FeWebshop.Api.Data;
using HAZ.FeWebshop.Api.Models;
using HAZ.FeWebshop.Api.Services;
using HAZ.FeWebshop.Infrastructure.Agents;
using HAZ.FeWebshop.Domain.Services;
using HAZ.FeWebshop.Domain.Infrastructure.Agents;
using Serilog;
using HAZ.FeWebshop.Domain.Infrastructure.Repositories;
using HAZ.FeWebshop.Infrastructure.Repositories;
using Swashbuckle.Swagger.Model;

namespace HAZ.FeWebshop.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);


            // Dependency Injection
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            WinkelAgentConfig winkelAgentConfig = new WinkelAgentConfig()
            {
                WinkelenServiceUri = new Uri(Configuration.GetConnectionString("WinkelenServiceUri"))
            };
            services.AddSingleton(winkelAgentConfig);
            services.AddScoped<IWinkelenAgent, WinkelenAgent>();
            services.AddScoped<IArtikelRepository, ArtikelRepository>();
            services.AddScoped<IBestellingService, BestellingService>();
            services.AddScoped<IArtikelService, ArtikelService>();

            services.AddDbContext<DbContext>(
                options => options.UseMySql(Configuration.GetConnectionString("DataAccessMysqlProvider"))
            );

            services.AddDbContext<WebshopContext>
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
                    Title = "Kantilever Webshop Service",
                    Description = "Kantilever Webshop Service",
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
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Use swagger
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
