using System.ComponentModel.DataAnnotations;
using BookMarketplace.Model;

namespace BookMarketplace.Web.DTOs.Oglas;

public class OglasCreateDTO
{
    [Required]
    [StringLength(150, MinimumLength = 3)]
    public string Naslov { get; set; } = string.Empty;

    [Required]
    [StringLength(2000, MinimumLength = 10)]
    public string Opis { get; set; } = string.Empty;

    [Range(0.01, 100000)]
    public decimal Cijena { get; set; }

    public DateTime? DatumIsteka { get; set; }

    [Required]
    public int KorisnikId { get; set; }

    [Required]
    public int GradId { get; set; }

    [Required]
    public TipOglasa TipOglasa { get; set; }

    [Required]
    public StanjeArtikla StanjeArtikla { get; set; }

    public string? KnjigaNaziv { get; set; }
    public string? KnjigaAutor { get; set; }
    public string? KnjigaISBN { get; set; }
    public string? KnjigaIzdavac { get; set; }
    public int? KnjigaGodinaIzdanja { get; set; }
    public int? KnjigaBrojStrana { get; set; }
    public string? KnjigaJezik { get; set; }
    public ZanrKnjige? KnjigaZanr { get; set; }

    public string? IgraNaziv { get; set; }
    public int? IgraMinBrojIgraca { get; set; }
    public int? IgraMaxBrojIgraca { get; set; }
    public int? IgraMinimalnaDob { get; set; }
    public int? IgraTrajanjeMins { get; set; }
    public ZanrIgre? IgraZanr { get; set; }
}