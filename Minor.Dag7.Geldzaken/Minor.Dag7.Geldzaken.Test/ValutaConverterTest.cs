using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Dag7.Geldzaken.Test
{
    [TestClass]
    public class ValutaConverterTest
    {
        [TestMethod]
        public void TestConvertGuldenToEuro()
        {
            //Arrange
            var target = new ValutaConverter();

            //Act
            decimal result = target.ConvertToEuro(Muntsoort.Gulden, 10M);

            //Assert
            Assert.AreEqual(4.54M, result);
        }

        [TestMethod]
        public void TestConvertEuroToEuro()
        {
            //Arrange
            var target = new ValutaConverter();

            //Act
            decimal result = target.ConvertToEuro(Muntsoort.Euro, 10M);

            //Assert
            Assert.AreEqual(10M, result);
        }

        [TestMethod]
        public void TestConvertFlorijnToEuro()
        {
            //Arrange
            var target = new ValutaConverter();

            //Act
            decimal result = target.ConvertToEuro(Muntsoort.Florijn, 10M);

            //Assert
            Assert.AreEqual(4.54M, result);
        }

        [TestMethod]
        public void TestConvertDukaatToEuro()
        {
            //Arrange
            var target = new ValutaConverter();

            //Act
            decimal result = target.ConvertToEuro(Muntsoort.Dukaat, 10M);

            //Assert
            Assert.AreEqual(4.3210M, result);
        }

        [TestMethod]
        public void TestConvertDukaatFromEuro()
        {
            //Arrange
            var target = new ValutaConverter();

            //Act
            decimal result = target.ConvertFromEuro(Muntsoort.Dukaat, 10M);

            //Assert
            Assert.AreEqual(51M, result);
        }

        [TestMethod]
        public void TestConvertFlorijnFromEuro()
        {
            //Arrange
            var target = new ValutaConverter();

            //Act
            decimal result = target.ConvertFromEuro(Muntsoort.Florijn, 10M);

            //Assert
            Assert.AreEqual(22.03710M, result);
        }

        [TestMethod]
        public void TestConvertEuroFromEuro()
        {
            //Arrange
            var target = new ValutaConverter();

            //Act
            decimal result = target.ConvertFromEuro(Muntsoort.Euro, 10M);

            //Assert
            Assert.AreEqual(10M, result);
        }

        [TestMethod]
        public void TestConvertGuldenFromEuro()
        {
            //Arrange
            var target = new ValutaConverter();

            //Act
            decimal result = target.ConvertFromEuro(Muntsoort.Gulden, 10M);

            //Assert
            Assert.AreEqual(22.03710M, result);
        }
        [TestMethod]
        public void TestConvertOnbekendFromEuro()
        {
            //Arrange
            var target = new ValutaConverter();

            //Act
            Action action = () => target.ConvertFromEuro((Muntsoort)int.MaxValue, 10M);

            //Assert
            Assert.ThrowsException<MuntsoortNietOndersteundException>(action);
        }

        [TestMethod]
        public void TestConvertOnbekendToEuro()
        {
            //Arrange
            var target = new ValutaConverter();

            //Act
            Action action = () => target.ConvertToEuro((Muntsoort)int.MaxValue, 10M);

            //Assert
            Assert.ThrowsException<MuntsoortNietOndersteundException>(action);
        }
    }
}
