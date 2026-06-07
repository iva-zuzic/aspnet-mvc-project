using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Web.DTOs.Poruka;

public class PorukaUpdateDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(2000, MinimumLength = 1)]
    public string Sadrzaj { get; set; } = string.Empty;

    public bool Procitano { get; set; }
}