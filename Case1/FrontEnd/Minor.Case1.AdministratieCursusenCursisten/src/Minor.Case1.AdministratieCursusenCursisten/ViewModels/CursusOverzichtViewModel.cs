using Minor.Case1.AdministratieCursusenCursisten.Agents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursisten.ViewModels
{
    public class CursusOverzichtViewModel
    {
        public IEnumerable<CursusInstantie> CursusInstanties = new List<CursusInstantie>();

        public int Jaar { get; internal set; }
        public int Weeknr { get; set; }
    }
}
