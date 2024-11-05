using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxService.Migrations
{
    /// <inheritdoc />
    public partial class AddStateToTaxRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "TaxRates",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "TaxRates");
        }
    }
}
