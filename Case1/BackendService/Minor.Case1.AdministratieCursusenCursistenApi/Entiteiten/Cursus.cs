using System.ComponentModel.DataAnnotations;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten
{
    public class Cursus
    {
        [Key]
        [StringLength(10)]
        public string Code { get; set; }

        /// <summary>
        /// Duur in Dagen
        /// </summary>
        public int Duur { get; set; }

        [StringLength(300)]
        public string Titel { get; set; }
    }
}