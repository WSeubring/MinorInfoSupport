using Lapiwe.GMS.FrontEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.ViewModels
{
    public class KeuringsVerzoekenViewModel
    {
        public IEnumerable<KeuringsVerzoek> Verzoeken { get; set; }

        public KeuringsVerzoekenViewModel(IEnumerable<KeuringsVerzoek> verzoeken)
        {
            Verzoeken = verzoeken;
        }
    }
}
