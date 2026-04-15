namespace BookMarketplace.Models;

// Predstavlja N-N vezu između Korisnika i Oglasa
public class Favorit
{
    public int KorisnikId { get; set; }
    public Korisnik Korisnik { get; set; } = null!;

    public int OglasId { get; set; }
    public Oglas Oglas { get; set; } = null!;

    public DateTime DatumDodavanja { get; set; }
}
