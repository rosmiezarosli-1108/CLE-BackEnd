using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AlterBookingAndContainerColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Companies_FromId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Companies_MTOrLadenId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Companies_ToId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FromId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ToId",
                table: "Containers",
                newName: "ConsigneeId");

            migrationBuilder.RenameColumn(
                name: "MTOrLadenId",
                table: "Containers",
                newName: "PortId");

            migrationBuilder.RenameIndex(
                name: "IX_Containers_ToId",
                table: "Containers",
                newName: "IX_Containers_ConsigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_Containers_MTOrLadenId",
                table: "Containers",
                newName: "IX_Containers_PortId");

            migrationBuilder.AddColumn<string>(
                name: "DepotId",
                table: "Containers",
                type: "varchar(6)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_DepotId",
                table: "Containers",
                column: "DepotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Companies_ConsigneeId",
                table: "Containers",
                column: "ConsigneeId",
                principalTable: "Companies",
                principalColumn: "CompanyCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Companies_DepotId",
                table: "Containers",
                column: "DepotId",
                principalTable: "Companies",
                principalColumn: "CompanyCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Companies_PortId",
                table: "Containers",
                column: "PortId",
                principalTable: "Companies",
                principalColumn: "CompanyCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Companies_ConsigneeId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Companies_DepotId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Companies_PortId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_DepotId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "DepotId",
                table: "Containers");

            migrationBuilder.RenameColumn(
                name: "PortId",
                table: "Containers",
                newName: "MTOrLadenId");

            migrationBuilder.RenameColumn(
                name: "ConsigneeId",
                table: "Containers",
                newName: "ToId");

            migrationBuilder.RenameIndex(
                name: "IX_Containers_PortId",
                table: "Containers",
                newName: "IX_Containers_MTOrLadenId");

            migrationBuilder.RenameIndex(
                name: "IX_Containers_ConsigneeId",
                table: "Containers",
                newName: "IX_Containers_ToId");

            migrationBuilder.AddColumn<string>(
                name: "FromId",
                table: "Bookings",
                type: "varchar(6)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FromId",
                table: "Bookings",
                column: "FromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Companies_FromId",
                table: "Bookings",
                column: "FromId",
                principalTable: "Companies",
                principalColumn: "CompanyCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Companies_MTOrLadenId",
                table: "Containers",
                column: "MTOrLadenId",
                principalTable: "Companies",
                principalColumn: "CompanyCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Companies_ToId",
                table: "Containers",
                column: "ToId",
                principalTable: "Companies",
                principalColumn: "CompanyCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
