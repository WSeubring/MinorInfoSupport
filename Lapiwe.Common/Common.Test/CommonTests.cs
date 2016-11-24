using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Test
{
    [TestClass]
    public class CommonTests
    {
        [TestMethod]
        public void TestRoutingKey()
        {
            // Arrange
            TestEvent target = new TestEvent();

            // Assert
            Assert.AreEqual("Common.Test.TestEvent" ,target.RoutingKey);
        }
    }
}
