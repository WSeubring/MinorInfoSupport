using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PersoonQueries
{
    public static List<string> GetVoorlettersVanPersonenMetLetterInNaam(List<string> personenLijst, char letter)
    {
        letter = char.ToLower(letter);
        var VoorlettersVanPersonenMetLetterInNaamQuery = from naam in personenLijst
                                                         where naam.ToLower().Contains(letter)
                                                         select naam[0].ToString();

        return VoorlettersVanPersonenMetLetterInNaamQuery.ToList();
    }

    public static List<string> GetVoorlettersVanPersonenMetLetterInNaamExtensionSyntax(List<string> personenLijst, char letter)
    {
        letter = char.ToLower(letter);
        var VoorlettersVanPersonenMetLetterInNaamQuery = personenLijst.Where(n => n.ToLower().Contains(letter)).Select(n => n[0].ToString());

        return VoorlettersVanPersonenMetLetterInNaamQuery.ToList();
    }

    public static List<int> GetAantalLettersVanDeNaamVanPersonenWaarDeNaamBegintMetGegevenLetter(List<string> personenLijst, char letter)
    {
        letter = char.ToLower(letter);

        var nLettersVanDeNamenDieMetGegevenLetterBeginnenQuery = personenLijst.Where(n => n.ToLower().Contains(letter))
                                                                         .OrderBy(n => n.Length)
                                                                         .Select(n => n.Length);

        return nLettersVanDeNamenDieMetGegevenLetterBeginnenQuery.ToList();
    }

    public static List<int> GetAantalLettersVanDeNaamVanPersonenWaarDeNaamBegintMetGegevenLetterComprehesionSyntax(List<string> personenLijst, char letter)
    {
        letter = char.ToLower(letter);

        var nLettersVanDeNamenDieMetGegevenLetterBeginnenQuery = from naam in personenLijst
                                                                 where naam.ToLower().Contains(letter)
                                                                 orderby naam.Length
                                                                 select naam.Length;

        return nLettersVanDeNamenDieMetGegevenLetterBeginnenQuery.ToList();
    }

    public static List<int> AantalPersonenMetMetDeZelfdeLengteNaamDesc(List<string> personenLijst)
    {
        var aantalPersonenMetMetDeZelfdeLengteNaamDescQuery = from naam in personenLijst
                                                              group naam by naam.Length into naamlengths
                                                              orderby naamlengths.Key ascending
                                                              select naamlengths.Count();

        return aantalPersonenMetMetDeZelfdeLengteNaamDescQuery.ToList();
    }
    public static List<int> AantalPersonenMetMetDeZelfdeLengteNaamDescExtensionSyntax(List<string> personenLijst)
    {
        var aantalPersonenMetMetDeZelfdeLengteNaamDescQuery = personenLijst.GroupBy(n => n.Length)
                                                                           .OrderBy(n => n.Key)
                                                                           .Select(n => n.Count());

        return aantalPersonenMetMetDeZelfdeLengteNaamDescQuery.ToList();
    }


    public static List<string> GetPersonenMetDeKortsteNaamZonderGegevenLetterInDeNaam(List<string> personenLijst, char letter)
    {
        letter = char.ToLower(letter);
        int lengthShortestWord = personenLijst.Min(n => n.Length);
        var personenMettKortsteNaamZonderGegevenLetterInNaamQuery = personenLijst.Where(n => !n.ToLower().Contains(letter))
                                                                                 .Where(n =>n.Length == lengthShortestWord )
                                                                                 .Select(n => n);

        return personenMettKortsteNaamZonderGegevenLetterInNaamQuery.ToList();
    }

    public static List<string> GetPersonenMetDeKortsteNaamZonderGegevenLetterInDeNaamComprehensionSyntax(List<string> personenLijst, char letter)
    {
        letter = char.ToLower(letter);
        int lengthShortestWord = personenLijst.Min(n => n.Length);
        var personenMettKortsteNaamZonderGegevenLetterInNaamQuery = from naam in personenLijst
                                                                    where !naam.ToLower().Contains(letter) &&
                                                                           naam.Length == lengthShortestWord
                                                                    select naam;

        return personenMettKortsteNaamZonderGegevenLetterInNaamQuery.ToList();
    }
}