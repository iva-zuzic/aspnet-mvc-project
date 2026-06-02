using BookMarketplace.Web.DTOs.DrustvenaIgra;
using BookMarketplace.Web.DTOs.Grad;
using BookMarketplace.Web.DTOs.Knjiga;
using BookMarketplace.Web.DTOs.Korisnik;
using BookMarketplace.Web.DTOs.Slika;

namespace BookMarketplace.Web.DTOs.Oglas;

public class OglasDTO
{
    public int Id { get; set; }
    public string Naslov { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public decimal Cijena { get; set; }
    public DateTime DatumObjave { get; set; }
    public DateTime? DatumIsteka { get; set; }
    public DateTime? DatumIzmjene { get; set; }

    public string Status { get; set; } = string.Empty;
    public string TipOglasa { get; set; } = string.Empty;
    public string StanjeArtikla { get; set; } = string.Empty;

    public GradDTO? Grad { get; set; }
    public KorisnikDTO? Korisnik { get; set; }

    public KnjigaDTO? Knjiga { get; set; }
    public DrustvenaIgraDTO? DrustvenaIgra { get; set; }

    public List<SlikaDTO> Slike { get; set; } = new();
}