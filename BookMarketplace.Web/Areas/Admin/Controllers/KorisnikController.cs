using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BookMarketplace.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class KorisnikController : Controller
{
    private readonly BookMarketplaceDbContext _context;

    public KorisnikController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var korisnici = await _context.Korisnici
            .OrderBy(k => k.ImeIPrezime)
            .ToListAsync();

        return View(korisnici);
    }

    public async Task<IActionResult> Details(int id)
    {
        var korisnik = await _context.Korisnici
            .IgnoreQueryFilters()
            .Include(k => k.Oglasi)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (korisnik == null)
            return NotFound();

        return View(korisnik);
    }

    [ActionName("Delete")]
    public async Task<IActionResult> DeleteGet(int id)
    {
        var korisnik = await _context.Korisnici
            .Include(k => k.Oglasi)
            .Include(k => k.Favoriti)
            .Include(k => k.PoslanePoruke)
            .Include(k => k.PrimljenePoruke)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (korisnik == null)
        {
            return NotFound();
        }

        return View(korisnik);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var korisnik = await _context.Korisnici
            .Include(k => k.Oglasi)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (korisnik == null)
        {
            return NotFound();
        }

        korisnik.DeletedAt = DateTime.UtcNow;

        foreach (var oglas in korisnik.Oglasi)
        {
            oglas.Status = StatusOglasa.Izbrisan;
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { area = "Admin" });
    }
}