namespace BookMarketplace.Web.DTOs.Favorit;

public class FavoritDTO
{
    public int KorisnikId { get; set; }
    public string KorisnikIme { get; set; } = string.Empty;

    public int OglasId { get; set; }
    public string OglasNaslov { get; set; } = string.Empty;

    public DateTime DatumDodavanja { get; set; }
}