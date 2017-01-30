using Lapiwe.GMS.FrontEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.ViewModels
{
    public class OnderhoudsOpdrachtenViewModel
    {
        public IEnumerable<OnderhoudsOpdracht> OnderhoudsOpdrachten { get; set; }

        public OnderhoudsOpdrachtenViewModel(IEnumerable<OnderhoudsOpdracht> onderhoudsOpdrachten)
        {
            OnderhoudsOpdrachten = onderhoudsOpdrachten;
        }
    }
}
