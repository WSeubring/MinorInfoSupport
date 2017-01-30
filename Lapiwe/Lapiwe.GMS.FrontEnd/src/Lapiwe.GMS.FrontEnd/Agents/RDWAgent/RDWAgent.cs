using System;
using Lapiwe.GMS.FrontEnd.Agents.RDWAgent.Models;

namespace Lapiwe.GMS.FrontEnd.Agents.RDWAgent
{
    public class RDWAgent : IRDWAgent, IDisposable
    {
        private RDWService _rdw;

        public RDWAgent()
        {
            _rdw = new RDWService();
            _rdw.BaseUri = new Uri("http://lapiwe-rdw-api:80");
        }

        public void KeuringsVerzoek(KeuringsVerzoekCommand domainCommand)
        {
            _rdw.Verzoek(domainCommand);
        }

        public void Dispose()
        {
            _rdw?.Dispose();
        }
    }
}
