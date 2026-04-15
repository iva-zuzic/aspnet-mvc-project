using System;
using System.Collections.Generic;
using System.Linq;
using BookGameMarketplace.Models;

namespace BookGameMarketplace.MockRepositories
{
    public class DrustvenaIgraMockRepository
    {
        private List<DrustvenaIgra> _igre;

        public DrustvenaIgraMockRepository()
        {
            _igre = new List<DrustvenaIgra>()
            {
                new DrustvenaIgra
                {
                    Id = 1,
                    Naziv = "Catan - Osnovna igra",
                    MinBrojIgraca = 3,
                    MaxBrojIgraca = 4,
                    MinimalnasDob = 10,
                    TrajanjeMins = 75,
                    Zanr = ZanrIgre.Strategija,
                    OglasId = 2
                },
                new DrustvenaIgra
                {
                    Id = 2,
                    Naziv = "Ticket to Ride: Europa",
                    MinBrojIgraca = 2,
                    MaxBrojIgraca = 5,
                    MinimalnasDob = 8,
                    TrajanjeMins = 60,
                    Zanr = ZanrIgre.Strategija,
                    OglasId = 4
                },
                new DrustvenaIgra
                {
                    Id = 3,
                    Naziv = "Wingspan",
                    MinBrojIgraca = 1,
                    MaxBrojIgraca = 5,
                    MinimalnasDob = 10,
                    TrajanjeMins = 90,
                    Zanr = ZanrIgre.Strategija,
                    OglasId = 6
                },
                new DrustvenaIgra
                {
                    Id = 4,
                    Naziv = "Dixit",
                    MinBrojIgraca = 3,
                    MaxBrojIgraca = 6,
                    MinimalnasDob = 8,
                    TrajanjeMins = 30,
                    Zanr = ZanrIgre.Zabavna,
                    OglasId = 8
                },
                new DrustvenaIgra
                {
                    Id = 5,
                    Naziv = "Pandemic",
                    MinBrojIgraca = 2,
                    MaxBrojIgraca = 4,
                    MinimalnasDob = 8,
                    TrajanjeMins = 45,
                    Zanr = ZanrIgre.Kooperativna,
                    OglasId = 11
                },
                new DrustvenaIgra
                {
                    Id = 6,
                    Naziv = "7 Wonders",
                    MinBrojIgraca = 2,
                    MaxBrojIgraca = 7,
                    MinimalnasDob = 10,
                    TrajanjeMins = 30,
                    Zanr = ZanrIgre.Strategija,
                    OglasId = 12
                },
                new DrustvenaIgra
                {
                    Id = 7,
                    Naziv = "Codenames",
                    MinBrojIgraca = 2,
                    MaxBrojIgraca = 8,
                    MinimalnasDob = 10,
                    TrajanjeMins = 15,
                    Zanr = ZanrIgre.Zabavna,
                    OglasId = 13
                },
                new DrustvenaIgra
                {
                    Id = 8,
                    Naziv = "Azul",
                    MinBrojIgraca = 2,
                    MaxBrojIgraca = 4,
                    MinimalnasDob = 8,
                    TrajanjeMins = 45,
                    Zanr = ZanrIgre.Apstraktna,
                    OglasId = 14
                },
                new DrustvenaIgra
                {
                    Id = 9,
                    Naziv = "Exploding Kittens",
                    MinBrojIgraca = 2,
                    MaxBrojIgraca = 5,
                    MinimalnasDob = 7,
                    TrajanjeMins = 15,
                    Zanr = ZanrIgre.Zabavna,
                    OglasId = 15
                },
                new DrustvenaIgra
                {
                    Id = 10,
                    Naziv = "Dobble",
                    MinBrojIgraca = 2,
                    MaxBrojIgraca = 8,
                    MinimalnasDob = 6,
                    TrajanjeMins = 10,
                    Zanr = ZanrIgre.Zabavna,
                    OglasId = 16
                }
            };
        }

        public List<DrustvenaIgra> GetAll()
        {
            return _igre;
        }

        public DrustvenaIgra? GetById(int id)
        {
            return _igre.FirstOrDefault(i => i.Id == id);
        }
    }
}
