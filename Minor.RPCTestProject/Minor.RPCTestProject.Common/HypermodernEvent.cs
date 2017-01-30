using Minor.WSA.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.RPCTestProject.Common
{
    public class HypermodernEvent : DomainEvent
    {
        public int HypermodernGetal { get; set; }

        public HypermodernEvent()
        {
        }
    }
}
