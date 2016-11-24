using Microsoft.EntityFrameworkCore;
using MySQL.Data.Entity.Extensions;
using Lapiwe.GMS.FrontEnd.Enitities;

namespace Lapiwe.GMS.FrontEnd.DAL
{
    public class KlantContext : DbContext
    {
        public KlantContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(" server = frontendDB; userid = admin; pwd = admin; port = 6032 ; database = frontend; sslmode = none; ");
        }

        public DbSet<Klant> Klanten { get; set; }
    }
}
