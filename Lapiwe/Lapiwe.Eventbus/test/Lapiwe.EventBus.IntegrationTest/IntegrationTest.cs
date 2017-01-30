using Lapiwe.EventBus.Domain;
using Lapiwe.EventBus.Publishers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Minor.WSA.EventBus.IntegrationTest
{
    [TestClass]
    public class IntegrationTest
    {

        [TestMethod]
        public void PublishedTestEvent()
        {
            var options = new BusOptions { ExchangeName = "TestExchange1", HostName = "Localhost", Port = 5672, Username = "guest", Password = "guest" };
            using (var consumer = new TestDispatcher(options))
            using (var target = new EventPublisher(options))
            {

                target.Publish(new TestEvent());

                Thread.Sleep(100);

                Assert.AreEqual(1, consumer.receivedTestEventCount);
            }
        }

        [TestMethod]
        public void PublishedTestEventTwice()
        {
            var options = new BusOptions { ExchangeName = "TestExchange2", HostName = "Localhost", Port = 5672, Username = "guest", Password = "guest" };
            using (var consumer = new TestDispatcher(options))
            using (var target = new EventPublisher(options))
            {

                target.Publish(new TestEvent());
                target.Publish(new TestEvent());

                Thread.Sleep(100);

                Assert.AreEqual(2, consumer.receivedTestEventCount);
            }
        }

        [TestMethod]
        public void PublishedTestEventReceivedByTwoDispatchers()
        {
            var options = new BusOptions { ExchangeName = "TestExchange3", HostName = "Localhost", Port = 5672, Username = "guest", Password = "guest" };
            using (var consumer1 = new TestDispatcher(options))
            using (var consumer2 = new TestDispatcher(options))
            using (var target = new EventPublisher(options))
            {

                target.Publish(new TestEvent());

                Thread.Sleep(100);

                Assert.AreEqual(1, consumer1.receivedTestEventCount);
                Assert.AreEqual(1, consumer2.receivedTestEventCount);
            }
        }

        [TestMethod]
        public void PublishedTestEventIsNotReceivedByOtherRoutingKey()
        {
            var options = new BusOptions { ExchangeName = "TestExchange4", HostName = "Localhost", Port = 5672, Username = "guest", Password = "guest" };
            using (var consumer = new TestDispatcherAttribute(options))
            using (var target = new EventPublisher(options))
            {

                target.Publish(new TestEvent());

                Thread.Sleep(100);

                Assert.AreEqual(0, consumer.receivedTestEventCount);
            }
        }

        [TestMethod]
        public void PublishedTestEventIsReceivedByOtherRoutingKey()
        {
            var options = new BusOptions { ExchangeName = "TestExchange5", HostName = "Localhost", Port = 5672, Username = "guest", Password = "guest" };
            using (var consumer = new TestDispatcherAttribute(options))
            using (var target = new EventPublisher(options))
            {

                target.Publish(new TestEvent("Test.Routing"));

                Thread.Sleep(100);

                Assert.AreEqual(1, consumer.receivedTestEventCount);
            }
        }

        [TestMethod]
        public void PublishedTestEventMultiple()
        {
            var options = new BusOptions { ExchangeName = "TestExchange6", HostName = "Localhost", Port = 5672, Username = "guest", Password = "guest" };
            using (var consumer = new TestDispatcher(options))
            using (var consumerAttribute = new TestDispatcherWithoutOtherEvent(options))
            using (var target = new EventPublisher(options))
            {
                target.Publish(new TestEvent());
                target.Publish(new OtherTestEvent());

                Thread.Sleep(100);

                Assert.AreEqual(1, consumer.receivedTestEventCount);
                Assert.AreEqual(1, consumer.receivedOtherTestEventCount);
                Assert.AreEqual(2, consumer.receivedTotalTestCount);

                Assert.AreEqual(1, consumerAttribute.receivedTestEventCount);
            }
        }

        [TestMethod]
        public void DispatcherUsesBusOptionsAttribute()
        {
            var options = new BusOptions { ExchangeName = "TestExchange1", HostName = "Localhost", Port = 5672, Username = "guest", Password = "guest" };

            using (var consumer = new TestDispatcherBusOptionsAttribute(options))            
            using (var target = new TestPublisherAttr(options))
            {
                target.Publish(new TestEvent());

                Thread.Sleep(100);

                Assert.AreEqual(1, consumer.receivedTestEventCount);
            }
        }
    }
}
