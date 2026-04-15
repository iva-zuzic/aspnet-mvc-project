namespace BookMarketplace.Models;

public class Korisnik
{
    public int Id { get; set; }
    public string ImeIPrezime { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Lozinka { get; set; } = string.Empty;
    public string Telefon { get; set; } = string.Empty;
    public DateTime DatumRegistracije { get; set; }
    public UlogaKorisnika Uloga { get; set; }

    // 1-N: jedan korisnik može imati više oglasa
    public List<Oglas> Oglasi { get; set; } = [];

    // 1-N: jedan korisnik može imati više favorita (N-N veza s Oglasom)
    public List<Favorit> Favoriti { get; set; } = [];

    // 1-N: jedan korisnik može poslati više poruka
    public List<Poruka> PoslanePoruke { get; set; } = [];

    // 1-N: jedan korisnik može primiti više poruka
    public List<Poruka> PrimljenePoruke { get; set; } = [];
}
