namespace BookMarketplace.Models;

public class Poruka
{
    public int Id { get; set; }
    public string Sadrzaj { get; set; } = string.Empty;
    public bool Procitano { get; set; }
    public DateTime DatumSlanja { get; set; }

    // N-strana veze s Korisnikom (pošiljatelj)
    public int PosiljateljId { get; set; }
    public Korisnik Posiljatelj { get; set; } = null!;

    // N-strana veze s Korisnikom (primatelj)
    public int PrimateljId { get; set; }
    public Korisnik Primatelj { get; set; } = null!;

    // N-strana veze s Oglasom
    public int OglasId { get; set; }
    public Oglas Oglas { get; set; } = null!;
}
