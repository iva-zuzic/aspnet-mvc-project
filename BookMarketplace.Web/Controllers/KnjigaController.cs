using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers
{
    public class KnjigaController : Controller
    {
        private readonly BookMarketplaceDbContext _context;

        public KnjigaController(BookMarketplaceDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var oglasi = await _context.Oglasi
                .Where(o => o.TipOglasa == TipOglasa.Knjiga)
                .Include(o => o.Korisnik)
                .Include(o => o.Grad)
                .Include(o => o.Knjiga)
                .ToListAsync();

            return View(oglasi);
        }

        public async Task<IActionResult> Details(int id)
        {
            var oglas = await _context.Oglasi
                .Include(o => o.Korisnik)
                .Include(o => o.Grad)
                .Include(o => o.Knjiga)
                .FirstOrDefaultAsync(o => o.Id == id && o.TipOglasa == TipOglasa.Knjiga);

            if (oglas == null)
                return NotFound();

            return View(oglas);
        }
    }
}
