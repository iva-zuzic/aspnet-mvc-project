using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers
{
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
                .Include(o => o.Grad)
                .Include(o => o.Korisnik)
                .Include(o => o.Knjiga)
                .Include(o => o.DrustvenaIgra)
                .ToListAsync();
            return View(oglasi);
        }

        public async Task<IActionResult> Details(int id)
        {
            var oglas = await _context.Oglasi
                .Include(o => o.Grad)
                .Include(o => o.Korisnik)
                .Include(o => o.Knjiga)
                .Include(o => o.DrustvenaIgra)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (oglas is null)
                return NotFound();

            return View(oglas);
        }

        public IActionResult Create()
        {
            ViewBag.Gradovi = _context.Gradovi.ToList();
            ViewBag.Korisnici = _context.Korisnici.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Oglas oglas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oglas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Gradovi = _context.Gradovi.ToList();
            ViewBag.Korisnici = _context.Korisnici.ToList();

            return View(oglas);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var oglas = await _context.Oglasi.FindAsync(id);
            if (oglas == null) return NotFound();

            ViewBag.Gradovi = _context.Gradovi.ToList();
            ViewBag.Korisnici = _context.Korisnici.ToList();

            return View(oglas);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Oglas oglas)
        {
            if (id != oglas.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(oglas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Gradovi = _context.Gradovi.ToList();
            ViewBag.Korisnici = _context.Korisnici.ToList();

            return View(oglas);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var oglas = await _context.Oglasi
                .Include(o => o.Grad)
                .Include(o => o.Korisnik)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (oglas == null) return NotFound();

            return View(oglas);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oglas = await _context.Oglasi.FirstOrDefaultAsync(o => o.Id == id);
            if (oglas == null) return NotFound();

            _context.Oglasi.Remove(oglas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
