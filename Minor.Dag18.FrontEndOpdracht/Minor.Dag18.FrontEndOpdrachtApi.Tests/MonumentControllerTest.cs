using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using Enities;
using MInor.Dag18.FrontEndOprachtApi.Controllers;

namespace Minor.Dag18.FrontEndOpdrachtApi.Tests
{
    [TestClass]
    public class MonumentControllerTest
    {

        [TestMethod]
        public void GetMonuments()
        {
            //Arrange
            MonumentenRepositoryMock repositoryMock = new MonumentenRepositoryMock();
            var target = new MonumentController(repositoryMock);

            //Act
            target.Get();

            //Assert
            Assert.AreEqual(1, repositoryMock.FindAllCount);
        }

        [TestMethod]
        public void GetMonumentWithId()
        {
            //Arrange
            MonumentenRepositoryMock repositoryMock = new MonumentenRepositoryMock();
            var target = new MonumentController(repositoryMock);

            //Act
            target.Get(10);

            //Assert
            Assert.AreEqual(1, repositoryMock.GetByIdCalls.Count);
            Assert.AreEqual(10, repositoryMock.GetByIdCalls.First());
        }

        [TestMethod]
        public void PostMonument()
        {
            //Arrange
            MonumentenRepositoryMock repositoryMock = new MonumentenRepositoryMock();
            var target = new MonumentController(repositoryMock);

            //Act
            target.Post(new Monument() { ID = 1, Naam= "Test Towers"});

            //Assert
            Assert.AreEqual(1, repositoryMock.AddCalls.First().ID);
            Assert.AreEqual("Test Towers", repositoryMock.AddCalls.First().Naam);
        }

        [TestMethod]
        public void DeleteMonument()
        {
            //Arrange
            MonumentenRepositoryMock repositoryMock = new MonumentenRepositoryMock();
            var target = new MonumentController(repositoryMock);

            //Act
            target.Delete(21);

            //Assert
            Assert.AreEqual(21, repositoryMock.DeleteCalls.First());
        }
    }
}
