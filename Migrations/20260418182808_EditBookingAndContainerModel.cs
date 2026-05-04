using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class EditBookingAndContainerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ROTDate",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Containers",
                newName: "AssignedTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredTime",
                table: "Containers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrouteTime",
                table: "Containers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RFCTime",
                table: "Containers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ROTDate",
                table: "Containers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveredTime",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "EnrouteTime",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "RFCTime",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "ROTDate",
                table: "Containers");

            migrationBuilder.RenameColumn(
                name: "AssignedTime",
                table: "Containers",
                newName: "TimeStamp");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ROTDate",
                table: "Bookings",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
