using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.Entities
{
    public class Auto : DomainEntity
    {
        [Key]
        public long ID { get; set; }
        public string Kenteken { get; set; }
        public int Kilometerstand { get; set; }

        public Auto(string kenteken, int kilometerstand)
        {
            Kenteken = kenteken;
            Kilometerstand = kilometerstand;
        }

        public Auto()
        {
        }
    }
}
