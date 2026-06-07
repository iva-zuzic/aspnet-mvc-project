using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Slika;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers.Api;

[Route("api/slike")]
[ApiController]
public class SlikeApiController : ControllerBase
{
    private readonly BookMarketplaceDbContext _context;

    public SlikeApiController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SlikaDTO>>> GetAll(int? oglasId = null)
    {
        var slikeQuery = _context.Slike.AsQueryable();

        if (oglasId.HasValue)
        {
            slikeQuery = slikeQuery.Where(s => s.OglasId == oglasId.Value);
        }

        var slike = await slikeQuery
            .OrderBy(s => s.OglasId)
            .ThenBy(s => s.RedoslijedPrikaza)
            .ToListAsync();

        return Ok(slike.Select(ToDTO));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SlikaDTO>> GetById(int id)
    {
        var slika = await _context.Slike
            .FirstOrDefaultAsync(s => s.Id == id);

        if (slika == null)
        {
            return NotFound();
        }

        return Ok(ToDTO(slika));
    }

    [HttpPost]
    public async Task<ActionResult<SlikaDTO>> Create(SlikaCreateDTO model)
    {
        var oglasExists = await _context.Oglasi.AnyAsync(o => o.Id == model.OglasId);

        if (!oglasExists)
        {
            return BadRequest("Oglas ne postoji.");
        }

        var slika = new Slika
        {
            OglasId = model.OglasId,
            Putanja = model.Putanja.Trim(),
            RedoslijedPrikaza = model.RedoslijedPrikaza
        };

        _context.Slike.Add(slika);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = slika.Id },
            ToDTO(slika));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SlikaDTO>> Update(int id, SlikaUpdateDTO model)
    {
        if (id != model.Id)
        {
            return BadRequest("ID iz rute se ne podudara s ID-em iz modela.");
        }

        var slika = await _context.Slike
            .Include(s => s.Oglas)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (slika == null)
        {
            return NotFound();
        }

        slika.Putanja = model.Putanja.Trim();
        slika.RedoslijedPrikaza = model.RedoslijedPrikaza;

        if (slika.Oglas != null)
        {
            slika.Oglas.DatumIzmjene = DateTime.Now;
        }

        await _context.SaveChangesAsync();

        return Ok(ToDTO(slika));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var slika = await _context.Slike
            .Include(s => s.Oglas)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (slika == null)
        {
            return NotFound();
        }

        if (slika.Oglas != null)
        {
            slika.Oglas.DatumIzmjene = DateTime.Now;
        }

        _context.Slike.Remove(slika);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static SlikaDTO ToDTO(Slika slika)
    {
        return new SlikaDTO
        {
            Id = slika.Id,
            OglasId = slika.OglasId,
            Putanja = slika.Putanja,
            RedoslijedPrikaza = slika.RedoslijedPrikaza
        };
    }
}