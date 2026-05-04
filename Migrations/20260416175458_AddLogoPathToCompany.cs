using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddLogoPathToCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "Companies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyCode",
                keyValue: "A0001",
                column: "LogoPath",
                value: "");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyCode",
                keyValue: "A0002",
                column: "LogoPath",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "Companies");
        }
    }
}
