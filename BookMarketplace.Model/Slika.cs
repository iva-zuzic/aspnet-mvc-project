using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarketplace.Model;

public class Slika
{
    [Key]
    public int Id { get; set; }

    public string Putanja { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long FileSize { get; set; }

    public DateTime CreatedAt { get; set; }

    public int RedoslijedPrikaza { get; set; }

    [ForeignKey("Oglas")]
    public int OglasId { get; set; }

    public virtual Oglas Oglas { get; set; } = null!;
}