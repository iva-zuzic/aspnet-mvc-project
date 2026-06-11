using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers;

[Authorize]
public class PorukaController : Controller
{
    private readonly BookMarketplaceDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public PorukaController(
        BookMarketplaceDbContext context,
        UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var korisnik = await DohvatiTrenutniKorisnikProfilAsync();

        if (korisnik == null)
        {
            return RedirectToAction("MyAccount", "Account");
        }

        var porukeQuery = _context.Poruke
            .Include(p => p.Posiljatelj)
            .Include(p => p.Primatelj)
            .Include(p => p.Oglas)
            .AsQueryable();

        if (!User.IsInRole("Admin"))
        {
            porukeQuery = porukeQuery.Where(p =>
                p.PosiljateljId == korisnik.Id ||
                p.PrimateljId == korisnik.Id);
        }

        var poruke = await porukeQuery
            .OrderByDescending(p => p.DatumSlanja)
            .ToListAsync();

        return View(poruke);
    }

    public async Task<IActionResult> Details(int id)
    {
        var korisnik = await DohvatiTrenutniKorisnikProfilAsync();

        if (korisnik == null)
        {
            return RedirectToAction("MyAccount", "Account");
        }

        var poruka = await _context.Poruke
            .Include(p => p.Posiljatelj)
            .Include(p => p.Primatelj)
            .Include(p => p.Oglas)
                .ThenInclude(o => o.Grad)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (poruka == null)
        {
            return NotFound();
        }

        var smijeVidjeti = User.IsInRole("Admin")
            || poruka.PosiljateljId == korisnik.Id
            || poruka.PrimateljId == korisnik.Id;

        if (!smijeVidjeti)
        {
            return Forbid();
        }

        if (poruka.PrimateljId == korisnik.Id && !poruka.Procitano)
        {
            poruka.Procitano = true;
            await _context.SaveChangesAsync();
        }

        return View(poruka);
    }

    [HttpGet]
    public async Task<IActionResult> Create(int oglasId)
    {
        var posiljatelj = await DohvatiTrenutniKorisnikProfilAsync();

        if (posiljatelj == null)
        {
            return RedirectToAction("MyAccount", "Account");
        }

        var oglas = await _context.Oglasi
            .Include(o => o.Korisnik)
            .FirstOrDefaultAsync(o => o.Id == oglasId);

        if (oglas == null)
        {
            return NotFound();
        }

        if (oglas.KorisnikId == posiljatelj.Id)
        {
            TempData["ErrorMessage"] = "Ne možete poslati poruku sami sebi za vlastiti oglas.";
            return RedirectToOglasDetails(oglas);
        }

        var model = new PorukaCreateModel
        {
            OglasId = oglas.Id,
            OglasNaslov = oglas.Naslov,
            PrimateljIme = oglas.Korisnik.ImeIPrezime
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PorukaCreateModel model)
    {
        var posiljatelj = await DohvatiTrenutniKorisnikProfilAsync();

        if (posiljatelj == null)
        {
            return RedirectToAction("MyAccount", "Account");
        }

        var oglas = await _context.Oglasi
            .Include(o => o.Korisnik)
            .FirstOrDefaultAsync(o => o.Id == model.OglasId);

        if (oglas == null)
        {
            return NotFound();
        }

        if (oglas.KorisnikId == posiljatelj.Id)
        {
            TempData["ErrorMessage"] = "Ne možete poslati poruku sami sebi za vlastiti oglas.";
            return RedirectToOglasDetails(oglas);
        }

        if (!ModelState.IsValid)
        {
            model.OglasNaslov = oglas.Naslov;
            model.PrimateljIme = oglas.Korisnik.ImeIPrezime;

            return View(model);
        }

        var poruka = new Poruka
        {
            Sadrzaj = model.Sadrzaj.Trim(),
            Procitano = false,
            DatumSlanja = DateTime.Now,
            PosiljateljId = posiljatelj.Id,
            PrimateljId = oglas.KorisnikId,
            OglasId = oglas.Id
        };

        _context.Poruke.Add(poruka);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Poruka je uspješno poslana.";

        return RedirectToOglasDetails(oglas);
    }

    private async Task<Korisnik?> DohvatiTrenutniKorisnikProfilAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return null;
        }

        return await _context.Korisnici
            .FirstOrDefaultAsync(k => k.AppUserId == user.Id);
    }

    private RedirectToActionResult RedirectToOglasDetails(Oglas oglas)
    {
        if (oglas.TipOglasa == TipOglasa.Knjiga)
        {
            return RedirectToAction("Details", "Knjiga", new { id = oglas.Id });
        }

        return RedirectToAction("Details", "DrustvenaIgra", new { id = oglas.Id });
    }
}