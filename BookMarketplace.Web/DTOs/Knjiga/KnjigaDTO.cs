namespace BookMarketplace.Web.DTOs.Knjiga;

public class KnjigaDTO
{
    public int Id { get; set; }
    public int OglasId { get; set; }

    public string Naziv { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public string Izdavac { get; set; } = string.Empty;
    public int GodinaIzdanja { get; set; }
    public int BrojStrana { get; set; }
    public string Jezik { get; set; } = string.Empty;
    public string Zanr { get; set; } = string.Empty;
}