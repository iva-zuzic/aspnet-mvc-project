using BookMarketplace.DAL;
using BookMarketplace.Model;
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
        public async Task<IActionResult> Create(Korisnik korisnik)
        {
            korisnik.DatumRegistracije = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                korisnik.DatumRegistracije = DateTime.Now;
                _context.Add(korisnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(korisnik);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var korisnik = await _context.Korisnici.FindAsync(id);
            if (korisnik == null)
                return NotFound();

            return View(korisnik);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Korisnik korisnik)
        {
            if (id != korisnik.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(korisnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(korisnik);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var korisnik = await _context.Korisnici
                .FirstOrDefaultAsync(k => k.Id == id);

            if (korisnik == null)
                return NotFound();

            return View(korisnik);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var korisnik = await _context.Korisnici.FindAsync(id);

            if (korisnik == null)
                return NotFound();

            _context.Korisnici.Remove(korisnik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
