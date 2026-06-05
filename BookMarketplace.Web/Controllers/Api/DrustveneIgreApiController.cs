using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.DrustvenaIgra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers.Api;

[Route("api/drustvene-igre")]
[ApiController]
public class DrustveneIgreApiController : ControllerBase
{
    private readonly BookMarketplaceDbContext _context;

    public DrustveneIgreApiController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DrustvenaIgraDTO>>> GetAll(string? query = null)
    {
        var igreQuery = _context.DrustveneIgre
            .Include(i => i.Oglas)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            var searchTerm = query.Trim().ToLower();

            igreQuery = igreQuery.Where(i =>
                i.Naziv.ToLower().Contains(searchTerm) ||
                i.Oglas.Naslov.ToLower().Contains(searchTerm));
        }

        var igre = await igreQuery
            .OrderBy(i => i.Naziv)
            .ToListAsync();

        return Ok(igre.Select(ToDTO));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DrustvenaIgraDTO>> GetById(int id)
    {
        var igra = await _context.DrustveneIgre
            .Include(i => i.Oglas)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (igra == null)
        {
            return NotFound();
        }

        return Ok(ToDTO(igra));
    }

    [HttpPost]
    public async Task<ActionResult<DrustvenaIgraDTO>> Create(DrustvenaIgraCreateDTO model)
    {
        if (model.MinBrojIgraca > model.MaxBrojIgraca)
        {
            return BadRequest("Minimalni broj igrača ne može biti veći od maksimalnog.");
        }

        var oglas = await _context.Oglasi
            .Include(o => o.DrustvenaIgra)
            .FirstOrDefaultAsync(o => o.Id == model.OglasId);

        if (oglas == null)
        {
            return BadRequest("Oglas ne postoji.");
        }

        if (oglas.TipOglasa != TipOglasa.DrustvenaIgra)
        {
            return BadRequest("Odabrani oglas nije oglas za društvenu igru.");
        }

        if (oglas.DrustvenaIgra != null)
        {
            return BadRequest("Ovaj oglas već ima povezanu društvenu igru.");
        }

        var igra = new DrustvenaIgra
        {
            OglasId = model.OglasId,
            Naziv = model.Naziv.Trim(),
            MinBrojIgraca = model.MinBrojIgraca,
            MaxBrojIgraca = model.MaxBrojIgraca,
            MinimalnaDob = model.MinimalnaDob,
            TrajanjeMins = model.TrajanjeMins,
            Zanr = model.Zanr
        };

        _context.DrustveneIgre.Add(igra);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = igra.Id },
            ToDTO(igra));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DrustvenaIgraDTO>> Update(int id, DrustvenaIgraUpdateDTO model)
    {
        if (id != model.Id)
        {
            return BadRequest("ID iz rute se ne podudara s ID-em iz modela.");
        }

        if (model.MinBrojIgraca > model.MaxBrojIgraca)
        {
            return BadRequest("Minimalni broj igrača ne može biti veći od maksimalnog.");
        }

        var igra = await _context.DrustveneIgre
            .Include(i => i.Oglas)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (igra == null)
        {
            return NotFound();
        }

        igra.Naziv = model.Naziv.Trim();
        igra.MinBrojIgraca = model.MinBrojIgraca;
        igra.MaxBrojIgraca = model.MaxBrojIgraca;
        igra.MinimalnaDob = model.MinimalnaDob;
        igra.TrajanjeMins = model.TrajanjeMins;
        igra.Zanr = model.Zanr;

        if (igra.Oglas != null)
        {
            igra.Oglas.DatumIzmjene = DateTime.Now;
        }

        await _context.SaveChangesAsync();

        return Ok(ToDTO(igra));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var igra = await _context.DrustveneIgre
            .Include(i => i.Oglas)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (igra == null)
        {
            return NotFound();
        }

        if (igra.Oglas != null)
        {
            igra.Oglas.Status = StatusOglasa.Izbrisan;
            igra.Oglas.DatumIzmjene = DateTime.Now;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static DrustvenaIgraDTO ToDTO(DrustvenaIgra igra)
    {
        return new DrustvenaIgraDTO
        {
            Id = igra.Id,
            OglasId = igra.OglasId,
            Naziv = igra.Naziv,
            MinBrojIgraca = igra.MinBrojIgraca,
            MaxBrojIgraca = igra.MaxBrojIgraca,
            MinimalnaDob = igra.MinimalnaDob,
            TrajanjeMins = igra.TrajanjeMins,
            Zanr = igra.Zanr.ToString(),
            BrojIgracaPrikaz = igra.BrojIgracaPrikaz
        };
    }
}