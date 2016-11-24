using Microsoft.EntityFrameworkCore;
using MySQL.Data.Entity.Extensions;
using Lapiwe.GMS.FrontEnd.Enitities;

namespace Lapiwe.GMS.FrontEnd.DAL
{
    public class KlantContext : DbContext
    {
        public KlantContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(" server = frontend-db; userid = root; pwd = 12345; port = 6032 ; database = frontend; sslmode = none; ");
        }

        public DbSet<Klant> Klanten { get; set; }
    }
}
