using Lapiwe.EventBus.Dispatchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.WSA.EventBus.IntegrationTest
{
    internal class TestDispatcherWithDuplicateParameters : EventDispatcher
    {

        // This class will throw an exception when constructed.

        public void TestEvent1(TestEvent e)
        {

        }

        public void TestEvent2(TestEvent e)
        {

        }
    }
}
