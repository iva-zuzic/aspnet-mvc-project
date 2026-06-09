using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookMarketplace.Model;

public class AppUser : IdentityUser
{
    [MaxLength(11)]
    public string? OIB { get; set; }

    [MaxLength(13)]
    public string? JMBG { get; set; }
}