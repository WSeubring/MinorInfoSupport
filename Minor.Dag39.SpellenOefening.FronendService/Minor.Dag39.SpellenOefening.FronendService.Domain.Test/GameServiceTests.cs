
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Dag39.SpellenOefening.FronendService.Domain.Models;
using Minor.Dag39.SpellenOefening.FronendService.Domain.Services;
using Minor.Dag39.SpellenOefening.FronendService.Infrastructure.Repositories;
using Moq;
using System.Collections.Generic;

namespace Minor.Dag39.SpellenOefening.FronendService.Domain.Test
{
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public void StartGame()
        {
            // Arrange
            var mock = new Mock<IGameRepository>(MockBehavior.Strict);
            var target = new GameService(mock.Object);
            var game = new Game()
            {
                Players = new List<Player>()
                {
                    new Player("1"),
                    new Player("2"),
                    new Player("3")
                }
            };

            // Act
            target.StartGame(game);

            // Assert 

        }
    }
}
