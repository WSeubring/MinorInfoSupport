using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Domain.Services;
using HAZ.FeWebshop.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using Kantilever.Catalogusbeheer.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Listener.EventListener
{
    public class CatalogDispatcher : EventDispatcher
    {
        public long EventsReceived = 0;
        private DbContextOptions<WebshopContext> _dbContext;
        private ILogger<CatalogDispatcher> _logger; 

        public CatalogDispatcher(BusOptions options, DbContextOptions<WebshopContext> dbContext, ILoggerFactory loggerFactory) : base(options)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<CatalogDispatcher>();
        }

        public void ArtikelAanCatalogusToegevoegd(ArtikelAanCatalogusToegevoegd artikelAanCatalogusToegevoegdEvent)
        {
            try
            {
                _logger.LogDebug("ArtikelAanCatalogusToegevoegdEvent ontvangen");

                Interlocked.Increment(ref EventsReceived);
                var artikel = new Artikel();
                artikel.ReplayCatalogusToegevoegdEvent(artikelAanCatalogusToegevoegdEvent);

                using (var artikelService = new ArtikelService(new ArtikelRepository(new WebshopContext(_dbContext))))
                {
                    artikelService.AddArtikel(artikel);
                }

                _logger.LogDebug("ArtikelAanCatalogusToegevoegdEvent Succesvol verwerkt.");
            }
            catch(Exception e)
            {
                _logger.LogCritical($"An error occured in CatalogDispatcher.ArtikelAanCatalogusToegevoegd: {e.Message} /r StackTrace: {e.StackTrace}");
            }
        }

        public void ArtikelUitCatalogusVerwijderd(ArtikelUitCatalogusVerwijderd artikelUitCatalogusVerwijderdEvent)
        {
            try
            {
                _logger.LogDebug("ArtikelUitCatalogusVerwijderdEvent ontvangen");

                Interlocked.Increment(ref EventsReceived);
                using (var artikelService = new ArtikelService(new ArtikelRepository(new WebshopContext(_dbContext))))
                {
                    int artikelnummer = artikelUitCatalogusVerwijderdEvent.Artikelnummer;
                    Artikel artikel = artikelService.GetArtikel(artikelnummer);
                    if (artikel != null)
                    {
                        artikelService.RemoveArtikel(artikelnummer);
                    }
                }

                _logger.LogDebug("ArtikelUitCatalogusVerwijderdEvent Succesvol verwerkt.");
            }
            catch (Exception e)
            {
                _logger.LogCritical($"An error occured in CatalogDispatcher.ArtikelUitCatalogusVerwijderd: {e.Message} /r StackTrace: {e.StackTrace}");
            }
        }
    }
}
