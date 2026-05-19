using BookMarketplace.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers
{
    [Route("gradovi")]
    public class GradController : Controller
    {
        private readonly BookMarketplaceDbContext _context;

        public GradController(BookMarketplaceDbContext context)
        {
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var gradovi = await _context.Gradovi
                .Include(g => g.Oglasi)
                .OrderBy(g => g.Naziv)
                .ToListAsync();

            return View(gradovi);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string? term)
        {
            var query = _context.Gradovi
                .Include(g => g.Oglasi)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                var searchTerm = term.Trim().ToLower();

                query = query.Where(g =>
                    g.Naziv.ToLower().StartsWith(searchTerm) ||
                    g.PostanskiBroj.StartsWith(searchTerm));
            }

            var gradovi = await query
                .OrderBy(g => g.Naziv)
                .ToListAsync();

            return PartialView("_GradCards", gradovi);
        }

        [Route("{id}")]
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