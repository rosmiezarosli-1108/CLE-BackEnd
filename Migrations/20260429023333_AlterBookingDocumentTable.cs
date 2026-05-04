using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AlterBookingDocumentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDocuments_Bookings_BLOrBookingNumber",
                table: "BookingDocuments");

            migrationBuilder.RenameColumn(
                name: "BLOrBookingNumber",
                table: "BookingDocuments",
                newName: "ROTNumber");

            migrationBuilder.RenameIndex(
                name: "IX_BookingDocuments_BLOrBookingNumber",
                table: "BookingDocuments",
                newName: "IX_BookingDocuments_ROTNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDocuments_Bookings_ROTNumber",
                table: "BookingDocuments",
                column: "ROTNumber",
                principalTable: "Bookings",
                principalColumn: "ROTNumber",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDocuments_Bookings_ROTNumber",
                table: "BookingDocuments");

            migrationBuilder.RenameColumn(
                name: "ROTNumber",
                table: "BookingDocuments",
                newName: "BLOrBookingNumber");

            migrationBuilder.RenameIndex(
                name: "IX_BookingDocuments_ROTNumber",
                table: "BookingDocuments",
                newName: "IX_BookingDocuments_BLOrBookingNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDocuments_Bookings_BLOrBookingNumber",
                table: "BookingDocuments",
                column: "BLOrBookingNumber",
                principalTable: "Bookings",
                principalColumn: "ROTNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
