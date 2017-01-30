using Lapiwe.Audit.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySQL.Data.Entity.Extensions;

namespace Lapiwe.Audit.Infrastructure.Context
{
    public class AuditLogDbContext : DbContext
    {
        public DbSet<SerializedEvent> SerializedEvents { get; set; }

        public AuditLogDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"server=db;userid=admin;password=1234;database=auditlog;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SerializedEvent>()
                .HasKey(serializedEvent => serializedEvent.ID);

            modelBuilder.Entity<SerializedEvent>()
                .Property(serializedEvent => serializedEvent.EventType)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<SerializedEvent>()
                .Property(serializedEvent => serializedEvent.RoutingKey)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<SerializedEvent>()
                .Property(serializedEvent => serializedEvent.Body)
                .HasMaxLength(3999)
                .IsRequired();
        }
    }
}
