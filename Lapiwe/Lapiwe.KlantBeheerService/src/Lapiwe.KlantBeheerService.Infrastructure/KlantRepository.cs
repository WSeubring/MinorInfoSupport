using System;
using Microsoft.EntityFrameworkCore;
using Lapiwe.KlantBeheerService.Domain;

namespace Lapiwe.KlantBeheerService.Infrastructure
{
    public class KlantRepository : IRepository
    {
        private DbContextOptions<KlantContext> _options;

        public KlantRepository(DbContextOptions<KlantContext> options)
        {
            _options = options;
        }

        public void Insert(Klant klant)
        {
            using (var context = new KlantContext(_options))
            {
                context.Klanten.Add(klant);
                context.SaveChanges();
            }
        }
    }

}