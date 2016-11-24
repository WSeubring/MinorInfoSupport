using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lapiwe.GMS.FrontEnd.ViewModels;
using MySQL.Data.Entity.Extensions;

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
        public DbSet<KlantGegegevensViewModel> Klanten { get; set; }
    }
}
