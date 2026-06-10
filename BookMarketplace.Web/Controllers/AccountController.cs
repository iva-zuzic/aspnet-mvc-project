using BookMarketplace.Model;
using BookMarketplace.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookMarketplace.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookMarketplace.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly BookMarketplaceDbContext _context;

    public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        BookMarketplaceDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        PopuniGradove();
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            PopuniGradove();
            return View(model);
        }

        var user = new AppUser
        {
            UserName = model.Email,
            Email = model.Email,
            KorisnickoIme = model.KorisnickoIme.Trim(),
            OIB = model.OIB,
            JMBG = model.JMBG
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var korisnik = new Korisnik
            {
                ImeIPrezime = model.ImeIPrezime.Trim(),
                KorisnickoIme = model.KorisnickoIme.Trim(),
                Email = model.Email.Trim(),
                Lozinka = string.Empty,
                Telefon = model.Telefon?.Trim() ?? string.Empty,
                DatumRegistracije = DateTime.Now,
                Uloga = UlogaKorisnika.Korisnik,
                AppUserId = user.Id,
                GradId = model.GradId
            };

            _context.Korisnici.Add(korisnik);
            await _context.SaveChangesAsync();

            await _userManager.AddToRoleAsync(user, "Korisnik");
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        PopuniGradove();
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Neuspješna prijava. Provjerite email i lozinku.");

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyAccount()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return RedirectToAction(nameof(Login));
        }

        var korisnik = await _context.Korisnici
            .FirstOrDefaultAsync(k => k.AppUserId == user.Id);

        if (korisnik == null)
        {
            korisnik = new Korisnik
            {
                ImeIPrezime = user.Email ?? string.Empty,
                KorisnickoIme = !string.IsNullOrWhiteSpace(user.KorisnickoIme)
                    ? user.KorisnickoIme
                    : user.Email,
                Email = user.Email ?? string.Empty,
                Lozinka = string.Empty,
                Telefon = string.Empty,
                DatumRegistracije = DateTime.Now,
                Uloga = UlogaKorisnika.Korisnik,
                AppUserId = user.Id
            };

            _context.Korisnici.Add(korisnik);
            await _context.SaveChangesAsync();
        }

        var oglasi = await _context.Oglasi
            .Include(o => o.Knjiga)
            .Include(o => o.DrustvenaIgra)
            .Include(o => o.Grad)
            .Include(o => o.Slike)
            .Where(o => o.KorisnikId == korisnik.Id)
            .OrderByDescending(o => o.DatumObjave)
            .ToListAsync();

        var roles = await _userManager.GetRolesAsync(user);

        var model = new MyAccountViewModel
        {
            ImeIPrezime = korisnik.ImeIPrezime,
            KorisnickoIme = !string.IsNullOrWhiteSpace(korisnik.KorisnickoIme)
                ? korisnik.KorisnickoIme
                : user.KorisnickoIme,
            Email = user.Email ?? string.Empty,
            OIB = user.OIB,
            JMBG = user.JMBG,
            Roles = roles.ToList(),
            Telefon = korisnik.Telefon,
            BrojOglasa = oglasi.Count,
            Oglasi = oglasi
        };

        return View(model);
    }

    private void PopuniGradove()
    {
        ViewBag.Gradovi = new SelectList(
            _context.Gradovi.OrderBy(g => g.Naziv),
            "Id",
            "Naziv");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyAds()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return RedirectToAction(nameof(Login));
        }

        var korisnik = await _context.Korisnici
            .FirstOrDefaultAsync(k => k.AppUserId == user.Id);

        if (korisnik == null)
        {
            return RedirectToAction(nameof(MyAccount));
        }

        var oglasi = await _context.Oglasi
            .Include(o => o.Knjiga)
            .Include(o => o.DrustvenaIgra)
            .Include(o => o.Grad)
            .Include(o => o.Slike)
            .Where(o => o.KorisnikId == korisnik.Id)
            .OrderByDescending(o => o.DatumObjave)
            .ToListAsync();

        return View(oglasi);
    }
}