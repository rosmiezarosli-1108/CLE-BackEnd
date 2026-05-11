using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CLE_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class CreateALETables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PickUpTotalSlot",
                table: "TimeSlots",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DropOffTotalSlot",
                table: "TimeSlots",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedTime",
                table: "Containers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RTAcceptedTime",
                table: "Containers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceivedBy",
                table: "Containers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectedRemarks",
                table: "Containers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AleBookings",
                columns: table => new
                {
                    ROTNumber = table.Column<string>(type: "text", nullable: false),
                    AWBNumber = table.Column<string>(type: "text", nullable: false),
                    HouseAWBNumber = table.Column<string>(type: "text", nullable: false),
                    MovementType = table.Column<string>(type: "text", nullable: false),
                    SCN = table.Column<string>(type: "text", nullable: false),
                    TripType = table.Column<string>(type: "text", nullable: true),
                    TerminalLocation = table.Column<string>(type: "varchar(6)", nullable: false),
                    ETA = table.Column<DateOnly>(type: "date", nullable: false),
                    SealNumber = table.Column<string>(type: "text", nullable: true),
                    ForwardingRemarks = table.Column<string>(type: "text", nullable: true),
                    HaulierRemarks = table.Column<string>(type: "text", nullable: true),
                    TerminalRemarks = table.Column<string>(type: "text", nullable: true),
                    ForwardingId = table.Column<string>(type: "varchar(6)", nullable: false),
                    ShippingAgentId = table.Column<string>(type: "varchar(6)", nullable: false),
                    BillingParty = table.Column<string>(type: "text", nullable: false),
                    CustomFormNo = table.Column<string>(type: "text", nullable: true),
                    CustomReceiptNo = table.Column<string>(type: "text", nullable: true),
                    DICNumber = table.Column<string>(type: "text", nullable: true),
                    ZBNumber = table.Column<string>(type: "text", nullable: true),
                    ContainerQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AleBookings", x => x.ROTNumber);
                    table.ForeignKey(
                        name: "FK_AleBookings_Companies_ForwardingId",
                        column: x => x.ForwardingId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AleBookings_Companies_ShippingAgentId",
                        column: x => x.ShippingAgentId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AleBookings_Companies_TerminalLocation",
                        column: x => x.TerminalLocation,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AleTimeSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<string>(type: "text", nullable: false),
                    PickUpTotalSlot = table.Column<int>(type: "integer", nullable: true),
                    DropOffTotalSlot = table.Column<int>(type: "integer", nullable: true),
                    TerminalId = table.Column<string>(type: "varchar(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AleTimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AleTimeSlots_Companies_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContainerAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContainerId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: true),
                    Changes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerAudits", x => x.AuditId);
                    table.ForeignKey(
                        name: "FK_ContainerAudits_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "ContainerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AleBookingDocuments",
                columns: table => new
                {
                    BookingDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ROTNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AleBookingDocuments", x => x.BookingDocumentId);
                    table.ForeignKey(
                        name: "FK_AleBookingDocuments_AleBookings_ROTNumber",
                        column: x => x.ROTNumber,
                        principalTable: "AleBookings",
                        principalColumn: "ROTNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AleContainers",
                columns: table => new
                {
                    ContainerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContainerNumber = table.Column<string>(type: "text", nullable: true),
                    ContainerType = table.Column<string>(type: "text", nullable: false),
                    ContainerSize = table.Column<string>(type: "text", nullable: false),
                    VGM = table.Column<string>(type: "text", nullable: true),
                    TrailerType = table.Column<string>(type: "text", nullable: true),
                    ConsigneeId = table.Column<string>(type: "varchar(6)", nullable: false),
                    HaulierId = table.Column<string>(type: "varchar(6)", nullable: false),
                    TerminalId = table.Column<string>(type: "varchar(6)", nullable: true),
                    ROTDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ROTNumber = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AssignedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EnrouteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AcceptedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GatedInTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GatedOutTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveredTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RFCTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RejectedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RTAssignedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RTEnrouteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RTAcceptedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RTGatedInTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RTGatedOutTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RTDeliveredTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RTRFCTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TimeStatus = table.Column<string>(type: "text", nullable: true),
                    TurnAroundTime = table.Column<int>(type: "integer", nullable: true),
                    DGCRate = table.Column<double>(type: "double precision", nullable: true),
                    DGCReductionEligibility = table.Column<bool>(type: "boolean", nullable: false),
                    DGCReduction = table.Column<double>(type: "double precision", nullable: true),
                    DeletedRemarks = table.Column<string>(type: "text", nullable: true),
                    RejectedRemarks = table.Column<string>(type: "text", nullable: true),
                    ReceivedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovedAKPSTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedCustomsTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedBothTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AleContainers", x => x.ContainerId);
                    table.ForeignKey(
                        name: "FK_AleContainers_AleBookings_ROTNumber",
                        column: x => x.ROTNumber,
                        principalTable: "AleBookings",
                        principalColumn: "ROTNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AleContainers_Companies_ConsigneeId",
                        column: x => x.ConsigneeId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AleContainers_Companies_HaulierId",
                        column: x => x.HaulierId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AleContainers_Companies_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode");
                });

            migrationBuilder.CreateTable(
                name: "AleAssignedHauliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DriverId = table.Column<Guid>(type: "uuid", nullable: false),
                    PMId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeSlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrailerId = table.Column<Guid>(type: "uuid", nullable: true),
                    ContainerId = table.Column<int>(type: "integer", nullable: false),
                    ROTNumber = table.Column<string>(type: "text", nullable: false),
                    HaulierId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AleAssignedHauliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AleAssignedHauliers_AleBookings_ROTNumber",
                        column: x => x.ROTNumber,
                        principalTable: "AleBookings",
                        principalColumn: "ROTNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AleAssignedHauliers_AleContainers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "AleContainers",
                        principalColumn: "ContainerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AleAssignedHauliers_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AleAssignedHauliers_PrimeMovers_PMId",
                        column: x => x.PMId,
                        principalTable: "PrimeMovers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AleAssignedHauliers_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AleAssignedHauliers_Trailers_TrailerId",
                        column: x => x.TrailerId,
                        principalTable: "Trailers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AleContainerAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "text", nullable: false),
                    ContainerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AleContainerAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AleContainerAddresses_AleContainers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "AleContainers",
                        principalColumn: "ContainerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AleContainerAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContainerId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: true),
                    Changes = table.Column<string>(type: "text", nullable: true),
                    AleContainerContainerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AleContainerAudits", x => x.AuditId);
                    table.ForeignKey(
                        name: "FK_AleContainerAudits_AleContainers_AleContainerContainerId",
                        column: x => x.AleContainerContainerId,
                        principalTable: "AleContainers",
                        principalColumn: "ContainerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AleAssignedHauliers_ContainerId",
                table: "AleAssignedHauliers",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_AleAssignedHauliers_DriverId",
                table: "AleAssignedHauliers",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_AleAssignedHauliers_PMId",
                table: "AleAssignedHauliers",
                column: "PMId");

            migrationBuilder.CreateIndex(
                name: "IX_AleAssignedHauliers_ROTNumber",
                table: "AleAssignedHauliers",
                column: "ROTNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AleAssignedHauliers_TimeSlotId",
                table: "AleAssignedHauliers",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_AleAssignedHauliers_TrailerId",
                table: "AleAssignedHauliers",
                column: "TrailerId");

            migrationBuilder.CreateIndex(
                name: "IX_AleBookingDocuments_ROTNumber",
                table: "AleBookingDocuments",
                column: "ROTNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AleBookings_ForwardingId",
                table: "AleBookings",
                column: "ForwardingId");

            migrationBuilder.CreateIndex(
                name: "IX_AleBookings_ShippingAgentId",
                table: "AleBookings",
                column: "ShippingAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_AleBookings_TerminalLocation",
                table: "AleBookings",
                column: "TerminalLocation");

            migrationBuilder.CreateIndex(
                name: "IX_AleContainerAddresses_ContainerId",
                table: "AleContainerAddresses",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_AleContainerAudits_AleContainerContainerId",
                table: "AleContainerAudits",
                column: "AleContainerContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_AleContainers_ConsigneeId",
                table: "AleContainers",
                column: "ConsigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_AleContainers_HaulierId",
                table: "AleContainers",
                column: "HaulierId");

            migrationBuilder.CreateIndex(
                name: "IX_AleContainers_ROTNumber",
                table: "AleContainers",
                column: "ROTNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AleContainers_TerminalId",
                table: "AleContainers",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_AleTimeSlots_TerminalId",
                table: "AleTimeSlots",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerAudits_ContainerId",
                table: "ContainerAudits",
                column: "ContainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AleAssignedHauliers");

            migrationBuilder.DropTable(
                name: "AleBookingDocuments");

            migrationBuilder.DropTable(
                name: "AleContainerAddresses");

            migrationBuilder.DropTable(
                name: "AleContainerAudits");

            migrationBuilder.DropTable(
                name: "AleTimeSlots");

            migrationBuilder.DropTable(
                name: "ContainerAudits");

            migrationBuilder.DropTable(
                name: "AleContainers");

            migrationBuilder.DropTable(
                name: "AleBookings");

            migrationBuilder.DropColumn(
                name: "DropOffTotalSlot",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "AcceptedTime",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "RTAcceptedTime",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "ReceivedBy",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "RejectedRemarks",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "PickUpTotalSlot",
                table: "TimeSlots");
        }
    }
}
