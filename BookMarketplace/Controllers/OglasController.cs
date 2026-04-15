using System.Collections.Generic;
using BookMarketplace.MockRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketplace.Controllers
{
    public class OglasController : Controller
    {
        private readonly OglasMockRepository _repository;

        public OglasController(OglasMockRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var oglasi = _repository.GetAll();
            return View(oglasi);
        }

        public IActionResult Details(int id)
        {
            var oglas = _repository.GetById(id);
            if (oglas is null)
                return NotFound();

            return View(oglas);
        }
    }
}
