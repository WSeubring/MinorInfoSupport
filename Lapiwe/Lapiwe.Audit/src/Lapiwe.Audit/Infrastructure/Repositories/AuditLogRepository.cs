using Lapiwe.Audit.Domain;
using Lapiwe.Audit.Domain.Contracts;
using Lapiwe.Audit.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lapiwe.Audit.Infrastructure.Repositories
{
    public class AuditLogRepository : IRepository
    {
        private AuditLogDbContext _context;

        public AuditLogRepository(AuditLogDbContext context)
        {
            _context = context;
        }

        public void Insert(SerializedEvent serializedEvent)
        {
            _context.SerializedEvents.Add(serializedEvent);
            _context.SaveChanges();
        }

        public IEnumerable<SerializedEvent> FindBy(Expression<Func<SerializedEvent, bool>> filter)
        {
            return _context.SerializedEvents.Where(filter).ToList();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
