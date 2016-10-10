using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<Type, Key>
    {
        IEnumerable<Type> FindAll();
        void Add(Type item);
        void Delete(Key key);

        Type FindByKey(Key key);
    }
}