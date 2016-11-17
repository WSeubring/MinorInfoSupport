using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minor.Dag39.SpellenOefening.FronendService.WebApplication.Controllers;
using Moq;
using Minor.Dag39.SpellenOefening.FronendService.Domain.Services;
using Minor.Dag39.SpellenOefening.FronendService.Facade.ViewModels;
using Minor.Dag39.SpellenOefening.FronendService.Domain.Models;

namespace Minor.Dag39.SpellenOefening.WebApplication.Tests
{
    [TestClass]
    public class SpellenControllerTest
    {
        public static void Main(string[] args)
        {
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new SpellenController(null);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void StartGame()
        {
            // Arrange
            var mock = new Mock<IGameService>(MockBehavior.Strict);
            var controller = new SpellenController(mock.Object);
            StartGameViewModel viewmodel = new StartGameViewModel()
            {
                Player1 = "player1",
                Player2 = "player2",
                Player3 = "player3"
            };
            var game = new Game()
            {
                ID = 1,
                Players = new List<Player>()
                {
                    new Player("player1"),
                    new Player("player2"),
                    new Player("player3")
                }
            };

            mock.Setup(s => s.StartGame(It.IsAny<Game>())).Returns(game);

            //Act
            var result = controller.Index(viewmodel);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            mock.Verify(s => s.StartGame(It.IsAny<Game>()), Times.Once);
        }

        [TestMethod]
        public void PlayGame()
        {
            // Arrange
            var mock = new Mock<IGameService>(MockBehavior.Strict);
            var game = new Game()
            {
                ID = 1,
                Players = new List<Player>
                {
                    new Player("test1"),
                    new Player("player2"),
                    new Player("testplayer3")
                }
            };
            mock.Setup(s => s.GetGameById(1)).Returns(game);
            game.Winner = new Player("player2");
            mock.Setup(s => s.PlayGame(It.IsAny<Game>())).Returns(game);

            var controller = new SpellenController(mock.Object);

            //Act
            var result = controller.PlayGame(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(PlayGameViewModel));
            mock.Verify(s => s.PlayGame(It.IsAny<Game>()), Times.Once);
            Assert.AreEqual("player2", ((result as ViewResult).Model as PlayGameViewModel).Game.Winner.Name);
        }
    }
}
