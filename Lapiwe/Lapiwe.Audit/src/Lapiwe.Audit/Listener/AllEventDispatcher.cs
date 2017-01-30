using Lapiwe.Audit.Domain;
using Lapiwe.Audit.Domain.Contracts;
using Lapiwe.EventBus.Dispatchers;
using Lapiwe.EventBus.Domain;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Lapiwe.Audit.Listener
{
    public class AllEventDispatcher : EventDispatcher
    {
        private IRepository _repo;

        public AllEventDispatcher(IRepository repo, BusOptions options = null) : base(options)
        {
            _repo = repo;
        }

        protected override void OnReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var typeEvent = e.BasicProperties.Type;
            var message = Encoding.UTF8.GetString(e.Body);

            _repo.Insert(new SerializedEvent { RoutingKey = e.RoutingKey, EventType = typeEvent, Body = message, TimeReceived = DateTime.Now });
        }        
    }
}
