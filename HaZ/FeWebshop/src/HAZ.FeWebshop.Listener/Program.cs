using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using HAZ.FeWebshop.Infrastructure.Repositories;
using HAZ.FeWebshop.Listener.EventListener;
using Serilog;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.IO;
using InfoSupport.WSA.Logging.Model;

namespace HAZ.FeWebshop.Listener
{
    public class Program
    {
        private ILogger<Program> _logger;
        private CatalogDispatcher _catalogDispatcher;
        private MagazijnDispatcher _magazijnDispatcher;

        public static void Main(string[] args)
        {            
            var program = new Program();
            program.Start();
        }

        public IConfigurationRoot Configuration { get; private set; }

        public void Start(bool replay = true)
        {
            LoadConfig();
            
            var loggerFactory = LoadLoggerFactory();
            _logger = loggerFactory.CreateLogger<Program>();

            StartupHelper.WaitForMysql(_logger, Configuration);
            StartupHelper.WaitForRabbitMQ(_logger);

            if (replay)
            {
                new Task(() =>
                {
                    ReplayEventsCatalogDispatcher();
                    ReplayEventsMagazijnDispatcher();
                }).Start();
            }
            StartDispatchers(loggerFactory);
        }

        private ILoggerFactory LoadLoggerFactory()
        {
            var loggerFactory = new LoggerFactory();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.ConfigurationSection(Configuration.GetSection("SerilogDebug"))
                .ReadFrom.ConfigurationSection(Configuration.GetSection("SerilogError"))
                .CreateLogger();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog(Log.Logger);

            return loggerFactory;
        }

        public void LoadConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"./appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void StartDispatchers(ILoggerFactory loggerFactory)
        {
            _logger.LogInformation("Starting Dispatchers");
            
            var catalogBusoptions = BusOptions.CreateFromEnvironment();
            catalogBusoptions.QueueName = "catalogDispatcherQueue";

            _catalogDispatcher = new CatalogDispatcher(catalogBusoptions, StartupHelper.GetDbContext(Configuration), loggerFactory);
            _catalogDispatcher.Open();

            var magazijnBusoptions = BusOptions.CreateFromEnvironment();
            catalogBusoptions.QueueName = "magazijnDispatcherQueue";
            _magazijnDispatcher = new MagazijnDispatcher(magazijnBusoptions, StartupHelper.GetDbContext(Configuration), loggerFactory);
            _magazijnDispatcher.Open();
        }

        public void StopDispatchers()
        {
            _catalogDispatcher.Dispose();
            _magazijnDispatcher.Dispose();
        }

        public void ReplayEventsCatalogDispatcher()
        {
            _logger.LogInformation("Replay Events");

            var replayEndpoint = Environment.GetEnvironmentVariable("startup-replay-endpoint") ?? "ReplayService";
            if (replayEndpoint != null)
            {
                var replayBusOptions = BusOptions.CreateFromEnvironment();
                replayBusOptions.QueueName = "catalogDispatcherQueueReplay";
                replayBusOptions.ExchangeName = "HAZ.FeWebshop.Listener.Catalog";

                using (var listener = new CatalogDispatcher(replayBusOptions, StartupHelper.GetDbContext(Configuration), LoadLoggerFactory()))
                using (var auditlogproxy = new MicroserviceProxy(replayEndpoint, replayBusOptions))
                {
                    listener.Open();

                    var replayCommand = new ReplayEventsCommand
                    {
                        ExchangeName = replayBusOptions.ExchangeName,
                        RoutingKeyExpression = "Kantilever.Catalogusbeheer.#"
                    };

                    _logger.LogInformation($"Start replaying Events on Exchange={replayCommand.ExchangeName}...");
                    ReplayResult result = auditlogproxy.Execute<ReplayResult>(replayCommand);
                    
                    _logger.LogInformation($"Receiving {result.Count} events:");
                    while (listener.EventsReceived < result.Count)
                    {
                        Thread.Sleep(100);
                    }
                    
                    Console.WriteLine("Done replaying events.");
                }
            }
        }

        public void ReplayEventsMagazijnDispatcher()
        {
            _logger.LogInformation("Replay Events");

            var replayEndpoint = Environment.GetEnvironmentVariable("startup-replay-endpoint") ?? "ReplayService";
            if (replayEndpoint != null)
            {
                var replayBusOptions = BusOptions.CreateFromEnvironment();
                replayBusOptions.QueueName = "magazijnDispatcherQueueReplay";
                replayBusOptions.ExchangeName = "HAZ.FeWebshop.Listener.Magazijn";

                using (var listener = new MagazijnDispatcher(replayBusOptions, StartupHelper.GetDbContext(Configuration), LoadLoggerFactory()))
                using (var auditlogproxy = new MicroserviceProxy(replayEndpoint, replayBusOptions))
                {
                    listener.Open();

                    var replayCommand = new ReplayEventsCommand
                    {
                        ExchangeName = replayBusOptions.ExchangeName,
                        RoutingKeyExpression = "Kantilever.Magazijnbeheer.#"
                    };

                    _logger.LogInformation($"Start replaying Events on Exchange={replayCommand.ExchangeName}...");
                    ReplayResult result = auditlogproxy.Execute<ReplayResult>(replayCommand);
                    _logger.LogInformation($"Receiving {result.Count} events:");
                    while (listener.EventsReceived < result.Count)
                    {
                        Thread.Sleep(100);
                    }
                    Console.WriteLine("Done replaying events.");
                }
            }
        }


    }
}
