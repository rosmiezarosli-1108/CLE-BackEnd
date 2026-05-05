using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTotalSlotToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"TimeSlots\" ALTER COLUMN \"TotalSlot\" DROP DEFAULT;");
            migrationBuilder.Sql("ALTER TABLE \"TimeSlots\" ALTER COLUMN \"TotalSlot\" TYPE integer USING \"TotalSlot\"::integer;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TotalSlot",
                table: "TimeSlots",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
