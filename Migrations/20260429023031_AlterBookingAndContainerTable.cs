using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AlterBookingAndContainerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedHauliers_Bookings_BLOrBookingNumber",
                table: "AssignedHauliers");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDocuments_Bookings_BLOrBookingNumber",
                table: "BookingDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Companies_HaulierId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Bookings_BLOrBookingNumber",
                table: "Containers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_HaulierId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "HaulierId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BLOrBookingNumber",
                table: "Containers",
                newName: "ROTNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Containers_BLOrBookingNumber",
                table: "Containers",
                newName: "IX_Containers_ROTNumber");

            migrationBuilder.RenameColumn(
                name: "BLOrBookingNumber",
                table: "AssignedHauliers",
                newName: "ROTNumber");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedHauliers_BLOrBookingNumber",
                table: "AssignedHauliers",
                newName: "IX_AssignedHauliers_ROTNumber");

            migrationBuilder.AddColumn<string>(
                name: "HaulierId",
                table: "Containers",
                type: "varchar(6)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CustomFormNo",
                table: "Bookings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ContainerQuantity",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomReceiptNo",
                table: "Bookings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DICNumber",
                table: "Bookings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseBLNumber",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZBNumber",
                table: "Bookings",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "ROTNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_HaulierId",
                table: "Containers",
                column: "HaulierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedHauliers_Bookings_ROTNumber",
                table: "AssignedHauliers",
                column: "ROTNumber",
                principalTable: "Bookings",
                principalColumn: "ROTNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDocuments_Bookings_BLOrBookingNumber",
                table: "BookingDocuments",
                column: "BLOrBookingNumber",
                principalTable: "Bookings",
                principalColumn: "ROTNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Bookings_ROTNumber",
                table: "Containers",
                column: "ROTNumber",
                principalTable: "Bookings",
                principalColumn: "ROTNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Companies_HaulierId",
                table: "Containers",
                column: "HaulierId",
                principalTable: "Companies",
                principalColumn: "CompanyCode",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedHauliers_Bookings_ROTNumber",
                table: "AssignedHauliers");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDocuments_Bookings_BLOrBookingNumber",
                table: "BookingDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Bookings_ROTNumber",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Companies_HaulierId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_HaulierId",
                table: "Containers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "HaulierId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "ContainerQuantity",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomReceiptNo",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DICNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "HouseBLNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ZBNumber",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ROTNumber",
                table: "Containers",
                newName: "BLOrBookingNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Containers_ROTNumber",
                table: "Containers",
                newName: "IX_Containers_BLOrBookingNumber");

            migrationBuilder.RenameColumn(
                name: "ROTNumber",
                table: "AssignedHauliers",
                newName: "BLOrBookingNumber");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedHauliers_ROTNumber",
                table: "AssignedHauliers",
                newName: "IX_AssignedHauliers_BLOrBookingNumber");

            migrationBuilder.AlterColumn<string>(
                name: "CustomFormNo",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HaulierId",
                table: "Bookings",
                type: "varchar(6)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "BLOrBookingNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HaulierId",
                table: "Bookings",
                column: "HaulierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedHauliers_Bookings_BLOrBookingNumber",
                table: "AssignedHauliers",
                column: "BLOrBookingNumber",
                principalTable: "Bookings",
                principalColumn: "BLOrBookingNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDocuments_Bookings_BLOrBookingNumber",
                table: "BookingDocuments",
                column: "BLOrBookingNumber",
                principalTable: "Bookings",
                principalColumn: "BLOrBookingNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Companies_HaulierId",
                table: "Bookings",
                column: "HaulierId",
                principalTable: "Companies",
                principalColumn: "CompanyCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Bookings_BLOrBookingNumber",
                table: "Containers",
                column: "BLOrBookingNumber",
                principalTable: "Bookings",
                principalColumn: "BLOrBookingNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
