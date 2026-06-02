namespace BookMarketplace.Web.DTOs.Korisnik;

public class KorisnikDTO
{
    public int Id { get; set; }
    public string ImeIPrezime { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefon { get; set; } = string.Empty;
    public string Uloga { get; set; } = string.Empty;
}