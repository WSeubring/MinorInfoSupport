using Minor.RoWe.Eventbus.Dispatchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minor.RoWe.Eventbus.Connectors;
using Minor.RoWe.Common.Events;
using System.Diagnostics;

namespace Minor.RoWe.Eventbus.Test
{
    public class DispatcherTest : EventDispatcher
    {
        public DispatcherTest(IRabbiMqConnection connection) : base(connection)
        {
        }

        public override string RoutingKey
        {
            get
            {
                return "#";
            }
        }

        public void Handler(GameEvent e)
        {
            Debug.WriteLine(e.RoutingKey);
        }
    }
}
