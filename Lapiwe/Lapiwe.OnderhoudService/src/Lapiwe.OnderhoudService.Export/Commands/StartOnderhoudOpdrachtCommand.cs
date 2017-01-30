using Lapiwe.Common.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lapiwe.OnderhoudService.Export
{

    public class StartOnderhoudOpdrachtCommand : DomainCommand
    {
        [Required]
        public Guid OnderhoudOpdrachtGuid { get; set; }

        public StartOnderhoudOpdrachtCommand() {}

        public StartOnderhoudOpdrachtCommand(Guid onderhoudOpdrachtGuid)
        {
            OnderhoudOpdrachtGuid = onderhoudOpdrachtGuid;
        }
    }
}
