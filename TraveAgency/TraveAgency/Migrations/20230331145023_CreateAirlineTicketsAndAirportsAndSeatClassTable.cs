using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TraveAgency.Migrations
{
    public partial class CreateAirlineTicketsAndAirportsAndSeatClassTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirlineTicketDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsTransfer = table.Column<bool>(type: "bit", nullable: false),
                    TransferTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlightDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    TransferPrice = table.Column<int>(type: "int", nullable: true),
                    IsReturn = table.Column<bool>(type: "bit", nullable: false),
                    ReturnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnPrice = table.Column<int>(type: "int", nullable: true),
                    HassBaggage = table.Column<bool>(type: "bit", nullable: false),
                    BaggageAllowance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaggagePrice = table.Column<int>(type: "int", nullable: true),
                    Handluggage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HassMealService = table.Column<bool>(type: "bit", nullable: false),
                    MealDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MealPrice = table.Column<int>(type: "int", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineTicketDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeatClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatPrice = table.Column<int>(type: "int", nullable: true),
                    Info = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirlineTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureAirportId = table.Column<int>(type: "int", nullable: false),
                    ArrivalAirportId = table.Column<int>(type: "int", nullable: false),
                    TransferAirportId = table.Column<int>(type: "int", nullable: true),
                    ReturnAirportId = table.Column<int>(type: "int", nullable: true),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightDuration = table.Column<TimeSpan>(type: "time", nullable: true),
                    TicketPrice = table.Column<int>(type: "int", nullable: true),
                    AirlineTicketDetailId = table.Column<int>(type: "int", nullable: true),
                    SeatClassId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirlineTickets_AirlineTicketDetails_AirlineTicketDetailId",
                        column: x => x.AirlineTicketDetailId,
                        principalTable: "AirlineTicketDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                   
                    table.ForeignKey(
                        name: "FK_AirlineTickets_Airports_ArrivalAirportId",
                        column: x => x.ArrivalAirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineTickets_Airports_DepartureAirportId",
                        column: x => x.DepartureAirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineTickets_Airports_ReturnAirportId",
                        column: x => x.ReturnAirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineTickets_Airports_TransferAirportId",
                        column: x => x.TransferAirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineTickets_SeatClasses_SeatClassId",
                        column: x => x.SeatClassId,
                        principalTable: "SeatClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_AirlineTicketDetailId",
                table: "AirlineTickets",
                column: "AirlineTicketDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_ArrivalAirportId",
                table: "AirlineTickets",
                column: "ArrivalAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_DepartureAirportId",
                table: "AirlineTickets",
                column: "DepartureAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_ReturnAirportId",
                table: "AirlineTickets",
                column: "ReturnAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_SeatClassId",
                table: "AirlineTickets",
                column: "SeatClassId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_TransferAirportId",
                table: "AirlineTickets",
                column: "TransferAirportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirlineTickets");

            migrationBuilder.DropTable(
                name: "AirlineTicketDetails");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "SeatClasses");
        }
    }
}
