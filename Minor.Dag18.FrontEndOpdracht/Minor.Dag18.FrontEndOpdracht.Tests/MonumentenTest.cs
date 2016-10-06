using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Minor.Dag18.FrontEndOpdracht.Controllers;
using Minor.Dag18.FrontEndOpdracht.Models;
using System.Collections.Generic;
using System.Linq;


namespace Minor.Dag18.FrontEndOpdracht.Tests
{
    [TestClass]
    public class MonumentenControllerTest
    {

        [TestMethod]
        public void GeeftEenViewResult()
        {
            //Arrange
            var monumentenAgent = new DummyMonumentenAgent();
            var target = new MonumentenController(monumentenAgent);

            //Act
            IActionResult result = target.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void BevatEenModelMetEenLegeLijstVanMonument()
        {
            //Arrange
            var monumentenAgent = new DummyMonumentenAgent();
            var target = new MonumentenController(monumentenAgent);

            //Act
            IActionResult result = target.Index();

            //Assert
            var model = (result as ViewResult).Model;
            Assert.AreEqual(0, (model as IEnumerable<Monument>).Count());
        }

        [TestMethod]
        public void BevatEenModelMet1Monument()
        {
            //Arrange
            var monumentenAgent = new DummyMonumentenAgent();
            monumentenAgent.Add(new Monument() { Naam = "TestMonument", ID = 1 });
            var target = new MonumentenController(monumentenAgent);

            //Act
            IActionResult result = target.Index();

            //Assert
            var model = (result as ViewResult).Model;
            var monumenten = (model as IEnumerable<Monument>);
            Assert.AreEqual(1, monumenten.Count());
            Assert.AreEqual("TestMonument", monumenten.First().Naam);
        }

        [TestMethod]
        public void DeleteEenMonument()
        {
            //Assert
            var monumentenAgent = new DummyMonumentenAgent();
            monumentenAgent.Add(new Monument() { Naam = "TestMonument", ID = 1 });
            monumentenAgent.Add(new Monument() { Naam = "TestMonument2", ID = 2 });
            monumentenAgent.Add(new Monument() { Naam = "TestMonument3", ID = 3 });
            var target = new MonumentenController(monumentenAgent);

            //Act
            IActionResult result = target.Delete(1);


            //Assert
            var monumenten = monumentenAgent.GetAllMonumenten();
            Assert.AreEqual(2, monumenten.Count());
        }

        //[TestMethod]
        //public void AddEenMonument()
        //{
        //    //Assert
        //    var monumentenAgent = new DummyMonumentenAgent();
        //    monumentenAgent.Add(new Monument() { Naam = "TestMonument", ID = 1 });

        //    var target = new MonumentenController(monumentenAgent);

        //    //Act
        //    IActionResult result = target.Add(new Monument() { Naam = "NieuwTestMonument", ID = 5});


        //    //Assert
        //    var monumenten = monumentenAgent.GetAllMonumenten();
        //    Assert.AreEqual(2, monumenten.Count());
        //    Assert.AreEqual("NieuwTestMonument", monumenten.Single(m => m.ID == 5).Naam);
        //}
    }
}