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
        void AddFromTextFile(string text);
    }
}
