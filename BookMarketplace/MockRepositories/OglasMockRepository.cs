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
                },
                new Oglas
                {
                    Id = 11,
                    Naslov = "Igra prijestolja",
                    Opis = "George R.R. Martin, eng. izdanje, tvrdi uvez.",
                    Cijena = 18.00m,
                    DatumObjave = new DateTime(2025, 3, 18),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.Knjiga,
                    StanjeArtikla = StanjeArtikla.Dobro,
                    KorisnikId = 4,
                    GradId = 4
                },
                new Oglas
                {
                    Id = 12,
                    Naslov = "Da Vincijev kod",
                    Opis = "Dan Brown, uzbudljiv triler, čitana jednom.",
                    Cijena = 14.00m,
                    DatumObjave = new DateTime(2025, 3, 20),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.Knjiga,
                    StanjeArtikla = StanjeArtikla.KaoNovo,
                    KorisnikId = 5,
                    GradId = 5
                },
                new Oglas
                {
                    Id = 13,
                    Naslov = "Fondacija",
                    Opis = "Isaac Asimov, klasik znanstvene fantastike, meki uvez.",
                    Cijena = 11.00m,
                    DatumObjave = new DateTime(2025, 3, 22),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.Knjiga,
                    StanjeArtikla = StanjeArtikla.Dobro,
                    KorisnikId = 5,
                    GradId = 5
                },
                new Oglas
                {
                    Id = 14,
                    Naslov = "Mali princ",
                    Opis = "Saint-Exupéry, hrvatsko izdanje, ilustrirano.",
                    Cijena = 9.00m,
                    DatumObjave = new DateTime(2025, 3, 25),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.Knjiga,
                    StanjeArtikla = StanjeArtikla.KaoNovo,
                    KorisnikId = 6,
                    GradId = 6
                },
                new Oglas
                {
                    Id = 15,
                    Naslov = "Pas Baskervillea",
                    Opis = "Sherlock Holmes klasik, eng. izdanje.",
                    Cijena = 10.00m,
                    DatumObjave = new DateTime(2025, 3, 28),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.Knjiga,
                    StanjeArtikla = StanjeArtikla.Prihvatljivo,
                    KorisnikId = 6,
                    GradId = 6
                },
                new Oglas
                {
                    Id = 16,
                    Naslov = "7 Wonders",
                    Opis = "Strateška igra za 2-7 igrača, kompletna.",
                    Cijena = 35.00m,
                    DatumObjave = new DateTime(2025, 4, 1),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.DrustvenaIgra,
                    StanjeArtikla = StanjeArtikla.Dobro,
                    KorisnikId = 7,
                    GradId = 7
                },
                new Oglas
                {
                    Id = 17,
                    Naslov = "Codenames",
                    Opis = "Zabavna igra za veće grupe, sve karte na broju.",
                    Cijena = 22.00m,
                    DatumObjave = new DateTime(2025, 4, 3),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.DrustvenaIgra,
                    StanjeArtikla = StanjeArtikla.KaoNovo,
                    KorisnikId = 7,
                    GradId = 7
                },
                new Oglas
                {
                    Id = 18,
                    Naslov = "Azul",
                    Opis = "Prekrasna apstraktna igra, pločice u savršenom stanju.",
                    Cijena = 38.00m,
                    DatumObjave = new DateTime(2025, 4, 5),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.DrustvenaIgra,
                    StanjeArtikla = StanjeArtikla.Novo,
                    KorisnikId = 8,
                    GradId = 8
                },
                new Oglas
                {
                    Id = 19,
                    Naslov = "Exploding Kittens",
                    Opis = "Brza i smiješna kartaška igra, kompletna.",
                    Cijena = 18.00m,
                    DatumObjave = new DateTime(2025, 4, 8),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.DrustvenaIgra,
                    StanjeArtikla = StanjeArtikla.Dobro,
                    KorisnikId = 8,
                    GradId = 8
                },
                new Oglas
                {
                    Id = 20,
                    Naslov = "Dobble",
                    Opis = "Igra brzog zapažanja, idealna za djecu i odrasle.",
                    Cijena = 15.00m,
                    DatumObjave = new DateTime(2025, 4, 10),
                    Status = StatusOglasa.Aktivan,
                    TipOglasa = TipOglasa.DrustvenaIgra,
                    StanjeArtikla = StanjeArtikla.KaoNovo,
                    KorisnikId = 9,
                    GradId = 9
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
