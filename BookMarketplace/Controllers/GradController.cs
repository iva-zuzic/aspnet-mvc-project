using System.Collections.Generic;
using System.Linq;
using BookMarketplace.MockRepositories;
using BookMarketplace.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketplace.Controllers
{
    public class GradController : Controller
    {
        private readonly GradMockRepository _gradRepository;
        private readonly OglasMockRepository _oglasRepository;

        public GradController(GradMockRepository gradRepository, OglasMockRepository oglasRepository)
        {
            _gradRepository = gradRepository;
            _oglasRepository = oglasRepository;
        }

        public IActionResult Index()
        {
            var gradovi = _gradRepository.GetAll();
            var oglasi = _oglasRepository.GetAll();

            foreach (var grad in gradovi)
            {
                grad.Oglasi = oglasi.Where(o => o.GradId == grad.Id).ToList();
            }

            return View(gradovi);
        }

        public IActionResult Details(int id)
        {
            var grad = _gradRepository.GetById(id);
            if (grad is null)
                return NotFound();

            grad.Oglasi = _oglasRepository.GetAll().Where(o => o.GradId == grad.Id).ToList();

            return View(grad);
        }
    }
}
