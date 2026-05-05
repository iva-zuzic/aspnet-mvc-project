using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarketplace.Model;

public class Knjiga
{
    [Key]
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public string Izdavac { get; set; } = string.Empty;
    public int GodinaIzdanja { get; set; }
    public string Jezik { get; set; } = string.Empty;
    public ZanrKnjige Zanr { get; set; }


    [ForeignKey("Oglas")]
    public int OglasId { get; set; }
    public virtual Oglas Oglas { get; set; } = null!;
}
