using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Web.DTOs.Favorit;

public class FavoritCreateDTO
{
    [Required]
    public int KorisnikId { get; set; }

    [Required]
    public int OglasId { get; set; }
}