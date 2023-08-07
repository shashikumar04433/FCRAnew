using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class AddApprovalMatrix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalMatrixs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    RiskTypeId = table.Column<int>(type: "int", nullable: true),
                    GeographicPresenceId = table.Column<int>(type: "int", nullable: true),
                    CustomerSegmentId = table.Column<int>(type: "int", nullable: true),
                    BusinessSegmentId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalMatrixs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalMatrixs_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalMatrixs_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovalMatrixs_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "FormMaster",
                columns: new[] { "Id", "Action", "Area", "Controller", "Description", "IconClass", "IsAdmin", "MenuId", "Name", "Sequence" },
                values: new object[] { 24, "Index", "", "ApprovalMatrix", null, "fas fa-comment-dots", true, 2, "Approval Matrix", 5 });

            migrationBuilder.InsertData(
                table: "FormMaster",
                columns: new[] { "Id", "Action", "Area", "Controller", "Description", "IconClass", "IsAdmin", "MenuId", "Name", "Sequence" },
                values: new object[] { 25, "Index", "Admin", "RiskAssessmentVersion", null, "fas fa-comment-dots", true, 2, "Risk Assessment Version", 6 });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "FormId", "RoleId", "Add", "Edit", "FormName", "View" },
                values: new object[] { 24, 1, true, true, "Approval Matrix", true });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "FormId", "RoleId", "Add", "Edit", "FormName", "View" },
                values: new object[] { 25, 1, true, true, "Risk Assessment Version", true });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalMatrixs_CreatedBy",
                table: "ApprovalMatrixs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalMatrixs_CustomerId",
                table: "ApprovalMatrixs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalMatrixs_UpdatedBy",
                table: "ApprovalMatrixs",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalMatrixs");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 24, 1 });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 25, 1 });

            migrationBuilder.DeleteData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 25);
        }
    }
}
