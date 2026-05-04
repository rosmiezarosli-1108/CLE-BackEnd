using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddROTNum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BLOrBookingNumber",
                table: "Containers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(12)");

            migrationBuilder.AddColumn<string>(
                name: "DeletedRemarks",
                table: "Containers",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BLOrBookingNumber",
                table: "Bookings",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(12)");

            migrationBuilder.AddColumn<string>(
                name: "ROTNumber",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SCN",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "BLOrBookingNumber",
                table: "BookingDocuments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(12)");

            migrationBuilder.AlterColumn<string>(
                name: "BLOrBookingNumber",
                table: "AssignedHauliers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(12)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedRemarks",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "ROTNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SCN",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "BLOrBookingNumber",
                table: "Containers",
                type: "varchar(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BLOrBookingNumber",
                table: "Bookings",
                type: "varchar(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BLOrBookingNumber",
                table: "BookingDocuments",
                type: "varchar(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BLOrBookingNumber",
                table: "AssignedHauliers",
                type: "varchar(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
