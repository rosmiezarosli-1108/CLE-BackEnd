using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyCode = table.Column<string>(type: "varchar(6)", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    SSMNo = table.Column<string>(type: "text", nullable: false),
                    SSTNo = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false),
                    ManagerName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "text", nullable: false),
                    FaxNumber = table.Column<string>(type: "text", nullable: true),
                    PICName = table.Column<string>(type: "text", nullable: false),
                    HandphoneNumber = table.Column<string>(type: "text", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    CCEmailAddress = table.Column<string>(type: "text", nullable: true),
                    CLEKmailNotification = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyCode);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(20)", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    CompanyCode = table.Column<string>(type: "varchar(6)", nullable: false),
                    Access = table.Column<string>(type: "text", nullable: false),
                    AccessLevel = table.Column<string>(type: "text", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    ContactNumber = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyCode",
                        column: x => x.CompanyCode,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyCode", "Address", "CCEmailAddress", "CLEKmailNotification", "CompanyName", "EmailAddress", "FaxNumber", "HandphoneNumber", "ManagerName", "PICName", "Region", "Role", "SSMNo", "SSTNo", "TelephoneNumber" },
                values: new object[,]
                {
                    { "A0001", "Butterworth, 13000, Penang", "finance@gmail.com.my", "operater@gmail.com", "ABC Forwarders", "nesh@gmail.com.my", "03-12345679", "012-3456789", "Pradeep", "Thanesh", "PNG", "Forwarder", "123456-A", "W10-1234-5678", "03-12345678" },
                    { "A0002", "Port Klang, 57000, Penang", "finance@hotmail.com.my", "operater@hotmail.com", "ABC Haulier", "lee@hotmail.com.my", "03-12345679", "012-3456789", "Tristen", "Lee Jia Jun", "PKG", "Haulier", "123456-B", "W11-1234-5678", "03-12345678" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Access", "AccessLevel", "CompanyCode", "CompanyName", "ContactNumber", "EmailAddress", "FullName", "Password", "Status", "UpdatedBy" },
                values: new object[,]
                {
                    { "MNG00001", "CLE & ALE", "Full-Access", "A0001", "ABC Forwarders", "0123456789", "deep@gmail.com", "Pradeep", "123456", "Active", "System" },
                    { "MNG00002", "CLE & ALE", "Full-Access", "A0002", "ABC Haulier", "0123456789", "tristen@hotmail.com", "Tristen", "123456", "Active", "System" },
                    { "STF00001", "CLE", "Half-Access", "A0001", "ABC Forwarders", "0123456789", "nesh@gmail.com", "Thanesh", "123456", "Active", "System" },
                    { "STF0002", "ALE", "Full-Access", "A0002", "ABC Haulier", "0123456789", "vincent@hotmail.com", "Vincent", "123456", "Active", "System" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyCode",
                table: "Users",
                column: "CompanyCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
