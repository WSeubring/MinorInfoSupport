using Lapiwe.IS.RDW.Agents.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Lapiwe.IS.RDW.Agents
{
    public class RDWAgent : IRDWAgent
    {
        public async Task<string> SendKeuringsVerzoekAsync(string verzoek)
        {
            var uri = new Uri("http://rdwserver:18423/rdw/APKKeuringsverzoek");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(uri, new StringContent(verzoek, Encoding.UTF8, "application/xml"));
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();
                return stringResponse;
            }
        }
    }
}
