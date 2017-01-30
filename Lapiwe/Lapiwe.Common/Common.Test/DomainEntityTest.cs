using Lapiwe.Common.Domain;
using Lapiwe.Common.Test.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.Common.Test
{
    [TestClass]
    public class DomainEntityTest
    {
        [TestMethod]
        public void CreeerEenAuto()
        {
            // Arrange
            DomainEntity entity = new Auto();

            // Assert that the generated guid is not empty
            Assert.AreNotSame(new Guid(), entity.Guid);
        }

        [TestMethod]
        public void GebruikEenAuto()
        {
            // Arrange
            Guid guid = Guid.NewGuid();
            DomainEntity entity = new Auto(guid);

            // Assert
            Assert.AreEqual(guid, entity.Guid);
        }
    }
}
