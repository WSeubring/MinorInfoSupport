public delegate void LeeftijdChangedEventHandler(object sender, LeeftijdChangedEventArgs e);

public class LeeftijdChangedEventArgs
{
    public LeeftijdChangedEventArgs(string Naam, int OudeLeeftijd, int NieuweLeeftijd)
    {
        this.Naam = Naam;
        this.OudeLeeftijd = OudeLeeftijd;
        this.NieuweLeeftijd = NieuweLeeftijd;

    }

    public string Naam { get; private set; }
    public int OudeLeeftijd { get; private set; }
    public int NieuweLeeftijd { get; private set; }
}