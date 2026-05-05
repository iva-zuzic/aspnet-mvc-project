using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers
{
    public class DrustvenaIgraController : Controller
    {
        private readonly BookMarketplaceDbContext _context;

        public DrustvenaIgraController(BookMarketplaceDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var oglasi = await _context.Oglasi
                .Where(o => o.TipOglasa == TipOglasa.DrustvenaIgra)
                .Include(o => o.DrustvenaIgra)
                .Include(o => o.Grad)
                .Include(o => o.Korisnik)
                .ToListAsync();

            return View(oglasi);
        }
    
        public async Task<IActionResult> Details(int id)
        {
            var oglas = await _context.Oglasi
                .Include(o => o.DrustvenaIgra)
                .Include(o => o.Grad)
                .Include(o => o.Korisnik)
                .FirstOrDefaultAsync(o => o.Id == id && o.TipOglasa == TipOglasa.DrustvenaIgra);

            if (oglas == null)
                return NotFound();

            return View(oglas);
        }
    }
}