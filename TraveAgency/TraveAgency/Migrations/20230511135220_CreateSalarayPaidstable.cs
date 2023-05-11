using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TraveAgency.Migrations
{
    public partial class CreateSalarayPaidstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalaryPaids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: DateTime.UtcNow.AddHours(4)),
                    KassaId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPaids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryPaids_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalaryPaids_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaryPaids_Kassa_KassaId",
                        column: x => x.KassaId,
                        principalTable: "Kassa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeId",
                table: "Employees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPaids_AppUserId",
                table: "SalaryPaids",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPaids_EmployeeId",
                table: "SalaryPaids",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPaids_KassaId",
                table: "SalaryPaids",
                column: "KassaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_EmployeeId",
                table: "Employees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_EmployeeId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "SalaryPaids");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Employees");
        }
    }
}
