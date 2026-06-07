using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Poruka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers.Api;

[Route("api/poruke")]
[ApiController]
public class PorukeApiController : ControllerBase
{
    private readonly BookMarketplaceDbContext _context;

    public PorukeApiController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PorukaDTO>>> GetAll(
        int? korisnikId = null,
        int? oglasId = null,
        string? query = null)
    {
        var porukeQuery = _context.Poruke
            .Include(p => p.Posiljatelj)
            .Include(p => p.Primatelj)
            .Include(p => p.Oglas)
            .AsQueryable();

        if (korisnikId.HasValue)
        {
            porukeQuery = porukeQuery.Where(p =>
                p.PosiljateljId == korisnikId.Value ||
                p.PrimateljId == korisnikId.Value);
        }

        if (oglasId.HasValue)
        {
            porukeQuery = porukeQuery.Where(p => p.OglasId == oglasId.Value);
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            var searchTerm = query.Trim().ToLower();

            porukeQuery = porukeQuery.Where(p =>
                p.Sadrzaj.ToLower().Contains(searchTerm) ||
                p.Posiljatelj.ImeIPrezime.ToLower().Contains(searchTerm) ||
                p.Primatelj.ImeIPrezime.ToLower().Contains(searchTerm) ||
                p.Oglas.Naslov.ToLower().Contains(searchTerm));
        }

        var poruke = await porukeQuery
            .OrderByDescending(p => p.DatumSlanja)
            .ToListAsync();

        return Ok(poruke.Select(ToDTO));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PorukaDTO>> GetById(int id)
    {
        var poruka = await _context.Poruke
            .Include(p => p.Posiljatelj)
            .Include(p => p.Primatelj)
            .Include(p => p.Oglas)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (poruka == null)
        {
            return NotFound();
        }

        return Ok(ToDTO(poruka));
    }

    [HttpPost]
    public async Task<ActionResult<PorukaDTO>> Create(PorukaCreateDTO model)
    {
        if (model.PosiljateljId == model.PrimateljId)
        {
            return BadRequest("Pošiljatelj i primatelj ne mogu biti isti korisnik.");
        }

        var posiljateljExists = await _context.Korisnici.AnyAsync(k =>
            k.Id == model.PosiljateljId &&
            k.DeletedAt == null);

        if (!posiljateljExists)
        {
            return BadRequest("Pošiljatelj ne postoji.");
        }

        var primateljExists = await _context.Korisnici.AnyAsync(k =>
            k.Id == model.PrimateljId &&
            k.DeletedAt == null);

        if (!primateljExists)
        {
            return BadRequest("Primatelj ne postoji.");
        }

        var oglasExists = await _context.Oglasi.AnyAsync(o =>
            o.Id == model.OglasId &&
            o.Status != StatusOglasa.Izbrisan);

        if (!oglasExists)
        {
            return BadRequest("Oglas ne postoji ili je izbrisan.");
        }

        var poruka = new Poruka
        {
            PosiljateljId = model.PosiljateljId,
            PrimateljId = model.PrimateljId,
            OglasId = model.OglasId,
            Sadrzaj = model.Sadrzaj.Trim(),
            DatumSlanja = DateTime.Now,
            Procitano = false
        };

        _context.Poruke.Add(poruka);
        await _context.SaveChangesAsync();

        var createdPoruka = await _context.Poruke
            .Include(p => p.Posiljatelj)
            .Include(p => p.Primatelj)
            .Include(p => p.Oglas)
            .FirstAsync(p => p.Id == poruka.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdPoruka.Id },
            ToDTO(createdPoruka));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PorukaDTO>> Update(int id, PorukaUpdateDTO model)
    {
        if (id != model.Id)
        {
            return BadRequest("ID iz rute se ne podudara s ID-em iz modela.");
        }

        var poruka = await _context.Poruke
            .Include(p => p.Posiljatelj)
            .Include(p => p.Primatelj)
            .Include(p => p.Oglas)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (poruka == null)
        {
            return NotFound();
        }

        poruka.Sadrzaj = model.Sadrzaj.Trim();
        poruka.Procitano = model.Procitano;

        await _context.SaveChangesAsync();

        return Ok(ToDTO(poruka));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var poruka = await _context.Poruke
            .FirstOrDefaultAsync(p => p.Id == id);

        if (poruka == null)
        {
            return NotFound();
        }

        _context.Poruke.Remove(poruka);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static PorukaDTO ToDTO(Poruka poruka)
    {
        return new PorukaDTO
        {
            Id = poruka.Id,
            PosiljateljId = poruka.PosiljateljId,
            PosiljateljIme = poruka.Posiljatelj?.ImeIPrezime ?? string.Empty,
            PrimateljId = poruka.PrimateljId,
            PrimateljIme = poruka.Primatelj?.ImeIPrezime ?? string.Empty,
            OglasId = poruka.OglasId,
            OglasNaslov = poruka.Oglas?.Naslov ?? string.Empty,
            Sadrzaj = poruka.Sadrzaj,
            DatumSlanja = poruka.DatumSlanja,
            Procitano = poruka.Procitano
        };
    }
}