using Lapiwe.GMS.FrontEnd.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.Test
{
    [TestClass]
    public class KlantControllerTests
    {
        [TestMethod]
        public void KlantDetailsWithExistingKlant()
        {
            // Arrange
            var mock = new Mock<KlantContext>(MockBehavior.Strict);

            var target = new KlantController(mock);


            // Act
            target.KlanDetails(1);

            // Assert

        }
    }
}
