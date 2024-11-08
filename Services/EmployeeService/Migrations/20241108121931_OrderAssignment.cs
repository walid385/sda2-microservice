using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Migrations
{
    /// <inheritdoc />
    public partial class OrderAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderAssignments_Employees_EmployeeId1",
                table: "OrderAssignments");

            migrationBuilder.DropIndex(
                name: "IX_OrderAssignments_EmployeeId1",
                table: "OrderAssignments");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "OrderAssignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId1",
                table: "OrderAssignments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderAssignments_EmployeeId1",
                table: "OrderAssignments",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAssignments_Employees_EmployeeId1",
                table: "OrderAssignments",
                column: "EmployeeId1",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
