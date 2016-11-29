using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.IS.RDW.Agents.Interfaces
{
    public interface IRDWAgent
    {
        Task<keuringsregistratie> SendKeuringsVerzoekAsync(keuringsverzoek verzoek);
    }
}
