using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Web.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Ime i prezime su obavezni.")]
    [StringLength(100, ErrorMessage = "Ime i prezime može imati najviše 100 znakova.")]
    [Display(Name = "Ime i prezime")]
    public string ImeIPrezime { get; set; } = string.Empty;

    [Required(ErrorMessage = "Korisničko ime je obavezno.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Korisničko ime mora imati između 3 i 50 znakova.")]
    [RegularExpression(@"^[a-zA-Z0-9._-]+$", ErrorMessage = "Korisničko ime smije sadržavati samo slova, brojeve, točku, crticu i donju crticu.")]
    [Display(Name = "Korisničko ime")]
    public string KorisnickoIme { get; set; } = string.Empty;

    [StringLength(30, ErrorMessage = "Telefon može imati najviše 30 znakova.")]
    [Display(Name = "Telefon")]
    public string? Telefon { get; set; }

    [Required(ErrorMessage = "Grad je obavezan.")]
    [Display(Name = "Grad")]
    public int GradId { get; set; }

    [Required(ErrorMessage = "Email je obavezan.")]
    [EmailAddress(ErrorMessage = "Unesite ispravnu email adresu.")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lozinka je obavezna.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Lozinka mora imati najmanje 6 znakova.")]
    [DataType(DataType.Password)]
    [Display(Name = "Lozinka")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Potvrda lozinke je obavezna.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Lozinke se ne podudaraju.")]
    [Display(Name = "Potvrdi lozinku")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "OIB je obavezan.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "OIB mora imati točno 11 znamenki.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "OIB mora sadržavati samo 11 znamenki.")]
    [Display(Name = "OIB")]
    public string OIB { get; set; } = string.Empty;

    [Required(ErrorMessage = "JMBG je obavezan.")]
    [StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG mora imati točno 13 znamenki.")]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "JMBG mora sadržavati samo 13 znamenki.")]
    [Display(Name = "JMBG")]
    public string JMBG { get; set; } = string.Empty;
}