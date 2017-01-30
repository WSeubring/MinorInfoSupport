using HAZ.PsWinkelen.Exporting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Domain.Infrastructure
{
    public interface IArtikelRepository : IRepository<Artikel, int>
    {
        bool Exist(int artikelnummer);
    }
}
