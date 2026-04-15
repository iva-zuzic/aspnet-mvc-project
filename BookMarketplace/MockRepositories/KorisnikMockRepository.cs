using System;
using System.Collections.Generic;
using System.Linq;
using BookMarketplace.Models;

namespace BookMarketplace.MockRepositories
{
    public class KorisnikMockRepository
    {
        private List<Korisnik> _korisnici;

        public KorisnikMockRepository()
        {
            _korisnici = new List<Korisnik>()
            {
                new Korisnik
                {
                    Id = 1,
                    ImeIPrezime = "Ana Anić",
                    Email = "ana.anic@email.com",
                    Lozinka = "lozinka123",
                    Telefon = "091-234-5678",
                    DatumRegistracije = new DateTime(2024, 1, 15),
                    Uloga = UlogaKorisnika.Korisnik
                },
                new Korisnik
                {
                    Id = 2,
                    ImeIPrezime = "Pero Perić",
                    Email = "pero.peric@email.com",
                    Lozinka = "lozinka456",
                    Telefon = "092-345-6789",
                    DatumRegistracije = new DateTime(2024, 3, 20),
                    Uloga = UlogaKorisnika.Korisnik
                },
                new Korisnik
                {
                    Id = 3,
                    ImeIPrezime = "Maja Majić",
                    Email = "maja.majic@email.com",
                    Lozinka = "lozinka789",
                    Telefon = "095-456-7890",
                    DatumRegistracije = new DateTime(2024, 6, 10),
                    Uloga = UlogaKorisnika.Korisnik
                },
                new Korisnik
                {
                    Id = 4,
                    ImeIPrezime = "Ivan Ivić",
                    Email = "ivan.ivic@email.com",
                    Lozinka = "lozinka321",
                    Telefon = "093-567-8901",
                    DatumRegistracije = new DateTime(2023, 9, 1),
                    Uloga = UlogaKorisnika.Korisnik
                },
                new Korisnik
                {
                    Id = 5,
                    ImeIPrezime = "Marko Marković",
                    Email = "marko.markovic@email.com",
                    Lozinka = "lozinka654",
                    Telefon = "091-678-9012",
                    DatumRegistracije = new DateTime(2023, 11, 15),
                    Uloga = UlogaKorisnika.Korisnik
                },
                new Korisnik
                {
                    Id = 6,
                    ImeIPrezime = "Ivana Horvat",
                    Email = "ivana.horvat@email.com",
                    Lozinka = "lozinka987",
                    Telefon = "092-789-0123",
                    DatumRegistracije = new DateTime(2024, 2, 20),
                    Uloga = UlogaKorisnika.Korisnik
                },
                new Korisnik
                {
                    Id = 7,
                    ImeIPrezime = "Tomislav Tomić",
                    Email = "tomislav.tomic@email.com",
                    Lozinka = "admin001",
                    Telefon = "095-890-1234",
                    DatumRegistracije = new DateTime(2023, 5, 10),
                    Uloga = UlogaKorisnika.Admin
                },
                new Korisnik
                {
                    Id = 8,
                    ImeIPrezime = "Lucija Lukić",
                    Email = "lucija.lukic@email.com",
                    Lozinka = "lozinka111",
                    Telefon = "099-901-2345",
                    DatumRegistracije = new DateTime(2024, 4, 5),
                    Uloga = UlogaKorisnika.Korisnik
                },
                new Korisnik
                {
                    Id = 9,
                    ImeIPrezime = "Stjepan Stjepić",
                    Email = "stjepan.stjepic@email.com",
                    Lozinka = "lozinka222",
                    Telefon = "091-012-3456",
                    DatumRegistracije = new DateTime(2024, 7, 22),
                    Uloga = UlogaKorisnika.Korisnik
                },
                new Korisnik
                {
                    Id = 10,
                    ImeIPrezime = "Petra Petrić",
                    Email = "petra.petric@email.com",
                    Lozinka = "lozinka333",
                    Telefon = "092-123-4567",
                    DatumRegistracije = new DateTime(2024, 8, 30),
                    Uloga = UlogaKorisnika.Korisnik
                }
            };
        }

        public List<Korisnik> GetAll()
        {
            return _korisnici;
        }

        public Korisnik? GetById(int id)
        {
            return _korisnici.FirstOrDefault(k => k.Id == id);
        }
    }
}
