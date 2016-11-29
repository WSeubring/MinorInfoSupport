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
        public async Task<keuringsregistratie> SendKeuringsVerzoekAsync(keuringsverzoek verzoek)
        {
            using (var client = new HttpClient())
            {
                string keuringsVerzoekXml = SerializeKeuringsverzoekToXml(verzoek);
                var uri = new Uri("http://rdwserver:18423/rdw/APKKeuringsverzoek");

                var response = await client.PostAsync(uri, new StringContent(keuringsVerzoekXml, Encoding.UTF8, "application/xml"));
                response.EnsureSuccessStatusCode();

                var stringResponse = await response.Content.ReadAsStringAsync();
                keuringsregistratie keuringsRegistratie = SerializeToKeuringsregistratie(stringResponse);
                return keuringsRegistratie;
            }
        }

        private static keuringsregistratie SerializeToKeuringsregistratie(string xml)
        {
            XmlSerializer keuringsregistratieSerializer = new XmlSerializer(typeof(keuringsregistratie));
            using (var xmlReader = XmlReader.Create(new StringReader(xml)))
            {
                xmlReader.MoveToContent();
                xml = xmlReader.ReadInnerXml();
            }
            using (var stringReader = new StringReader(xml))
            {
                var keuringsRegistratie = (keuringsregistratie)keuringsregistratieSerializer.Deserialize(stringReader);
                return keuringsRegistratie;
            }
        }

        private string SerializeKeuringsverzoekToXml(keuringsverzoek verzoek)
        {
            XmlSerializer keuringsverzoekSerializer = new XmlSerializer(typeof(keuringsverzoek));
            using (var memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream, new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = true
                }))
                {
                    keuringsverzoekSerializer.Serialize(writer, verzoek);
                    var result = Encoding.UTF8.GetString(memoryStream.ToArray());
                    result = "<apkKeuringsverzoekRequestMessage xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" + result;
                    result = result + "</apkKeuringsverzoekRequestMessage>";
                    return result
                }
            }
        }
    }
}
