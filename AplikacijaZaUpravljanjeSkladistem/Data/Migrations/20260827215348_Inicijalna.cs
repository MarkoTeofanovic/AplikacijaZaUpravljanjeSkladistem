using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplikacijaZaUpravljanjeSkladistem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicijalna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KorisnickoIme = table.Column<string>(type: "TEXT", nullable: false),
                    LozinkaHash = table.Column<string>(type: "TEXT", nullable: false),
                    Uloga = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    Sifra = table.Column<string>(type: "TEXT", nullable: false),
                    Cena = table.Column<decimal>(type: "TEXT", nullable: false),
                    KolicinaNaStanju = table.Column<int>(type: "INTEGER", nullable: false),
                    KategorijaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proizvodi_Kategorije_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nalozi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BrojNaloga = table.Column<string>(type: "TEXT", nullable: false),
                    Datum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    KorisnikId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipNaloga = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Primalac = table.Column<string>(type: "TEXT", nullable: true),
                    Dobavljac = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nalozi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nalozi_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StavkeNaloga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NalogId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProizvodId = table.Column<int>(type: "INTEGER", nullable: false),
                    Kolicina = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeNaloga", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StavkeNaloga_Nalozi_NalogId",
                        column: x => x.NalogId,
                        principalTable: "Nalozi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkeNaloga_Proizvodi_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvodi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nalozi_KorisnikId",
                table: "Nalozi",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodi_KategorijaId",
                table: "Proizvodi",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeNaloga_NalogId",
                table: "StavkeNaloga",
                column: "NalogId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeNaloga_ProizvodId",
                table: "StavkeNaloga",
                column: "ProizvodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StavkeNaloga");

            migrationBuilder.DropTable(
                name: "Nalozi");

            migrationBuilder.DropTable(
                name: "Proizvodi");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Kategorije");
        }
    }
}
