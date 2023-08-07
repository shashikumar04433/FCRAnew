using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class addExitRemarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExitRemarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExitRemarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExitRemarks_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExitRemarks_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExitRemarks_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 1,
                column: "RankType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 3,
                column: "RankType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 4,
                column: "RankType",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 5,
                column: "RankType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 6,
                column: "RankType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 7,
                column: "RankType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 8,
                column: "RankType",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 9,
                column: "RankType",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 11,
                column: "RankType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 12,
                column: "RankType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Business Segment");

            migrationBuilder.InsertData(
                table: "MenuMaster",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "IconClass", "IsActive", "IsAdmin", "Name", "ParentMenuId", "Sequence", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 6, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "fas fa-clipboard-list", true, false, "Audit Log", null, 3, null, null });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 10, 1 },
                column: "FormName",
                value: "Business Segment");

            migrationBuilder.InsertData(
                table: "FormMaster",
                columns: new[] { "Id", "Action", "Area", "Controller", "Description", "IconClass", "IsAdmin", "MenuId", "Name", "Sequence" },
                values: new object[] { 22, "Index", "", "ExitRemarks", null, "fas fa-comment-dots", false, 6, "Exit Remarks", 1 });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "FormId", "RoleId", "Add", "Edit", "FormName", "View" },
                values: new object[] { 22, 1, true, true, "Exit Remarks", true });

            migrationBuilder.CreateIndex(
                name: "IX_ExitRemarks_CreatedBy",
                table: "ExitRemarks",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ExitRemarks_CustomerId",
                table: "ExitRemarks",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExitRemarks_UpdatedBy",
                table: "ExitRemarks",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExitRemarks");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 22, 1 });

            migrationBuilder.DeleteData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 1,
                column: "RankType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 3,
                column: "RankType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 4,
                column: "RankType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 5,
                column: "RankType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 6,
                column: "RankType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 7,
                column: "RankType",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 8,
                column: "RankType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 9,
                column: "RankType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 11,
                column: "RankType",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DefaultScale",
                keyColumn: "Id",
                keyValue: 12,
                column: "RankType",
                value: 5);

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Customer Segment");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 10, 1 },
                column: "FormName",
                value: "Customer Segment");
        }
    }
}
