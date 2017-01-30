using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using HAZ.PsWinkelen.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using Serilog;
using Microsoft.EntityFrameworkCore;
using System.IO;
using HAZ.PsWinkelen.Listener.EventListener;
using HAZ.PsWinkelen.Domain.Services;
using System;
using InfoSupport.WSA.Logging.Model;
using System.Threading;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Listener
{
    public class Program : IDisposable
    {
        private ILogger<Program> _logger;
        private CatalogDispatcher _catalogDispatcher;

        public static void Main(string[] args)
        {
            var program = new Program();
            program.Start();
        }

        public IConfigurationRoot Configuration { get; private set; }

        public void Start()
        {
            LoadConfig();

            var loggerFactory = LoadLoggerFactory();
            _logger = loggerFactory.CreateLogger<Program>();

            StartupHelper.WaitForMysql(_logger, Configuration);
            StartupHelper.WaitForRabbitMQ(_logger);

            var replay = Environment.GetEnvironmentVariable("replay-events") != "false";
            if (replay)
            {
                new Task(() =>
                {
                    ReplayEvents();
                }).Start();
            }
            StartDispatcher(loggerFactory);
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
                .AddJsonFile(@"./appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void StartDispatcher(ILoggerFactory loggerFactory)
        {
            _logger.LogInformation("Starting Dispatchers");
            var busoptions = BusOptions.CreateFromEnvironment();
            busoptions.QueueName = "HAZ.PsWinkelen.Listener";

            _catalogDispatcher = new CatalogDispatcher(busoptions, StartupHelper.GetDbContext(Configuration), loggerFactory);
            _catalogDispatcher.Open();


        }

        public void ReplayEvents()
        {
            _logger.LogInformation("Replay Events");

            var replayEndpoint = Environment.GetEnvironmentVariable("startup-replay-endpoint") ?? "ReplayService";
            if (replayEndpoint != null)
            {
                var replayBusOptions = BusOptions.CreateFromEnvironment();
                replayBusOptions.QueueName = "HAZ.PsWinkelen.Listener.Replay";
                replayBusOptions.ExchangeName = "HAZ.PsWinkelen.Listener.Exchange";

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

        public void Dispose()
        {
            _catalogDispatcher?.Dispose();
        }
    }
}
