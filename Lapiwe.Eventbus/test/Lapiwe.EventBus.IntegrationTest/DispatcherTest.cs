using Lapiwe.EventBus.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.WSA.EventBus.IntegrationTest
{
    [TestClass]
    public class DispatcherTest
    {
        [TestMethod]
        public void Dispatcher_DuplicateMethodWithParameterName()
        {
            // Arrange
            Action act = () => new TestDispatcherWithDuplicateParameters();

            Assert.ThrowsException<DuplicateMethodWithSameEventParameterException>(act);
        }
    }
}
