using Microsoft.EntityFrameworkCore.Migrations;

namespace TraveAgency.Migrations
{
    public partial class AddIsDeactiveColumnToTourCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "TourCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "TourCategories");
        }
    }
}
