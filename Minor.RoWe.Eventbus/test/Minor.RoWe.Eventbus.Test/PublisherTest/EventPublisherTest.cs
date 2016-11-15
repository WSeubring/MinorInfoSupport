using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.RoWe.Common.Events;
using Minor.RoWe.Eventbus.Connectors;
using Minor.RoWe.Eventbus.Options;
using Minor.RoWe.Eventbus.Publishers;
using Moq;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Minor.RoWe.Eventbus.Test.PublisherTest
{
    [TestClass]
    public class EventPublisherTest
    {

    
        public void SendEvent()
        {
            var options = new BusOptions()
            {
                ExchangeName = "TestExchange",
                HostName = "LocalHost",
                Password = "gast",
                Port = 80,
                QueueName = "TestQueue",
                Username = "Gast"
            };
        
            var mock = new Mock<IRabbiMqConnection>(MockBehavior.Strict);
            var channelMock = new Mock<IModel>(MockBehavior.Strict);
            var target = new EventPublisher(mock.Object);

            var testEvent = new GameEvent()
            {
                CorrelationID = new Guid(),
                RoutingKey = "Testing.game.created",
                TimpeStamp = new DateTime(2016, 11, 9)
            };

            var basicProperties = new BasicProperties()
            {

                Type = testEvent.GetType().ToString()
            };

            mock.Setup(e => e.Channel).Returns(channelMock.Object);
            mock.Setup(e => e.Options).Returns(options);

            var bodyList = new List<byte[]>();
            channelMock.Setup(c => c.BasicPublish(
                               options.ExchangeName,
                                testEvent.RoutingKey,
                                false,
                                It.IsAny<IBasicProperties>(),
                                It.IsAny<byte[]>()

                ));
            
            target.Publish(testEvent);
           
            channelMock.Verify(c => c.BasicPublish(
                                               options.ExchangeName,
                                testEvent.RoutingKey,
                                false,
                                It.IsAny<IBasicProperties>(),
                                It.IsAny<byte[]>()
                                ), Times.Once);
        }


        [TestMethod]
        public void Test()
        {
            var option = new BusOptions();
            using (var connection = new RabbitMqConnection(option))
            using (var pub = new EventPublisher(connection))
            using (var dis = new DispatcherTest(connection)) 
            {                pub.Publish(new GameEvent()
                {
                    CorrelationID = new Guid(),
                    RoutingKey = "Test",
                    TimpeStamp = DateTime.Now
                });
                Thread.Sleep(5000);
            }
            
            
             
        }
    }
}
