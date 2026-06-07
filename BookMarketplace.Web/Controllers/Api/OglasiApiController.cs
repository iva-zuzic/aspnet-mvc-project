using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.DrustvenaIgra;
using BookMarketplace.Web.DTOs.Grad;
using BookMarketplace.Web.DTOs.Knjiga;
using BookMarketplace.Web.DTOs.Korisnik;
using BookMarketplace.Web.DTOs.Oglas;
using BookMarketplace.Web.DTOs.Slika;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers.Api;

[Route("api/oglasi")]
[ApiController]
public class OglasiApiController : ControllerBase
{
    private readonly BookMarketplaceDbContext _context;

    public OglasiApiController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OglasDTO>>> GetAll(string? query = null)
    {
        var oglasiQuery = _context.Oglasi
            .Include(o => o.Korisnik)
            .Include(o => o.Grad)
            .Include(o => o.Knjiga)
            .Include(o => o.DrustvenaIgra)
            .Include(o => o.Slike)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            var searchTerm = query.Trim().ToLower();

            oglasiQuery = oglasiQuery.Where(o =>
                o.Naslov.ToLower().Contains(searchTerm) ||
                o.Opis.ToLower().Contains(searchTerm) ||
                (o.Knjiga != null &&
                    (
                        o.Knjiga.Naziv.ToLower().Contains(searchTerm) ||
                        o.Knjiga.Autor.ToLower().Contains(searchTerm)
                    )) ||
                (o.DrustvenaIgra != null &&
                    o.DrustvenaIgra.Naziv.ToLower().Contains(searchTerm)));
        }

        var oglasi = await oglasiQuery
            .OrderByDescending(o => o.DatumObjave)
            .ToListAsync();

        return Ok(oglasi.Select(ToDTO));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OglasDTO>> GetById(int id)
    {
        var oglas = await _context.Oglasi
            .Include(o => o.Korisnik)
            .Include(o => o.Grad)
            .Include(o => o.Knjiga)
            .Include(o => o.DrustvenaIgra)
            .Include(o => o.Slike)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (oglas == null)
        {
            return NotFound();
        }

        return Ok(ToDTO(oglas));
    }

