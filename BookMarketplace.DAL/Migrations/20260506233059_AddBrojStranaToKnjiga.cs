using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMarketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBrojStranaToKnjiga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrojStrana",
                table: "Knjige",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojStrana",
                table: "Knjige");
        }
    }
}
