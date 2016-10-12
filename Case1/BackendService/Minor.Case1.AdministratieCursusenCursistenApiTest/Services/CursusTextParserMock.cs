using System;
using System.Collections.Generic;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;
using Minor.Case1.AdministratieCursusenCursistenApi.Services;

namespace Minor.Case1.AdministratieCursusenCursistenApiTest
{
    internal class CursusTextParserMock : ICursusTextParser
    {
        public List<string> CallsOpParse = new List<string>();

        public List<CursusInstantie> Parse(string text)
        {
            CallsOpParse.Add(text);
            return new List<CursusInstantie>()
            {
                new CursusInstantie()
                {
                    //CursusCode = "Code",
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
                    //CursusCode = "Code2",
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