    [HttpPost]
    public async Task<ActionResult<OglasDTO>> Create(OglasCreateDTO model)
    {
        var validationError = await ValidateCreateModel(model);

        if (validationError != null)
        {
            return BadRequest(validationError);
        }

        var oglas = new Oglas
        {
            Naslov = model.Naslov.Trim(),
            Opis = model.Opis.Trim(),
            Cijena = model.Cijena,
            DatumObjave = DateTime.Now,
            DatumIsteka = model.DatumIsteka,
            Status = StatusOglasa.Aktivan,
            TipOglasa = model.TipOglasa,
            StanjeArtikla = model.StanjeArtikla,
            KorisnikId = model.KorisnikId,
            GradId = model.GradId
        };

        if (model.TipOglasa == TipOglasa.Knjiga)
        {
            oglas.Knjiga = new Knjiga
            {
                Naziv = model.KnjigaNaziv?.Trim() ?? string.Empty,
                Autor = model.KnjigaAutor?.Trim() ?? string.Empty,
                ISBN = model.KnjigaISBN?.Trim() ?? string.Empty,
                Izdavac = model.KnjigaIzdavac?.Trim() ?? string.Empty,
                GodinaIzdanja = model.KnjigaGodinaIzdanja ?? 0,
                BrojStrana = model.KnjigaBrojStrana ?? 0,
                Jezik = model.KnjigaJezik?.Trim() ?? string.Empty,
                Zanr = model.KnjigaZanr ?? ZanrKnjige.Drama
            };
        }
        else if (model.TipOglasa == TipOglasa.DrustvenaIgra)
        {
            oglas.DrustvenaIgra = new DrustvenaIgra
            {
                Naziv = model.IgraNaziv?.Trim() ?? string.Empty,
                MinBrojIgraca = model.IgraMinBrojIgraca ?? 1,
                MaxBrojIgraca = model.IgraMaxBrojIgraca ?? 1,
                MinimalnaDob = model.IgraMinimalnaDob ?? 0,
                TrajanjeMins = model.IgraTrajanjeMins ?? 0,
                Zanr = model.IgraZanr ?? ZanrIgre.Zabavna
            };
        }

        _context.Oglasi.Add(oglas);
        await _context.SaveChangesAsync();

        var createdOglas = await _context.Oglasi
            .Include(o => o.Korisnik)
            .Include(o => o.Grad)
            .Include(o => o.Knjiga)
            .Include(o => o.DrustvenaIgra)
            .Include(o => o.Slike)
            .FirstAsync(o => o.Id == oglas.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdOglas.Id },
            ToDTO(createdOglas));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OglasDTO>> Update(int id, OglasUpdateDTO model)
    {
        if (id != model.Id)
        {
            return BadRequest("ID iz rute se ne podudara s ID-em iz modela.");
        }

        var oglas = await _context.Oglasi
            .Include(o => o.Knjiga)
            .Include(o => o.DrustvenaIgra)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (oglas == null)
        {
            return NotFound();
        }

        var gradExists = await _context.Gradovi.AnyAsync(g => g.Id == model.GradId);

        if (!gradExists)
        {
            return BadRequest("Odabrani grad ne postoji.");
        }

        oglas.Naslov = model.Naslov.Trim();
        oglas.Opis = model.Opis.Trim();
        oglas.Cijena = model.Cijena;
        oglas.DatumIsteka = model.DatumIsteka;
        oglas.DatumIzmjene = DateTime.Now;
        oglas.GradId = model.GradId;
        oglas.StanjeArtikla = model.StanjeArtikla;

        if (oglas.TipOglasa == TipOglasa.Knjiga && oglas.Knjiga != null)
        {
            oglas.Knjiga.Naziv = model.KnjigaNaziv?.Trim() ?? string.Empty;
            oglas.Knjiga.Autor = model.KnjigaAutor?.Trim() ?? string.Empty;
            oglas.Knjiga.ISBN = model.KnjigaISBN?.Trim() ?? string.Empty;
            oglas.Knjiga.Izdavac = model.KnjigaIzdavac?.Trim() ?? string.Empty;
            oglas.Knjiga.GodinaIzdanja = model.KnjigaGodinaIzdanja ?? 0;
            oglas.Knjiga.BrojStrana = model.KnjigaBrojStrana ?? 0;
            oglas.Knjiga.Jezik = model.KnjigaJezik?.Trim() ?? string.Empty;
            oglas.Knjiga.Zanr = model.KnjigaZanr ?? ZanrKnjige.Drama;
        }
        else if (oglas.TipOglasa == TipOglasa.DrustvenaIgra && oglas.DrustvenaIgra != null)
        {
            oglas.DrustvenaIgra.Naziv = model.IgraNaziv?.Trim() ?? string.Empty;
            oglas.DrustvenaIgra.MinBrojIgraca = model.IgraMinBrojIgraca ?? 1;
            oglas.DrustvenaIgra.MaxBrojIgraca = model.IgraMaxBrojIgraca ?? 1;
            oglas.DrustvenaIgra.MinimalnaDob = model.IgraMinimalnaDob ?? 0;
            oglas.DrustvenaIgra.TrajanjeMins = model.IgraTrajanjeMins ?? 0;
            oglas.DrustvenaIgra.Zanr = model.IgraZanr ?? ZanrIgre.Zabavna;
        }

        await _context.SaveChangesAsync();

        var updatedOglas = await _context.Oglasi
            .Include(o => o.Korisnik)
            .Include(o => o.Grad)
            .Include(o => o.Knjiga)
            .Include(o => o.DrustvenaIgra)
            .Include(o => o.Slike)
            .FirstAsync(o => o.Id == id);

        return Ok(ToDTO(updatedOglas));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var oglas = await _context.Oglasi
            .FirstOrDefaultAsync(o => o.Id == id);

        if (oglas == null)
        {
            return NotFound();
        }

        oglas.Status = StatusOglasa.Izbrisan;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<string?> ValidateCreateModel(OglasCreateDTO model)
    {
        var korisnikExists = await _context.Korisnici.AnyAsync(k => k.Id == model.KorisnikId);

        if (!korisnikExists)
        {
            return "Odabrani korisnik ne postoji.";
        }

        var gradExists = await _context.Gradovi.AnyAsync(g => g.Id == model.GradId);

        if (!gradExists)
        {
            return "Odabrani grad ne postoji.";
        }

        if (model.DatumIsteka <= DateTime.Now)
        {
            return "Datum isteka oglasa mora biti u budućnosti.";
        }

        if (model.TipOglasa == TipOglasa.Knjiga)
        {
            if (string.IsNullOrWhiteSpace(model.KnjigaNaziv))
            {
                return "Naziv knjige je obavezan.";
            }

            if (string.IsNullOrWhiteSpace(model.KnjigaAutor))
            {
                return "Autor knjige je obavezan.";
            }

            if (model.KnjigaGodinaIzdanja == null)
            {
                return "Godina izdanja je obavezna.";
            }

            if (model.KnjigaBrojStrana == null)
            {
                return "Broj strana je obavezan.";
            }

            if (model.KnjigaZanr == null)
            {
                return "Žanr knjige je obavezan.";
            }
        }

        if (model.TipOglasa == TipOglasa.DrustvenaIgra)
        {
            if (string.IsNullOrWhiteSpace(model.IgraNaziv))
            {
                return "Naziv igre je obavezan.";
            }

            if (model.IgraMinBrojIgraca == null)
            {
                return "Minimalni broj igrača je obavezan.";
            }

            if (model.IgraMaxBrojIgraca == null)
            {
                return "Maksimalni broj igrača je obavezan.";
            }

            if (model.IgraMinBrojIgraca > model.IgraMaxBrojIgraca)
            {
                return "Minimalni broj igrača ne može biti veći od maksimalnog.";
            }

            if (model.IgraMinimalnaDob == null)
            {
                return "Minimalna dob je obavezna.";
            }

            if (model.IgraTrajanjeMins == null)
            {
                return "Trajanje igre je obavezno.";
            }

            if (model.IgraZanr == null)
            {
                return "Žanr igre je obavezan.";
            }
        }

        return null;
    }

    private static OglasDTO ToDTO(Oglas oglas)
    {
        return new OglasDTO
        {
            Id = oglas.Id,
            Naslov = oglas.Naslov,
            Opis = oglas.Opis,
            Cijena = oglas.Cijena,
            DatumObjave = oglas.DatumObjave,
            DatumIsteka = oglas.DatumIsteka,
            DatumIzmjene = oglas.DatumIzmjene,
            Status = oglas.Status.ToString(),
            TipOglasa = oglas.TipOglasa.ToString(),
            StanjeArtikla = oglas.StanjeArtikla.ToString(),

            Grad = oglas.Grad == null
                ? null
                : new GradDTO
                {
                    Id = oglas.Grad.Id,
                    Naziv = oglas.Grad.Naziv,
                    PostanskiBroj = oglas.Grad.PostanskiBroj
                },

            Korisnik = oglas.Korisnik == null
                ? null
                : new KorisnikDTO
                {
                    Id = oglas.Korisnik.Id,
                    ImeIPrezime = oglas.Korisnik.ImeIPrezime,
                    Email = oglas.Korisnik.Email,
                    Telefon = oglas.Korisnik.Telefon,
                    Uloga = oglas.Korisnik.Uloga.ToString()
                },

            Knjiga = oglas.Knjiga == null
                ? null
                : new KnjigaDTO
                {
                    Id = oglas.Knjiga.Id,
                    OglasId = oglas.Knjiga.OglasId,
                    Naziv = oglas.Knjiga.Naziv,
                    Autor = oglas.Knjiga.Autor,
                    ISBN = oglas.Knjiga.ISBN,
                    Izdavac = oglas.Knjiga.Izdavac,
                    GodinaIzdanja = oglas.Knjiga.GodinaIzdanja,
                    BrojStrana = oglas.Knjiga.BrojStrana,
                    Jezik = oglas.Knjiga.Jezik,
                    Zanr = oglas.Knjiga.Zanr.ToString()
                },

            DrustvenaIgra = oglas.DrustvenaIgra == null
                ? null
                : new DrustvenaIgraDTO
                {
                    Id = oglas.DrustvenaIgra.Id,
                    OglasId = oglas.DrustvenaIgra.OglasId,
                    Naziv = oglas.DrustvenaIgra.Naziv,
                    MinBrojIgraca = oglas.DrustvenaIgra.MinBrojIgraca,
                    MaxBrojIgraca = oglas.DrustvenaIgra.MaxBrojIgraca,
                    MinimalnaDob = oglas.DrustvenaIgra.MinimalnaDob,
                    TrajanjeMins = oglas.DrustvenaIgra.TrajanjeMins,
                    Zanr = oglas.DrustvenaIgra.Zanr.ToString(),
                    BrojIgracaPrikaz = oglas.DrustvenaIgra.BrojIgracaPrikaz
                },

            Slike = oglas.Slike
                .OrderBy(s => s.RedoslijedPrikaza)
                .Select(s => new SlikaDTO
                {
                    Id = s.Id,
                    OglasId = s.OglasId,
                    Putanja = s.Putanja,
                    RedoslijedPrikaza = s.RedoslijedPrikaza
                })
                .ToList()
        };
    }
}