using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class AddCustomerConfigMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FormMaster",
                columns: new[] { "Id", "Action", "Area", "Controller", "Description", "IconClass", "IsAdmin", "MenuId", "Name", "Sequence" },
                values: new object[] { 23, "CustomerConfiguration", "Admin", "Customer", null, "fas fa-user-friends", true, 1, "Customer Configuration", 2 });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "FormId", "RoleId", "Add", "Edit", "FormName", "View" },
                values: new object[] { 23, 1, true, true, "Customer Configuration", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 23, 1 });

            migrationBuilder.DeleteData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 23);
        }
    }
}
