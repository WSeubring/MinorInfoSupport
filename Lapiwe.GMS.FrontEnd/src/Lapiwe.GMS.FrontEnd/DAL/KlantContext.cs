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
           optionsBuilder.UseMySQL(@"server=db;userid=admin;pwd=admin;port=3306;database=frontendDB;sslmode=none;");
        }

        public DbSet<Klant> Klanten { get; set; }
    }
}
