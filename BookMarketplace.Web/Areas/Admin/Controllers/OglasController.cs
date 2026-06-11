using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BookMarketplace.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OglasController : Controller
{
    private readonly BookMarketplaceDbContext _context;

    public OglasController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var oglasi = await _context.Oglasi
            .IgnoreQueryFilters()
            .Include(o => o.Korisnik)
            .Include(o => o.Grad)
            .OrderByDescending(o => o.DatumObjave)
            .ToListAsync();

        return View(oglasi);
    }

    public async Task<IActionResult> Details(int id)
    {
        var oglas = await _context.Oglasi
            .IgnoreQueryFilters()
            .Include(o => o.Korisnik)
            .Include(o => o.Grad)
            .Include(o => o.Slike)
            .Include(o => o.Knjiga)
            .Include(o => o.DrustvenaIgra)
            .Include(o => o.Poruke)
            .Include(o => o.Favoriti)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (oglas == null)
        {
            return NotFound();
        }

        return View(oglas);
    }

    [ActionName("Delete")]
    public async Task<IActionResult> DeleteGet(int id)
    {
        var oglas = await _context.Oglasi
            .IgnoreQueryFilters()
            .Include(o => o.Korisnik)
            .Include(o => o.Grad)
            .Include(o => o.Poruke)
            .Include(o => o.Favoriti)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (oglas == null)
        {
            return NotFound();
        }

        return View(oglas);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var oglas = await _context.Oglasi
            .IgnoreQueryFilters()
            .Include(o => o.Poruke)
            .Include(o => o.Favoriti)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (oglas == null)
        {
            return NotFound();
        }

        oglas.Status = StatusOglasa.Izbrisan;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { area = "Admin" });
    }

    [ActionName("Restore")]
    public async Task<IActionResult> RestoreGet(int id)
    {
        var oglas = await _context.Oglasi
            .IgnoreQueryFilters()
            .Include(o => o.Korisnik)
            .Include(o => o.Grad)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (oglas == null)
        {
            return NotFound();
        }

        if (oglas.Status != StatusOglasa.Izbrisan)
        {
            return RedirectToAction(nameof(Index), new { area = "Admin" });
        }

        if (oglas.Korisnik.DeletedAt != null)
        {
            return RedirectToAction(nameof(Index), new { area = "Admin" });
        }

        return View(oglas);
    }

    [HttpPost]
    [ActionName("Restore")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RestorePost(int id)
    {
        var oglas = await _context.Oglasi
            .IgnoreQueryFilters()
            .Include(o => o.Korisnik)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (oglas == null)
        {
            return NotFound();
        }

        if (oglas.Korisnik.DeletedAt != null)
        {
            return RedirectToAction(nameof(Index), new { area = "Admin" });
        }

        oglas.Status = StatusOglasa.Aktivan;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { area = "Admin" });
    }
}