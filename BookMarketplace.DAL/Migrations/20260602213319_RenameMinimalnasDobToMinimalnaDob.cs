using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMarketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenameMinimalnasDobToMinimalnaDob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimalnasDob",
                table: "DrustveneIgre",
                newName: "MinimalnaDob");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimalnaDob",
                table: "DrustveneIgre",
                newName: "MinimalnasDob");
        }
    }
}
