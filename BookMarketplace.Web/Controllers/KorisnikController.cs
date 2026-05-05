using BookMarketplace.DAL; 
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
    }
}
