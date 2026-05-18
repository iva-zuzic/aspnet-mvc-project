using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers
{
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
                .Include(k => k.Oglasi)
                .ToListAsync();

            return View(korisnici);
        }

        public async Task<IActionResult> Details(int id)
        {
            var korisnik = await _context.Korisnici
                .Include(k => k.Oglasi)
                .ThenInclude(o => o.Grad)
                .FirstOrDefaultAsync(k => k.Id == id);

            if (korisnik is null)
                return NotFound();

            return View(korisnik);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KorisnikCreateModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var korisnik = new Korisnik
            {
                ImeIPrezime = model.ImeIPrezime.Trim(),
                Email = model.Email.Trim(),
                Lozinka = model.Lozinka,
                Telefon = model.Telefon.Trim(),
                DatumRegistracije = DateTime.Now,
                Uloga = UlogaKorisnika.Korisnik
            };

            _context.Korisnici.Add(korisnik);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = korisnik.Id });
        }

        [ActionName("Edit")]
        public async Task<IActionResult> EditGet(int id)
        {
            var korisnik = await _context.Korisnici
                .FirstOrDefaultAsync(k => k.Id == id);

            if (korisnik == null)
                return NotFound();

            var model = new KorisnikEditModel
            {
                Id = korisnik.Id,
                ImeIPrezime = korisnik.ImeIPrezime,
                Email = korisnik.Email,
                Telefon = korisnik.Telefon
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(KorisnikEditModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var korisnik = await _context.Korisnici
                .FirstOrDefaultAsync(k => k.Id == model.Id);

            if (korisnik == null)
                return NotFound();

            korisnik.ImeIPrezime = model.ImeIPrezime.Trim();
            korisnik.Email = model.Email.Trim();
            korisnik.Telefon = model.Telefon.Trim();

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = korisnik.Id });
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteGet(int id)
        {
            var korisnik = await _context.Korisnici
                .Include(k => k.Oglasi)
                .FirstOrDefaultAsync(k => k.Id == id);

            if (korisnik == null)
                return NotFound();

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
                return NotFound();

            korisnik.DeletedAt = DateTime.UtcNow;

            foreach (var oglas in korisnik.Oglasi)
            {
                oglas.Status = StatusOglasa.Izbrisan;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}