using HAZ.FeBestellingen.Domain.Infrastructure.Agents;
using InfoSupport.WSA.Infrastructure;
using Kantilever.Magazijnbeheer.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Infrastructure.Agents
{
    public class MagazijnBeheerAgent: IMagazijnBeheerAgent
    {
        private ILogger<MagazijnBeheerAgent> _logger;

        public MagazijnBeheerAgent(ILogger<MagazijnBeheerAgent> logger)
        {
            _logger = logger;
        }

        public void SendHaalArtikelUitMagazijnCommand(HaalArtikelUitMagazijnCommand command)
        {
            var busOptions = BusOptions.CreateFromEnvironment();
            using (var proxy = new MicroserviceProxy("Kantilever.Magazijnbeheer", busOptions))
            {
                try
                {
                    proxy.Execute(command);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message + "; StackTrace: " + e.StackTrace);
                }
            }
        }
    }
}
