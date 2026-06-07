using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Favorit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers.Api;

[Route("api/favoriti")]
[ApiController]
public class FavoritiApiController : ControllerBase
{
    private readonly BookMarketplaceDbContext _context;

    public FavoritiApiController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FavoritDTO>>> GetAll(
        int? korisnikId = null,
        int? oglasId = null)
    {
        var favoritiQuery = _context.Favoriti
            .Include(f => f.Korisnik)
            .Include(f => f.Oglas)
            .AsQueryable();

        if (korisnikId.HasValue)
        {
            favoritiQuery = favoritiQuery.Where(f => f.KorisnikId == korisnikId.Value);
        }

        if (oglasId.HasValue)
        {
            favoritiQuery = favoritiQuery.Where(f => f.OglasId == oglasId.Value);
        }

        var favoriti = await favoritiQuery
            .OrderByDescending(f => f.DatumDodavanja)
            .ToListAsync();

        return Ok(favoriti.Select(ToDTO));
    }

    [HttpGet("{korisnikId}/{oglasId}")]
    public async Task<ActionResult<FavoritDTO>> GetById(int korisnikId, int oglasId)
    {
        var favorit = await _context.Favoriti
            .Include(f => f.Korisnik)
            .Include(f => f.Oglas)
            .FirstOrDefaultAsync(f =>
                f.KorisnikId == korisnikId &&
                f.OglasId == oglasId);

        if (favorit == null)
        {
            return NotFound();
        }

        return Ok(ToDTO(favorit));
    }

    [HttpPost]
    public async Task<ActionResult<FavoritDTO>> Create(FavoritCreateDTO model)
    {
        var korisnikExists = await _context.Korisnici.AnyAsync(k =>
            k.Id == model.KorisnikId &&
            k.DeletedAt == null);

        if (!korisnikExists)
        {
            return BadRequest("Korisnik ne postoji.");
        }

        var oglasExists = await _context.Oglasi.AnyAsync(o =>
            o.Id == model.OglasId &&
            o.Status != StatusOglasa.Izbrisan);

        if (!oglasExists)
        {
            return BadRequest("Oglas ne postoji ili je izbrisan.");
        }

        var favoritExists = await _context.Favoriti.AnyAsync(f =>
            f.KorisnikId == model.KorisnikId &&
            f.OglasId == model.OglasId);

        if (favoritExists)
        {
            return BadRequest("Oglas je već dodan u favorite.");
        }

        var favorit = new Favorit
        {
            KorisnikId = model.KorisnikId,
            OglasId = model.OglasId,
            DatumDodavanja = DateTime.Now
        };

        _context.Favoriti.Add(favorit);
        await _context.SaveChangesAsync();

        var createdFavorit = await _context.Favoriti
            .Include(f => f.Korisnik)
            .Include(f => f.Oglas)
            .FirstAsync(f =>
                f.KorisnikId == favorit.KorisnikId &&
                f.OglasId == favorit.OglasId);

        return CreatedAtAction(
            nameof(GetById),
            new { korisnikId = createdFavorit.KorisnikId, oglasId = createdFavorit.OglasId },
            ToDTO(createdFavorit));
    }

    [HttpPut("{korisnikId}/{oglasId}")]
    public async Task<ActionResult<FavoritDTO>> Update(
        int korisnikId,
        int oglasId,
        FavoritUpdateDTO model)
    {
        if (korisnikId != model.KorisnikId || oglasId != model.OglasId)
        {
            return BadRequest("ID vrijednosti iz rute se ne podudaraju s modelom.");
        }

        var favorit = await _context.Favoriti
            .Include(f => f.Korisnik)
            .Include(f => f.Oglas)
            .FirstOrDefaultAsync(f =>
                f.KorisnikId == korisnikId &&
                f.OglasId == oglasId);

        if (favorit == null)
        {
            return NotFound();
        }

        favorit.DatumDodavanja = model.DatumDodavanja;

        await _context.SaveChangesAsync();

        return Ok(ToDTO(favorit));
    }

    [HttpDelete("{korisnikId}/{oglasId}")]
    public async Task<IActionResult> Delete(int korisnikId, int oglasId)
    {
        var favorit = await _context.Favoriti
            .FirstOrDefaultAsync(f =>
                f.KorisnikId == korisnikId &&
                f.OglasId == oglasId);

        if (favorit == null)
        {
            return NotFound();
        }

        _context.Favoriti.Remove(favorit);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static FavoritDTO ToDTO(Favorit favorit)
    {
        return new FavoritDTO
        {
            KorisnikId = favorit.KorisnikId,
            KorisnikIme = favorit.Korisnik?.ImeIPrezime ?? string.Empty,
            OglasId = favorit.OglasId,
            OglasNaslov = favorit.Oglas?.Naslov ?? string.Empty,
            DatumDodavanja = favorit.DatumDodavanja
        };
    }
}