using HAZ.PsWinkelen.Exporting.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Infrastructure.Repositories
{
    public class PsWinkelenContext : DbContext, IPsWinkelenContext
    {

        public virtual DbSet<Artikel> Artikelen { get; set; }

        public PsWinkelenContext(DbContextOptions<PsWinkelenContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
