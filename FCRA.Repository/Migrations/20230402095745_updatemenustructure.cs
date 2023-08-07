using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class updatemenustructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
               table: "FormMaster",
               keyColumn: "Id",
               keyValue: 22,
               columns: new[] { "MenuId", "Name", "Sequence" },
               values: new object[] { 2, "Audit Log", 4 });

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 20,
                column: "IconClass",
                value: "fas fa-tasks");

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Controller", "IconClass", "Name" },
                values: new object[] { "Home", "fas fa-home", "Home" });

           

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconClass",
                value: "fas fa-users");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 21, 1 },
                column: "FormName",
                value: "Home");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 22, 1 },
                column: "FormName",
                value: "Audit Log");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 20,
                column: "IconClass",
                value: "fas fa-diagnoses");

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Controller", "IconClass", "Name" },
                values: new object[] { "RiskCompare", "fas fa-not-equal", "Risk Comparison" });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconClass",
                value: "fas fa-users-cog");

            migrationBuilder.InsertData(
                table: "MenuMaster",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "IconClass", "IsActive", "IsAdmin", "Name", "ParentMenuId", "Sequence", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 5, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "fas fa-drafting-compass", true, true, "Risk Assessment", 1, 3, null, null },
                    { 6, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "fas fa-clipboard-list", true, false, "Audit Log", null, 3, null, null }
                });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 21, 1 },
                column: "FormName",
                value: "Risk Comparison");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "FormId", "RoleId" },
                keyValues: new object[] { 22, 1 },
                column: "FormName",
                value: "Exit Remarks");

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "MenuId", "Name", "Sequence" },
                values: new object[] { 6, "Exit Remarks", 1 });
        }
    }
}
