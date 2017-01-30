using HaZ.DSBestellingenBeheer.Entities;
using HaZ.DSBestellingenBeheer.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaZ.DSBestellingenBeheer.Infrastructure.Repositories
{
    public class BestellingRepository : BaseRepository<Bestelling, int, BestellingContext>
    {
        public BestellingRepository(BestellingContext context) : base(context)
        {
        }

        protected override DbSet<Bestelling> GetDbSet()
        {
            return _context.Bestellingen;
        }

        protected override int GetKeyFrom(Bestelling item)
        {
            return item.Id;
        }
    }
}
