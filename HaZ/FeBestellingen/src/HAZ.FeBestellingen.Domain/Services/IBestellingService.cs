using HAZ.FeBestellingen.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Domain.Services
{
    public interface IBestellingService : IDisposable
    {
        void AddBestelling(Bestelling bestelling);
        Bestelling GetNextBestelling();
        void PickBestelling(int bestelnummer);
        Bestelling GetBestelling(int bestelnummer);
    }
}
