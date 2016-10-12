using System;
using System.Collections.Generic;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using Minor.Case1.AdministratieCursusenCursistenApi.Exceptions;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Services
{
    public class CursusTextParser : ICursusTextParser
    {
        private static readonly Regex _titelPattern = new Regex(@"Titel:\s*(?<titel>.{1,300}$)");
        private static readonly Regex _cursusCodePattern = new Regex(@"Cursuscode:\s*(?<cursuscode>.{1,10}$)");
        private static readonly Regex _duurPattern = new Regex(@"Duur:\s*(?<duur>[1-5])\s.*");
        private static readonly Regex _startDatumPattern = new Regex(@"Startdatum:\s*(?<startdatum>(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((1[6-9]|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$");

        public List<CursusInstantie> Parse(string filetext)
        {
            if (string.IsNullOrEmpty(filetext))
            {
                throw new ArgumentNullException();
            }

            var cursusInstanties = new List<CursusInstantie>();
            int currentLineNumber = 0;

            using (var text = new StringReader(filetext))
            {
                string firstline = null;
                while ((firstline = text.ReadLine()) != null)
                {
                    currentLineNumber++;

                    if (!string.IsNullOrWhiteSpace(firstline))
                    {
                        string currentLine = firstline;
                        ValidateSyntax(_titelPattern, currentLine, currentLineNumber);
                        var title = _titelPattern.Match(currentLine).Groups["titel"].Value;

                        currentLineNumber++;
                        currentLine = text.ReadLine();
                        ValidateSyntax(_cursusCodePattern, currentLine, currentLineNumber);
                        var cursusCode = _cursusCodePattern.Match(currentLine).Groups["cursuscode"].Value;

                        currentLineNumber++;
                        currentLine = text.ReadLine();
                        ValidateSyntax(_duurPattern, currentLine, currentLineNumber);
                        var duur = int.Parse(_duurPattern.Match(currentLine).Groups["duur"].Value);

                        currentLineNumber++;
                        currentLine = text.ReadLine();
                        ValidateSyntax(_startDatumPattern, currentLine, currentLineNumber);
                        var startDatumText = _startDatumPattern.Match(currentLine).Groups["startdatum"].Value;
                        DateTime startDatum = DateTime.ParseExact(startDatumText, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        cursusInstanties.Add(new CursusInstantie()
                        {
                            //CursusCode = cursusCode,
                            StartDatum = startDatum,
                            Cursus = new Cursus()
                            {
                                Code = cursusCode,
                                Duur = duur,
                                Titel = title
                            }
                        });
                    }
                }
            }

            return cursusInstanties;
        }
        private void ValidateSyntax(Regex pattern, string line, int linenumber)
        {
            if (!pattern.IsMatch(line))
            {
                throw new SyntaxException($"Regel: {linenumber} voldoet niet aan de syntax.");
            }
        }
    }
}
