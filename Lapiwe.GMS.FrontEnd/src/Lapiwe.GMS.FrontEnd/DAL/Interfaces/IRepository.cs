using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.DAL.Interfaces
{
    public interface IRepository<TEntity, TKey> : IDisposable
    {
        IEnumerable<TEntity> FindAll();

        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> filter);

        TEntity Find(TKey id);

        void Insert(TEntity item);

        void Update(TEntity item);

        void Delete(TEntity item);
    }
}
