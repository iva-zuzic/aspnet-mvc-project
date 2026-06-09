namespace BookMarketplace.Web.ViewModels;

public class MyAccountViewModel
{
    public string Email { get; set; } = string.Empty;
    public string? OIB { get; set; }
    public string? JMBG { get; set; }
    public List<string> Roles { get; set; } = new();

    public string? ImeIPrezime { get; set; }
    public string? Telefon { get; set; }
    public int BrojOglasa { get; set; }
}