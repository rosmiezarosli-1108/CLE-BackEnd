using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddPortCompanyInBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PortLocation",
                table: "Bookings",
                type: "varchar(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PortLocation",
                table: "Bookings",
                column: "PortLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Companies_PortLocation",
                table: "Bookings",
                column: "PortLocation",
                principalTable: "Companies",
                principalColumn: "CompanyCode",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Companies_PortLocation",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PortLocation",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "PortLocation",
                table: "Bookings",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(6)");
        }
    }
}
