using System.Collections.Generic;
using System.Linq;
using BookMarketplace.MockRepositories;
using BookMarketplace.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketplace.Controllers
{
    public class DrustvenaIgraController : Controller
    {
        private readonly OglasMockRepository _oglasRepository;
        private readonly DrustvenaIgraMockRepository _igraRepository;
        private readonly GradMockRepository _gradRepository;
        private readonly KorisnikMockRepository _korisnikRepository;

        public DrustvenaIgraController(
            OglasMockRepository oglasRepository,
            DrustvenaIgraMockRepository igraRepository,
            GradMockRepository gradRepository,
            KorisnikMockRepository korisnikRepository)
        {
            _oglasRepository = oglasRepository;
            _igraRepository = igraRepository;
            _gradRepository = gradRepository;
            _korisnikRepository = korisnikRepository;
        }

        public IActionResult Index()
        {
            var oglasi = _oglasRepository.GetByTip(TipOglasa.DrustvenaIgra);
            var igre = _igraRepository.GetAll();
            var gradovi = _gradRepository.GetAll();
            var korisnici = _korisnikRepository.GetAll();

            foreach (var oglas in oglasi)
            {
                oglas.DrustvenaIgra = igre.FirstOrDefault(i => i.OglasId == oglas.Id);
                oglas.Grad = gradovi.FirstOrDefault(g => g.Id == oglas.GradId)!;
                oglas.Korisnik = korisnici.FirstOrDefault(k => k.Id == oglas.KorisnikId)!;
            }

            return View(oglasi);
        }

        public IActionResult Details(int id)
        {
            var oglas = _oglasRepository.GetById(id);
            if (oglas is null || oglas.TipOglasa != TipOglasa.DrustvenaIgra)
                return NotFound();

            oglas.DrustvenaIgra = _igraRepository.GetAll().FirstOrDefault(i => i.OglasId == oglas.Id);
            oglas.Grad = _gradRepository.GetAll().FirstOrDefault(g => g.Id == oglas.GradId)!;
            oglas.Korisnik = _korisnikRepository.GetAll().FirstOrDefault(k => k.Id == oglas.KorisnikId)!;

            return View(oglas);
        }
    }
}
