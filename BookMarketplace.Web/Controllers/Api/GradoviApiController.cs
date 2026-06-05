using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Grad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers.Api;

[Route("api/gradovi")]
[ApiController]
public class GradoviApiController : ControllerBase
{
    private readonly BookMarketplaceDbContext _context;

    public GradoviApiController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GradDTO>>> GetAll(string? query = null)
    {
        var gradoviQuery = _context.Gradovi.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            var searchTerm = query.Trim().ToLower();

            gradoviQuery = gradoviQuery.Where(g =>
                g.Naziv.ToLower().Contains(searchTerm) ||
                g.PostanskiBroj.ToLower().Contains(searchTerm));
        }

        var gradovi = await gradoviQuery
            .OrderBy(g => g.Naziv)
            .ToListAsync();

        return Ok(gradovi.Select(ToDTO));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GradDTO>> GetById(int id)
    {
        var grad = await _context.Gradovi
            .FirstOrDefaultAsync(g => g.Id == id);

        if (grad == null)
        {
            return NotFound();
        }

        return Ok(ToDTO(grad));
    }

    [HttpPost]
    public async Task<ActionResult<GradDTO>> Create(GradCreateDTO model)
    {
        var gradExists = await _context.Gradovi.AnyAsync(g =>
            g.Naziv.ToLower() == model.Naziv.Trim().ToLower() &&
            g.PostanskiBroj == model.PostanskiBroj.Trim());

        if (gradExists)
        {
            return BadRequest("Grad s tim nazivom i poštanskim brojem već postoji.");
        }

        var grad = new Grad
        {
            Naziv = model.Naziv.Trim(),
            PostanskiBroj = model.PostanskiBroj.Trim()
        };

        _context.Gradovi.Add(grad);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = grad.Id },
            ToDTO(grad));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GradDTO>> Update(int id, GradUpdateDTO model)
    {
        if (id != model.Id)
        {
            return BadRequest("ID iz rute se ne podudara s ID-em iz modela.");
        }

        var grad = await _context.Gradovi
            .FirstOrDefaultAsync(g => g.Id == id);

        if (grad == null)
        {
            return NotFound();
        }

        var duplicateExists = await _context.Gradovi.AnyAsync(g =>
            g.Id != id &&
            g.Naziv.ToLower() == model.Naziv.Trim().ToLower() &&
            g.PostanskiBroj == model.PostanskiBroj.Trim());

        if (duplicateExists)
        {
            return BadRequest("Grad s tim nazivom i poštanskim brojem već postoji.");
        }

        grad.Naziv = model.Naziv.Trim();
        grad.PostanskiBroj = model.PostanskiBroj.Trim();

        await _context.SaveChangesAsync();

        return Ok(ToDTO(grad));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var grad = await _context.Gradovi
            .FirstOrDefaultAsync(g => g.Id == id);

        if (grad == null)
        {
            return NotFound();
        }

        var hasOglasi = await _context.Oglasi.AnyAsync(o => o.GradId == id);

        if (hasOglasi)
        {
            return BadRequest("Grad se ne može obrisati jer postoje oglasi vezani uz taj grad.");
        }

        _context.Gradovi.Remove(grad);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static GradDTO ToDTO(Grad grad)
    {
        return new GradDTO
        {
            Id = grad.Id,
            Naziv = grad.Naziv,
            PostanskiBroj = grad.PostanskiBroj
        };
    }
}