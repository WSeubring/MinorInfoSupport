using System;

namespace Minor.Dag9.Events.Test
{
    internal class MockLeeftijdChanged
    {

        public bool LeeftijdChangedHasBeenCalled { get; internal set; }
        public LeeftijdChangedEventArgs e;
        internal void LeeftijdChangeHandeled(object sender, LeeftijdChangedEventArgs e)
        {
            LeeftijdChangedHasBeenCalled = true;
            this.e = e;
        }
    }
}