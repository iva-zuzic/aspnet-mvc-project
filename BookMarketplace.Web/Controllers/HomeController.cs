using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookMarketplace.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookMarketplaceDbContext _context;

        public HomeController(BookMarketplaceDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeIndexViewModel
            {
                Knjige = await _context.Knjige
                    .Include(k => k.Oglas)
                    .ThenInclude(o => o.Korisnik)
                    .Include(k => k.Oglas)
                    .ThenInclude(o => o.Slike)
                    .Where(k => k.Oglas.Status == StatusOglasa.Aktivan && k.Oglas.Korisnik.DeletedAt == null)
                    .ToListAsync()
            ,
                Igre = await _context.DrustveneIgre
                    .Include(i => i.Oglas)
                    .ThenInclude(o => o.Korisnik)
                    .Include(i => i.Oglas)
                    .ThenInclude(o => o.Slike)
                    .Where(i => i.Oglas.Status == StatusOglasa.Aktivan && i.Oglas.Korisnik.DeletedAt == null)
                    .ToListAsync()
            ,
                NajnovijiOglasi = await _context.Oglasi
                    .Where(o => o.Status == StatusOglasa.Aktivan)
                    .Include(o => o.Grad)
                    .Include(o => o.Korisnik)
                    .Include(o => o.Slike)
                    .OrderByDescending(o => o.DatumObjave)
                    .Take(12)
                    .ToListAsync()
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
