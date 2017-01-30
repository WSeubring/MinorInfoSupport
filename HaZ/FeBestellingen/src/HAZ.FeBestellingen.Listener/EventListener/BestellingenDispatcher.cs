using HaZ.DSBestellingenBeheer.Outgoing.Events;
using HAZ.FeBestellingen.Domain.Entities;
using HAZ.FeBestellingen.Domain.Services;
using HAZ.FeBestellingen.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Listener.EventListener
{
    public class BestellingenDispatcher : EventDispatcher
    {
        public long EventsReceived = 0;
        private DbContextOptions<BestellingenContext> _dbContext;
        private ILogger<BestellingenDispatcher> _logger;

        public BestellingenDispatcher(BusOptions options, DbContextOptions<BestellingenContext> dbContext, ILoggerFactory loggerFactory) : base(options)
        {
            _logger = loggerFactory.CreateLogger<BestellingenDispatcher>();
            _dbContext = dbContext;
        }

        public void BestellingToegevoegdEventReceived(BestellingToegevoegdEvent bestellingToegevoegdEvent)
        {
            try
            {
                _logger.LogDebug($"BestellingToegevoegdEvent received, with Bestelnummer: {bestellingToegevoegdEvent.Bestelnummer}");
                Interlocked.Increment(ref EventsReceived);
                var bestelling = new Bestelling();

                _logger.LogDebug($"Starting to map Event into Bestelling, with Bestelnummer: {bestellingToegevoegdEvent.Bestelnummer}");
                bestelling.ReplayBestellingToegevoegdEvent(bestellingToegevoegdEvent);
                _logger.LogDebug($"Event mapped into Bestelling with Bestelnummer: {bestelling.Bestelnummer}");

                _logger.LogDebug($"Trying to Connect to Database");
                using (IBestellingService bestellingService = new BestellingService(
                    new BestellingRepository(new BestellingenContext(_dbContext)), new EventPublisher(BusOptions.CreateFromEnvironment())))
                {
                    _logger.LogDebug($"Connected to Database");
                    _logger.LogDebug($"Trying to Add Bestelling, with Bestelnummer: {bestelling.Bestelnummer}");
                    bestellingService.AddBestelling(bestelling);
                    _logger.LogInformation($"Bestelling Added to Database with Bestelnummer: {bestelling.Bestelnummer}");
                    _logger.LogDebug($"Disconnecting from Database");
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Listener Event error occured at: {bestellingToegevoegdEvent.Timestamp} \n routingkey: {bestellingToegevoegdEvent.RoutingKey} \n errormessage: {e.Message}");
            }
        }
    }
}
