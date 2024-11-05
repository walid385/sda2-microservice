using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxService.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    TTID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaxYear = table.Column<int>(type: "INTEGER", nullable: false),
                    StateTax = table.Column<float>(type: "REAL", nullable: false),
                    CountyTax = table.Column<float>(type: "REAL", nullable: false),
                    CityRate = table.Column<float>(type: "REAL", nullable: false),
                    TaxRates = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.TTID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxRates");
        }
    }
}
