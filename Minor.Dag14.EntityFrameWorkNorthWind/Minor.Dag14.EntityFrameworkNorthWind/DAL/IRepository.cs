using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Minor.Dag14.EntityFrameworkNorthWind.DAL
{
    interface IRepository<TEntity, TKey>
    {
        IEnumerable<TEntity> FindAll();

        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> filter);

        TEntity Find(TKey id);

        void Insert(TEntity item);

        void Update(TEntity item);

        void Delete(TEntity item);
    }
}
