using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Domain.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Infrastructure.Repositories
{
    public class ArtikelRepository : BaseRepository<Artikel, int, WebshopContext>, IArtikelRepository
    {
        public ArtikelRepository(WebshopContext context) : base(context)
        {
        }

        protected override IQueryable<Artikel> GetDbSet()
        {
            return _context.Artikelen;
        }

        protected override int GetKeyFrom(Artikel item)
        {
            return item.Artikelnummer;
        }

        public bool Exist(int artikelnummer)
        {
            return GetDbSet().Count(a => GetKeyFrom(a) == artikelnummer) > 0;
        }
    }
}
