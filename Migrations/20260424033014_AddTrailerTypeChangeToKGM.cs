using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddTrailerTypeChangeToKGM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoyageNumber",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BGM",
                table: "Containers",
                newName: "VGM");

            migrationBuilder.AddColumn<string>(
                name: "TrailerType",
                table: "Containers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrailerType",
                table: "Containers");

            migrationBuilder.RenameColumn(
                name: "VGM",
                table: "Containers",
                newName: "BGM");

            migrationBuilder.AddColumn<string>(
                name: "VoyageNumber",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
