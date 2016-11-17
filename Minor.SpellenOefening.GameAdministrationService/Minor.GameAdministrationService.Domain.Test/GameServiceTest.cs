using Commands;
using Commands.Entities;
using Eventbus;
using Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories.Interfaces;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.GameAdministrationService.Domain.Test
{
    [TestClass]
    public class GameServiceTest
    {
        [TestMethod]
        public void StartGame()
        {
            // Arrange
            var mockRepo = new Mock<IGameRepository>(MockBehavior.Strict);
            var mockPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            mockRepo.Setup(repo => repo.Add(It.IsAny<Models.Game>()));
            mockPublisher.Setup(pub => pub.Publish(It.IsAny<GameStartedEvent>()));
            
            var target = new GameService(mockRepo.Object, mockPublisher);
            var command = new StartGameCommand()
            {
                Players = new List<Player>()
                {
                    new Player("1"),
                    new Player("2"),
                    new Player("3")
                }
            };

            // Act
            target.StartGame(command);

            // Assert
            mockRepo.Verify(repo => repo.Add(It.IsAny<Models.Game>()), Times.Once);
            mockPublisher.Verify(pub => pub.Publish(It.IsAny<GameStartedEvent>()), Times.Once);
        }
    }
}
