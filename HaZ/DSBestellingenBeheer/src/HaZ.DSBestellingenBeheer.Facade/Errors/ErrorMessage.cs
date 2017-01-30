using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaZ.DSBestellingenBeheer.Facade.Errors
{
    public class ErrorMessage
    {
        public ErrorType FoutType { get; set; }
        public string FoutMelding { get; set; }
        public string Oplossing { get; set; }

        public ErrorMessage(ErrorType foutType, string foutmelding, string oplossing = null)
        {
            FoutType = foutType;
            FoutMelding = foutmelding;
            Oplossing = oplossing;
        }
    }
}
