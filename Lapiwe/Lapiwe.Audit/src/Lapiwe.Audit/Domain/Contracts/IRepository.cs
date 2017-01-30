using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lapiwe.Audit.Domain.Contracts
{
    public interface IRepository : IDisposable
    {
        void Insert(SerializedEvent serializedEvent);
        IEnumerable<SerializedEvent> FindBy(Expression<Func<SerializedEvent, bool>> filter);
    }
}