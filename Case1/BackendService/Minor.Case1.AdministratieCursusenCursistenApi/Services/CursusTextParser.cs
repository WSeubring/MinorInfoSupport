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
        private readonly Regex _titelPattern = new Regex(@"Titel:\s*(.{1,300}$)");
        private readonly Regex _cursusCodePattern = new Regex(@"Cursuscode:\s*(.{1,10}$)");
        private readonly Regex _duurPattern = new Regex(@"Duur:\s*([1-5])\s[dagen]");
        private readonly Regex _startDatumPattern = new Regex(@"Startdatum:\s*((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((1[6-9]|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$");
        private readonly Regex _emptyLinePattern = new Regex(@"\s*");

        private int _currentLineNumber = 0;

        public List<CursusInstantie> Parse(string filetext)
        {
            if (string.IsNullOrEmpty(filetext))
            {
                throw new ArgumentNullException();
            }

            var cursusInstanties = new List<CursusInstantie>();

            using (var reader = new StringReader(filetext))
            {
                while (reader.Peek() > 0)
                {
                    string title = ProccesNextLine(_titelPattern, reader);
                    string cursusCode = ProccesNextLine(_cursusCodePattern, reader);
                    string duurtext = ProccesNextLine(_duurPattern, reader);
                    string startDatumText = ProccesNextLine(_startDatumPattern, reader);
                    if(reader.Peek() > 0)
                    {
                        ProccesNextLine(_emptyLinePattern, reader);
                    }

                    cursusInstanties.Add(new CursusInstantie()
                    {
                        //CursusCode = cursusCode,
                        StartDatum = DateTime.ParseExact(startDatumText, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Cursus = new Cursus()
                        {
                            Code = cursusCode,
                            Duur = int.Parse(duurtext),
                            Titel = title
                        }
                    });
                }
            }

            return cursusInstanties;
        }

        private string ProccesNextLine(Regex pattern, StringReader reader)
        {
            _currentLineNumber++;
            string line = reader.ReadLine();
            ValidateSyntax(pattern, line);
            return pattern.Match(line).Groups[1].Value;
        }

        private void ValidateSyntax(Regex pattern, string line)
        {
            if (!pattern.IsMatch(line))
            {
                throw new InvalidSyntaxException($"Regel: {_currentLineNumber} voldoet niet aan de syntax.");
            }
        }
    }
}
