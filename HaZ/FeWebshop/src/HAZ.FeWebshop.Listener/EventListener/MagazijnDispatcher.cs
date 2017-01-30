using HAZ.FeWebshop.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kantilever.Magazijnbeheer;
using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Domain.Services;
using Kantilever.Magazijnbeheer.Events;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace HAZ.FeWebshop.Listener.EventListener
{
    public class MagazijnDispatcher : EventDispatcher
    {
        public long EventsReceived = 0;
        private DbContextOptions<WebshopContext> _dbContext;
        private ILogger<MagazijnDispatcher> _logger;

        public MagazijnDispatcher(BusOptions options, DbContextOptions<WebshopContext> dbContext, ILoggerFactory loggerFactory) : base(options)
        {
            _logger = loggerFactory.CreateLogger<MagazijnDispatcher>();
            _dbContext = dbContext;
        }

        public void ArtikelInMagazijnGezet(ArtikelInMagazijnGezet artikelInMagazijnGezetEvent)
        {
            try
            {
                Interlocked.Increment(ref EventsReceived);
                using (var artikelService = new ArtikelService(new ArtikelRepository(new WebshopContext(_dbContext))))
                {
                    Artikel artikel = artikelService.GetArtikel(artikelInMagazijnGezetEvent.ArtikelID);
                    if (artikel != null)
                    {
                        artikel.ReplayInMagazijnGezetEvent(artikelInMagazijnGezetEvent);
                        artikelService.UpdateArtikel(artikel);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical($"An error occured in MagazijnDispatcher.ArtikelInMagazijnGezet: {e.Message} /r StackTrace: {e.StackTrace}");
            }
        }

        public void ArtikelUitMagazijnGehaald(ArtikelUitMagazijnGehaald artikelUitMagazijnGehaaldEvent)
        {
            try
            {
                Interlocked.Increment(ref EventsReceived);
                Artikel artikel = null;
                using (var artikelService = new ArtikelService(new ArtikelRepository(new WebshopContext(_dbContext))))
                {
                    artikel = artikelService.GetArtikel(artikelUitMagazijnGehaaldEvent.ArtikelID);
                    if (artikel != null)
                    {
                        artikel.ReplayUitMagazijnGehaaldEvent(artikelUitMagazijnGehaaldEvent);
                        artikelService.UpdateArtikel(artikel);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical($"An error occured in MagazijnDispatcher.ArtikelUitMagazijnGehaald: {e.Message} /r StackTrace: {e.StackTrace}");
            }
        }
    }
}
