namespace BookMarketplace.Web.DTOs.Poruka;

public class PorukaDTO
{
    public int Id { get; set; }

    public int PosiljateljId { get; set; }
    public string PosiljateljIme { get; set; } = string.Empty;

    public int PrimateljId { get; set; }
    public string PrimateljIme { get; set; } = string.Empty;

    public int OglasId { get; set; }
    public string OglasNaslov { get; set; } = string.Empty;

    public string Sadrzaj { get; set; } = string.Empty;
    public DateTime DatumSlanja { get; set; }
    public bool Procitano { get; set; }
}