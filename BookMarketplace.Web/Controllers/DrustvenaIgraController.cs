using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers
{
    [Route("oglasi/igre")]
    public class DrustvenaIgraController : Controller
    {
        private readonly BookMarketplaceDbContext _context;

        public DrustvenaIgraController(BookMarketplaceDbContext context)
        {
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var oglasi = await _context.Oglasi
                .Where(o => o.TipOglasa == TipOglasa.DrustvenaIgra && o.Status == StatusOglasa.Aktivan)
                .Include(o => o.DrustvenaIgra)
                .Include(o => o.Grad)
                .Include(o => o.Korisnik)
                .ToListAsync();

            return View(oglasi);
        }

        [Route("{id}")]
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

        [HttpGet("search")]
        public async Task<IActionResult> Search(string? term)
        {
            var query = _context.Oglasi
                .Include(o => o.DrustvenaIgra)
                .Include(o => o.Grad)
                .Where(o => o.TipOglasa == TipOglasa.DrustvenaIgra)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                var searchTerm = term.Trim().ToLower();

                query = query.Where(o =>
                    o.DrustvenaIgra != null &&
                    o.DrustvenaIgra.Naziv.ToLower().StartsWith(searchTerm));
            }

            var igre = await query
                .OrderBy(o => o.DrustvenaIgra!.Naziv)
                .ToListAsync();

            return PartialView("_IgraCards", igre);
        }
    }
}