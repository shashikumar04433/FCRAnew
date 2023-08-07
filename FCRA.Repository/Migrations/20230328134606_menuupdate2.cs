using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class menuupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 3, 8 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 3, 9 });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 3,
                column: "Sequence",
                value: 2);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 4,
                column: "Sequence",
                value: 1);

            migrationBuilder.UpdateSQLObjects("20230328");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 4, 13 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 4, 14 });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 3,
                column: "Sequence",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 4,
                column: "Sequence",
                value: 2);
        }
    }
}
