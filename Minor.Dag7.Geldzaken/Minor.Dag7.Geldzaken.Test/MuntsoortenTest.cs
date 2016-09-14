using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Dag7.Geldzaken.Test
{
    [TestClass]
    public class MuntsoortenTest
    {
        [TestMethod]
        public void TestMuntsoortEuro()
        {
            //Act
            Muntsoort muntsoort = Muntsoort.Euro;

            //Assert
            Assert.AreEqual(Muntsoort.Euro, muntsoort);
            Assert.AreEqual("Euro", Muntsoort.Euro.ToString());
        }

        [TestMethod]
        public void TestValutaToString()
        {
            //Arrange
            Valuta valuta = new Valuta(Muntsoort.Euro, 10M);

            //Act
            String result = valuta.ToString();

            //Assert
            Assert.AreEqual("10,00 EUR", result);
        }

        [TestMethod]
        public void TestValutaGuldenToString()
        {
            //Arrange
            Valuta valuta = new Valuta(Muntsoort.Gulden, 5.01M);

            //Act
            String result = valuta.ToString();

            //Assert
            Assert.AreEqual("5,01 fl", result);
        }
        [TestMethod]
        public void TestValutaFlorijnToString()
        {
            //Arrange
            Valuta valuta = new Valuta(Muntsoort.Florijn, 2.50M);

            //Act
            String result = valuta.ToString();

            //Assert
            Assert.AreEqual("2,50 fl", result);
        }

        [TestMethod]
        public void TestValutaOnbekendeValutaToString()
        {
            //Arrange
            Valuta valuta = new Valuta((Muntsoort)int.MaxValue, 0M);

            //Act
            Action action = () => valuta.ToString();

            //Assert
            Assert.ThrowsException<MuntsoortNietOndersteundException>(action);
        }

        [TestMethod]
        public void TestValutaDukaatToString()
        {
            //Arrange
            Valuta valuta = new Valuta(Muntsoort.Florijn, 2.50M);

            //Act
            String result = valuta.ToString();

            //Assert
            Assert.AreEqual("2,50 fl", result);
        }
    }
}
