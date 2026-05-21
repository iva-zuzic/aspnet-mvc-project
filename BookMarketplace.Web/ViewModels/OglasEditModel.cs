using System.ComponentModel.DataAnnotations;
using BookMarketplace.Model;

namespace BookMarketplace.Web.ViewModels;

public class OglasEditModel : IValidatableObject
{
    public int Id { get; set; }

    public TipOglasa TipOglasa { get; set; }

    public int KorisnikId { get; set; }

    [Display(Name = "Vlasnik")]
    public string? KorisnikIme { get; set; }

    [Required(ErrorMessage = "Naslov je obavezan.")]
    [StringLength(150, MinimumLength = 3,
        ErrorMessage = "Naslov mora imati između 3 i 150 znakova.")]
    [Display(Name = "Naslov oglasa")]
    public string Naslov { get; set; } = string.Empty;

    [Required(ErrorMessage = "Opis je obavezan.")]
    [StringLength(2000, MinimumLength = 10,
        ErrorMessage = "Opis mora imati između 10 i 2000 znakova.")]
    [Display(Name = "Opis")]
    public string Opis { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cijena je obavezna.")]
    [Range(0.01, 100000, ErrorMessage = "Cijena mora biti između 0,01 € i 100.000 €.")]
    [Display(Name = "Cijena (€)")]
    public decimal Cijena { get; set; }

    [Required(ErrorMessage = "Datum i vrijeme isteka oglasa su obavezni.")]
    [Display(Name = "Oglas traje do")]
    [DataType(DataType.DateTime)]
    public DateTime? DatumIsteka { get; set; }

    [Required(ErrorMessage = "Grad je obavezan.")]
    [Display(Name = "Grad")]
    public int GradId { get; set; }

    [Required(ErrorMessage = "Stanje artikla je obavezno.")]
    [Display(Name = "Stanje artikla")]
    public StanjeArtikla StanjeArtikla { get; set; }

    [Display(Name = "Naziv knjige")]
    [StringLength(200)]
    public string? KnjigaNaziv { get; set; }

    [Display(Name = "Autor")]
    [StringLength(150)]
    public string? KnjigaAutor { get; set; }

    [Display(Name = "ISBN")]
    [StringLength(20)]
    public string? KnjigaISBN { get; set; }

    [Display(Name = "Izdavač")]
    [StringLength(100)]
    public string? KnjigaIzdavac { get; set; }

    [Display(Name = "Godina izdanja")]
    [Range(1500, 2100, ErrorMessage = "Godina izdanja mora biti između 1500. i 2100.")]
    public int? KnjigaGodinaIzdanja { get; set; }

    [Display(Name = "Broj strana")]
    [Range(1, 10000, ErrorMessage = "Broj strana mora biti između 1 i 10.000.")]
    public int? KnjigaBrojStrana { get; set; }

    [Display(Name = "Jezik")]
    [StringLength(50)]
    public string? KnjigaJezik { get; set; }

    [Display(Name = "Žanr knjige")]
    public ZanrKnjige? KnjigaZanr { get; set; }

    [Display(Name = "Naziv igre")]
    [StringLength(200)]
    public string? IgraNaziv { get; set; }

    [Display(Name = "Minimalni broj igrača")]
    [Range(1, 50, ErrorMessage = "Minimalni broj igrača mora biti između 1 i 50.")]
    public int? IgraMinBrojIgraca { get; set; }

    [Display(Name = "Maksimalni broj igrača")]
    [Range(1, 50, ErrorMessage = "Maksimalni broj igrača mora biti između 1 i 50.")]
    public int? IgraMaxBrojIgraca { get; set; }

    [Display(Name = "Minimalna dob")]
    [Range(1, 100, ErrorMessage = "Minimalna dob mora biti između 1 i 100 godina.")]
    public int? IgraMinimalnaDob { get; set; }

    [Display(Name = "Trajanje (minute)")]
    [Range(1, 1000, ErrorMessage = "Trajanje mora biti između 1 i 1000 minuta.")]
    public int? IgraTrajanjeMins { get; set; }

    [Display(Name = "Žanr igre")]
    public ZanrIgre? IgraZanr { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DatumIsteka == null)
        {
            yield return new ValidationResult(
                "Datum i vrijeme isteka oglasa su obavezni.",
                new[] { nameof(DatumIsteka) });
        }
        else if (DatumIsteka <= DateTime.Now)
        {
            yield return new ValidationResult(
                "Datum isteka oglasa mora biti u budućnosti.",
                new[] { nameof(DatumIsteka) });
        }

        if (TipOglasa == TipOglasa.Knjiga)
        {
            if (string.IsNullOrWhiteSpace(KnjigaNaziv))
                yield return new ValidationResult(
                    "Naziv knjige je obavezan.", 
                    new[] { nameof(KnjigaNaziv) });

            if (string.IsNullOrWhiteSpace(KnjigaAutor))
                yield return new ValidationResult(
                    "Autor je obavezan.", 
                    new[] { nameof(KnjigaAutor) });

            if (KnjigaZanr == null)
                yield return new ValidationResult(
                    "Žanr knjige je obavezan.", 
                    new[] { nameof(KnjigaZanr) });

            if (KnjigaGodinaIzdanja == null)
                yield return new ValidationResult(
                    "Godina izdanja je obavezna.",
                    new[] { nameof(KnjigaGodinaIzdanja) });

            if (KnjigaBrojStrana == null)
                yield return new ValidationResult(
                    "Broj strana je obavezan.",
                    new[] { nameof(KnjigaBrojStrana) });
        }

        if (TipOglasa == TipOglasa.DrustvenaIgra)
        {
            if (string.IsNullOrWhiteSpace(IgraNaziv))
                yield return new ValidationResult(
                    "Naziv igre je obavezan.", 
                    new[] { nameof(IgraNaziv) });

            if (IgraMinBrojIgraca == null)
                yield return new ValidationResult(
                    "Minimalni broj igrača je obavezan.", 
                    new[] { nameof(IgraMinBrojIgraca) });

            if (IgraMaxBrojIgraca == null)
                yield return new ValidationResult(
                    "Maksimalni broj igrača je obavezan.", 
                    new[] { nameof(IgraMaxBrojIgraca) });

            if (IgraMinBrojIgraca > IgraMaxBrojIgraca)
                yield return new ValidationResult(
                    "Minimalni broj igrača ne može biti veći od maksimalnog.", 
                    new[] { nameof(IgraMinBrojIgraca) });

            if (IgraZanr == null)
                yield return new ValidationResult(
                    "Žanr igre je obavezan.", 
                    new[] { nameof(IgraZanr) });

            if (IgraMinimalnaDob == null)
                yield return new ValidationResult(
                    "Minimalna dob je obavezna.",
                    new[] { nameof(IgraMinimalnaDob) });

            if (IgraTrajanjeMins == null)
                yield return new ValidationResult(
                    "Trajanje igre je obavezno.",
                    new[] { nameof(IgraTrajanjeMins) });
        }
    }
}