using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookMarketplace.Models;

public class Slika
{
    [Key]
    public int Id { get; set; }
    public string Putanja { get; set; } = string.Empty;
    public int RedoslijedPrikaza { get; set; }

    // N-strana veze s Oglasom (1-N)
    [ForeignKey("Oglas")]
    public int OglasId { get; set; }
    public virtual Oglas Oglas { get; set; } = null!;
}
