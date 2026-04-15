using System;
using System.Collections.Generic;
using System.Linq;
using BookMarketplace.Models;

namespace BookMarketplace.MockRepositories
{
    public class OglasMockRepository
    {
        private List<Oglas> _oglasi;

        public OglasMockRepository()
        {
            _oglasi = new List<Oglas>()
            {
                new Oglas
                {
                    Id = 1,
                    Naslov = "Harry Potter i Kamen mudraca",
                    Opis = "Knjiga u odličnom stanju, čitana jednom.",
                    Cijena = 15.00m,
                    DatumObjave = new DateTime(2025, 1, 10),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.Knjiga,
                    StanjeArtikla = StanjeArtikla.KaoNovo,
                    KorisnikId = 1,
                    GradId = 1
                },
                new Oglas
                {
                    Id = 2,
                    Naslov = "Catan - Osnovna igra",
                    Opis = "Kompletna igra, sve kartice i figurice na broju.",
                    Cijena = 45.00m,
                    DatumObjave = new DateTime(2025, 1, 15),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.DrustvenaIgra,
                    StanjeArtikla = StanjeArtikla.Dobro,
                    KorisnikId = 1,
                    GradId = 1
                },
                new Oglas
                {
                    Id = 3,
                    Naslov = "Dune",
                    Opis = "Klasik znanstvene fantastike, malo požutjele stranice.",
                    Cijena = 10.00m,
                    DatumObjave = new DateTime(2025, 2, 1),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.Knjiga,
                    StanjeArtikla = StanjeArtikla.Prihvatljivo,
                    KorisnikId = 1,
                    GradId = 1
                },
                new Oglas
                {
                    Id = 4,
                    Naslov = "Ticket to Ride: Europa",
                    Opis = "Popularna strategijska igra, igrana samo 3 puta.",
                    Cijena = 55.00m,
                    DatumObjave = new DateTime(2025, 1, 20),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.DrustvenaIgra,
                    StanjeArtikla = StanjeArtikla.KaoNovo,
                    KorisnikId = 2,
                    GradId = 2
                },
                new Oglas
                {
                    Id = 5,
                    Naslov = "1984",
                    Opis = "George Orwell, distopijski roman, dobro stanje.",
                    Cijena = 8.00m,
                    DatumObjave = new DateTime(2025, 2, 5),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.Knjiga,
                    StanjeArtikla = StanjeArtikla.Dobro,
                    KorisnikId = 2,
                    GradId = 2
                },
                new Oglas
                {
                    Id = 6,
                    Naslov = "Wingspan",
                    Opis = "Igra o pticama, kompletna s ekspanzijom.",
                    Cijena = 70.00m,
                    DatumObjave = new DateTime(2025, 2, 10),
                    DatumIzmjene = new DateTime(2025, 2, 15),
                    Status = StatusOglasa.Neaktivan,
                    TipOglasa = TipOglasa.DrustvenaIgra,
                    StanjeArtikla = StanjeArtikla.KaoNovo,
                    KorisnikId = 2,
                    GradId = 2
                },
                new Oglas
                {
                    Id = 7,
                    Naslov = "Ubojstvo u Orijent Expressu",
                    Opis = "Agatha Christie, kao novo.",
                    Cijena = 12.00m,
                    DatumObjave = new DateTime(2025, 3, 1),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.Knjiga,
                    StanjeArtikla = StanjeArtikla.KaoNovo,
                    KorisnikId = 3,
                    GradId = 3
                },
                new Oglas
                {
                    Id = 8,
                    Naslov = "Dixit",
                    Opis = "Kreativna igra asocijacija, perfektno stanje.",
                    Cijena = 30.00m,
                    DatumObjave = new DateTime(2025, 3, 5),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.DrustvenaIgra,
                    StanjeArtikla = StanjeArtikla.Novo,
                    KorisnikId = 3,
                    GradId = 3
                },
                new Oglas
                {
                    Id = 9,
                    Naslov = "Gospodar prstenova: Prstenova družina",
                    Opis = "Tolkien klasik, tvrdi uvez.",
                    Cijena = 20.00m,
                    DatumObjave = new DateTime(2025, 3, 10),
                    Status = StatusOglasa.Prodan,
                    TipOglasa = TipOglasa.Knjiga,
                    StanjeArtikla = StanjeArtikla.Dobro,
                    KorisnikId = 3,
                    GradId = 3
                },
                new Oglas
                {
                    Id = 10,
                    Naslov = "Pandemic - igra suradnje",
                    Opis = "Kooperativna igra za cijelu obitelj, komplet u odličnom stanju.",
                    Cijena = 40.00m,
                    DatumObjave = new DateTime(2025, 3, 15),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.DrustvenaIgra,
                    StanjeArtikla = StanjeArtikla.KaoNovo,
                    KorisnikId = 4,
                    GradId = 4
                }
            };
        }

        public List<Oglas> GetAll()
        {
            return _oglasi;
        }

        public Oglas? GetById(int id)
        {
            return _oglasi.FirstOrDefault(o => o.Id == id);
        }

        public List<Oglas> GetByKorisnikId(int korisnikId)
        {
            return _oglasi.Where(o => o.KorisnikId == korisnikId).ToList();
        }

        public List<Oglas> GetByStatus(StatusOglasa status)
        {
            return _oglasi.Where(o => o.Status == status).ToList();
        }

        public List<Oglas> GetByTip(TipOglasa tip)
        {
            return _oglasi.Where(o => o.TipOglasa == tip).ToList();
        }
    }
}
