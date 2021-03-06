﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;
using System.Linq.Expressions;

namespace Minor.Case1.AdministratieCursusenCursistenApi.DAL.Interfaces
{
    public interface IRepository<T, K>
    {
        IEnumerable<T> FindAll();
        void AddRange(IEnumerable<CursusInstantie> cursusInstanties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> filter);
    }
}
