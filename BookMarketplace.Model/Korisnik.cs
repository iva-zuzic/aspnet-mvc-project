using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Model;

public class Korisnik
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "Ime i prezime")]
    public string ImeIPrezime { get; set; } = string.Empty;
    [Display(Name = "Email adresa")]
    public string Email { get; set; } = string.Empty;
    [Display(Name = "Lozinka")]
    public string Lozinka { get; set; } = string.Empty;
    [Display(Name = "Broj telefona")]
    public string Telefon { get; set; } = string.Empty;
    [Display(Name = "Datum registracije")]
    public DateTime DatumRegistracije { get; set; }
    [Display(Name = "Uloga")]
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
