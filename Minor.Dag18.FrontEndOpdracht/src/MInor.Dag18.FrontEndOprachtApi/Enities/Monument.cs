using System.ComponentModel.DataAnnotations;

namespace Enities
{
    public class Monument
    {
        [Key]
        public int ID { get; set; }
        public string Naam { get; set; }
    }
}