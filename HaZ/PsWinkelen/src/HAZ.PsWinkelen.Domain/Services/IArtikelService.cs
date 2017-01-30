using HAZ.PsWinkelen.Exporting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Domain.Services
{
    public interface IArtikelService : IDisposable
    {
        IEnumerable<Artikel> GetAllArtikelen();
        void AddArtikel(Artikel artikel);
        Artikel GetArtikel(int artikelnummer);
    }
}
