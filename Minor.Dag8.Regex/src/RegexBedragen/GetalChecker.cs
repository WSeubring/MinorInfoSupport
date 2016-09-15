using System;
using System.Text.RegularExpressions;

public class GetalChecker
{
    private static readonly Regex pattern = new Regex(@"^-?[0-9]{1,3}(,[0-9]{3})*([.][0-9]{2}$)");

    public bool Check(string getal)
    {
        return pattern.IsMatch(getal);
    }
}