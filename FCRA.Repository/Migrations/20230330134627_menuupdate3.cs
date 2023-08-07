using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class menuupdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Sub Unit");

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 21,
                column: "Name",
                value: "Risk Comparison");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 11, 1 },
                column: "FormName",
                value: "Sub Unit");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 21, 1 },
                column: "FormName",
                value: "Risk Comparison");

            migrationBuilder.UpdateSQLObjects("20230330");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Business Segment");

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 21,
                column: "Name",
                value: "Risk Compare");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 11, 1 },
                column: "FormName",
                value: "Business Segment");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 21, 1 },
                column: "FormName",
                value: "Risk Compare");
        }
    }
}
