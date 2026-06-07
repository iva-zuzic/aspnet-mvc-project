using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Web.DTOs.Slika;

public class SlikaUpdateDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(500)]
    public string Putanja { get; set; } = string.Empty;

    [Range(0, 100)]
    public int RedoslijedPrikaza { get; set; }
}