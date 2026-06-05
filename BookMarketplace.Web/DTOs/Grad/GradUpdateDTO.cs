using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Web.DTOs.Grad;

public class GradUpdateDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Naziv { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string PostanskiBroj { get; set; } = string.Empty;
}