using HAZ.FeWebshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Domain.Infrastructure.Repositories
{
    public interface IArtikelRepository : IRepository<Artikel, int>
    {
        bool Exist(int artikelnummer);
    }
}
