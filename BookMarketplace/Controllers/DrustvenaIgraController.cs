using System.Collections.Generic;
using BookMarketplace.MockRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketplace.Controllers
{
    public class DrustvenaIgraController : Controller
    {
        private readonly DrustvenaIgraMockRepository _repository;

        public DrustvenaIgraController(DrustvenaIgraMockRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var igre = _repository.GetAll();
            return View(igre);
        }

        public IActionResult Details(int id)
        {
            var igra = _repository.GetById(id);
            if (igra is null)
                return NotFound();

            return View(igra);
        }
    }
}
