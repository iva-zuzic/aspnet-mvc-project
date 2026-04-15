using System;
using System.Collections.Generic;
using System.Linq;
using BookMarketplace.Models;

namespace BookMarketplace.MockRepositories
{
    public class KnjigaMockRepository
    {
        private List<Knjiga> _knjige;
        public KnjigaMockRepository()
        {
            _knjige = new List<Knjiga>()
            {
                new Knjiga
                {
                    Id = 1,
                    Naziv = "Harry Potter i Kamen mudraca",
                    Autor = "J.K. Rowling",
                    ISBN = "978-0545010221",
                    Izdavac = "Scholastic",
                    GodinaIzdanja = 2007,
                    Jezik = "Engleski",
                    Zanr = ZanrKnjige.Fantastika,
                    OglasId = 1
                },
                new Knjiga
                {
                    Id = 2,
                    Naziv = "Igra prijestolja",
                    Autor = "George R.R. Martin",
                    ISBN = "978-0553103540",
                    Izdavac = "Bantam Books",
                    GodinaIzdanja = 1996,
                    Jezik = "Engleski",
                    Zanr = ZanrKnjige.Fantastika,
                    OglasId = 2
                },
                new Knjiga
                {
                    Id = 3,
                    Naziv = "Ubojstvo u Orijent Expressu",
                    Autor = "Agatha Christie",
                    ISBN = "978-0062073488",
                    Izdavac = "HarperCollins",
                    GodinaIzdanja = 1934,
                    Jezik = "Engleski",
                    Zanr = ZanrKnjige.Krimi,
                    OglasId = 3
                },
                new Knjiga
                {
                    Id = 4,
                    Naziv = "Da Vincijev kod",
                    Autor = "Dan Brown",
                    ISBN = "978-0307474278",
                    Izdavac = "Doubleday",
                    GodinaIzdanja = 2003,
                    Jezik = "Engleski",
                    Zanr = ZanrKnjige.Triler,
                    OglasId = 4
                },
                new Knjiga
                {
                    Id = 5,
                    Naziv = "Ponos i predrasude",
                    Autor = "Jane Austen",
                    ISBN = "978-1503290563",
                    Izdavac = "CreateSpace Independent Publishing Platform",
                    GodinaIzdanja = 1813,
                    Jezik = "Engleski",
                    Zanr = ZanrKnjige.Romansa,
                    OglasId = 5
                },
                new Knjiga
                {
                    Id = 6,
                    Naziv = "Fondacija",
                    Autor = "Isaac Asimov",
                    ISBN = "978-0553293357",
                    Izdavac = "Bantam Books",
                    GodinaIzdanja = 1950,
                    Jezik = "Engleski",
                    Zanr = ZanrKnjige.ZnanstvenaFantastika,
                    OglasId = 6
                },
                new Knjiga
                {
                    Id = 7,
                    Naziv = "Zločin i kazna",
                    Autor = "Fjodor Dostojevski",
                    ISBN = "978-0140449136",
                    Izdavac = "Penguin Classics",
                    GodinaIzdanja = 1866,
                    Jezik = "Hrvatski",
                    Zanr = ZanrKnjige.Drama,
                    OglasId = 7
                },
                new Knjiga
                {
                    Id = 8,
                    Naziv = "Mali princ",
                    Autor = "Antoine de Saint-Exupéry",
                    ISBN = "978-0156012195",
                    Izdavac = "Harcourt",
                    GodinaIzdanja = 1943,
                    Jezik = "Hrvatski",
                    Zanr = ZanrKnjige.Drama,
                    OglasId = 8
                },
                new Knjiga
                {
                    Id = 9,
                    Naziv = "Sto godina samoće",
                    Autor = "Gabriel García Márquez",
                    ISBN = "978-0060883287",
                    Izdavac = "Harper & Row",
                    GodinaIzdanja = 1967,
                    Jezik = "Hrvatski",
                    Zanr = ZanrKnjige.Romansa,
                    OglasId = 9
                },
                new Knjiga
                {
                    Id = 10,
                    Naziv = "Pas Baskervillea",
                    Autor = "Arthur Conan Doyle",
                    ISBN = "978-0141034355",
                    Izdavac = "Penguin Classics",
                    GodinaIzdanja = 1902,
                    Jezik = "Engleski",
                    Zanr = ZanrKnjige.Krimi,
                    OglasId = 10
                }
            };

        }

        public List<Knjiga> GetAll()
        {
            return _knjige;
        }

        public Knjiga? GetById(int id)
        {
            return _knjige.FirstOrDefault(k => k.Id == id);
        }
    }
}
