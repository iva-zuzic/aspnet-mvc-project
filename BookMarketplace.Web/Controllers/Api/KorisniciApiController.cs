using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Korisnik;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers.Api;

[Route("api/korisnici")]
[ApiController]
public class KorisniciApiController : ControllerBase
{
    private readonly BookMarketplaceDbContext _context;

    public KorisniciApiController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<KorisnikDTO>>> GetAll(string? query = null)
    {
        var korisniciQuery = _context.Korisnici
            .Where(k => k.DeletedAt == null)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            var searchTerm = query.Trim().ToLower();

            korisniciQuery = korisniciQuery.Where(k =>
                k.ImeIPrezime.ToLower().Contains(searchTerm) ||
                k.Email.ToLower().Contains(searchTerm) ||
                k.Telefon.ToLower().Contains(searchTerm));
        }

        var korisnici = await korisniciQuery
            .OrderBy(k => k.ImeIPrezime)
            .ToListAsync();

        return Ok(korisnici.Select(ToDTO));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<KorisnikDTO>> GetById(int id)
    {
        var korisnik = await _context.Korisnici
            .FirstOrDefaultAsync(k => k.Id == id && k.DeletedAt == null);

        if (korisnik == null)
        {
            return NotFound();
        }

        return Ok(ToDTO(korisnik));
    }

    [HttpPost]
    public async Task<ActionResult<KorisnikDTO>> Create(KorisnikCreateDTO model)
    {
        var emailExists = await _context.Korisnici.AnyAsync(k =>
            k.Email.ToLower() == model.Email.Trim().ToLower() &&
            k.DeletedAt == null);

        if (emailExists)
        {
            return BadRequest("Korisnik s tom email adresom već postoji.");
        }

        var korisnik = new Korisnik
        {
            ImeIPrezime = model.ImeIPrezime.Trim(),
            Email = model.Email.Trim(),
            Telefon = model.Telefon.Trim(),
            Lozinka = model.Lozinka,
            Uloga = model.Uloga,
            DatumRegistracije = DateTime.Now
        };

        _context.Korisnici.Add(korisnik);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = korisnik.Id },
            ToDTO(korisnik));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<KorisnikDTO>> Update(int id, KorisnikUpdateDTO model)
    {
        if (id != model.Id)
        {
            return BadRequest("ID iz rute se ne podudara s ID-em iz modela.");
        }

        var korisnik = await _context.Korisnici
            .FirstOrDefaultAsync(k => k.Id == id && k.DeletedAt == null);

        if (korisnik == null)
        {
            return NotFound();
        }

        var emailExists = await _context.Korisnici.AnyAsync(k =>
            k.Id != id &&
            k.Email.ToLower() == model.Email.Trim().ToLower() &&
            k.DeletedAt == null);

        if (emailExists)
        {
            return BadRequest("Korisnik s tom email adresom već postoji.");
        }

        korisnik.ImeIPrezime = model.ImeIPrezime.Trim();
        korisnik.Email = model.Email.Trim();
        korisnik.Telefon = model.Telefon.Trim();
        korisnik.Uloga = model.Uloga;

        await _context.SaveChangesAsync();

        return Ok(ToDTO(korisnik));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var korisnik = await _context.Korisnici
            .FirstOrDefaultAsync(k => k.Id == id && k.DeletedAt == null);

        if (korisnik == null)
        {
            return NotFound();
        }

        korisnik.DeletedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static KorisnikDTO ToDTO(Korisnik korisnik)
    {
        return new KorisnikDTO
        {
            Id = korisnik.Id,
            ImeIPrezime = korisnik.ImeIPrezime,
            Email = korisnik.Email,
            Telefon = korisnik.Telefon,
            Uloga = korisnik.Uloga.ToString()
        };
    }
}