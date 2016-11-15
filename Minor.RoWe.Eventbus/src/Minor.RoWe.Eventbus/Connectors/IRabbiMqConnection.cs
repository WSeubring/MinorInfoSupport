using Minor.RoWe.Eventbus.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.RoWe.Eventbus.Connectors
{
    public interface IRabbiMqConnection : IDisposable
    {
        BusOptions Options { get; }
        IModel Channel { get; }
    }
}
