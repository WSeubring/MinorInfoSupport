using Lapiwe.Audit.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lapiwe.Audit.Test.Infrastructure.Context
{
    public class InMemoryAuditLogDbContext : AuditLogDbContext
    {
        public InMemoryAuditLogDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();
        }
    }
}
