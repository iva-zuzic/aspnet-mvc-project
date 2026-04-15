namespace BookMarketplace.Models;

public class Grad
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public string PostanskiBroj { get; set; } = string.Empty;

    // 1-N: jedan grad može imati više oglasa
    public List<Oglas> Oglasi { get; set; } = [];
}
