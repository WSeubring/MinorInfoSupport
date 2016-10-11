using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursistenApi.DAL.Interfaces
{
    public interface IRepository<T, K>
    {
        IEnumerable<T> FindAll();
    }
}
