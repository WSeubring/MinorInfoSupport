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

        public DbSet<KlantGegegevensViewModel> Klanten { get; set; }
    }
}
