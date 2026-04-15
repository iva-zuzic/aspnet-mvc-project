using System.Collections.Generic;
using BookMarketplace.MockRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketplace.Controllers
{
    public class PorukaController : Controller
    {
        private readonly PorukaMockRepository _repository;

        public PorukaController(PorukaMockRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var poruke = _repository.GetAll();
            return View(poruke);
        }

        public IActionResult Details(int id)
        {
            var poruka = _repository.GetById(id);
            if (poruka is null)
                return NotFound();

            return View(poruka);
        }
    }
}
