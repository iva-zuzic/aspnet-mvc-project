using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostanskiBroj = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeIPrezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRegistracije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uloga = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oglasi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DatumObjave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumIzmjene = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TipOglasa = table.Column<int>(type: "int", nullable: false),
                    StanjeArtikla = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    GradId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglasi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oglasi_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oglasi_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrustveneIgre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinBrojIgraca = table.Column<int>(type: "int", nullable: false),
                    MaxBrojIgraca = table.Column<int>(type: "int", nullable: false),
                    MinimalnasDob = table.Column<int>(type: "int", nullable: false),
                    TrajanjeMins = table.Column<int>(type: "int", nullable: false),
                    Zanr = table.Column<int>(type: "int", nullable: false),
                    OglasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrustveneIgre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrustveneIgre_Oglasi_OglasId",
                        column: x => x.OglasId,
                        principalTable: "Oglasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favoriti",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    OglasId = table.Column<int>(type: "int", nullable: false),
                    DatumDodavanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoriti", x => new { x.KorisnikId, x.OglasId });
                    table.ForeignKey(
                        name: "FK_Favoriti_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favoriti_Oglasi_OglasId",
                        column: x => x.OglasId,
                        principalTable: "Oglasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Knjige",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Izdavac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GodinaIzdanja = table.Column<int>(type: "int", nullable: false),
                    Jezik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zanr = table.Column<int>(type: "int", nullable: false),
                    OglasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knjige", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Knjige_Oglasi_OglasId",
                        column: x => x.OglasId,
                        principalTable: "Oglasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poruke",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Procitano = table.Column<bool>(type: "bit", nullable: false),
                    DatumSlanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PosiljateljId = table.Column<int>(type: "int", nullable: false),
                    PrimateljId = table.Column<int>(type: "int", nullable: false),
                    OglasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poruke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poruke_Korisnici_PosiljateljId",
                        column: x => x.PosiljateljId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Poruke_Korisnici_PrimateljId",
                        column: x => x.PrimateljId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Poruke_Oglasi_OglasId",
                        column: x => x.OglasId,
                        principalTable: "Oglasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slike",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Putanja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RedoslijedPrikaza = table.Column<int>(type: "int", nullable: false),
                    OglasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slike_Oglasi_OglasId",
                        column: x => x.OglasId,
                        principalTable: "Oglasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrustveneIgre_OglasId",
                table: "DrustveneIgre",
                column: "OglasId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favoriti_OglasId",
                table: "Favoriti",
                column: "OglasId");

            migrationBuilder.CreateIndex(
                name: "IX_Knjige_OglasId",
                table: "Knjige",
                column: "OglasId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oglasi_GradId",
                table: "Oglasi",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Oglasi_KorisnikId",
                table: "Oglasi",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Poruke_OglasId",
                table: "Poruke",
                column: "OglasId");

            migrationBuilder.CreateIndex(
                name: "IX_Poruke_PosiljateljId",
                table: "Poruke",
                column: "PosiljateljId");

            migrationBuilder.CreateIndex(
                name: "IX_Poruke_PrimateljId",
                table: "Poruke",
                column: "PrimateljId");

            migrationBuilder.CreateIndex(
                name: "IX_Slike_OglasId",
                table: "Slike",
                column: "OglasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrustveneIgre");

            migrationBuilder.DropTable(
                name: "Favoriti");

            migrationBuilder.DropTable(
                name: "Knjige");

            migrationBuilder.DropTable(
                name: "Poruke");

            migrationBuilder.DropTable(
                name: "Slike");

            migrationBuilder.DropTable(
                name: "Oglasi");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
