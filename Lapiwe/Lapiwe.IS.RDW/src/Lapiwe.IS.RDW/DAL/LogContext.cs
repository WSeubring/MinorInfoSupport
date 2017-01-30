using Lapiwe.IS.RDW.Models;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.Entity.Extensions;

namespace Lapiwe.IS.RDW.DAL
{
    public class LogContext : DbContext
    {
        public LogContext()
        {
        }
        public LogContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"server=db;userid=admin;password=1234;database=is-rdw-db;");
        }
        public virtual DbSet<Log> Logs{ get; set; }
    }
}
