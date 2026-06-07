using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Web.DTOs.Poruka;

public class PorukaCreateDTO
{
    [Required]
    public int PosiljateljId { get; set; }

    [Required]
    public int PrimateljId { get; set; }

    [Required]
    public int OglasId { get; set; }

    [Required]
    [StringLength(2000, MinimumLength = 1)]
    public string Sadrzaj { get; set; } = string.Empty;
}