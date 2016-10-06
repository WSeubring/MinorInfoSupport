using Enities;
using Microsoft.EntityFrameworkCore;

namespace Minor.Dag19.DAL{
    public class MonumentContext : DbContext
    {
        public MonumentContext()
        {
            Database.EnsureCreated();
        }

        public MonumentContext(DbContextOptions<MonumentContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS; Database=Northwind;Trusted_Connection=True;");
        } 

        public virtual DbSet<Monument> Monumenten { get; set; }
    }
}