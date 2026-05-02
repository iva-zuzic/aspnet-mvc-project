using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarketplace.Models;

public class Oglas
{
    [Key]
    public int Id { get; set; }
    public string Naslov { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public decimal Cijena { get; set; }
    public DateTime DatumObjave { get; set; }
    public DateTime? DatumIzmjene { get; set; }
    public StatusOglasa Status { get; set; }
    public TipOglasa TipOglasa { get; set; }
    public StanjeArtikla StanjeArtikla { get; set; }

    // N-strana veze s Korisnikom (1-N)
    [ForeignKey("Korisnik")]
    public int KorisnikId { get; set; }
    public virtual Korisnik Korisnik { get; set; } = null!;

    // N-strana veze s Gradom (1-N)
    [ForeignKey("Grad")]
    public int GradId { get; set; }
    public virtual Grad Grad { get; set; } = null!;

    // Ovisno o tipu, oglas ima ili Knjigu ili DrustvenuIgru (ali ne obje)
    public virtual Knjiga? Knjiga { get; set; }
    public virtual DrustvenaIgra? DrustvenaIgra { get; set; }

    // 1-N: jedan oglas može imati više slika
    public virtual ICollection<Slika> Slike { get; set; } = new List<Slika>();

    // 1-N: veza prema Favoritu (N-N veza s Korisnikom)
    public virtual ICollection<Favorit> Favoriti { get; set; } = new List<Favorit>();

    // 1-N: jedan oglas može imati više poruka
    public virtual ICollection<Poruka> Poruke { get; set; } = new List<Poruka>();
}
