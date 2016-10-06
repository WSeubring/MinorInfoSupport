using Enities;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IMonumentenRepository 
    {
        IEnumerable<Monument> FindAll();

        Monument FindById(int id);
        void Add(Monument item);
        void Delete(int id);
    }
}