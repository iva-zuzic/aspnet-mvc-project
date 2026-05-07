using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Model;

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

    public virtual ICollection<Oglas> Oglasi { get; set; } = new List<Oglas>();

    public virtual ICollection<Favorit> Favoriti { get; set; } = new List<Favorit>();

    public virtual ICollection<Poruka> PoslanePoruke { get; set; } = new List<Poruka>();

    public virtual ICollection<Poruka> PrimljenePoruke { get; set; } = new List<Poruka>();
}
