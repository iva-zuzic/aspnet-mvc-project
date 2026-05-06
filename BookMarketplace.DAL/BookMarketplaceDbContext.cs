using Microsoft.EntityFrameworkCore;
using BookMarketplace.Model;

namespace BookMarketplace.DAL;

public class BookMarketplaceDbContext : DbContext
{
    public BookMarketplaceDbContext(DbContextOptions<BookMarketplaceDbContext> options) : base(options)
    {}

    public DbSet<Korisnik> Korisnici { get; set; }
    public DbSet<Oglas> Oglasi { get; set; }
    public DbSet<Knjiga> Knjige { get; set; }
    public DbSet<DrustvenaIgra> DrustveneIgre { get; set; }
    public DbSet<Grad> Gradovi { get; set; }
    public DbSet<Slika> Slike { get; set; }
    public DbSet<Poruka> Poruke { get; set; }
    public DbSet<Favorit> Favoriti { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Favorit>()
            .HasKey(f => new { f.KorisnikId, f.OglasId });

        modelBuilder.Entity<Favorit>()
            .HasOne(f => f.Korisnik)
            .WithMany(k => k.Favoriti)
            .HasForeignKey(f => f.KorisnikId)
            .OnDelete(DeleteBehavior.Restrict);;

        modelBuilder.Entity<Favorit>()
            .HasOne(f => f.Oglas)
            .WithMany(o => o.Favoriti)
            .HasForeignKey(f => f.OglasId)
            .OnDelete(DeleteBehavior.Restrict);

        // Konfiguracija za Poruku
        modelBuilder.Entity<Poruka>()
            .HasOne(p => p.Posiljatelj)
            .WithMany(k => k.PoslanePoruke)
            .HasForeignKey(p => p.PosiljateljId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Poruka>()
            .HasOne(p => p.Primatelj)
            .WithMany(k => k.PrimljenePoruke)
            .HasForeignKey(p => p.PrimateljId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Oglas>()
            .Property(o => o.Cijena)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Grad>().HasData(
            new Grad { Id = 1, Naziv = "Zagreb", PostanskiBroj = "10000" },
            new Grad { Id = 2, Naziv = "Split", PostanskiBroj = "21000" },
            new Grad { Id = 3, Naziv = "Rijeka", PostanskiBroj = "51000" },
            new Grad { Id = 4, Naziv = "Osijek", PostanskiBroj = "31000" },
            new Grad { Id = 5, Naziv = "Zadar", PostanskiBroj = "23000" },
            new Grad { Id = 6, Naziv = "Pula", PostanskiBroj = "52100" },
            new Grad { Id = 7, Naziv = "Dubrovnik", PostanskiBroj = "20000" },
            new Grad { Id = 8, Naziv = "Varaždin", PostanskiBroj = "42000" },
            new Grad { Id = 9, Naziv = "Šibenik", PostanskiBroj = "22000" },
            new Grad { Id = 10, Naziv = "Slavonski Brod", PostanskiBroj = "35000" }
        );

        modelBuilder.Entity<Korisnik>().HasData(
            new Korisnik { Id = 1, ImeIPrezime = "Ana Anić", Email = "ana.anic@email.com", Lozinka = "lozinka123", Telefon = "091-234-5678", DatumRegistracije = new DateTime(2024, 1, 15), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 2, ImeIPrezime = "Pero Perić", Email = "pero.peric@email.com", Lozinka = "lozinka456", Telefon = "092-345-6789", DatumRegistracije = new DateTime(2024, 3, 20), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 3, ImeIPrezime = "Maja Majić", Email = "maja.majic@email.com", Lozinka = "lozinka789", Telefon = "095-456-7890", DatumRegistracije = new DateTime(2024, 6, 10), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 4, ImeIPrezime = "Ivan Ivić", Email = "ivan.ivic@email.com", Lozinka = "lozinka321", Telefon = "093-567-8901", DatumRegistracije = new DateTime(2023, 9, 1), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 5, ImeIPrezime = "Marko Marković", Email = "marko.markovic@email.com", Lozinka = "lozinka654", Telefon = "091-678-9012", DatumRegistracije = new DateTime(2023, 11, 15), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 6, ImeIPrezime = "Ivana Horvat", Email = "ivana.horvat@email.com", Lozinka = "lozinka987", Telefon = "092-789-0123", DatumRegistracije = new DateTime(2024, 2, 20), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 7, ImeIPrezime = "Tomislav Tomić", Email = "tomislav.tomic@email.com", Lozinka = "admin001", Telefon = "095-890-1234", DatumRegistracije = new DateTime(2023, 5, 10), Uloga = UlogaKorisnika.Admin },
            new Korisnik { Id = 8, ImeIPrezime = "Lucija Lukić", Email = "lucija.lukic@email.com", Lozinka = "lozinka111", Telefon = "099-901-2345", DatumRegistracije = new DateTime(2024, 4, 5), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 9, ImeIPrezime = "Stjepan Stjepić", Email = "stjepan.stjepic@email.com", Lozinka = "lozinka222", Telefon = "091-012-3456", DatumRegistracije = new DateTime(2024, 7, 22), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 10, ImeIPrezime = "Petra Petrić", Email = "petra.petric@email.com", Lozinka = "lozinka333", Telefon = "092-123-4567", DatumRegistracije = new DateTime(2024, 8, 30), Uloga = UlogaKorisnika.Korisnik }
        );

        modelBuilder.Entity<Oglas>().HasData(
            new Oglas { Id = 1, Naslov = "Harry Potter i Kamen mudraca", Opis = "Knjiga u odličnom stanju, čitana jednom.", Cijena = 15.00m, DatumObjave = new DateTime(2025, 1, 10), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 1, GradId = 1 },
            new Oglas { Id = 2, Naslov = "Catan - Osnovna igra", Opis = "Kompletna igra, sve kartice i figurice na broju.", Cijena = 45.00m, DatumObjave = new DateTime(2025, 1, 15), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 1, GradId = 1 },
            new Oglas { Id = 3, Naslov = "Dune", Opis = "Klasik znanstvene fantastike, malo požutjele stranice.", Cijena = 10.00m, DatumObjave = new DateTime(2025, 2, 1), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Prihvatljivo, KorisnikId = 1, GradId = 1 },
            new Oglas { Id = 4, Naslov = "Ticket to Ride: Europa", Opis = "Popularna strategijska igra, igrana samo 3 puta.", Cijena = 55.00m, DatumObjave = new DateTime(2025, 1, 20), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 2, GradId = 2 },
            new Oglas { Id = 5, Naslov = "1984", Opis = "George Orwell, distopijski roman, dobro stanje.", Cijena = 8.00m, DatumObjave = new DateTime(2025, 2, 5), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 2, GradId = 2 },
            new Oglas { Id = 6, Naslov = "Wingspan", Opis = "Igra o pticama, kompletna s ekspanzijom.", Cijena = 70.00m, DatumObjave = new DateTime(2025, 2, 10), DatumIzmjene = new DateTime(2025, 2, 15), Status = StatusOglasa.Neaktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 2, GradId = 2 },
            new Oglas { Id = 7, Naslov = "Ubojstvo u Orijent Expressu", Opis = "Agatha Christie, kao novo.", Cijena = 12.00m, DatumObjave = new DateTime(2025, 3, 1), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 3, GradId = 3 },
            new Oglas { Id = 8, Naslov = "Dixit", Opis = "Kreativna igra asocijacija, perfektno stanje.", Cijena = 30.00m, DatumObjave = new DateTime(2025, 3, 5), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Novo, KorisnikId = 3, GradId = 3 },
            new Oglas { Id = 9, Naslov = "Gospodar prstenova: Prstenova družina", Opis = "Tolkien klasik, tvrdi uvez.", Cijena = 20.00m, DatumObjave = new DateTime(2025, 3, 10), Status = StatusOglasa.Prodan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 3, GradId = 3 },
            new Oglas { Id = 10, Naslov = "Pandemic - igra suradnje", Opis = "Kooperativna igra za cijelu obitelj, komplet u odličnom stanju.", Cijena = 40.00m, DatumObjave = new DateTime(2025, 3, 15), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 4, GradId = 4 },
            new Oglas { Id = 11, Naslov = "Igra prijestolja", Opis = "George R.R. Martin, eng. izdanje, tvrdi uvez.", Cijena = 18.00m, DatumObjave = new DateTime(2025, 3, 18), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 4, GradId = 4 },
            new Oglas { Id = 12, Naslov = "Da Vincijev kod", Opis = "Dan Brown, uzbudljiv triler, čitana jednom.", Cijena = 14.00m, DatumObjave = new DateTime(2025, 3, 20), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 5, GradId = 5 },
            new Oglas { Id = 13, Naslov = "Fondacija", Opis = "Isaac Asimov, klasik znanstvene fantastike, meki uvez.", Cijena = 11.00m, DatumObjave = new DateTime(2025, 3, 22), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 5, GradId = 5 },
            new Oglas { Id = 14, Naslov = "Mali princ", Opis = "Saint-Exupéry, hrvatsko izdanje, ilustrirano.", Cijena = 9.00m, DatumObjave = new DateTime(2025, 3, 25), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 6, GradId = 6 },
            new Oglas { Id = 15, Naslov = "Pas Baskervillea", Opis = "Sherlock Holmes klasik, eng. izdanje.", Cijena = 10.00m, DatumObjave = new DateTime(2025, 3, 28), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Prihvatljivo, KorisnikId = 6, GradId = 6 },
            new Oglas { Id = 16, Naslov = "7 Wonders", Opis = "Strateška igra za 2-7 igrača, kompletna.", Cijena = 35.00m, DatumObjave = new DateTime(2025, 4, 1), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 7, GradId = 7 },
            new Oglas { Id = 17, Naslov = "Codenames", Opis = "Zabavna igra za veće grupe, sve karte na broju.", Cijena = 22.00m, DatumObjave = new DateTime(2025, 4, 3), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 7, GradId = 7 },
            new Oglas { Id = 18, Naslov = "Azul", Opis = "Prekrasna apstraktna igra, pločice u savršenom stanju.", Cijena = 38.00m, DatumObjave = new DateTime(2025, 4, 5), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Novo, KorisnikId = 8, GradId = 8 },
            new Oglas { Id = 19, Naslov = "Exploding Kittens", Opis = "Brza i smiješna kartaška igra, kompletna.", Cijena = 18.00m, DatumObjave = new DateTime(2025, 4, 8), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 8, GradId = 8 },
            new Oglas { Id = 20, Naslov = "Dobble", Opis = "Igra brzog zapažanja, idealna za djecu i odrasle.", Cijena = 15.00m, DatumObjave = new DateTime(2025, 4, 10), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 9, GradId = 9 }
        );

        modelBuilder.Entity<Knjiga>().HasData(
            new Knjiga { Id = 1, Naziv = "Harry Potter i Kamen mudraca", Autor = "J.K. Rowling", ISBN = "978-0545010221", Izdavac = "Scholastic", GodinaIzdanja = 2007, Jezik = "Engleski", Zanr = ZanrKnjige.Fantastika, OglasId = 1 },
            new Knjiga { Id = 2, Naziv = "Igra prijestolja", Autor = "George R.R. Martin", ISBN = "978-0553103540", Izdavac = "Bantam Books", GodinaIzdanja = 1996, Jezik = "Engleski", Zanr = ZanrKnjige.Fantastika, OglasId = 11 },
            new Knjiga { Id = 3, Naziv = "Ubojstvo u Orijent Expressu", Autor = "Agatha Christie", ISBN = "978-0062073488", Izdavac = "HarperCollins", GodinaIzdanja = 1934, Jezik = "Engleski", Zanr = ZanrKnjige.Krimi, OglasId = 7 },
            new Knjiga { Id = 4, Naziv = "Da Vincijev kod", Autor = "Dan Brown", ISBN = "978-0307474278", Izdavac = "Doubleday", GodinaIzdanja = 2003, Jezik = "Engleski", Zanr = ZanrKnjige.Triler, OglasId = 12 },
            new Knjiga { Id = 5, Naziv = "Ponos i predrasude", Autor = "Jane Austen", ISBN = "978-1503290563", Izdavac = "CreateSpace Independent Publishing Platform", GodinaIzdanja = 1813, Jezik = "Engleski", Zanr = ZanrKnjige.Romansa, OglasId = 5 },
            new Knjiga { Id = 6, Naziv = "Fondacija", Autor = "Isaac Asimov", ISBN = "978-0553293357", Izdavac = "Bantam Books", GodinaIzdanja = 1950, Jezik = "Engleski", Zanr = ZanrKnjige.ZnanstvenaFantastika, OglasId = 13 },
            new Knjiga { Id = 7, Naziv = "Dune", Autor = "Frank Herbert", ISBN = "978-0441013593", Izdavac = "Ace Books", GodinaIzdanja = 1965, Jezik = "Engleski", Zanr = ZanrKnjige.ZnanstvenaFantastika, OglasId = 3 },
            new Knjiga { Id = 8, Naziv = "Mali princ", Autor = "Antoine de Saint-Exupéry", ISBN = "978-0156012195", Izdavac = "Harcourt", GodinaIzdanja = 1943, Jezik = "Hrvatski", Zanr = ZanrKnjige.Drama, OglasId = 14 },
            new Knjiga { Id = 9, Naziv = "Sto godina samoće", Autor = "Gabriel García Márquez", ISBN = "978-0060883287", Izdavac = "Harper & Row", GodinaIzdanja = 1967, Jezik = "Hrvatski", Zanr = ZanrKnjige.Romansa, OglasId = 9 },
            new Knjiga { Id = 10, Naziv = "Pas Baskervillea", Autor = "Arthur Conan Doyle", ISBN = "978-0141034355", Izdavac = "Penguin Classics", GodinaIzdanja = 1902, Jezik = "Engleski", Zanr = ZanrKnjige.Krimi, OglasId = 15 }
        );

        modelBuilder.Entity<DrustvenaIgra>().HasData(
            new DrustvenaIgra { Id = 1, Naziv = "Catan - Osnovna igra", MinBrojIgraca = 3, MaxBrojIgraca = 4, MinimalnasDob = 10, TrajanjeMins = 75, Zanr = ZanrIgre.Strategija, OglasId = 2 },
            new DrustvenaIgra { Id = 2, Naziv = "Ticket to Ride: Europa", MinBrojIgraca = 2, MaxBrojIgraca = 5, MinimalnasDob = 8, TrajanjeMins = 60, Zanr = ZanrIgre.Strategija, OglasId = 4 },
            new DrustvenaIgra { Id = 3, Naziv = "Wingspan", MinBrojIgraca = 1, MaxBrojIgraca = 5, MinimalnasDob = 10, TrajanjeMins = 90, Zanr = ZanrIgre.Strategija, OglasId = 6 },
            new DrustvenaIgra { Id = 4, Naziv = "Dixit", MinBrojIgraca = 3, MaxBrojIgraca = 6, MinimalnasDob = 8, TrajanjeMins = 30, Zanr = ZanrIgre.Zabavna, OglasId = 8 },
            new DrustvenaIgra { Id = 5, Naziv = "Pandemic", MinBrojIgraca = 2, MaxBrojIgraca = 4, MinimalnasDob = 8, TrajanjeMins = 45, Zanr = ZanrIgre.Kooperativna, OglasId = 10 },
            new DrustvenaIgra { Id = 6, Naziv = "7 Wonders", MinBrojIgraca = 2, MaxBrojIgraca = 7, MinimalnasDob = 10, TrajanjeMins = 30, Zanr = ZanrIgre.Strategija, OglasId = 16 },
            new DrustvenaIgra { Id = 7, Naziv = "Codenames", MinBrojIgraca = 2, MaxBrojIgraca = 8, MinimalnasDob = 10, TrajanjeMins = 15, Zanr = ZanrIgre.Zabavna, OglasId = 17 },
            new DrustvenaIgra { Id = 8, Naziv = "Azul", MinBrojIgraca = 2, MaxBrojIgraca = 4, MinimalnasDob = 8, TrajanjeMins = 45, Zanr = ZanrIgre.Apstraktna, OglasId = 18 },
            new DrustvenaIgra { Id = 9, Naziv = "Exploding Kittens", MinBrojIgraca = 2, MaxBrojIgraca = 5, MinimalnasDob = 7, TrajanjeMins = 15, Zanr = ZanrIgre.Zabavna, OglasId = 19 },
            new DrustvenaIgra { Id = 10, Naziv = "Dobble", MinBrojIgraca = 2, MaxBrojIgraca = 8, MinimalnasDob = 6, TrajanjeMins = 10, Zanr = ZanrIgre.Zabavna, OglasId = 20 }
        );

        modelBuilder.Entity<Poruka>().HasData(
            new Poruka { Id = 1, Sadrzaj = "Je li knjiga još dostupna?", Procitano = true, DatumSlanja = new DateTime(2025, 1, 12, 10, 0, 0), PosiljateljId = 2, PrimateljId = 1, OglasId = 1 },
            new Poruka { Id = 2, Sadrzaj = "Da, slobodno mi se javi za preuzimanje!", Procitano = false, DatumSlanja = new DateTime(2025, 1, 12, 14, 30, 0), PosiljateljId = 1, PrimateljId = 2, OglasId = 1 },
            new Poruka { Id = 3, Sadrzaj = "Može li niža cijena za Catan?", Procitano = false, DatumSlanja = new DateTime(2025, 1, 17, 9, 0, 0), PosiljateljId = 3, PrimateljId = 1, OglasId = 2 },
            new Poruka { Id = 4, Sadrzaj = "Koliko dugo igra traje?", Procitano = true, DatumSlanja = new DateTime(2025, 1, 21, 11, 0, 0), PosiljateljId = 3, PrimateljId = 2, OglasId = 4 },
            new Poruka { Id = 5, Sadrzaj = "Oko 60 minuta prosječno.", Procitano = false, DatumSlanja = new DateTime(2025, 1, 21, 18, 0, 0), PosiljateljId = 2, PrimateljId = 3, OglasId = 4 },
            new Poruka { Id = 6, Sadrzaj = "Je li Wingspan kompletna s pravilima?", Procitano = true, DatumSlanja = new DateTime(2025, 2, 11, 9, 0, 0), PosiljateljId = 1, PrimateljId = 2, OglasId = 6 },
            new Poruka { Id = 7, Sadrzaj = "Da, sve je kompletno uključujući ekspanziju.", Procitano = false, DatumSlanja = new DateTime(2025, 2, 11, 13, 0, 0), PosiljateljId = 2, PrimateljId = 1, OglasId = 6 },
            new Poruka { Id = 8, Sadrzaj = "Može li se dogovoriti dostava za Dixit?", Procitano = false, DatumSlanja = new DateTime(2025, 3, 6, 10, 30, 0), PosiljateljId = 1, PrimateljId = 3, OglasId = 8 },
            new Poruka { Id = 9, Sadrzaj = "Imam još jednu kopiju Tolkiena, zanima li te?", Procitano = true, DatumSlanja = new DateTime(2025, 3, 12, 16, 0, 0), PosiljateljId = 3, PrimateljId = 2, OglasId = 9 },
            new Poruka { Id = 10, Sadrzaj = "Hvala, ali već sam nabavio.", Procitano = false, DatumSlanja = new DateTime(2025, 3, 12, 19, 45, 0), PosiljateljId = 2, PrimateljId = 3, OglasId = 9 }
        );
    }    

}