using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegexBedragen.Test
{
    [TestClass]
    public class GetalCheckerTest
    {
        [TestMethod]
        public void BegintMetOptineleMin()
        {
            //Arrange
            var target = new GetalChecker();

            //Act
            bool result = target.Check("-2.90");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EindigtNietOpTweeGetallen()
        {
            //Arrange
            var target = new GetalChecker();

            //Act
            bool result = target.Check("1.0");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EenPuntVoorDeLaatsteTweeGetallen()
        {
            //Arrange
            var target = new GetalChecker();

            //Act
            bool result = target.Check("100");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Niet_EenCommaVoorDeLaatsteTweeGetallen()
        {
            //Arrange
            var target = new GetalChecker();

            //Act
            bool result = target.Check("1,00");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Niet_BegintMetMindeDanEenGetal()
        {
            //Arrange
            var target = new GetalChecker();

            //Act
            bool result = target.Check("-.00");

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Niet_BegintMetMeerDanDrieGetal()
        {
            //Arrange
            var target = new GetalChecker();

            //Act
            bool result = target.Check("-1234.00");

            //Assert
             Assert.IsFalse(result);
        }

        [TestMethod]
        public void CommaPerDrieGetallen()
        {
            //Arrange
            var target = new GetalChecker();

            //Act
            bool result = target.Check("-12,233,044.00");

            //Assert
            Assert.IsTrue(result);
        }
    }
}
