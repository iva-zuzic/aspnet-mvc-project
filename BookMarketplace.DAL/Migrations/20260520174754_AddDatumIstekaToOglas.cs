using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMarketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDatumIstekaToOglas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatumIsteka",
                table: "Oglasi",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 1,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 2,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 3,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 4,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 5,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 6,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 7,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 8,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 9,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 10,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 11,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 12,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 13,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 14,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 15,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 16,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 17,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 18,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 19,
                column: "DatumIsteka",
                value: null);

            migrationBuilder.UpdateData(
                table: "Oglasi",
                keyColumn: "Id",
                keyValue: 20,
                column: "DatumIsteka",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumIsteka",
                table: "Oglasi");
        }
    }
}
