using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Models;

public class Grad
{
    [Key]
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public string PostanskiBroj { get; set; } = string.Empty;


    public virtual ICollection<Oglas> Oglasi { get; set; } = new List<Oglas>();
}
