using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMarketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddKorisnickoImeToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KorisnickoIme",
                table: "Korisnici",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KorisnickoIme",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 1,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 2,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 3,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 4,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 5,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 6,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 7,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 8,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 9,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 10,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 11,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 12,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 13,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 14,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 15,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 16,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 17,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 18,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 19,
                column: "KorisnickoIme",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 20,
                column: "KorisnickoIme",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KorisnickoIme",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "KorisnickoIme",
                table: "AspNetUsers");
        }
    }
}
