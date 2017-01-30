using HAZ.FeWebshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Domain.Services
{
    public interface IArtikelService : IDisposable
    {
        Artikel GetArtikel(int artikelnummer);
        void AddArtikel(Artikel artikel);
        IEnumerable<Artikel> GetAllArtikelen();
    }
}
