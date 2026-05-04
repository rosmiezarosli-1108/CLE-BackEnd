using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameToForwarding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Companies_ForwarderId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ForwarderRemarks",
                table: "Bookings",
                newName: "ForwardingRemarks");

            migrationBuilder.RenameColumn(
                name: "ForwarderId",
                table: "Bookings",
                newName: "ForwardingId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ForwarderId",
                table: "Bookings",
                newName: "IX_Bookings_ForwardingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Companies_ForwardingId",
                table: "Bookings",
                column: "ForwardingId",
                principalTable: "Companies",
                principalColumn: "CompanyCode",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Companies_ForwardingId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ForwardingRemarks",
                table: "Bookings",
                newName: "ForwarderRemarks");

            migrationBuilder.RenameColumn(
                name: "ForwardingId",
                table: "Bookings",
                newName: "ForwarderId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ForwardingId",
                table: "Bookings",
                newName: "IX_Bookings_ForwarderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Companies_ForwarderId",
                table: "Bookings",
                column: "ForwarderId",
                principalTable: "Companies",
                principalColumn: "CompanyCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
