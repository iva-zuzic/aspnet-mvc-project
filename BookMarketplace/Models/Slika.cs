namespace BookMarketplace.Models;

public class Slika
{
    public int Id { get; set; }
    public string Putanja { get; set; } = string.Empty;
    public int RedoslijedPrikaza { get; set; }

    // N-strana veze s Oglasom (1-N)
    public int OglasId { get; set; }
    public Oglas Oglas { get; set; } = null!;
}
