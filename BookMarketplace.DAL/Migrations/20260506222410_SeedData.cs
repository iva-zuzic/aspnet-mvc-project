using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookMarketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Gradovi",
                columns: new[] { "Id", "Naziv", "PostanskiBroj" },
                values: new object[,]
                {
                    { 1, "Zagreb", "10000" },
                    { 2, "Split", "21000" },
                    { 3, "Rijeka", "51000" },
                    { 4, "Osijek", "31000" },
                    { 5, "Zadar", "23000" },
                    { 6, "Pula", "52100" },
                    { 7, "Dubrovnik", "20000" },
                    { 8, "Varaždin", "42000" },
                    { 9, "Šibenik", "22000" },
                    { 10, "Slavonski Brod", "35000" }
                });

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "Id", "DatumRegistracije", "Email", "ImeIPrezime", "Lozinka", "Telefon", "Uloga" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ana.anic@email.com", "Ana Anić", "lozinka123", "091-234-5678", 0 },
                    { 2, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "pero.peric@email.com", "Pero Perić", "lozinka456", "092-345-6789", 0 },
                    { 3, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "maja.majic@email.com", "Maja Majić", "lozinka789", "095-456-7890", 0 },
                    { 4, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ivan.ivic@email.com", "Ivan Ivić", "lozinka321", "093-567-8901", 0 },
                    { 5, new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "marko.markovic@email.com", "Marko Marković", "lozinka654", "091-678-9012", 0 },
                    { 6, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ivana.horvat@email.com", "Ivana Horvat", "lozinka987", "092-789-0123", 0 },
                    { 7, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "tomislav.tomic@email.com", "Tomislav Tomić", "admin001", "095-890-1234", 1 },
                    { 8, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "lucija.lukic@email.com", "Lucija Lukić", "lozinka111", "099-901-2345", 0 },
                    { 9, new DateTime(2024, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "stjepan.stjepic@email.com", "Stjepan Stjepić", "lozinka222", "091-012-3456", 0 },
                    { 10, new DateTime(2024, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "petra.petric@email.com", "Petra Petrić", "lozinka333", "092-123-4567", 0 }
                });

            migrationBuilder.InsertData(
                table: "Oglasi",
                columns: new[] { "Id", "Cijena", "DatumIzmjene", "DatumObjave", "GradId", "KorisnikId", "Naslov", "Opis", "StanjeArtikla", "Status", "TipOglasa" },
                values: new object[,]
                {
                    { 1, 15.00m, null, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Harry Potter i Kamen mudraca", "Knjiga u odličnom stanju, čitana jednom.", 1, 0, 0 },
                    { 2, 45.00m, null, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Catan - Osnovna igra", "Kompletna igra, sve kartice i figurice na broju.", 2, 0, 1 },
                    { 3, 10.00m, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Dune", "Klasik znanstvene fantastike, malo požutjele stranice.", 3, 0, 0 },
                    { 4, 55.00m, null, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Ticket to Ride: Europa", "Popularna strategijska igra, igrana samo 3 puta.", 1, 0, 1 },
                    { 5, 8.00m, null, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "1984", "George Orwell, distopijski roman, dobro stanje.", 2, 0, 0 },
                    { 6, 70.00m, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Wingspan", "Igra o pticama, kompletna s ekspanzijom.", 1, 1, 1 },
                    { 7, 12.00m, null, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "Ubojstvo u Orijent Expressu", "Agatha Christie, kao novo.", 1, 0, 0 },
                    { 8, 30.00m, null, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "Dixit", "Kreativna igra asocijacija, perfektno stanje.", 0, 0, 1 },
                    { 9, 20.00m, null, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "Gospodar prstenova: Prstenova družina", "Tolkien klasik, tvrdi uvez.", 2, 2, 0 },
                    { 10, 40.00m, null, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, "Pandemic - igra suradnje", "Kooperativna igra za cijelu obitelj, komplet u odličnom stanju.", 1, 0, 1 },
                    { 11, 18.00m, null, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, "Igra prijestolja", "George R.R. Martin, eng. izdanje, tvrdi uvez.", 2, 0, 0 },
                    { 12, 14.00m, null, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, "Da Vincijev kod", "Dan Brown, uzbudljiv triler, čitana jednom.", 1, 0, 0 },
                    { 13, 11.00m, null, new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, "Fondacija", "Isaac Asimov, klasik znanstvene fantastike, meki uvez.", 2, 0, 0 },
                    { 14, 9.00m, null, new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 6, "Mali princ", "Saint-Exupéry, hrvatsko izdanje, ilustrirano.", 1, 0, 0 },
                    { 15, 10.00m, null, new DateTime(2025, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 6, "Pas Baskervillea", "Sherlock Holmes klasik, eng. izdanje.", 3, 0, 0 },
                    { 16, 35.00m, null, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 7, "7 Wonders", "Strateška igra za 2-7 igrača, kompletna.", 2, 0, 1 },
                    { 17, 22.00m, null, new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 7, "Codenames", "Zabavna igra za veće grupe, sve karte na broju.", 1, 0, 1 },
                    { 18, 38.00m, null, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 8, "Azul", "Prekrasna apstraktna igra, pločice u savršenom stanju.", 0, 0, 1 },
                    { 19, 18.00m, null, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 8, "Exploding Kittens", "Brza i smiješna kartaška igra, kompletna.", 2, 0, 1 },
                    { 20, 15.00m, null, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 9, "Dobble", "Igra brzog zapažanja, idealna za djecu i odrasle.", 1, 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "DrustveneIgre",
                columns: new[] { "Id", "MaxBrojIgraca", "MinBrojIgraca", "MinimalnasDob", "Naziv", "OglasId", "TrajanjeMins", "Zanr" },
                values: new object[,]
                {
                    { 1, 4, 3, 10, "Catan - Osnovna igra", 2, 75, 0 },
                    { 2, 5, 2, 8, "Ticket to Ride: Europa", 4, 60, 0 },
                    { 3, 5, 1, 10, "Wingspan", 6, 90, 0 },
                    { 4, 6, 3, 8, "Dixit", 8, 30, 4 },
                    { 5, 4, 2, 8, "Pandemic", 10, 45, 1 },
                    { 6, 7, 2, 10, "7 Wonders", 16, 30, 0 },
                    { 7, 8, 2, 10, "Codenames", 17, 15, 4 },
                    { 8, 4, 2, 8, "Azul", 18, 45, 3 },
                    { 9, 5, 2, 7, "Exploding Kittens", 19, 15, 4 },
                    { 10, 8, 2, 6, "Dobble", 20, 10, 4 }
                });

            migrationBuilder.InsertData(
                table: "Knjige",
                columns: new[] { "Id", "Autor", "GodinaIzdanja", "ISBN", "Izdavac", "Jezik", "Naziv", "OglasId", "Zanr" },
                values: new object[,]
                {
                    { 1, "J.K. Rowling", 2007, "978-0545010221", "Scholastic", "Engleski", "Harry Potter i Kamen mudraca", 1, 0 },
                    { 2, "George R.R. Martin", 1996, "978-0553103540", "Bantam Books", "Engleski", "Igra prijestolja", 11, 0 },
                    { 3, "Agatha Christie", 1934, "978-0062073488", "HarperCollins", "Engleski", "Ubojstvo u Orijent Expressu", 7, 4 },
                    { 4, "Dan Brown", 2003, "978-0307474278", "Doubleday", "Engleski", "Da Vincijev kod", 12, 1 },
                    { 5, "Jane Austen", 1813, "978-1503290563", "CreateSpace Independent Publishing Platform", "Engleski", "Ponos i predrasude", 5, 2 },
                    { 6, "Isaac Asimov", 1950, "978-0553293357", "Bantam Books", "Engleski", "Fondacija", 13, 3 },
                    { 7, "Frank Herbert", 1965, "978-0441013593", "Ace Books", "Engleski", "Dune", 3, 3 },
                    { 8, "Antoine de Saint-Exupéry", 1943, "978-0156012195", "Harcourt", "Hrvatski", "Mali princ", 14, 5 },
                    { 9, "Gabriel García Márquez", 1967, "978-0060883287", "Harper & Row", "Hrvatski", "Sto godina samoće", 9, 2 },
                    { 10, "Arthur Conan Doyle", 1902, "978-0141034355", "Penguin Classics", "Engleski", "Pas Baskervillea", 15, 4 }
                });

            migrationBuilder.InsertData(
                table: "Poruke",
                columns: new[] { "Id", "DatumSlanja", "OglasId", "PosiljateljId", "PrimateljId", "Procitano", "Sadrzaj" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 1, true, "Je li knjiga još dostupna?" },
                    { 2, new DateTime(2025, 1, 12, 14, 30, 0, 0, DateTimeKind.Unspecified), 1, 1, 2, false, "Da, slobodno mi se javi za preuzimanje!" },
                    { 3, new DateTime(2025, 1, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 1, false, "Može li niža cijena za Catan?" },
                    { 4, new DateTime(2025, 1, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), 4, 3, 2, true, "Koliko dugo igra traje?" },
                    { 5, new DateTime(2025, 1, 21, 18, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, 3, false, "Oko 60 minuta prosječno." },
                    { 6, new DateTime(2025, 2, 11, 9, 0, 0, 0, DateTimeKind.Unspecified), 6, 1, 2, true, "Je li Wingspan kompletna s pravilima?" },
                    { 7, new DateTime(2025, 2, 11, 13, 0, 0, 0, DateTimeKind.Unspecified), 6, 2, 1, false, "Da, sve je kompletno uključujući ekspanziju." },
                    { 8, new DateTime(2025, 3, 6, 10, 30, 0, 0, DateTimeKind.Unspecified), 8, 1, 3, false, "Može li se dogovoriti dostava za Dixit?" },
                    { 9, new DateTime(2025, 3, 12, 16, 0, 0, 0, DateTimeKind.Unspecified), 9, 3, 2, true, "Imam još jednu kopiju Tolkiena, zanima li te?" },
                    { 10, new DateTime(2025, 3, 12, 19, 45, 0, 0, DateTimeKind.Unspecified), 9, 2, 3, false, "Hvala, ali već sam nabavio." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Poruke",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Poruke",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Poruke",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Poruke",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Poruke",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Poruke",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Poruke",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Poruke",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Poruke",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Poruke",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
