using System;
using System.Collections.Generic;
using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Domain.Infrastructure.Repositories;

namespace HAZ.FeWebshop.Domain.Services
{
    public class ArtikelService : IArtikelService, IDisposable
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

        public void UpdateArtikel(Artikel artikel)
        {
            _artikelRepository.Update(artikel);
        }

        public void RemoveArtikel(int artikelnummer)
        {
            _artikelRepository.Delete(artikelnummer);
        }

        public void Dispose()
        {
            _artikelRepository?.Dispose();
        }
    }
}
