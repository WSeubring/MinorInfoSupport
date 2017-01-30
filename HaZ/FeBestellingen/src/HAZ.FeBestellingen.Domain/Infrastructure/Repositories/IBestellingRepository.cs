using HAZ.FeBestellingen.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Domain.Infrastructure.Repositories
{
    public interface IBestellingRepository : IRepository<Bestelling, int>
    {
        bool Exist(int bestelnummer);
        Bestelling GetBestellingWithBestellingregels();
        Bestelling GetBestellingByID(int bestelnummer);
    }
}
