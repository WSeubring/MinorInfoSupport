using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Dag9.Events
{
    public class Werknemer : Persoon
    {
        public decimal Salaris { get; private set; }
        public Werknemer(string Naam, int Leeftijd, decimal Salaris) : base(Naam, Leeftijd)
        {
            this.Salaris = Salaris;
        }
    }
}
