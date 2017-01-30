using HAZ.PsWinkelen.Exporting.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Infrastructure.Repositories
{
    public interface IPsWinkelenContext
    {
        DbSet<Artikel> Artikelen { get; set; }
        int SaveChanges();
    }
}
