using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lapiwe.Common.Test
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
            Assert.AreEqual("Lapiwe.Common.Test.TestEvent" ,target.RoutingKey);
        }
    }
}
