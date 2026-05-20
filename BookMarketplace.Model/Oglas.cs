using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarketplace.Model;

public class Oglas
{
    [Key]
    public int Id { get; set; }
    public string Naslov { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public decimal Cijena { get; set; }
    public DateTime DatumObjave { get; set; }
    public DateTime? DatumIsteka { get; set; }
    public DateTime? DatumIzmjene { get; set; }
    public StatusOglasa Status { get; set; }
    public TipOglasa TipOglasa { get; set; }
    public StanjeArtikla StanjeArtikla { get; set; }

    [ForeignKey("Korisnik")]
    public int KorisnikId { get; set; }
    public virtual Korisnik Korisnik { get; set; } = null!;

    [ForeignKey("Grad")]
    public int GradId { get; set; }
    public virtual Grad Grad { get; set; } = null!;

    public virtual Knjiga? Knjiga { get; set; }
    public virtual DrustvenaIgra? DrustvenaIgra { get; set; }
    

    public virtual ICollection<Slika> Slike { get; set; } = new List<Slika>();

    public virtual ICollection<Favorit> Favoriti { get; set; } = new List<Favorit>();

    public virtual ICollection<Poruka> Poruke { get; set; } = new List<Poruka>();
}
