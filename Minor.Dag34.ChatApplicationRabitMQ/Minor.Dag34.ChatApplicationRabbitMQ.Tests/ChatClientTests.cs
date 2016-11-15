using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Minor.Dag34.ChatApplicationRabitMQ;
using Moq;
using System.Text;

namespace Minor.Dag34.ChatApplicationRabbitMQ.Tests
{
    [TestClass]
    public class ChatClientTests
    {
        [TestMethod]
        public void SendMessage()
        {
            // Arrange
            var mockChannel = new Mock<IModel>();
            var target = new MessageSender(mockChannel.Object);

            // Act
            target.send("Test");

            // Assert
            var expectedBytes = Encoding.UTF8.GetBytes("Test");
            mockChannel.Verify(channel => channel.BasicPublish("messages", "", null, expectedBytes), Times.Once);
        }
    }
}

