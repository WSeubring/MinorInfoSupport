using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lapiwe.IS.RDW.DAL;
using Lapiwe.Common.Infastructure;
using Lapiwe.IS.RDW.Export.Commands;
using Lapiwe.IS.RDW.Agents.Interfaces;
using Lapiwe.IS.RDW.Export.Events;
using Lapiwe.IS.RDW.Agents;
using System.Net.Http;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using Lapiwe.IS.RDW.Models;
using Swashbuckle.SwaggerGen.Annotations;

namespace Lapiwe.IS.RDW.Controllers
{
    [Route("api/[controller]")]
    public class KeuringController : Controller
    {
        private IEventPublisher _publisher;
        private LogContext _logContext;
        private IRDWAgent _RDWAgent;

        public KeuringController(LogContext logcontext, IEventPublisher publisher, IRDWAgent rdwAgent)
        {
            _publisher = publisher;
            _logContext = logcontext;
            _RDWAgent = rdwAgent;
        }

        [HttpPost]
        [SwaggerOperation("Verzoek")]
        public async Task<IActionResult> Verzoek([FromBody]KeuringsVerzoekCommand keuringsVerzoekCommand)
        {
            var verzoek = new keuringsverzoek()
            {
                keuringsdatum = DateTime.Now,
                voertuig = new keuringsverzoekVoertuig()
                {
                    kenteken = keuringsVerzoekCommand.Kenteken,
                    kilometerstand = keuringsVerzoekCommand.Kilometerstand,
                    naam = keuringsVerzoekCommand.Klantnaam
                },
                keuringsinstantie = new keuringsinstantie() 
                {
                    naam = "Lapiwe Garage",
                    kvk = "7251 3824",
                    plaats = "veenendaal",
                    type = "Garage"
                    
                }
            };

            string response;
            try
            {
                string verzoekXml = SerializeKeuringsverzoekToXml(verzoek);
                response = await _RDWAgent.SendKeuringsVerzoekAsync(verzoekXml);
                var keuringsregistratie = SerializeToKeuringsregistratie(response);

                bool isSteekproef = keuringsregistratie.steekproef != null;

                _logContext.Logs.Add(new Log()
                {
                    Xml = verzoekXml,
                    Type = typeof(keuringsverzoek).ToString()
                });

                _logContext.Logs.Add(new Log()
                {
                    Xml = response,
                    Type = typeof(keuringsregistratie).ToString()
                });
                _logContext.SaveChanges();

                if (!isSteekproef)
                {
                    _publisher.Publish(new KeuringVerwerktZonderSteekproefEvent()
                    {
                        OnderhoudsGuid = keuringsVerzoekCommand.OnderhoudsGuid,
                        Keuringsdatum = keuringsregistratie.keuringsdatum
                    });
                }
                else if (isSteekproef)
                {
                    _publisher.Publish(new KeuringVerwerktMetSteekproefEvent()
                    {
                        OnderhoudsGuid = keuringsVerzoekCommand.OnderhoudsGuid,
                        SteefproefDatum = (DateTime)keuringsregistratie.steekproef,
                        Keuringsdatum = keuringsregistratie.keuringsdatum
                    });
                }
            }
            catch (AggregateException exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        private keuringsregistratie SerializeToKeuringsregistratie(string xml)
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
            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter, new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = true
                }))
                {
                    keuringsverzoekSerializer.Serialize(writer, verzoek);
                    var result = stringWriter.ToString();
                    result = "<apkKeuringsverzoekRequestMessage xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" + result;
                    result = result + "</apkKeuringsverzoekRequestMessage>";
                    return result;
                }
            }
        }
    }
}
