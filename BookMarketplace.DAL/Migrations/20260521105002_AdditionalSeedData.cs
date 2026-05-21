using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookMarketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Gradovi",
                columns: new[] { "Id", "Naziv", "PostanskiBroj" },
                values: new object[,]
                {
                    { 11, "Karlovac", "47000" },
                    { 12, "Sisak", "44000" },
                    { 13, "Koprivnica", "48000" },
                    { 14, "Bjelovar", "43000" },
                    { 15, "Čakovec", "40000" },
                    { 16, "Vukovar", "32000" },
                    { 17, "Požega", "34000" },
                    { 18, "Virovitica", "33000" },
                    { 19, "Gospić", "53000" },
                    { 20, "Makarska", "21300" },
                    { 21, "Samobor", "10430" },
                    { 22, "Velika Gorica", "10410" },
                    { 23, "Vinkovci", "32100" },
                    { 24, "Đakovo", "31400" },
                    { 25, "Knin", "22300" },
                    { 26, "Trogir", "21220" },
                    { 27, "Opatija", "51410" },
                    { 28, "Rovinj", "52210" },
                    { 29, "Pazin", "52000" },
                    { 30, "Kutina", "44320" }
                });

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "Id", "DatumRegistracije", "DeletedAt", "Email", "ImeIPrezime", "Lozinka", "Telefon", "Uloga" },
                values: new object[,]
                {
                    { 11, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "nikola.nikolic@email.com", "Nikola Nikolić", "lozinka444", "091-111-2233", 0 },
                    { 12, new DateTime(2024, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "katarina.katic@email.com", "Katarina Katić", "lozinka555", "092-222-3344", 0 },
                    { 13, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "filip.filipovic@email.com", "Filip Filipović", "lozinka666", "095-333-4455", 0 },
                    { 14, new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "helena.herak@email.com", "Helena Herak", "lozinka777", "099-444-5566", 0 },
                    { 15, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "dario.daric@email.com", "Dario Darić", "lozinka888", "091-555-6677", 0 },
                    { 16, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "lana.lanic@email.com", "Lana Lanić", "lozinka999", "092-666-7788", 0 },
                    { 17, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "matej.matic@email.com", "Matej Matić", "lozinkaaaa", "095-777-8899", 0 },
                    { 18, new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "sara.saric@email.com", "Sara Sarić", "lozinkabbb", "099-888-9900", 0 },
                    { 19, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "josip.josipovic@email.com", "Josip Josipović", "lozinkaccc", "091-999-0011", 0 },
                    { 20, new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "tea.teovic@email.com", "Tea Teović", "lozinkaddd", "092-000-1122", 0 }
                });

            migrationBuilder.InsertData(
                table: "Oglasi",
                columns: new[] { "Id", "Cijena", "DatumIsteka", "DatumIzmjene", "DatumObjave", "GradId", "KorisnikId", "Naslov", "Opis", "StanjeArtikla", "Status", "TipOglasa" },
                values: new object[,]
                {
                    { 21, 9.00m, null, null, new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 11, "Starac i more", "Hemingway klasik, meki uvez, vrlo dobro stanje.", 2, 0, 0 },
                    { 22, 16.00m, null, null, new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 12, "Zločin i kazna", "Dostojevski, hrvatsko izdanje, tvrdi uvez.", 1, 0, 0 },
                    { 23, 12.00m, null, null, new DateTime(2025, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 13, "Alisa u Zemlji čudesa", "Lewis Carroll, ilustrirano izdanje.", 0, 0, 0 },
                    { 24, 11.00m, null, null, new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, 14, "Proces", "Franz Kafka, klasik moderne književnosti.", 2, 2, 0 },
                    { 25, 25.00m, null, null, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, 15, "Rat i mir", "Tolstoj, opsežno izdanje, dva toma.", 3, 0, 0 },
                    { 26, 18.00m, null, null, new DateTime(2025, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, 16, "Sapiens: Kratka povijest čovječanstva", "Yuval Noah Harari, bestseler, čitana jednom.", 1, 0, 0 },
                    { 27, 8.00m, null, null, new DateTime(2025, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, 17, "Stranac", "Albert Camus, meki uvez, odlično stanje.", 2, 1, 0 },
                    { 28, 14.00m, null, null, new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 18, "Majstor i Margarita", "Bulgakov, hrvatsko izdanje, tvrdi uvez.", 1, 0, 0 },
                    { 29, 13.00m, null, null, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, 19, "Kratka povijest vremena", "Stephen Hawking, popularno-znanstvena knjiga.", 2, 0, 0 },
                    { 30, 10.00m, null, null, new DateTime(2025, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, 20, "Autobiografija jednog jogija", "Paramahansa Yogananda, spiritualna klasika.", 3, 0, 0 },
                    { 31, 12.00m, null, null, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 11, "Saboteur", "Kartaška igra kopanja tunela, kompletna.", 2, 0, 1 },
                    { 32, 28.00m, null, null, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 12, "Carcassonne", "Klasična igra polaganja pločica, sve kompletno.", 1, 0, 1 },
                    { 33, 32.00m, null, null, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 13, "Splendor", "Elegantna igra sakupljanja dragulja.", 0, 2, 1 },
                    { 34, 35.00m, null, null, new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, 14, "Monopoly - Zagreb Edition", "Posebno hrvatsko izdanje, igrano par puta.", 2, 0, 1 },
                    { 35, 8.00m, null, null, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 15, "Uno", "Klasična kartaška igra, kompletna kutija.", 3, 0, 1 },
                    { 36, 20.00m, null, null, new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, 16, "Scrabble - Hrvatski", "Hrvatska verzija s posebnim slovima (č, ć, š, ž, đ).", 1, 0, 1 },
                    { 37, 40.00m, null, null, new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 17, "Risk", "Igra osvajanja svijeta, kompletna s figuricama.", 2, 1, 1 },
                    { 38, 10.00m, null, null, new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 26, 18, "Hanabi", "Kooperativna kartaška igra, savršeno stanje.", 0, 0, 1 },
                    { 39, 18.00m, null, null, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, 19, "Kingdomino", "Brza igra za obitelj, pločice u odličnom stanju.", 1, 0, 1 },
                    { 40, 25.00m, null, null, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, 20, "Trivial Pursuit", "Klasična kviz igra, hrvatsko izdanje.", 2, 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "DrustveneIgre",
                columns: new[] { "Id", "MaxBrojIgraca", "MinBrojIgraca", "MinimalnasDob", "Naziv", "OglasId", "TrajanjeMins", "Zanr" },
                values: new object[,]
                {
                    { 11, 10, 3, 8, "Saboteur", 31, 30, 4 },
                    { 12, 5, 2, 7, "Carcassonne", 32, 45, 0 },
                    { 13, 4, 2, 10, "Splendor", 33, 30, 0 },
                    { 14, 6, 2, 8, "Monopoly - Zagreb Edition", 34, 120, 4 },
                    { 15, 10, 2, 7, "Uno", 35, 20, 4 },
                    { 16, 4, 2, 10, "Scrabble - Hrvatski", 36, 60, 5 },
                    { 17, 6, 2, 10, "Risk", 37, 180, 0 },
                    { 18, 5, 2, 8, "Hanabi", 38, 25, 1 },
                    { 19, 4, 2, 8, "Kingdomino", 39, 20, 0 },
                    { 20, 6, 2, 12, "Trivial Pursuit", 40, 60, 5 }
                });

            migrationBuilder.InsertData(
                table: "Knjige",
                columns: new[] { "Id", "Autor", "BrojStrana", "GodinaIzdanja", "ISBN", "Izdavac", "Jezik", "Naziv", "OglasId", "Zanr" },
                values: new object[,]
                {
                    { 11, "Ernest Hemingway", 127, 1952, "978-0684801223", "Scribner", "Hrvatski", "Starac i more", 21, 5 },
                    { 12, "Fjodor Dostojevski", 671, 1866, "978-0486415871", "Školska knjiga", "Hrvatski", "Zločin i kazna", 22, 5 },
                    { 13, "Lewis Carroll", 176, 1865, "978-0141439761", "Penguin Classics", "Hrvatski", "Alisa u Zemlji čudesa", 23, 0 },
                    { 14, "Franz Kafka", 255, 1925, "978-0805209990", "Schocken Books", "Hrvatski", "Proces", 24, 5 },
                    { 15, "Lav Tolstoj", 1225, 1869, "978-0199232765", "Oxford University Press", "Hrvatski", "Rat i mir", 25, 5 },
                    { 16, "Yuval Noah Harari", 443, 2015, "978-0062316097", "Harper", "Hrvatski", "Sapiens: Kratka povijest čovječanstva", 26, 7 },
                    { 17, "Albert Camus", 123, 1942, "978-0679720201", "Vintage", "Hrvatski", "Stranac", 27, 5 },
                    { 18, "Mihail Bulgakov", 432, 1967, "978-0141180144", "Penguin Classics", "Hrvatski", "Majstor i Margarita", 28, 0 },
                    { 19, "Stephen Hawking", 212, 1988, "978-0553380163", "Bantam Books", "Hrvatski", "Kratka povijest vremena", 29, 7 },
                    { 20, "Paramahansa Yogananda", 498, 1946, "978-0876120798", "Self-Realization Fellowship", "Hrvatski", "Autobiografija jednog jogija", 30, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "DrustveneIgre",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
