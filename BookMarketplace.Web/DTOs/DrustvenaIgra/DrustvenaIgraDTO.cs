namespace BookMarketplace.Web.DTOs.DrustvenaIgra;

public class DrustvenaIgraDTO
{
    public int Id { get; set; }
    public int OglasId { get; set; }

    public string Naziv { get; set; } = string.Empty;
    public int MinBrojIgraca { get; set; }
    public int MaxBrojIgraca { get; set; }
    public int MinimalnaDob { get; set; }
    public int TrajanjeMins { get; set; }
    public string Zanr { get; set; } = string.Empty;
    public string BrojIgracaPrikaz { get; set; } = string.Empty;
}