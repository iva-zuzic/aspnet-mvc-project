using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Models;

public class Korisnik
{
    [Key]
    public int Id { get; set; }
    public string ImeIPrezime { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Lozinka { get; set; } = string.Empty;
    public string Telefon { get; set; } = string.Empty;
    public DateTime DatumRegistracije { get; set; }
    public UlogaKorisnika Uloga { get; set; }

    // 1-N: jedan korisnik može imati više oglasa
    public virtual ICollection<Oglas> Oglasi { get; set; } = new List<Oglas>();

    // 1-N: jedan korisnik može imati više favorita (N-N veza s Oglasom)
    public virtual ICollection<Favorit> Favoriti { get; set; } = new List<Favorit>();

    // 1-N: jedan korisnik može poslati više poruka
    public virtual ICollection<Poruka> PoslanePoruke { get; set; } = new List<Poruka>();

    // 1-N: jedan korisnik može primiti više poruka
    public virtual ICollection<Poruka> PrimljenePoruke { get; set; } = new List<Poruka>();
}
