using IntegrationTest.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTest.DAL
{
    public class PsWinkelenContext : DbContext
    {

        public PsWinkelenContext(DbContextOptions<PsWinkelenContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<Artikel> Artikelen { get; set; }

    }
}
