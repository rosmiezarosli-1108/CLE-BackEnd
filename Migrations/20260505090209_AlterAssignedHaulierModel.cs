using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AlterAssignedHaulierModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "AssignedHauliers");

            migrationBuilder.DropColumn(
                name: "PMNumber",
                table: "AssignedHauliers");

            migrationBuilder.DropColumn(
                name: "TimeSlot",
                table: "AssignedHauliers");

            migrationBuilder.AddColumn<Guid>(
                name: "DriverId",
                table: "AssignedHauliers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PMId",
                table: "AssignedHauliers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TimeSlotId",
                table: "AssignedHauliers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TrailerId",
                table: "AssignedHauliers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignedHauliers_DriverId",
                table: "AssignedHauliers",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedHauliers_PMId",
                table: "AssignedHauliers",
                column: "PMId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedHauliers_TimeSlotId",
                table: "AssignedHauliers",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedHauliers_TrailerId",
                table: "AssignedHauliers",
                column: "TrailerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedHauliers_Drivers_DriverId",
                table: "AssignedHauliers",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedHauliers_PrimeMovers_PMId",
                table: "AssignedHauliers",
                column: "PMId",
                principalTable: "PrimeMovers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedHauliers_TimeSlots_TimeSlotId",
                table: "AssignedHauliers",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedHauliers_Trailers_TrailerId",
                table: "AssignedHauliers",
                column: "TrailerId",
                principalTable: "Trailers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedHauliers_Drivers_DriverId",
                table: "AssignedHauliers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedHauliers_PrimeMovers_PMId",
                table: "AssignedHauliers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedHauliers_TimeSlots_TimeSlotId",
                table: "AssignedHauliers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedHauliers_Trailers_TrailerId",
                table: "AssignedHauliers");

            migrationBuilder.DropIndex(
                name: "IX_AssignedHauliers_DriverId",
                table: "AssignedHauliers");

            migrationBuilder.DropIndex(
                name: "IX_AssignedHauliers_PMId",
                table: "AssignedHauliers");

            migrationBuilder.DropIndex(
                name: "IX_AssignedHauliers_TimeSlotId",
                table: "AssignedHauliers");

            migrationBuilder.DropIndex(
                name: "IX_AssignedHauliers_TrailerId",
                table: "AssignedHauliers");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "AssignedHauliers");

            migrationBuilder.DropColumn(
                name: "PMId",
                table: "AssignedHauliers");

            migrationBuilder.DropColumn(
                name: "TimeSlotId",
                table: "AssignedHauliers");

            migrationBuilder.DropColumn(
                name: "TrailerId",
                table: "AssignedHauliers");

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "AssignedHauliers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PMNumber",
                table: "AssignedHauliers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TimeSlot",
                table: "AssignedHauliers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
