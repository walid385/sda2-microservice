using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagementService.Migrations
{
    /// <inheritdoc />
    public partial class Floats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "TotalAmount",
                table: "Orders",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");
        }
    }
}
