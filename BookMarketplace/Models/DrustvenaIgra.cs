namespace BookMarketplace.Models;

public class DrustvenaIgra
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public int MinBrojIgraca { get; set; }
    public int MaxBrojIgraca { get; set; }
    public int MinimalnasDob { get; set; }
    public int TrajanjeMins { get; set; }
    public ZanrIgre Zanr { get; set; }

    // N-strana veze s Oglasom (1-1)
    public int OglasId { get; set; }
    public Oglas Oglas { get; set; } = null!;
}
