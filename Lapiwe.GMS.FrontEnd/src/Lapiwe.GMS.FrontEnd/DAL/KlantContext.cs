using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lapiwe.GMS.FrontEnd.ViewModels;

namespace Lapiwe.GMS.FrontEnd.DAL
{
    public class KlantContext : DbContext
    {
        public KlantContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(" server = ; userid = root; pwd = Pa$$w0rd; port = 3306 ; database = ShopDB; sslmode = none; ");
        }
        public DbSet<KlantGegegevensViewModel> Klanten { get; set; }
    }
}
