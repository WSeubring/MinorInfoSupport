using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.Entities
{
    public class Klant : DomainEntity
    {
        [Key]
        public long ID { get; set; }
        public string Naam { get; set; }
        public string Telefoonummer { get; set; }

        public Klant(string naam, string telefoonnummer)
        {
            Naam = naam;
            Telefoonummer = telefoonnummer;
        }

        public Klant()
        {
        }
    }
}
