using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Areas.Admin.ViewModels;

public class GradCreateModel
{
    [Required(ErrorMessage = "Naziv grada je obavezan.")]
    [StringLength(100, MinimumLength = 2, 
        ErrorMessage = "Naziv mora imati između 2 i 100 znakova.")]
    [Display(Name = "Naziv grada")]
    public string Naziv { get; set; } = string.Empty;

    [Required(ErrorMessage = "Poštanski broj je obavezan.")]
    [RegularExpression(@"^\d{5}$", 
        ErrorMessage = "Poštanski broj mora imati točno 5 znamenki.")]
    [Display(Name = "Poštanski broj")]
    public string PostanskiBroj { get; set; } = string.Empty;
}