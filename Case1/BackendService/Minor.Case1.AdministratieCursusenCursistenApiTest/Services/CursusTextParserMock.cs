using System;
using System.Collections.Generic;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;
using Minor.Case1.AdministratieCursusenCursistenApi.Services;
using Minor.Case1.AdministratieCursusenCursistenApi.Exceptions;

namespace Minor.Case1.AdministratieCursusenCursistenApiTest
{
    internal class CursusTextParserMock : ICursusTextParser
    {
        public List<string> CallsOpParse = new List<string>();

        public List<CursusInstantie> Parse(string text)
        {
            CallsOpParse.Add(text);
            if(text == "SyntaxError")
            {
                throw new InvalidSyntaxException("Fout op regel: 4");
            }
            if (string.IsNullOrEmpty(text))
            {
                return new List<CursusInstantie>();
            }
            if (text == "TestItem")
            {
                return new List<CursusInstantie>()
            {
                new CursusInstantie()
                {
                    StartDatum = new DateTime(2016, 10, 10),
                    Cursus = new Cursus()
                    {
                        Code="CODE21",
                        Duur=5,
                        Titel="Mock"
                    }
                } };
            }
            return new List<CursusInstantie>()
            {
                new CursusInstantie()
                {
                    StartDatum = new DateTime(2016, 10, 10),
                    Cursus = new Cursus()
                    {
                        Code="CODE",
                        Duur=5,
                        Titel="Mock"
                    }
                },
                new CursusInstantie()
                {
                    StartDatum = new DateTime(2016, 1, 1),
                    Cursus = new Cursus()
                    {
                        Code = "CODE2",
                        Duur = 2,
                        Titel = "Mock2"
                    }
                }};
        }
    }
}