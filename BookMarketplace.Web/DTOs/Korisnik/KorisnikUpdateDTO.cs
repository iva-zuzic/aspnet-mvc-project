using System.ComponentModel.DataAnnotations;
using BookMarketplace.Model;

namespace BookMarketplace.Web.DTOs.Korisnik;

public class KorisnikUpdateDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string ImeIPrezime { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string Telefon { get; set; } = string.Empty;

    [Required]
    public UlogaKorisnika Uloga { get; set; }
}