using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    RegisterId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OpenTotal = table.Column<float>(type: "REAL", nullable: false),
                    CloseTotal = table.Column<float>(type: "REAL", nullable: false),
                    OpenEmpId = table.Column<int>(type: "INTEGER", nullable: false),
                    OpenTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CloseTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DropTotal = table.Column<float>(type: "REAL", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.RegisterId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<float>(type: "REAL", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<string>(type: "TEXT", nullable: false),
                    Subtotal = table.Column<float>(type: "REAL", nullable: true),
                    Cost = table.Column<float>(type: "REAL", nullable: true),
                    Discount = table.Column<float>(type: "REAL", nullable: true),
                    Tax = table.Column<float>(type: "REAL", nullable: true),
                    TaxRate = table.Column<float>(type: "REAL", nullable: true),
                    Cash = table.Column<float>(type: "REAL", nullable: true),
                    Credit = table.Column<float>(type: "REAL", nullable: true),
                    CartPurchase = table.Column<bool>(type: "INTEGER", nullable: true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
