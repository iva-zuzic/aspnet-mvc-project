using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMarketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddGradToKorisnikProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradId",
                table: "Korisnici",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 1,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 2,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 3,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 4,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 5,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 6,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 7,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 8,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 9,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 10,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 11,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 12,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 13,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 14,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 15,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 16,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 17,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 18,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 19,
                column: "GradId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "Id",
                keyValue: 20,
                column: "GradId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_GradId",
                table: "Korisnici",
                column: "GradId");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnici_Gradovi_GradId",
                table: "Korisnici",
                column: "GradId",
                principalTable: "Gradovi",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnici_Gradovi_GradId",
                table: "Korisnici");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_GradId",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "GradId",
                table: "Korisnici");
        }
    }
}
