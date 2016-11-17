using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Commands;
using Minor.SpellenOefening.GameAdministrationService.Facade.Controllers;
using Commands.Entities;
using Minor.SpellenOefening.GameAdministrationService.Domain.Services.Interfaces;

namespace GameAdministrationService.Facade.Test
{
    [TestClass]
    public class GameControllerTest
    {
        [TestMethod]
        public void StartGame()
        {
            // Arrange
            var mock = new Mock<IGameService>(MockBehavior.Strict);
            mock.Setup(service => service.StartGame(It.IsAny<StartGameCommand>()));

            var target = new GameController(mock.Object);
            var command = new StartGameCommand()
            {
                Players = new List<Player>()
                {
                    new Player("1"),
                    new Player("2"),
                    new Player("3")
                }
            };

            //Act
            var result = target.StartGame(command);

            //Assert
            mock.Verify(service => service.StartGame(command), Times.Once);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void PlayGame()
        {
            // Arrange
            var mock = new Mock<IGameService>(MockBehavior.Strict);
            mock.Setup(service => service.PlayGame(It.IsAny<PlayGameCommand>()));

            var target = new GameController(mock.Object);
            var command = new PlayGameCommand()
            {
                GameID = 1
            };

            //Act
            var result = target.PlayGame(command);

            //Assert
            mock.Verify(service => service.PlayGame(command), Times.Once);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
