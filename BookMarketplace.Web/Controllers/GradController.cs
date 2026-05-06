using BookMarketplace.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers
{
    public class GradController : Controller
    {
        private readonly BookMarketplaceDbContext _context;

        public GradController(BookMarketplaceDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var gradovi = await _context.Gradovi
                .Include(g => g.Oglasi)
                .ToListAsync();

            return View(gradovi);
        }

        public async Task<IActionResult> Details(int id)
        {
            var grad = await _context.Gradovi
                .Include(g => g.Oglasi)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (grad is null)
                return NotFound();

            return View(grad);
        }
    }
}
