using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers
{
    [Route("oglasi/knjige")]
    public class KnjigaController : Controller
    {
        private readonly BookMarketplaceDbContext _context;

        public KnjigaController(BookMarketplaceDbContext context)
        {
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var oglasi = await _context.Oglasi
                .Where(o => o.TipOglasa == TipOglasa.Knjiga && o.Status == StatusOglasa.Aktivan)
                .Include(o => o.Korisnik)
                .Include(o => o.Grad)
                .Include(o => o.Knjiga)
                .ToListAsync();

            return View(oglasi);
        }

        [Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var oglas = await _context.Oglasi
                .Include(o => o.Korisnik)
                .Include(o => o.Grad)
                .Include(o => o.Knjiga)
                .Include(o => o.Slike)
                .FirstOrDefaultAsync(o =>
                    o.Id == id &&
                    o.TipOglasa == TipOglasa.Knjiga &&
                    o.Status == StatusOglasa.Aktivan);

            if (oglas == null)
                return NotFound();

            return View(oglas);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string? term)
        {
            var query = _context.Oglasi
                .Include(o => o.Knjiga)
                .Include(o => o.Grad)
                .Include(o => o.Korisnik)
                .Where(o =>
                    o.TipOglasa == TipOglasa.Knjiga &&
                    o.Status == StatusOglasa.Aktivan &&
                    o.Korisnik.DeletedAt == null)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                var searchTerm = term.Trim().ToLower();

                query = query.Where(o =>
                    o.Knjiga != null &&
                    (
                        o.Knjiga.Naziv.ToLower().StartsWith(searchTerm) ||
                        o.Knjiga.Autor.ToLower().StartsWith(searchTerm)
                    ));
            }

            var knjige = await query
                .OrderBy(o => o.Knjiga!.Naziv)
                .ToListAsync();

            return PartialView("_KnjigaCards", knjige);
        }
    }
}
