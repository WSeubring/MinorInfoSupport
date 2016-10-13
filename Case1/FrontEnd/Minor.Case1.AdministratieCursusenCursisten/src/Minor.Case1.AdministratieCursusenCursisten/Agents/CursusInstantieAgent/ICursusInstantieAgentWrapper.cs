using Minor.Case1.AdministratieCursusenCursisten.Agents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursisten.Agents
{
    public interface ICursusInstantieAgentWrapper
    {
        IEnumerable<CursusInstantie> Get();
        AddFromFileResultReport AddFromTextFile(string text);
        IEnumerable<CursusInstantie> Get(int jaar, int week);
    }
}
