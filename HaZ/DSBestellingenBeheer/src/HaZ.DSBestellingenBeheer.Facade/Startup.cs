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
using HaZ.DSBestellingenBeheer.Facade.Data;
using HaZ.DSBestellingenBeheer.Facade.Models;
using HaZ.DSBestellingenBeheer.Facade.Services;
using Swashbuckle.Swagger.Model;
using Serilog;
using HaZ.DSBestellingenBeheer.Services.Interfaces;
using HaZ.DSBestellingenBeheer.Entities;
using HaZ.DSBestellingenBeheer.Infrastructure.Repositories;
using HaZ.DSBestellingenBeheer.Services;
using HaZ.DSBestellingenBeheer.Infrastructure.DAL;
using InfoSupport.WSA.Infrastructure;

namespace HaZ.DSBestellingenBeheer.Facade
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

            services.AddDbContext<BestellingContext>(
                options => options.UseMySql(Configuration.GetConnectionString("DataAccessMysqlProvider"))
            );

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "DS Bestellingen Beheer Service",
                    Description = "De service die de bestellingen beheert van Kantilever.",
                    TermsOfService = "None"
                });
            });

            services.AddSingleton(BusOptions.CreateFromEnvironment());
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<IRepository<Bestelling, int>, BestellingRepository>();
            services.AddScoped<BestellingService, BestellingService>();

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

            //app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
