using Lapiwe.Audit.Domain;
using Lapiwe.Audit.Domain.Contracts;
using Lapiwe.Audit.Publisher;
using Lapiwe.EventBus.Dispatchers;
using Lapiwe.EventBus.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lapiwe.Audit.Listener
{
    public class AuditDispatcher : EventDispatcher, IDisposable
    {
        private IRepository _repository;
        private IPublisher _publisher;

        public AuditDispatcher(IRepository repo, IPublisher publisher, BusOptions options = null) : base(options)
        {
            _repository = repo;
            _publisher = publisher;
        }

        public void OnReceived(SendAllEventCommand command)
        {
            var results = _repository.FindBy(s => s.TimeReceived >= command.StartTime 
                                            && s.TimeReceived <= command.EndTime 
                                            && isRoutingKeyMatch(command.RoutingKey, s.RoutingKey)
                                            );
            var orderedResult = results.OrderBy(s => s.TimeReceived);
            foreach (var serializedEvent in orderedResult)
            {
                _publisher.Publish(command.returnQueueName, serializedEvent);
            }
            
        }

        public bool isRoutingKeyMatch(string commandRoutingKey, string routingKey)
        {
            var preRegex = commandRoutingKey.Replace(".", "\\.").Replace("*", "[a-zA-Z]+").Replace("#", @"[a-zA-Z.]*");
            var pattern = $"^{preRegex}$";
            return Regex.IsMatch(routingKey, pattern);
        }

    }
}
