using HAZ.FeBestellingen.Infrastructure.Repositories;
using HAZ.FeBestellingen.Listener.EventListener;
using InfoSupport.WSA.Infrastructure;
using InfoSupport.WSA.Logging.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Listener
{
    public class Program : IDisposable
    {
        private ILogger<Program> _logger;
        private BestellingenDispatcher _bestellingenDispatcher;

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
                    ReplayEvents(loggerFactory);
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
            var bestellingenBusoptions = BusOptions.CreateFromEnvironment();
            bestellingenBusoptions.QueueName = "bestellingenDispatcherQueue";

            _bestellingenDispatcher = new BestellingenDispatcher(bestellingenBusoptions, StartupHelper.GetDbContext(Configuration), loggerFactory);
            _bestellingenDispatcher.Open();
        }

        public void StopDispatchers()
        {
            _bestellingenDispatcher.Dispose();
        }

        public void ReplayEvents(ILoggerFactory loggerFactory)
        {
            _logger.LogInformation("Replay Events");

            var replayEndpoint = Environment.GetEnvironmentVariable("startup-replay-endpoint") ?? "ReplayService";
            if (replayEndpoint != null)
            {
                var replayBusOptions = BusOptions.CreateFromEnvironment();
                replayBusOptions.QueueName = "febestellingen.listener.replay";
                replayBusOptions.ExchangeName = "HAZ.PsWinkelen.Listener.Exchange";

                using (var listener = new BestellingenDispatcher(replayBusOptions, StartupHelper.GetDbContext(Configuration), loggerFactory))
                using (var auditlogproxy = new MicroserviceProxy(replayEndpoint, replayBusOptions))
                {
                    listener.Open();

                    var replayCommand = new ReplayEventsCommand
                    {
                        ExchangeName = replayBusOptions.ExchangeName,
                        RoutingKeyExpression = "HaZ.DSBestellingenBeheer.BestellingToegevoegd"
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
            _bestellingenDispatcher?.Dispose();
        }
    }
}