using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Knjiga;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers.Api;

[Route("api/knjige")]
[ApiController]
public class KnjigeApiController : ControllerBase
{
    private readonly BookMarketplaceDbContext _context;

    public KnjigeApiController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<KnjigaDTO>>> GetAll(string? query = null)
    {
        var knjigeQuery = _context.Knjige
            .Include(k => k.Oglas)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            var searchTerm = query.Trim().ToLower();

            knjigeQuery = knjigeQuery.Where(k =>
                k.Naziv.ToLower().Contains(searchTerm) ||
                k.Autor.ToLower().Contains(searchTerm) ||
                k.ISBN.ToLower().Contains(searchTerm) ||
                k.Izdavac.ToLower().Contains(searchTerm) ||
                k.Jezik.ToLower().Contains(searchTerm) ||
                k.Oglas.Naslov.ToLower().Contains(searchTerm));
        }

        var knjige = await knjigeQuery
            .OrderBy(k => k.Naziv)
            .ToListAsync();

        return Ok(knjige.Select(ToDTO));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<KnjigaDTO>> GetById(int id)
    {
        var knjiga = await _context.Knjige
            .Include(k => k.Oglas)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (knjiga == null)
        {
            return NotFound();
        }

        return Ok(ToDTO(knjiga));
    }

    [HttpPost]
    public async Task<ActionResult<KnjigaDTO>> Create(KnjigaCreateDTO model)
    {
        var oglas = await _context.Oglasi
            .Include(o => o.Knjiga)
            .FirstOrDefaultAsync(o => o.Id == model.OglasId);

        if (oglas == null)
        {
            return BadRequest("Oglas ne postoji.");
        }

        if (oglas.TipOglasa != TipOglasa.Knjiga)
        {
            return BadRequest("Odabrani oglas nije oglas za knjigu.");
        }

        if (oglas.Knjiga != null)
        {
            return BadRequest("Ovaj oglas već ima povezanu knjigu.");
        }

        var knjiga = new Knjiga
        {
            OglasId = model.OglasId,
            Naziv = model.Naziv.Trim(),
            Autor = model.Autor.Trim(),
            ISBN = model.ISBN.Trim(),
            Izdavac = model.Izdavac.Trim(),
            GodinaIzdanja = model.GodinaIzdanja,
            BrojStrana = model.BrojStrana,
            Jezik = model.Jezik.Trim(),
            Zanr = model.Zanr
        };

        _context.Knjige.Add(knjiga);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = knjiga.Id },
            ToDTO(knjiga));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<KnjigaDTO>> Update(int id, KnjigaUpdateDTO model)
    {
        if (id != model.Id)
        {
            return BadRequest("ID iz rute se ne podudara s ID-em iz modela.");
        }

        var knjiga = await _context.Knjige
            .Include(k => k.Oglas)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (knjiga == null)
        {
            return NotFound();
        }

        knjiga.Naziv = model.Naziv.Trim();
        knjiga.Autor = model.Autor.Trim();
        knjiga.ISBN = model.ISBN.Trim();
        knjiga.Izdavac = model.Izdavac.Trim();
        knjiga.GodinaIzdanja = model.GodinaIzdanja;
        knjiga.BrojStrana = model.BrojStrana;
        knjiga.Jezik = model.Jezik.Trim();
        knjiga.Zanr = model.Zanr;

        if (knjiga.Oglas != null)
        {
            knjiga.Oglas.DatumIzmjene = DateTime.Now;
        }

        await _context.SaveChangesAsync();

        return Ok(ToDTO(knjiga));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var knjiga = await _context.Knjige
            .Include(k => k.Oglas)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (knjiga == null)
        {
            return NotFound();
        }

        if (knjiga.Oglas != null)
        {
            knjiga.Oglas.Status = StatusOglasa.Izbrisan;
            knjiga.Oglas.DatumIzmjene = DateTime.Now;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static KnjigaDTO ToDTO(Knjiga knjiga)
    {
        return new KnjigaDTO
        {
            Id = knjiga.Id,
            OglasId = knjiga.OglasId,
            Naziv = knjiga.Naziv,
            Autor = knjiga.Autor,
            ISBN = knjiga.ISBN,
            Izdavac = knjiga.Izdavac,
            GodinaIzdanja = knjiga.GodinaIzdanja,
            BrojStrana = knjiga.BrojStrana,
            Jezik = knjiga.Jezik,
            Zanr = knjiga.Zanr.ToString()
        };
    }
}