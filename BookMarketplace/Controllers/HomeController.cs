using BookMarketplace.MockRepositories;
using BookMarketplace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookMarketplace.Controllers
{
    public class HomeController : Controller
    {
        private readonly OglasMockRepository _oglasRepository;
        private readonly KnjigaMockRepository _knjigaRepository;
        private readonly DrustvenaIgraMockRepository _igraRepository;

        public HomeController(
            OglasMockRepository oglasRepository,
            KnjigaMockRepository knjigaRepository,
            DrustvenaIgraMockRepository igraRepository)
        {
            _oglasRepository = oglasRepository;
            _knjigaRepository = knjigaRepository;
            _igraRepository = igraRepository;
        }

        public IActionResult Index()
        {
            var vm = new HomeIndexViewModel
            {
                Knjige = _knjigaRepository.GetAll(),
                Igre = _igraRepository.GetAll(),
                NajnovijiOglasi = _oglasRepository.GetAll()
                    .OrderByDescending(o => o.DatumObjave)
                    .Take(8)
                    .ToList()
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
