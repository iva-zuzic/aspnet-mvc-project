using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMarketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedAtToKorisnik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Korisnici",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 1,
                column: "BrojStrana",
                value: 320);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 2,
                column: "BrojStrana",
                value: 694);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 3,
                column: "BrojStrana",
                value: 256);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 4,
                column: "BrojStrana",
                value: 489);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 5,
                column: "BrojStrana",
                value: 279);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 6,
                column: "BrojStrana",
                value: 255);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 7,
                column: "BrojStrana",
                value: 412);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 8,
                column: "BrojStrana",
                value: 96);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 9,
                column: "BrojStrana",
                value: 417);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 10,
                column: "BrojStrana",
                value: 256);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 2,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 3,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 4,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 5,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 6,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 7,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 8,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 9,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 10,
                column: "DeletedAt",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Korisnici");

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 1,
                column: "BrojStrana",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 2,
                column: "BrojStrana",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 3,
                column: "BrojStrana",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 4,
                column: "BrojStrana",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 5,
                column: "BrojStrana",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 6,
                column: "BrojStrana",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 7,
                column: "BrojStrana",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 8,
                column: "BrojStrana",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 9,
                column: "BrojStrana",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Knjige",
                keyColumn: "Id",
                keyValue: 10,
                column: "BrojStrana",
                value: 0);
        }
    }
}
