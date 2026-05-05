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
                    .ToListAsync()
            ,
                Igre = await _context.DrustveneIgre
                    .Include(i => i.Oglas)
                    .ToListAsync()
            ,
                NajnovijiOglasi = await _context.Oglasi
                    .Include(o => o.Grad)
                    .Include(o => o.Korisnik)
                    .OrderByDescending(o => o.DatumObjave)
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
