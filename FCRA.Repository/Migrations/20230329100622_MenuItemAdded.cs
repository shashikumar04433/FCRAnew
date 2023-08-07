using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class MenuItemAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FormMaster",
                columns: new[] { "Id", "Action", "Area", "Controller", "Description", "IconClass", "IsAdmin", "MenuId", "Name", "Sequence" },
                values: new object[] { 21, "Index", "", "RiskCompare", null, "fas fa-not-equal", false, null, "Risk Compare", 2 });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "FormId", "RoleId", "Add", "Edit", "FormName", "View" },
                values: new object[] { 21, 1, true, true, "Risk Compare", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 21, 1 });

            migrationBuilder.DeleteData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 21);
        }
    }
}
