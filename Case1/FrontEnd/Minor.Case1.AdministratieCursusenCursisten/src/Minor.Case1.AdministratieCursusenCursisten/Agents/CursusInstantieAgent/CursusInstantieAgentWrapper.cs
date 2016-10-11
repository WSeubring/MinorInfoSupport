using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Minor.Case1.AdministratieCursusenCursisten.Agents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursisten.Agents
{
    public class CursusInstantieAgentWrapper : ICursusInstantieAgentWrapper
    {
        private CursusInstantieAgent _agent;
        public CursusInstantieAgentWrapper()
        {
            _agent = new CursusInstantieAgent();
            var apiHostUrl = "http://localhost:32326/";
            _agent.BaseUri = new Uri(apiHostUrl);
        }

        public void AddFromTextFile(string text)
        {
            //_agent.AddFromTextFile(text);
        }

        public IEnumerable<CursusInstantie> Get()
        {
            return _agent.Get();
        }
    }
}
