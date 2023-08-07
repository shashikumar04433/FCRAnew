using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class AddApprovalStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 24,
                column: "IconClass",
                value: "fas fa-building");

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 25,
                column: "Sequence",
                value: 7);

            migrationBuilder.InsertData(
                table: "FormMaster",
                columns: new[] { "Id", "Action", "Area", "Controller", "Description", "IconClass", "IsAdmin", "MenuId", "Name", "Sequence" },
                values: new object[] { 26, "Index", "Admin", "ApprovalStatus", null, "fas fa-building", true, 2, "Approval Status", 6 });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "FormId", "RoleId", "Add", "Edit", "FormName", "View" },
                values: new object[] { 26, 1, true, true, "Approval Status", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 26, 1 });

            migrationBuilder.DeleteData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 24,
                column: "IconClass",
                value: "fas fa-comment-dots");

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 25,
                column: "Sequence",
                value: 6);
        }
    }
}
