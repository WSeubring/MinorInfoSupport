using HAZ.PsWinkelen.Domain.Infrastructure;
using HAZ.PsWinkelen.Exporting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Infrastructure.Repositories
{
    public class ArtikelRepository : BaseRepository<Artikel, int, PsWinkelenContext>, IArtikelRepository
    {
        public ArtikelRepository(PsWinkelenContext context) : base(context)
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
