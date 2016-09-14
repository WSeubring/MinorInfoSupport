using System;
using System.Collections.Generic;

public class ValutaConverter
{
    private Dictionary<Muntsoort, decimal> convertionRatesVanEuro = new Dictionary<Muntsoort, decimal>()
    {
        { Muntsoort.Gulden, 2.20371M},
        { Muntsoort.Florijn, 2.20371M},
        { Muntsoort.Euro, 1M},
        { Muntsoort.Dukaat, 5.1M}
    };

    private Dictionary<Muntsoort, decimal> convertionRatesNaarEuro = new Dictionary<Muntsoort, decimal>()
    {
        { Muntsoort.Gulden, 0.454M},
        { Muntsoort.Florijn, 0.454M},
        { Muntsoort.Euro, 1M},
        { Muntsoort.Dukaat, 0.4321M}
    };

    public ValutaConverter()
    {
    }

    public decimal ConvertToEuro(Muntsoort muntsoort, decimal bedrag)
    {
        decimal convertionrate = 0;
        try
        {
            convertionrate = convertionRatesNaarEuro[muntsoort];
        }
        catch (KeyNotFoundException)
        {
            throw new MuntsoortNietOndersteundException();
        }

        return bedrag * convertionrate;
    }

    public decimal ConvertFromEuro(Muntsoort muntsoort, decimal bedrag)
    {
        decimal convertionrate = 0;
        try
        {
            convertionrate = convertionRatesVanEuro[muntsoort];
        }
        catch (KeyNotFoundException){
            throw new MuntsoortNietOndersteundException();
        }
        return bedrag * convertionrate;
    }
}