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

        [HttpGet]
        public IActionResult Post()
        {
            var result = new RDWAgent().SendKeuringsVerzoekAsync(new keuringsverzoek()
            {
                keuringsdatum = new DateTime(2016, 11, 11),
                voertuig = new keuringsverzoekVoertuig()
                {
                    kenteken = "12-34-as",
                    kilometerstand = 10,
                    naam = "Henk",
                    type = voertuigtype.personenauto
                },
                keuringsinstantie = new keuringsinstantie()
                {
                    kvk = "3013 5370",
                    naam = "Garage Voorbeeld B.V.",
                    plaats = "Wijk bij Voorbeeld",
                    type = "garage"
                }
            }).Result;
            return null;
        }

        public async Task<IActionResult> Verzoek(KeuringsVerzoekCommand keuringsVerzoekCommand)
        {
            var verzoek = new keuringsverzoek()
            {
                voertuig = new keuringsverzoekVoertuig()
                {
                    kenteken = keuringsVerzoekCommand.Kenteken,
                    kilometerstand = keuringsVerzoekCommand.Kilometerstand,
                    naam = keuringsVerzoekCommand.Klantnaam
                }
            };

            var response = await _RDWAgent.SendKeuringsVerzoekAsync(verzoek); //TODO can throw a HttpRequestException

            _logContext.KeuringsVerzoeken.Add(verzoek);
            _logContext.KeuringsRegistraties.Add(response);
            _logContext.SaveChanges();

            if (!response.steekproefSpecified)
            {
                _publisher.Publish(new KeuringVerwerktZonderSteekproefEvent()
                {
                    OnderhoudsGuid = keuringsVerzoekCommand.OnderhoudsGuid
                });
            } 

            return Ok();
        }
    }
}
