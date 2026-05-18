using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Web.ViewModels;

public class KorisnikEditModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ime i prezime je obavezno.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Ime mora imati između 3 i 100 znakova.")]
    [Display(Name = "Ime i prezime")]
    public string ImeIPrezime { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email je obavezan.")]
    [EmailAddress(ErrorMessage = "Unesite ispravnu email adresu.")]
    [StringLength(150)]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon je obavezan.")]
    [StringLength(20)]
    [Display(Name = "Telefon")]
    public string Telefon { get; set; } = string.Empty;
}