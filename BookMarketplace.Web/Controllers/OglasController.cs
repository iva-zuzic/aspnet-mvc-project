using BookMarketplace.DAL;
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
    }
}
