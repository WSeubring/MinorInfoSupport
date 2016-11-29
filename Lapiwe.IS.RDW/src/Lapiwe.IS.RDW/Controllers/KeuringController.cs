using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lapiwe.IS.RDW.DAL;
using Lapiwe.Common.Infastructure;
using Lapiwe.IS.RDW.Export.Commands;

namespace Lapiwe.IS.RDW.Controllers
{
    [Route("api/[controller]")]
    public class KeuringController : Controller
    {
        private IEventPublisher _publisher;
        private LogContext _logContext;

        public KeuringController(LogContext logcontext, IEventPublisher publisher, IRDWAgent rdwAgent)
        {
            _publisher = publisher;
            _logContext = logcontext;
        }

        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            return null;
        }

        public object KeuringsVerzoek(KeuringsVerzoekCommand keuringVerzoekCommand)
        {
            var verzoek = new keuringsverzoek()
            {
                //TODO PARSE command naar verzoek
            }



            throw new NotImplementedException();
        }
    }
}
