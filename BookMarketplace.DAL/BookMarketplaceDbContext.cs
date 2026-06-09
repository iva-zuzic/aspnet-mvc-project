using Microsoft.EntityFrameworkCore;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookMarketplace.DAL;

public class BookMarketplaceDbContext : IdentityDbContext<AppUser>
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

        modelBuilder.Entity<Korisnik>()
            .HasOne(k => k.AppUser)
            .WithOne()
            .HasForeignKey<Korisnik>(k => k.AppUserId)
            .OnDelete(DeleteBehavior.SetNull);

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
            new Grad { Id = 10, Naziv = "Slavonski Brod", PostanskiBroj = "35000" },
            new Grad { Id = 11, Naziv = "Karlovac", PostanskiBroj = "47000" },
            new Grad { Id = 12, Naziv = "Sisak", PostanskiBroj = "44000" },
            new Grad { Id = 13, Naziv = "Koprivnica", PostanskiBroj = "48000" },
            new Grad { Id = 14, Naziv = "Bjelovar", PostanskiBroj = "43000" },
            new Grad { Id = 15, Naziv = "Čakovec", PostanskiBroj = "40000" },
            new Grad { Id = 16, Naziv = "Vukovar", PostanskiBroj = "32000" },
            new Grad { Id = 17, Naziv = "Požega", PostanskiBroj = "34000" },
            new Grad { Id = 18, Naziv = "Virovitica", PostanskiBroj = "33000" },
            new Grad { Id = 19, Naziv = "Gospić", PostanskiBroj = "53000" },
            new Grad { Id = 20, Naziv = "Makarska", PostanskiBroj = "21300" },
            new Grad { Id = 21, Naziv = "Samobor", PostanskiBroj = "10430" },
            new Grad { Id = 22, Naziv = "Velika Gorica", PostanskiBroj = "10410" },
            new Grad { Id = 23, Naziv = "Vinkovci", PostanskiBroj = "32100" },
            new Grad { Id = 24, Naziv = "Đakovo", PostanskiBroj = "31400" },
            new Grad { Id = 25, Naziv = "Knin", PostanskiBroj = "22300" },
            new Grad { Id = 26, Naziv = "Trogir", PostanskiBroj = "21220" },
            new Grad { Id = 27, Naziv = "Opatija", PostanskiBroj = "51410" },
            new Grad { Id = 28, Naziv = "Rovinj", PostanskiBroj = "52210" },
            new Grad { Id = 29, Naziv = "Pazin", PostanskiBroj = "52000" },
            new Grad { Id = 30, Naziv = "Kutina", PostanskiBroj = "44320" }
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
            new Korisnik { Id = 10, ImeIPrezime = "Petra Petrić", Email = "petra.petric@email.com", Lozinka = "lozinka333", Telefon = "092-123-4567", DatumRegistracije = new DateTime(2024, 8, 30), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 11, ImeIPrezime = "Nikola Nikolić", Email = "nikola.nikolic@email.com", Lozinka = "lozinka444", Telefon = "091-111-2233", DatumRegistracije = new DateTime(2024, 9, 5), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 12, ImeIPrezime = "Katarina Katić", Email = "katarina.katic@email.com", Lozinka = "lozinka555", Telefon = "092-222-3344", DatumRegistracije = new DateTime(2024, 9, 18), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 13, ImeIPrezime = "Filip Filipović", Email = "filip.filipovic@email.com", Lozinka = "lozinka666", Telefon = "095-333-4455", DatumRegistracije = new DateTime(2024, 10, 2), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 14, ImeIPrezime = "Helena Herak", Email = "helena.herak@email.com", Lozinka = "lozinka777", Telefon = "099-444-5566", DatumRegistracije = new DateTime(2024, 10, 20), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 15, ImeIPrezime = "Dario Darić", Email = "dario.daric@email.com", Lozinka = "lozinka888", Telefon = "091-555-6677", DatumRegistracije = new DateTime(2024, 11, 1), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 16, ImeIPrezime = "Lana Lanić", Email = "lana.lanic@email.com", Lozinka = "lozinka999", Telefon = "092-666-7788", DatumRegistracije = new DateTime(2024, 11, 15), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 17, ImeIPrezime = "Matej Matić", Email = "matej.matic@email.com", Lozinka = "lozinkaaaa", Telefon = "095-777-8899", DatumRegistracije = new DateTime(2024, 12, 3), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 18, ImeIPrezime = "Sara Sarić", Email = "sara.saric@email.com", Lozinka = "lozinkabbb", Telefon = "099-888-9900", DatumRegistracije = new DateTime(2024, 12, 20), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 19, ImeIPrezime = "Josip Josipović", Email = "josip.josipovic@email.com", Lozinka = "lozinkaccc", Telefon = "091-999-0011", DatumRegistracije = new DateTime(2025, 1, 8), Uloga = UlogaKorisnika.Korisnik },
            new Korisnik { Id = 20, ImeIPrezime = "Tea Teović", Email = "tea.teovic@email.com", Lozinka = "lozinkaddd", Telefon = "092-000-1122", DatumRegistracije = new DateTime(2025, 1, 25), Uloga = UlogaKorisnika.Korisnik }
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
            new Oglas { Id = 20, Naslov = "Dobble", Opis = "Igra brzog zapažanja, idealna za djecu i odrasle.", Cijena = 15.00m, DatumObjave = new DateTime(2025, 4, 10), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 9, GradId = 9 },
            new Oglas { Id = 21, Naslov = "Starac i more", Opis = "Hemingway klasik, meki uvez, vrlo dobro stanje.", Cijena = 9.00m, DatumObjave = new DateTime(2025, 4, 12), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 11, GradId = 11 },
            new Oglas { Id = 22, Naslov = "Zločin i kazna", Opis = "Dostojevski, hrvatsko izdanje, tvrdi uvez.", Cijena = 16.00m, DatumObjave = new DateTime(2025, 4, 14), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 12, GradId = 13 },
            new Oglas { Id = 23, Naslov = "Alisa u Zemlji čudesa", Opis = "Lewis Carroll, ilustrirano izdanje.", Cijena = 12.00m, DatumObjave = new DateTime(2025, 4, 16), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Novo, KorisnikId = 13, GradId = 15 },
            new Oglas { Id = 24, Naslov = "Proces", Opis = "Franz Kafka, klasik moderne književnosti.", Cijena = 11.00m, DatumObjave = new DateTime(2025, 4, 18), Status = StatusOglasa.Prodan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 14, GradId = 17 },
            new Oglas { Id = 25, Naslov = "Rat i mir", Opis = "Tolstoj, opsežno izdanje, dva toma.", Cijena = 25.00m, DatumObjave = new DateTime(2025, 4, 20), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Prihvatljivo, KorisnikId = 15, GradId = 19 },
            new Oglas { Id = 26, Naslov = "Sapiens: Kratka povijest čovječanstva", Opis = "Yuval Noah Harari, bestseler, čitana jednom.", Cijena = 18.00m, DatumObjave = new DateTime(2025, 4, 22), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 16, GradId = 21 },
            new Oglas { Id = 27, Naslov = "Stranac", Opis = "Albert Camus, meki uvez, odlično stanje.", Cijena = 8.00m, DatumObjave = new DateTime(2025, 4, 24), Status = StatusOglasa.Neaktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 17, GradId = 23 },
            new Oglas { Id = 28, Naslov = "Majstor i Margarita", Opis = "Bulgakov, hrvatsko izdanje, tvrdi uvez.", Cijena = 14.00m, DatumObjave = new DateTime(2025, 4, 26), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 18, GradId = 25 },
            new Oglas { Id = 29, Naslov = "Kratka povijest vremena", Opis = "Stephen Hawking, popularno-znanstvena knjiga.", Cijena = 13.00m, DatumObjave = new DateTime(2025, 4, 28), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 19, GradId = 27 },
            new Oglas { Id = 30, Naslov = "Autobiografija jednog jogija", Opis = "Paramahansa Yogananda, spiritualna klasika.", Cijena = 10.00m, DatumObjave = new DateTime(2025, 4, 30), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.Knjiga, StanjeArtikla = StanjeArtikla.Prihvatljivo, KorisnikId = 20, GradId = 29 },
            new Oglas { Id = 31, Naslov = "Saboteur", Opis = "Kartaška igra kopanja tunela, kompletna.", Cijena = 12.00m, DatumObjave = new DateTime(2025, 5, 1), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 11, GradId = 12 },
            new Oglas { Id = 32, Naslov = "Carcassonne", Opis = "Klasična igra polaganja pločica, sve kompletno.", Cijena = 28.00m, DatumObjave = new DateTime(2025, 5, 2), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 12, GradId = 14 },
            new Oglas { Id = 33, Naslov = "Splendor", Opis = "Elegantna igra sakupljanja dragulja.", Cijena = 32.00m, DatumObjave = new DateTime(2025, 5, 3), Status = StatusOglasa.Prodan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Novo, KorisnikId = 13, GradId = 16 },
            new Oglas { Id = 34, Naslov = "Monopoly - Zagreb Edition", Opis = "Posebno hrvatsko izdanje, igrano par puta.", Cijena = 35.00m, DatumObjave = new DateTime(2025, 5, 4), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 14, GradId = 18 },
            new Oglas { Id = 35, Naslov = "Uno", Opis = "Klasična kartaška igra, kompletna kutija.", Cijena = 8.00m, DatumObjave = new DateTime(2025, 5, 5), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Prihvatljivo, KorisnikId = 15, GradId = 20 },
            new Oglas { Id = 36, Naslov = "Scrabble - Hrvatski", Opis = "Hrvatska verzija s posebnim slovima (č, ć, š, ž, đ).", Cijena = 20.00m, DatumObjave = new DateTime(2025, 5, 6), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 16, GradId = 22 },
            new Oglas { Id = 37, Naslov = "Risk", Opis = "Igra osvajanja svijeta, kompletna s figuricama.", Cijena = 40.00m, DatumObjave = new DateTime(2025, 5, 7), Status = StatusOglasa.Neaktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 17, GradId = 24 },
            new Oglas { Id = 38, Naslov = "Hanabi", Opis = "Kooperativna kartaška igra, savršeno stanje.", Cijena = 10.00m, DatumObjave = new DateTime(2025, 5, 8), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Novo, KorisnikId = 18, GradId = 26 },
            new Oglas { Id = 39, Naslov = "Kingdomino", Opis = "Brza igra za obitelj, pločice u odličnom stanju.", Cijena = 18.00m, DatumObjave = new DateTime(2025, 5, 9), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.KaoNovo, KorisnikId = 19, GradId = 28 },
            new Oglas { Id = 40, Naslov = "Trivial Pursuit", Opis = "Klasična kviz igra, hrvatsko izdanje.", Cijena = 25.00m, DatumObjave = new DateTime(2025, 5, 10), Status = StatusOglasa.Aktivan, TipOglasa = TipOglasa.DrustvenaIgra, StanjeArtikla = StanjeArtikla.Dobro, KorisnikId = 20, GradId = 30 }
        );

        modelBuilder.Entity<Knjiga>().HasData(
            new Knjiga { Id = 1, Naziv = "Harry Potter i Kamen mudraca", Autor = "J.K. Rowling", ISBN = "978-0545010221", Izdavac = "Scholastic", GodinaIzdanja = 2007, Jezik = "Engleski", Zanr = ZanrKnjige.Fantastika, OglasId = 1, BrojStrana = 320 },
            new Knjiga { Id = 2, Naziv = "Igra prijestolja", Autor = "George R.R. Martin", ISBN = "978-0553103540", Izdavac = "Bantam Books", GodinaIzdanja = 1996, Jezik = "Engleski", Zanr = ZanrKnjige.Fantastika, OglasId = 11, BrojStrana = 694 },
            new Knjiga { Id = 3, Naziv = "Ubojstvo u Orijent Expressu", Autor = "Agatha Christie", ISBN = "978-0062073488", Izdavac = "HarperCollins", GodinaIzdanja = 1934, Jezik = "Engleski", Zanr = ZanrKnjige.Krimi, OglasId = 7, BrojStrana = 256 },
            new Knjiga { Id = 4, Naziv = "Da Vincijev kod", Autor = "Dan Brown", ISBN = "978-0307474278", Izdavac = "Doubleday", GodinaIzdanja = 2003, Jezik = "Engleski", Zanr = ZanrKnjige.Triler, OglasId = 12, BrojStrana = 489 },
            new Knjiga { Id = 5, Naziv = "Ponos i predrasude", Autor = "Jane Austen", ISBN = "978-1503290563", Izdavac = "CreateSpace Independent Publishing Platform", GodinaIzdanja = 1813, Jezik = "Engleski", Zanr = ZanrKnjige.Romansa, OglasId = 5, BrojStrana = 279 },
            new Knjiga { Id = 6, Naziv = "Fondacija", Autor = "Isaac Asimov", ISBN = "978-0553293357", Izdavac = "Bantam Books", GodinaIzdanja = 1950, Jezik = "Engleski", Zanr = ZanrKnjige.ZnanstvenaFantastika, OglasId = 13, BrojStrana = 255 },
            new Knjiga { Id = 7, Naziv = "Dune", Autor = "Frank Herbert", ISBN = "978-0441013593", Izdavac = "Ace Books", GodinaIzdanja = 1965, Jezik = "Engleski", Zanr = ZanrKnjige.ZnanstvenaFantastika, OglasId = 3, BrojStrana = 412 },
            new Knjiga { Id = 8, Naziv = "Mali princ", Autor = "Antoine de Saint-Exupéry", ISBN = "978-0156012195", Izdavac = "Harcourt", GodinaIzdanja = 1943, Jezik = "Hrvatski", Zanr = ZanrKnjige.Drama, OglasId = 14, BrojStrana = 96 },
            new Knjiga { Id = 9, Naziv = "Sto godina samoće", Autor = "Gabriel García Márquez", ISBN = "978-0060883287", Izdavac = "Harper & Row", GodinaIzdanja = 1967, Jezik = "Hrvatski", Zanr = ZanrKnjige.Romansa, OglasId = 9, BrojStrana = 417 },
            new Knjiga { Id = 10, Naziv = "Pas Baskervillea", Autor = "Arthur Conan Doyle", ISBN = "978-0141034355", Izdavac = "Penguin Classics", GodinaIzdanja = 1902, Jezik = "Engleski", Zanr = ZanrKnjige.Krimi, OglasId = 15, BrojStrana = 256 },
            new Knjiga { Id = 11, Naziv = "Starac i more", Autor = "Ernest Hemingway", ISBN = "978-0684801223", Izdavac = "Scribner", GodinaIzdanja = 1952, Jezik = "Hrvatski", Zanr = ZanrKnjige.Drama, OglasId = 21, BrojStrana = 127 },
            new Knjiga { Id = 12, Naziv = "Zločin i kazna", Autor = "Fjodor Dostojevski", ISBN = "978-0486415871", Izdavac = "Školska knjiga", GodinaIzdanja = 1866, Jezik = "Hrvatski", Zanr = ZanrKnjige.Drama, OglasId = 22, BrojStrana = 671 },
            new Knjiga { Id = 13, Naziv = "Alisa u Zemlji čudesa", Autor = "Lewis Carroll", ISBN = "978-0141439761", Izdavac = "Penguin Classics", GodinaIzdanja = 1865, Jezik = "Hrvatski", Zanr = ZanrKnjige.Fantastika, OglasId = 23, BrojStrana = 176 },
            new Knjiga { Id = 14, Naziv = "Proces", Autor = "Franz Kafka", ISBN = "978-0805209990", Izdavac = "Schocken Books", GodinaIzdanja = 1925, Jezik = "Hrvatski", Zanr = ZanrKnjige.Drama, OglasId = 24, BrojStrana = 255 },
            new Knjiga { Id = 15, Naziv = "Rat i mir", Autor = "Lav Tolstoj", ISBN = "978-0199232765", Izdavac = "Oxford University Press", GodinaIzdanja = 1869, Jezik = "Hrvatski", Zanr = ZanrKnjige.Drama, OglasId = 25, BrojStrana = 1225 },
            new Knjiga { Id = 16, Naziv = "Sapiens: Kratka povijest čovječanstva", Autor = "Yuval Noah Harari", ISBN = "978-0062316097", Izdavac = "Harper", GodinaIzdanja = 2015, Jezik = "Hrvatski", Zanr = ZanrKnjige.Strucna, OglasId = 26, BrojStrana = 443 },
            new Knjiga { Id = 17, Naziv = "Stranac", Autor = "Albert Camus", ISBN = "978-0679720201", Izdavac = "Vintage", GodinaIzdanja = 1942, Jezik = "Hrvatski", Zanr = ZanrKnjige.Drama, OglasId = 27, BrojStrana = 123 },
            new Knjiga { Id = 18, Naziv = "Majstor i Margarita", Autor = "Mihail Bulgakov", ISBN = "978-0141180144", Izdavac = "Penguin Classics", GodinaIzdanja = 1967, Jezik = "Hrvatski", Zanr = ZanrKnjige.Fantastika, OglasId = 28, BrojStrana = 432 },
            new Knjiga { Id = 19, Naziv = "Kratka povijest vremena", Autor = "Stephen Hawking", ISBN = "978-0553380163", Izdavac = "Bantam Books", GodinaIzdanja = 1988, Jezik = "Hrvatski", Zanr = ZanrKnjige.Strucna, OglasId = 29, BrojStrana = 212 },
            new Knjiga { Id = 20, Naziv = "Autobiografija jednog jogija", Autor = "Paramahansa Yogananda", ISBN = "978-0876120798", Izdavac = "Self-Realization Fellowship", GodinaIzdanja = 1946, Jezik = "Hrvatski", Zanr = ZanrKnjige.Biografija, OglasId = 30, BrojStrana = 498 }
        );

        modelBuilder.Entity<DrustvenaIgra>().HasData(
            new DrustvenaIgra { Id = 1, Naziv = "Catan - Osnovna igra", MinBrojIgraca = 3, MaxBrojIgraca = 4, MinimalnaDob = 10, TrajanjeMins = 75, Zanr = ZanrIgre.Strategija, OglasId = 2 },
            new DrustvenaIgra { Id = 2, Naziv = "Ticket to Ride: Europa", MinBrojIgraca = 2, MaxBrojIgraca = 5, MinimalnaDob = 8, TrajanjeMins = 60, Zanr = ZanrIgre.Strategija, OglasId = 4 },
            new DrustvenaIgra { Id = 3, Naziv = "Wingspan", MinBrojIgraca = 1, MaxBrojIgraca = 5, MinimalnaDob = 10, TrajanjeMins = 90, Zanr = ZanrIgre.Strategija, OglasId = 6 },
            new DrustvenaIgra { Id = 4, Naziv = "Dixit", MinBrojIgraca = 3, MaxBrojIgraca = 6, MinimalnaDob = 8, TrajanjeMins = 30, Zanr = ZanrIgre.Zabavna, OglasId = 8 },
            new DrustvenaIgra { Id = 5, Naziv = "Pandemic", MinBrojIgraca = 2, MaxBrojIgraca = 4, MinimalnaDob = 8, TrajanjeMins = 45, Zanr = ZanrIgre.Kooperativna, OglasId = 10 },
            new DrustvenaIgra { Id = 6, Naziv = "7 Wonders", MinBrojIgraca = 2, MaxBrojIgraca = 7, MinimalnaDob = 10, TrajanjeMins = 30, Zanr = ZanrIgre.Strategija, OglasId = 16 },
            new DrustvenaIgra { Id = 7, Naziv = "Codenames", MinBrojIgraca = 2, MaxBrojIgraca = 8, MinimalnaDob = 10, TrajanjeMins = 15, Zanr = ZanrIgre.Zabavna, OglasId = 17 },
            new DrustvenaIgra { Id = 8, Naziv = "Azul", MinBrojIgraca = 2, MaxBrojIgraca = 4, MinimalnaDob = 8, TrajanjeMins = 45, Zanr = ZanrIgre.Apstraktna, OglasId = 18 },
            new DrustvenaIgra { Id = 9, Naziv = "Exploding Kittens", MinBrojIgraca = 2, MaxBrojIgraca = 5, MinimalnaDob = 7, TrajanjeMins = 15, Zanr = ZanrIgre.Zabavna, OglasId = 19 },
            new DrustvenaIgra { Id = 10, Naziv = "Dobble", MinBrojIgraca = 2, MaxBrojIgraca = 8, MinimalnaDob = 6, TrajanjeMins = 10, Zanr = ZanrIgre.Zabavna, OglasId = 20 },
            new DrustvenaIgra { Id = 11, Naziv = "Saboteur", MinBrojIgraca = 3, MaxBrojIgraca = 10, MinimalnaDob = 8, TrajanjeMins = 30, Zanr = ZanrIgre.Zabavna, OglasId = 31 },
            new DrustvenaIgra { Id = 12, Naziv = "Carcassonne", MinBrojIgraca = 2, MaxBrojIgraca = 5, MinimalnaDob = 7, TrajanjeMins = 45, Zanr = ZanrIgre.Strategija, OglasId = 32 },
            new DrustvenaIgra { Id = 13, Naziv = "Splendor", MinBrojIgraca = 2, MaxBrojIgraca = 4, MinimalnaDob = 10, TrajanjeMins = 30, Zanr = ZanrIgre.Strategija, OglasId = 33 },
            new DrustvenaIgra { Id = 14, Naziv = "Monopoly - Zagreb Edition", MinBrojIgraca = 2, MaxBrojIgraca = 6, MinimalnaDob = 8, TrajanjeMins = 120, Zanr = ZanrIgre.Zabavna, OglasId = 34 },
            new DrustvenaIgra { Id = 15, Naziv = "Uno", MinBrojIgraca = 2, MaxBrojIgraca = 10, MinimalnaDob = 7, TrajanjeMins = 20, Zanr = ZanrIgre.Zabavna, OglasId = 35 },
            new DrustvenaIgra { Id = 16, Naziv = "Scrabble - Hrvatski", MinBrojIgraca = 2, MaxBrojIgraca = 4, MinimalnaDob = 10, TrajanjeMins = 60, Zanr = ZanrIgre.Edukativna, OglasId = 36 },
            new DrustvenaIgra { Id = 17, Naziv = "Risk", MinBrojIgraca = 2, MaxBrojIgraca = 6, MinimalnaDob = 10, TrajanjeMins = 180, Zanr = ZanrIgre.Strategija, OglasId = 37 },
            new DrustvenaIgra { Id = 18, Naziv = "Hanabi", MinBrojIgraca = 2, MaxBrojIgraca = 5, MinimalnaDob = 8, TrajanjeMins = 25, Zanr = ZanrIgre.Kooperativna, OglasId = 38 },
            new DrustvenaIgra { Id = 19, Naziv = "Kingdomino", MinBrojIgraca = 2, MaxBrojIgraca = 4, MinimalnaDob = 8, TrajanjeMins = 20, Zanr = ZanrIgre.Strategija, OglasId = 39 },
            new DrustvenaIgra { Id = 20, Naziv = "Trivial Pursuit", MinBrojIgraca = 2, MaxBrojIgraca = 6, MinimalnaDob = 12, TrajanjeMins = 60, Zanr = ZanrIgre.Edukativna, OglasId = 40 }
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

        modelBuilder.Entity<Korisnik>().HasQueryFilter(k => k.DeletedAt == null);
        modelBuilder.Entity<Oglas>().HasQueryFilter(o => o.Status != StatusOglasa.Izbrisan);
    }    

}