using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarketplace.Model;

public class Favorit
{
    [ForeignKey("Korisnik")]
    public int KorisnikId { get; set; }
    public virtual Korisnik Korisnik { get; set; } = null!;

    [ForeignKey("Oglas")]
    public int OglasId { get; set; }
    public virtual Oglas Oglas { get; set; } = null!;

    public DateTime DatumDodavanja { get; set; }
}
