using HAZ.FeBestellingen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Infrastructure.Repositories
{
    public class BestellingenContext : DbContext
    {
        public virtual DbSet<Bestelling> Bestellingen { get; set; }

        public BestellingenContext(DbContextOptions<BestellingenContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
