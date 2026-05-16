using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Areas.Admin.Controllers;

[Area("Admin")]
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
            .Include(k => k.Favoriti)
            .Include(k => k.PoslanePoruke)
            .Include(k => k.PrimljenePoruke)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (korisnik == null)
        {
            return NotFound();
        }

        if (korisnik.Oglasi.Any() ||
            korisnik.Favoriti.Any() ||
            korisnik.PoslanePoruke.Any() ||
            korisnik.PrimljenePoruke.Any())
        {
            ModelState.AddModelError("",
                $"Korisnik '{korisnik.ImeIPrezime}' ne može biti obrisan jer ima povezane oglase, favorite ili poruke.");
            return View(korisnik);
        }

        _context.Korisnici.Remove(korisnik);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { area = "Admin" });
    }
}