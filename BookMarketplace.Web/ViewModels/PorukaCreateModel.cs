using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Web.ViewModels;

public class PorukaCreateModel
{
    public int OglasId { get; set; }

    public string OglasNaslov { get; set; } = string.Empty;

    public string PrimateljIme { get; set; } = string.Empty;

    [Required(ErrorMessage = "Poruka je obavezna.")]
    [StringLength(1000, MinimumLength = 2, ErrorMessage = "Poruka mora imati između 2 i 1000 znakova.")]
    [Display(Name = "Poruka")]
    public string Sadrzaj { get; set; } = string.Empty;
}