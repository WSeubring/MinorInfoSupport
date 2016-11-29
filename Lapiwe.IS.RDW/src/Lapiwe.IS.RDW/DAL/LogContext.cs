using Microsoft.EntityFrameworkCore;


namespace Lapiwe.IS.RDW.DAL
{
    public class LogContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySqlServer(@"Server=.\SQLEXPRESS; Database=Northwind;Trusted_Connection=True;");
        }
        public virtual DbSet<string> Logs { get; set; }
    }
}
