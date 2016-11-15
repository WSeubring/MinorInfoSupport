using Commands;
using Exeptions;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace BKE.RoomManagementService.Facade.Test
{
    [TestClass]
    public class WebApiTests
    {
        [TestMethod]
        public void CreateNewRoom()
        {
            // Arrange
            var mock = new Mock<IRoom>(Moq.MockBehavior.Strict);
            mock.Setup(room => room.Create(It.IsAny<CreateRoomCommand>())).Returns(mock.Object);
            var target = new RoomManagementController(mock.Object);
            var crc = new CreateRoomCommand();

            // Act
            var result = target.CreateRoom(crc);

            // Assert
            mock.Verify(room => room.Create(crc), Times.Once);
            Assert.IsInstanceOfType(result, typeof(IRoom));
        }

        [TestMethod]
        public void CreateRoomArgumentNullExeption()
        {
            // Arrange
            var mock = new Mock<IRoom>(Moq.MockBehavior.Strict);
            var target = new RoomManagementController(mock.Object);

            // Act
            Action action = () => target.CreateRoom(null);

            // Assert
            Assert.ThrowsException<ArgumentNullException>(action);
        }

        [TestMethod]
        public void DuplicateRoomName()
        {
            // Arrange
            var mock = new Mock<IRoom>(Moq.MockBehavior.Strict);
            mock.Setup(room => room.Create(It.IsAny<CreateRoomCommand>()))
                .Throws<RoomnameAlreadyInUseExeption>();
            var target = new RoomManagementController(mock.Object);
            var crc = new CreateRoomCommand();

            // Act
            var result = target.CreateRoom(crc);

            // Assert
            mock.Verify(room => room.Create(crc), Times.Once);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
    }
}
