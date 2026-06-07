using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Web.DTOs.Favorit;

public class FavoritUpdateDTO
{
    [Required]
    public int KorisnikId { get; set; }

    [Required]
    public int OglasId { get; set; }

    public DateTime DatumDodavanja { get; set; }
}