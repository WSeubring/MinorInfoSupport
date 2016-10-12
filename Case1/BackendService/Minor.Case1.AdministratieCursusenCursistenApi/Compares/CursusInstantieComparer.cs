using System;
using System.Collections.Generic;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Controllers
{
    internal class CursusInstantieComparer : IEqualityComparer<CursusInstantie>
    {
        public bool Equals(CursusInstantie x, CursusInstantie y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(CursusInstantie obj)
        {
            return obj.GetHashCode();
        }
    }
}