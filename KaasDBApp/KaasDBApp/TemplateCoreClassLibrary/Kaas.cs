using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Kaas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(32)]
    public string CategorieNaam { get; set; }
    /// <summary>
    /// Leeftijd in weken.
    /// </summary>
    public int Leeftijd { get; set; }
}