using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Lapiwe.OnderhoudService.Infrastructure;
using Lapiwe.Common.Infastructure;
using Lapiwe.EventBus.Publishers;
using Swashbuckle.Swagger.Model;
using MySQL.Data.Entity.Extensions;

namespace Lapiwe.OnderhoudService.Facade
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            var connectionString = Configuration.GetConnectionString("DataAccessMySqlProvider") ?? "server=db;userid=admin;password=1234;database=onderhoud";

            services.AddDbContext<OnderhoudContext>(context => context.UseMySQL(connectionString));

            services.AddScoped<IRepository, OnderhoudRepository>();
            services.AddScoped<IEventPublisher, EventPublisher>();

            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "OnderhoudOpdracht Service",
                    TermsOfService = "&copy; Lapiwe"
                });
            });


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseSwagger();
            app.UseSwaggerUi();

            app.UseMvc();
        }
    }
}
