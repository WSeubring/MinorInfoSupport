using HaZ.DSBestellingenBeheer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaZ.DSBestellingenBeheer.Infrastructure.DAL
{
    public class BestellingContext : DbContext
    {
        public virtual DbSet<Bestelling> Bestellingen { get; set; }

        public BestellingContext()
        {
            Database.EnsureCreated();
        }

        public BestellingContext(DbContextOptions<BestellingContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bestelling>().Property(b => b.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}
