using BookMarketplace.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers
{
    public class PorukaController : Controller
    {
        private readonly BookMarketplaceDbContext _context;

        public PorukaController(BookMarketplaceDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var poruke = await _context.Poruke
                .Include(p => p.Posiljatelj)
                .Include(p => p.Primatelj)
                .Include(p => p.Oglas)
                .ToListAsync();
            return View(poruke);
        }

        public async Task<IActionResult> Details(int id)
        {
            var poruka = await _context.Poruke
                .Include(p => p.Posiljatelj)
                .Include(p => p.Primatelj)
                .Include(p => p.Oglas)
                .ThenInclude(o => o.Grad)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (poruka is null)
                return NotFound();

            return View(poruka);
        }
    }
}
