using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddTimesForRoundTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContainerNumber",
                table: "Containers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "RTAssignedTime",
                table: "Containers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RTDeliveredTime",
                table: "Containers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RTEnrouteTime",
                table: "Containers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RTGatedInTime",
                table: "Containers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RTGatedOutTime",
                table: "Containers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RTRFCTime",
                table: "Containers",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RTAssignedTime",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "RTDeliveredTime",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "RTEnrouteTime",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "RTGatedInTime",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "RTGatedOutTime",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "RTRFCTime",
                table: "Containers");

            migrationBuilder.AlterColumn<string>(
                name: "ContainerNumber",
                table: "Containers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
