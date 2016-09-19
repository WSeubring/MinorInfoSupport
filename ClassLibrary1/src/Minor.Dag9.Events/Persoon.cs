using System;

namespace Minor.Dag9.Events
{
    public class Persoon
    {
        public event LeeftijdChangedEventHandler LeeftijdChanged;

        private int _leeftijd;
        public string Naam { get; set; }
        public int Leeftijd
        {
            get { return _leeftijd; }
            set {
                int oudeLeeftijd = _leeftijd;
                _leeftijd = value;
                onLeeftijdChanged(new LeeftijdChangedEventArgs(Naam, oudeLeeftijd, _leeftijd));
            }
        }

        public Persoon(string naam, int leeftijd)
        {
            Naam = naam;
            _leeftijd = leeftijd;
        }
        public virtual void onLeeftijdChanged(LeeftijdChangedEventArgs e)
        {
            LeeftijdChanged?.Invoke(this, e);
        }

        public void Verjaar()
        {
            Leeftijd += 1;
        }
    }
}