using BookMarketplace.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMarketplace.Model;

namespace BookMarketplace.Controllers;

[Route("api")]
public class ApiController : Controller
{
    private readonly BookMarketplaceDbContext _context;

    public ApiController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    [Route("gradovi")]
    public async Task<IActionResult> SearchGradovi(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return Json(new List<object>());

        var searchTerm = term.Trim().ToLower();

        var rezultati = await _context.Gradovi
            .Where(g => g.Naziv.ToLower().StartsWith(searchTerm))
            .OrderBy(g => g.Naziv)
            .Take(10)
            .Select(g => new 
            { 
                id = g.Id, 
                naziv = g.Naziv, 
                postanskiBroj = g.PostanskiBroj 
            })
            .ToListAsync();

        return Json(rezultati);
    }

    [Route("korisnici")]
    public async Task<IActionResult> SearchKorisnici(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return Json(new List<object>());

        var searchTerm = term.Trim().ToLower();

        var rezultati = await _context.Korisnici
            .Where(k => k.DeletedAt == null)
            .Where(k => k.ImeIPrezime.ToLower().StartsWith(searchTerm))
            .OrderBy(k => k.ImeIPrezime)
            .Take(10)
            .Select(k => new 
            { 
                id = k.Id, 
                naziv = k.ImeIPrezime 
            })
            .ToListAsync();

        return Json(rezultati);
    }

    [HttpGet("global-search")]
    public async Task<IActionResult> GlobalSearch(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return Json(new List<object>());

        var searchTerm = term.Trim().ToLower();

        var knjige = await _context.Oglasi
            .Include(o => o.Korisnik)
            .Include(o => o.Knjiga)
            .Where(o =>
                o.TipOglasa == TipOglasa.Knjiga &&
                o.Status == StatusOglasa.Aktivan &&
                o.Korisnik.DeletedAt == null)
            .Where(o =>
                o.Knjiga != null &&
                (
                    o.Knjiga.Naziv.ToLower().StartsWith(searchTerm) ||
                    o.Knjiga.Autor.ToLower().StartsWith(searchTerm)
                ))
            .OrderBy(o => o.Knjiga!.Naziv)
            .Take(5)
            .Select(o => new
            {
                id = o.Id,
                naziv = o.Knjiga!.Naziv,
                opis = o.Knjiga.Autor,
                tip = "Knjiga",
                url = Url.Action("Details", "Knjiga", new { id = o.Id })
            })
            .ToListAsync();

        var igre = await _context.Oglasi
            .Include(o => o.Korisnik)
            .Include(o => o.DrustvenaIgra)
            .Where(o =>
                o.TipOglasa == TipOglasa.DrustvenaIgra &&
                o.Status == StatusOglasa.Aktivan &&
                o.Korisnik.DeletedAt == null)
            .Where(o =>
                o.DrustvenaIgra != null &&
                o.DrustvenaIgra.Naziv.ToLower().StartsWith(searchTerm))
            .OrderBy(o => o.DrustvenaIgra!.Naziv)
            .Take(5)
            .Select(o => new
            {
                id = o.Id,
                naziv = o.DrustvenaIgra!.Naziv,
                opis = "Društvena igra",
                tip = "Igra",
                url = Url.Action("Details", "DrustvenaIgra", new { id = o.Id })
            })
            .ToListAsync();

        var rezultati = knjige
            .Cast<object>()
            .Concat(igre)
            .ToList();

        return Json(rezultati);
    }
}