using System.ComponentModel.DataAnnotations;
using BookMarketplace.Model;

namespace BookMarketplace.Web.DTOs.Knjiga;

public class KnjigaUpdateDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Naziv { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Autor { get; set; } = string.Empty;

    [StringLength(20)]
    public string ISBN { get; set; } = string.Empty;

    [StringLength(100)]
    public string Izdavac { get; set; } = string.Empty;

    [Range(1, 3000)]
    public int GodinaIzdanja { get; set; }

    [Range(1, 10000)]
    public int BrojStrana { get; set; }

    [Required]
    [StringLength(50)]
    public string Jezik { get; set; } = string.Empty;

    [Required]
    public ZanrKnjige Zanr { get; set; }
}