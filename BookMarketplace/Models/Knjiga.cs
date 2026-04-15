namespace BookMarketplace.Models;

public class Knjiga
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public string Izdavac { get; set; } = string.Empty;
    public int GodinaIzdanja { get; set; }
    public string Jezik { get; set; } = string.Empty;
    public ZanrKnjige Zanr { get; set; }

    // N-strana veze s Oglasom (1-1)
    public int OglasId { get; set; }
    public Oglas Oglas { get; set; } = null!;
}
