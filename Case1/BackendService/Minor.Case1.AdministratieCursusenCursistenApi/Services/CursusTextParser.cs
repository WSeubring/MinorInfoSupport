using System;
using System.Collections.Generic;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Services
{
    public class CursusTextParser
    {
        private static readonly Regex _titlePattern = new Regex(@"Title:\s*(.*)");
        private static readonly Regex _cursusCodePattern = new Regex(@"Cursuscode:\s*(.*)");
        private static readonly Regex _DuurPattern = new Regex(@"Duur:\s*([1-5])\s.*");
        private static readonly Regex _StartDatumPattern = new Regex(@"Duur:\s*([1-5])\s.*");

        public List<CursusInstantie> Parse(string text)
        {
            var cursusInstanties = new List<CursusInstantie>();

            using (var stringReader = new StringReader(text))
            {
                var line = stringReader.ReadLine();
                var title = _titlePattern.Match(line).Value;
                var cursusCode = _cursusCodePattern.Match(stringReader.ReadLine()).Groups[0].ToString();
                var duur = int.Parse(_DuurPattern.Match(stringReader.ReadLine()).Groups[0].ToString());
                var startDatumText = _StartDatumPattern.Match(stringReader.ReadLine()).Groups[0].ToString();
                DateTime startDatum = DateTime.ParseExact(startDatumText, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                cursusInstanties.Add(new CursusInstantie()
                {
                    CursusCode = cursusCode,
                    StartDatum = startDatum,
                    Cursus = new Cursus()
                    {
                        Code = cursusCode,
                        Duur = duur,
                        Titel = title
                    }
                });
            }

            return cursusInstanties;
        }
    }
}