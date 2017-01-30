using JetBrains.Annotations;
using Lapiwe.KlantBeheerService.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lapiwe.KlantBeheerService.Infrastructure
{
    public class KlantContext : DbContext
    {
        public KlantContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Klant> Klanten { get; set; }
    }

}