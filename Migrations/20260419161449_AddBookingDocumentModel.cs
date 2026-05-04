using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingDocumentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingDocuments",
                columns: table => new
                {
                    BookingDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BLOrBookingNumber = table.Column<string>(type: "varchar(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDocuments", x => x.BookingDocumentId);
                    table.ForeignKey(
                        name: "FK_BookingDocuments_Bookings_BLOrBookingNumber",
                        column: x => x.BLOrBookingNumber,
                        principalTable: "Bookings",
                        principalColumn: "BLOrBookingNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDocuments_BLOrBookingNumber",
                table: "BookingDocuments",
                column: "BLOrBookingNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDocuments");
        }
    }
}
