using Commands;
using Commands.Entities;
using Eventbus;
using Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.SpellenOefening.GameAdministrationService.Domain.Infrastructure;
using Moq;
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
            
            var target = new GameService(mockRepo.Object, mockPublisher.Object);
            var command = new StartGameCommand()
            {
                Players = new List<StartGamePlayer>()
                {
                    new StartGamePlayer("1"),
                    new StartGamePlayer("2"),
                    new StartGamePlayer("3")
                }
            };

            // Act
            target.StartGame(command);

            // Assert
            mockRepo.Verify(repo => repo.Add(It.IsAny<Models.Game>()), Times.Once);
            mockPublisher.Verify(pub => pub.Publish(It.IsAny<GameStartedEvent>()), Times.Once);
        }
        [TestMethod]
        public void PlayGame()
        {
            // Arrange
            var mockRepo = new Mock<IGameRepository>(MockBehavior.Strict);
            var mockPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            mockRepo.Setup(repo => repo.Add(It.IsAny<Models.Game>()));
            mockPublisher.Setup(pub => pub.Publish(It.IsAny<GamePlayedEvent>()));

            var target = new GameService(mockRepo.Object, mockPublisher.Object);
            var command = new PlayGameCommand()
            {
                GameID = 1
            };

            // Act
            target.PlayGame(command);

            // Assert
            mockRepo.Verify(repo => repo.Add(It.IsAny<Models.Game>()), Times.Once);
            mockPublisher.Verify(pub => pub.Publish(It.IsAny<GamePlayedEvent>()), Times.Once);
        }
    }
}
