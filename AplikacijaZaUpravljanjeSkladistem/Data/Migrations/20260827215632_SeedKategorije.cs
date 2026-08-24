using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AplikacijaZaUpravljanjeSkladistem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedKategorije : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kategorije",
                columns: new[] { "Id", "Naziv", "Opis" },
                values: new object[,]
                {
                    { 1, "Elektronika", null },
                    { 2, "Alat", null },
                    { 3, "Kancelarijski materijal", null },
                    { 4, "Ostalo", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kategorije",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kategorije",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Kategorije",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Kategorije",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
