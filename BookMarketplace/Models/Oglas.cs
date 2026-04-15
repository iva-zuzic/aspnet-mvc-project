namespace BookMarketplace.Models;

public class Oglas
{
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
    public int KorisnikId { get; set; }
    public Korisnik Korisnik { get; set; } = null!;

    // N-strana veze s Gradom (1-N)
    public int GradId { get; set; }
    public Grad Grad { get; set; } = null!;

    // Ovisno o tipu, oglas ima ili Knjigu ili DrustvenuIgru (ali ne obje)
    public Knjiga? Knjiga { get; set; }
    public DrustvenaIgra? DrustvenaIgra { get; set; }

    // 1-N: jedan oglas može imati više slika
    public List<Slika> Slike { get; set; } = [];

    // 1-N: veza prema Favoritu (N-N veza s Korisnikom)
    public List<Favorit> Favoriti { get; set; } = [];

    // 1-N: jedan oglas može imati više poruka
    public List<Poruka> Poruke { get; set; } = [];
}
