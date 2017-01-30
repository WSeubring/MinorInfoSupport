using HAZ.PsWinkelen.Domain.Infrastructure;
using HAZ.PsWinkelen.Exporting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Domain.Services
{
    public class ArtikelService : IArtikelService
    {
        private IArtikelRepository _artikelRepository;

        public ArtikelService(IArtikelRepository artikelRepository)
        {
            _artikelRepository = artikelRepository;
        }

        public IEnumerable<Artikel> GetAllArtikelen()
        {
            return _artikelRepository.FindAll();
        }

        public void AddArtikel(Artikel artikel)
        {
            if (!_artikelRepository.Exist(artikel.Artikelnummer))
            {
                _artikelRepository.Insert(artikel);
            }
            else
            {
                _artikelRepository.Update(artikel);
            }
        }

        public Artikel GetArtikel(int artikelnummer)
        {
            return _artikelRepository.Find(artikelnummer);
        }

        public void Dispose()
        {
            _artikelRepository?.Dispose();
        }
    }
}
