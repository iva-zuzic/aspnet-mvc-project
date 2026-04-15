using System.Collections.Generic;
using BookMarketplace.MockRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketplace.Controllers
{
    public class KnjigaController : Controller
    {
        private readonly KnjigaMockRepository _repository;

        public KnjigaController(KnjigaMockRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var knjige = _repository.GetAll();
            return View(knjige);
        }

        public IActionResult Details(int id)
        {
            var knjiga = _repository.GetById(id);
            if (knjiga is null)
                return NotFound();
            
            return View(knjiga);
        }
    }
}
