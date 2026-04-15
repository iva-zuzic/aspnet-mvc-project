using System.Collections.Generic;
using BookMarketplace.MockRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketplace.Controllers
{
    public class GradController : Controller
    {
        private readonly GradMockRepository _repository;

        public GradController(GradMockRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var gradovi = _repository.GetAll();
            return View(gradovi);
        }

        public IActionResult Details(int id)
        {
            var grad = _repository.GetById(id);
            if (grad is null)
                return NotFound();

            return View(grad);
        }
    }
}
