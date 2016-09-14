using System;

public struct Valuta
{
    private Muntsoort _muntsoort;
    private decimal _bedrag;

    public Valuta(Muntsoort muntsoort, decimal bedrag)
    {
        _muntsoort= muntsoort;
        _bedrag = bedrag;
    }

    public override string ToString()
    {
        string valutaAfkorting = GetValutaAfkorting(_muntsoort);

        return (string.Format("{0:N2} ", _bedrag) + valutaAfkorting).Replace('.',',');
    }

    private string GetValutaAfkorting(Muntsoort muntsoort)
    {
        switch (muntsoort) {
            case Muntsoort.Euro:
                return "EUR";
            case Muntsoort.Gulden:
            case Muntsoort.Florijn:
                return "fl";
        }
        throw new MuntsoortNietOndersteundException();
    }
}