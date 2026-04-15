using System.Collections.Generic;
using System.Linq;
using BookMarketplace.MockRepositories;
using BookMarketplace.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketplace.Controllers
{
    public class KnjigaController : Controller
    {
        private readonly OglasMockRepository _oglasRepository;
        private readonly KnjigaMockRepository _knjigaRepository;
        private readonly GradMockRepository _gradRepository;
        private readonly KorisnikMockRepository _korisnikRepository;

        public KnjigaController(
            OglasMockRepository oglasRepository,
            KnjigaMockRepository knjigaRepository,
            GradMockRepository gradRepository,
            KorisnikMockRepository korisnikRepository)
        {
            _oglasRepository = oglasRepository;
            _knjigaRepository = knjigaRepository;
            _gradRepository = gradRepository;
            _korisnikRepository = korisnikRepository;
        }

        public IActionResult Index()
        {
            var oglasi = _oglasRepository.GetByTip(TipOglasa.Knjiga);
            var knjige = _knjigaRepository.GetAll();
            var gradovi = _gradRepository.GetAll();
            var korisnici = _korisnikRepository.GetAll();

            foreach (var oglas in oglasi)
            {
                oglas.Knjiga = knjige.FirstOrDefault(k => k.OglasId == oglas.Id);
                oglas.Grad = gradovi.FirstOrDefault(g => g.Id == oglas.GradId)!;
                oglas.Korisnik = korisnici.FirstOrDefault(k => k.Id == oglas.KorisnikId)!;
            }

            return View(oglasi);
        }

        public IActionResult Details(int id)
        {
            var oglas = _oglasRepository.GetById(id);
            if (oglas is null || oglas.TipOglasa != TipOglasa.Knjiga)
                return NotFound();

            oglas.Knjiga = _knjigaRepository.GetAll().FirstOrDefault(k => k.OglasId == oglas.Id);
            oglas.Grad = _gradRepository.GetAll().FirstOrDefault(g => g.Id == oglas.GradId)!;
            oglas.Korisnik = _korisnikRepository.GetAll().FirstOrDefault(k => k.Id == oglas.KorisnikId)!;

            return View(oglas);
        }
    }
}
