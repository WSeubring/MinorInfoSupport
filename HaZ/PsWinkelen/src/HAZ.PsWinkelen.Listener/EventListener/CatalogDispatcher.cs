using HAZ.PsWinkelen.Domain.Services;
using HAZ.PsWinkelen.Exporting.Entities;
using HAZ.PsWinkelen.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using Kantilever.Catalogusbeheer.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Listener.EventListener
{
    public class CatalogDispatcher : EventDispatcher
    {
        public long EventsReceived = 0;
        private DbContextOptions<PsWinkelenContext> _dbContext;
        private ILogger<CatalogDispatcher> _logger; 

        public CatalogDispatcher(BusOptions options, DbContextOptions<PsWinkelenContext> dbContext, ILoggerFactory loggerFactory) : base(options)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<CatalogDispatcher>();
        }

        public void ArtikelAanCatalogusToegevoegd(ArtikelAanCatalogusToegevoegd artikelToegevoegdEvent)
        {
            try
            {
                _logger.LogDebug($"CatalogDispatcher: ArtikelAanCatalogusToegevoegd ontvangen, artikelnummer: {artikelToegevoegdEvent?.Artikelnummer}");
                Interlocked.Increment(ref EventsReceived);
                var artikel = new Artikel();
                artikel.ReplayCatalogusToegevoegdEvent(artikelToegevoegdEvent);

                using (ArtikelService artikelService = new ArtikelService(new ArtikelRepository(new PsWinkelenContext(_dbContext))))
                {
                    artikelService.AddArtikel(artikel);
                }
                _logger.LogDebug($"CatalogDispatcher: ArtikelAanCatalogusToegevoegd verwerkt, artikelnummer: {artikelToegevoegdEvent?.Artikelnummer}");
            }
            catch (Exception e)
            {
                _logger.LogCritical($"An unexpected error occurd. \n errormessage: {e.Message} \n Stacktrace: {e.StackTrace}");
            }
        }
    }
}
