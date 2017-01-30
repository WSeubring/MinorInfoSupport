using Lapiwe.GMS.FrontEnd.Agents.RDWAgent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.Agents.RDWAgent
{
    public interface IRDWAgent
    {
        void KeuringsVerzoek(KeuringsVerzoekCommand domainCommand); 
    }
}
