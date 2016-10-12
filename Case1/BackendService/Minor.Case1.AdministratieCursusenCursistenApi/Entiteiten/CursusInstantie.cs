using System;
using System.ComponentModel.DataAnnotations;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten
{
    public class CursusInstantie : IEquatable<CursusInstantie>
    {
        public int ID{ get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDatum{ get; set; }

        [Required]
        public Cursus Cursus { get; set; }

        public bool Equals(CursusInstantie other)
        {
            if( this.Cursus.Code == other.Cursus.Code &&
                this.Cursus.Duur == other.Cursus.Duur &&
                this.Cursus.Titel == other.Cursus.Titel &&
                this.StartDatum == other.StartDatum )
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return  Cursus.Code.GetHashCode() ^ Cursus.Duur.GetHashCode() ^ Cursus.Titel.GetHashCode();
        }
    }
}
