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
        /// <summary>
        /// Sends the text to API to add the cursusinstanties
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AddFromFileResultReport AddFromTextFile(string text)
        {
          return _agent.AddFromTextFile(text);
        }

        /// <summary>
        /// Send the request to get all CursusInstanties to the Api
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CursusInstantie> Get()
        {
            return _agent.Get();
        }

        /// <summary>
        /// Sends a request to the server to get all CursusInstanties within the given year and week
        /// </summary>
        /// <param name="jaar"></param>
        /// <param name="week"></param>
        /// <returns></returns>
        public IEnumerable<CursusInstantie> Get(int jaar, int week)
        {
            return _agent.GetByYearAndWeek(jaar, week);
        }
    }
}
