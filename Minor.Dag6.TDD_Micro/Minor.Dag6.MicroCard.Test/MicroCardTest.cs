using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Dag6.Micro;
using System;

namespace Minor.Dag6.MicroCard.Test
{
    [TestClass]
    public class MicroCardTest
    {
        [TestMethod]
        public void CardBalanceOf10()
        {
            // Arrange

            //Act
            Card card = new Card(10M);
            decimal result = card.Balance;

            //Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void CardPaymentEqualToBalance()
        {
            // Arrange
            Card card = new Card(10M);

            //Act
            card.Payment(10M);
            decimal result = card.Balance;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CardPaymentOf5()
        {
            // Arrange
            Card card = new Card(10M);

            //Act
            card.Payment(5M);
            decimal result = card.Balance;

            //Assert
            Assert.AreEqual(5, result);
        }
        [TestMethod]
        public void NormalCardPaymentHigherThenBalance()
        {
            // Arrange
            Card card = new NormalCard(10M);

            //Act
            Action action = () => card.Payment(20M);

            //Assert
            Assert.ThrowsException<InvalidOperationException>(action);
        }
        [TestMethod]
        public void VIPCardPaymentWithDiscount()
        {
            // Act
            VIPCard card = new VIPCard(50, 10M);
            decimal result = card.Discount;

            // Assert
            Assert.AreEqual(50, result);

        }
    }
}
