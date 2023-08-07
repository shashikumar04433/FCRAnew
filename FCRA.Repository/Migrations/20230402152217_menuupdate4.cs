using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class menuupdate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "IconClass", "Name" },
                values: new object[] { "fas fa-chart-bar", "Summary" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 21, 1 },
                column: "FormName",
                value: "Summary");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "IconClass", "Name" },
                values: new object[] { "fas fa-home", "Home" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 21, 1 },
                column: "FormName",
                value: "Home");
        }
    }
}
