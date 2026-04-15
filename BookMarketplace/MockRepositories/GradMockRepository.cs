using System;
using System.Collections.Generic;
using System.Linq;
using BookMarketplace.Models;

namespace BookMarketplace.MockRepositories
{
    public class GradMockRepository
    {
        private List<Grad> _gradovi;

        public GradMockRepository()
        {
            _gradovi = new List<Grad>()
            {
                new Grad
                {
                    Id = 1,
                    Naziv = "Zagreb",
                    PostanskiBroj = "10000"
                },
                new Grad
                {
                    Id = 2,
                    Naziv = "Split",
                    PostanskiBroj = "21000"
                },
                new Grad
                {
                    Id = 3,
                    Naziv = "Rijeka",
                    PostanskiBroj = "51000"
                },
                new Grad
                {
                    Id = 4,
                    Naziv = "Osijek",
                    PostanskiBroj = "31000"
                },
                new Grad
                {
                    Id = 5,
                    Naziv = "Zadar",
                    PostanskiBroj = "23000"
                },
                new Grad
                {
                    Id = 6,
                    Naziv = "Pula",
                    PostanskiBroj = "52100"
                },
                new Grad
                {
                    Id = 7,
                    Naziv = "Dubrovnik",
                    PostanskiBroj = "20000"
                },
                new Grad
                {
                    Id = 8,
                    Naziv = "Varaždin",
                    PostanskiBroj = "42000"
                },
                new Grad
                {
                    Id = 9,
                    Naziv = "Šibenik",
                    PostanskiBroj = "22000"
                },
                new Grad
                {
                    Id = 10,
                    Naziv = "Slavonski Brod",
                    PostanskiBroj = "35000"
                }
            };
        }

        public List<Grad> GetAll()
        {
            return _gradovi;
        }

        public Grad? GetById(int id)
        {
            return _gradovi.FirstOrDefault(g => g.Id == id);
        }
    }
}
