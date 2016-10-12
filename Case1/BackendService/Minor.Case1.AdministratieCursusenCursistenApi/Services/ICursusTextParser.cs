using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;
using System.Collections.Generic;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Services
{
    public interface ICursusTextParser
    {
        List<CursusInstantie> Parse(string text);
    }
}