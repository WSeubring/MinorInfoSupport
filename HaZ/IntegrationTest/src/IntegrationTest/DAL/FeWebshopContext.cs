using IntegrationTest.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTest.DAL
{
    public class FeBestellingenContext : DbContext
    {

        public FeBestellingenContext(DbContextOptions<FeBestellingenContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<Bestelling> Bestellingen { get; set; }

    }
}
