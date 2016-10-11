using System;
using System.ComponentModel.DataAnnotations;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten
{
    public class CursusInstantie
    {
        public int ID{ get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDatum{ get; set; }

        [StringLength(10)]
        public string CursusCode {get; set;}
        public Cursus Cursus { get; set; }

    }
}
