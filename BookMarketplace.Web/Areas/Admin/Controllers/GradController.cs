using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMarketplace.Areas.Admin.ViewModels;

namespace BookMarketplace.Areas.Admin.Controllers;

[Area("Admin")]
public class GradController : Controller
{
    private readonly BookMarketplaceDbContext _context;

    public GradController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var gradovi = await _context.Gradovi
            .OrderBy(g => g.Naziv)
            .ToListAsync();

        return View(gradovi);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GradCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var grad = new Grad
        {
            Naziv = model.Naziv.Trim(),
            PostanskiBroj = model.PostanskiBroj.Trim()
        };

        _context.Gradovi.Add(grad);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { area = "Admin" });
    }


    [ActionName("Edit")]
    public async Task<IActionResult> EditGet(int id)
    {
        var grad = await _context.Gradovi.FirstOrDefaultAsync(g => g.Id == id);

        if (grad == null)
        {
            return NotFound();
        }

        var model = new GradEditModel
        {
            Id = grad.Id,
            Naziv = grad.Naziv,
            PostanskiBroj = grad.PostanskiBroj
        };

        return View(model);
    }

    [HttpPost]
    [ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPost(int id, GradEditModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var grad = await _context.Gradovi.FirstOrDefaultAsync(g => g.Id == id);

        if (grad == null)
        {
            return NotFound();
        }

        grad.Naziv = model.Naziv.Trim();
        grad.PostanskiBroj = model.PostanskiBroj.Trim();

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { area = "Admin" });
    }


    [ActionName("Delete")]
    public async Task<IActionResult> DeleteGet(int id)
    {
        var grad = await _context.Gradovi
            .Include(g => g.Oglasi)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (grad == null)
        {
            return NotFound();
        }

        return View(grad);
    }


    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var grad = await _context.Gradovi
            .Include(g => g.Oglasi)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (grad == null)
        {
            return NotFound();
        }

        if (grad.Oglasi.Any())
        {
            ModelState.AddModelError("",
                $"Grad '{grad.Naziv}' ne može biti obrisan jer ima {grad.Oglasi.Count} povezanih oglasa.");
            return View(grad);
        }

        _context.Gradovi.Remove(grad);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { area = "Admin" });
    }
}