using HAZ.FeBestellingen.Domain.Entities;
using HAZ.FeBestellingen.Domain.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Infrastructure.Repositories
{
    public class BestellingRepository : BaseRepository<Bestelling, int, BestellingenContext>, IBestellingRepository
    {
        public BestellingRepository(BestellingenContext context) : base(context)
        {
        }

        protected override IQueryable<Bestelling> GetDbSet()
        {
            return _context.Bestellingen;
        }

        protected override int GetKeyFrom(Bestelling item)
        {
            return item.Bestelnummer;
        }

        public bool Exist(int bestelnummer)
        {
            return GetDbSet().Count(a => GetKeyFrom(a) == bestelnummer) > 0;
        }

        public Bestelling GetBestellingWithBestellingregels()
        {
            return GetDbSet().Where(bestelling => bestelling.BestelStatus != "picked").Include(bestelling => bestelling.Bestelregels).FirstOrDefault();
        }

        public Bestelling GetBestellingByID(int bestelnummer)
        {
            return GetDbSet().Where(bestelling => bestelling.Bestelnummer == bestelnummer).Include(bestelling => bestelling.Bestelregels).Include(bestelling => bestelling.Klantgegevens).FirstOrDefault();
        }
    }
}
