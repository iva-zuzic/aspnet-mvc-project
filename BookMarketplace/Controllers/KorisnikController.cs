using System.Collections.Generic;
using BookMarketplace.MockRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketplace.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly KorisnikMockRepository _repository;

        public KorisnikController(KorisnikMockRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var korisnici = _repository.GetAll();
            return View(korisnici);
        }

        public IActionResult Details(int id)
        {
            var korisnik = _repository.GetById(id);
            if (korisnik is null)
                return NotFound();

            return View(korisnik);
        }
    }
}
