using System;
using System.Collections.Generic;
using System.Linq;
using BookGameMarketplace.Models;

namespace BookGameMarketplace.MockRepositories
{
    public class PorukaMockRepository
    {
        private List<Poruka> _poruke;

        public PorukaMockRepository()
        {
            _poruke = new List<Poruka>()
            {
                new Poruka
                {
                    Id = 1,
                    Sadrzaj = "Je li knjiga još dostupna?",
                    Procitano = true,
                    DatumSlanja = new DateTime(2025, 1, 12, 10, 0, 0),
                    PosiljateljId = 2,
                    PrimateljId = 1,
                    OglasId = 1
                },
                new Poruka
                {
                    Id = 2,
                    Sadrzaj = "Da, slobodno mi se javi za preuzimanje!",
                    Procitano = false,
                    DatumSlanja = new DateTime(2025, 1, 12, 14, 30, 0),
                    PosiljateljId = 1,
                    PrimateljId = 2,
                    OglasId = 1
                },
                new Poruka
                {
                    Id = 3,
                    Sadrzaj = "Može li niža cijena za Catan?",
                    Procitano = false,
                    DatumSlanja = new DateTime(2025, 1, 17, 9, 0, 0),
                    PosiljateljId = 3,
                    PrimateljId = 1,
                    OglasId = 2
                },
                new Poruka
                {
                    Id = 4,
                    Sadrzaj = "Koliko dugo igra traje?",
                    Procitano = true,
                    DatumSlanja = new DateTime(2025, 1, 21, 11, 0, 0),
                    PosiljateljId = 3,
                    PrimateljId = 2,
                    OglasId = 4
                },
                new Poruka
                {
                    Id = 5,
                    Sadrzaj = "Oko 60 minuta prosječno.",
                    Procitano = false,
                    DatumSlanja = new DateTime(2025, 1, 21, 18, 0, 0),
                    PosiljateljId = 2,
                    PrimateljId = 3,
                    OglasId = 4
                },
                new Poruka
                {
                    Id = 6,
                    Sadrzaj = "Je li Wingspan kompletna s pravilima?",
                    Procitano = true,
                    DatumSlanja = new DateTime(2025, 2, 11, 9, 0, 0),
                    PosiljateljId = 1,
                    PrimateljId = 2,
                    OglasId = 6
                },
                new Poruka
                {
                    Id = 7,
                    Sadrzaj = "Da, sve je kompletno uključujući ekspanziju.",
                    Procitano = false,
                    DatumSlanja = new DateTime(2025, 2, 11, 13, 0, 0),
                    PosiljateljId = 2,
                    PrimateljId = 1,
                    OglasId = 6
                },
                new Poruka
                {
                    Id = 8,
                    Sadrzaj = "Može li se dogovoriti dostava za Dixit?",
                    Procitano = false,
                    DatumSlanja = new DateTime(2025, 3, 6, 10, 30, 0),
                    PosiljateljId = 1,
                    PrimateljId = 3,
                    OglasId = 8
                },
                new Poruka
                {
                    Id = 9,
                    Sadrzaj = "Imam još jednu kopiju Tolkiena, zanima li te?",
                    Procitano = true,
                    DatumSlanja = new DateTime(2025, 3, 12, 16, 0, 0),
                    PosiljateljId = 3,
                    PrimateljId = 2,
                    OglasId = 9
                },
                new Poruka
                {
                    Id = 10,
                    Sadrzaj = "Hvala, ali već sam nabavio.",
                    Procitano = false,
                    DatumSlanja = new DateTime(2025, 3, 12, 19, 45, 0),
                    PosiljateljId = 2,
                    PrimateljId = 3,
                    OglasId = 9
                }
            };
        }

        public List<Poruka> GetAll()
        {
            return _poruke;
        }

        public Poruka? GetById(int id)
        {
            return _poruke.FirstOrDefault(p => p.Id == id);
        }

        public List<Poruka> GetByPosiljatelj(int posiljateljId)
        {
            return _poruke.Where(p => p.PosiljateljId == posiljateljId).ToList();
        }

        public List<Poruka> GetByPrimatelj(int primateljId)
        {
            return _poruke.Where(p => p.PrimateljId == primateljId).ToList();
        }
    }
}
