using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten
{
    public class AddFromFileResultReport
    {
        public int AantalDuplicatesInFile{ get; set; }
        public int AantalInsertedCursusInstanties { get; set; }
        public bool HasSyntaxError { get; set; }
        public int? LineOfSyntaxError { get; set; }
    }
}
