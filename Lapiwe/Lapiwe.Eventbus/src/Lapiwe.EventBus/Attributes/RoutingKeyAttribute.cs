using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.EventBus.Attributes
{
    public class RoutingKeyAttribute : Attribute 
    {
        public string Topic { get; }
        public RoutingKeyAttribute(string topic = "#")
        {
            Topic = topic;
        }
    }
}
