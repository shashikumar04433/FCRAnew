using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class ApprovalStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FinalStatus = table.Column<int>(type: "int", nullable: false),
                    PendingWithUserType = table.Column<int>(type: "int", nullable: false),
                    PendingFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalRequests_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalRequests_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalRequests_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovalStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalHistorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalHistorys_ApprovalRequests_ApprovalId",
                        column: x => x.ApprovalId,
                        principalTable: "ApprovalRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalHistorys_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalHistorys_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalHistorys_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ApprovalStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Submitted for Review" },
                    { 2, "Sent back by Reviewer" },
                    { 3, "Submitted for Approval" },
                    { 4, "Sent back by Approver" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalHistorys_ApprovalId",
                table: "ApprovalHistorys",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalHistorys_CreatedBy",
                table: "ApprovalHistorys",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalHistorys_CustomerId",
                table: "ApprovalHistorys",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalHistorys_UpdatedBy",
                table: "ApprovalHistorys",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_CreatedBy",
                table: "ApprovalRequests",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_CustomerId",
                table: "ApprovalRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_UpdatedBy",
                table: "ApprovalRequests",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalHistorys");

            migrationBuilder.DropTable(
                name: "ApprovalStatus");

            migrationBuilder.DropTable(
                name: "ApprovalRequests");
        }
    }
}
