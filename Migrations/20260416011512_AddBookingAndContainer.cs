using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingAndContainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Companies_CompanyCode",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BLOrBookingNumber = table.Column<string>(type: "varchar(12)", nullable: false),
                    MovementType = table.Column<string>(type: "text", nullable: false),
                    TripType = table.Column<string>(type: "text", nullable: false),
                    VoyageNumber = table.Column<string>(type: "text", nullable: false),
                    VesselName = table.Column<string>(type: "text", nullable: false),
                    PortLocation = table.Column<string>(type: "text", nullable: false),
                    ETA = table.Column<DateOnly>(type: "date", nullable: false),
                    Commodity = table.Column<string>(type: "text", nullable: true),
                    SpecialHandling = table.Column<string>(type: "text", nullable: true),
                    SealNumber = table.Column<string>(type: "text", nullable: true),
                    ForwarderRemarks = table.Column<string>(type: "text", nullable: true),
                    HaulierRemarks = table.Column<string>(type: "text", nullable: true),
                    DepotRemarks = table.Column<string>(type: "text", nullable: true),
                    FromId = table.Column<string>(type: "varchar(6)", nullable: false),
                    ForwarderId = table.Column<string>(type: "varchar(6)", nullable: false),
                    HaulierId = table.Column<string>(type: "varchar(6)", nullable: false),
                    ROTDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ShippingAgentId = table.Column<string>(type: "varchar(6)", nullable: false),
                    BillingParty = table.Column<string>(type: "text", nullable: false),
                    CustomFormNo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BLOrBookingNumber);
                    table.ForeignKey(
                        name: "FK_Bookings_Companies_ForwarderId",
                        column: x => x.ForwarderId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Companies_FromId",
                        column: x => x.FromId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Companies_HaulierId",
                        column: x => x.HaulierId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Companies_ShippingAgentId",
                        column: x => x.ShippingAgentId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    ContainerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContainerNumber = table.Column<string>(type: "text", nullable: false),
                    ContainerType = table.Column<string>(type: "text", nullable: false),
                    ContainerSize = table.Column<string>(type: "text", nullable: false),
                    BGM = table.Column<string>(type: "text", nullable: true),
                    ToId = table.Column<string>(type: "varchar(6)", nullable: false),
                    MTOrLadenId = table.Column<string>(type: "varchar(6)", nullable: true),
                    BLOrBookingNumber = table.Column<string>(type: "varchar(12)", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    GatedInTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GatedOutTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TimeStatus = table.Column<string>(type: "text", nullable: true),
                    TurnAroundTime = table.Column<int>(type: "integer", nullable: true),
                    DGCRate = table.Column<double>(type: "double precision", nullable: true),
                    DGCReductionEligibility = table.Column<bool>(type: "boolean", nullable: false),
                    DGCReduction = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.ContainerId);
                    table.ForeignKey(
                        name: "FK_Containers_Bookings_BLOrBookingNumber",
                        column: x => x.BLOrBookingNumber,
                        principalTable: "Bookings",
                        principalColumn: "BLOrBookingNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Containers_Companies_MTOrLadenId",
                        column: x => x.MTOrLadenId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode");
                    table.ForeignKey(
                        name: "FK_Containers_Companies_ToId",
                        column: x => x.ToId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignedHauliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DriverName = table.Column<string>(type: "text", nullable: false),
                    PMNumber = table.Column<string>(type: "text", nullable: false),
                    TimeSlot = table.Column<string>(type: "text", nullable: false),
                    ContainerId = table.Column<int>(type: "integer", nullable: false),
                    BLOrBookingNumber = table.Column<string>(type: "varchar(12)", nullable: false),
                    HaulierId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedHauliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedHauliers_Bookings_BLOrBookingNumber",
                        column: x => x.BLOrBookingNumber,
                        principalTable: "Bookings",
                        principalColumn: "BLOrBookingNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedHauliers_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "ContainerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContainerAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "text", nullable: false),
                    ContainerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerAddresses_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "ContainerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedHauliers_BLOrBookingNumber",
                table: "AssignedHauliers",
                column: "BLOrBookingNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedHauliers_ContainerId",
                table: "AssignedHauliers",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ForwarderId",
                table: "Bookings",
                column: "ForwarderId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FromId",
                table: "Bookings",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HaulierId",
                table: "Bookings",
                column: "HaulierId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ShippingAgentId",
                table: "Bookings",
                column: "ShippingAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerAddresses_ContainerId",
                table: "ContainerAddresses",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_BLOrBookingNumber",
                table: "Containers",
                column: "BLOrBookingNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_MTOrLadenId",
                table: "Containers",
                column: "MTOrLadenId");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ToId",
                table: "Containers",
                column: "ToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Companies_CompanyCode",
                table: "Users",
                column: "CompanyCode",
                principalTable: "Companies",
                principalColumn: "CompanyCode",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Companies_CompanyCode",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AssignedHauliers");

            migrationBuilder.DropTable(
                name: "ContainerAddresses");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Companies_CompanyCode",
                table: "Users",
                column: "CompanyCode",
                principalTable: "Companies",
                principalColumn: "CompanyCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
