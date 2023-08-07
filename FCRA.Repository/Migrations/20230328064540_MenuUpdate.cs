using System;
using FCRA.Models.Customers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class MenuUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DeleteData(
                table: "CustomerForm",
                keyColumn: "FormId",
                keyValue: 4);
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "FormId",
                keyValue: 4);
            migrationBuilder.DeleteData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<int>(
                name: "ParentMenuId",
                table: "MenuMaster",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 5,
                column: "Sequence",
                value: 1);

            migrationBuilder.InsertData(
                table: "MenuMaster",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "IconClass", "IsActive", "IsAdmin", "Name", "ParentMenuId", "Sequence", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 3, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "fas fa-th-list", true, true, "Mappings", 1, 1, null, null },
                    { 4, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "fas fa-user-graduate", true, true, "Customer Details", 1, 2, null, null },
                    { 5, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "fas fa-drafting-compass", true, true, "Risk Assessment", 1, 3, null, null }
                });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Reviewer" },
                    { 5, "Approver" }
                });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 3, 1 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 4, 8 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 4, 9 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 4, 10 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 4, 11 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 4, 12 });

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
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 3, 2 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 3, 4 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 3, 5 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 3, 6 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 3, 7 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "ParentMenuId",
                table: "MenuMaster");

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 5,
                column: "Sequence",
                value: 2);

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 2 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 3 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 4 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 5 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 6 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 7 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 8 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 9 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 10 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 11 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 12 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 13 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 14 });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "MenuId", "Sequence" },
                values: new object[] { 1, 15 });

            migrationBuilder.InsertData(
                table: "FormMaster",
                columns: new[] { "Id", "Action", "Area", "Controller", "Description", "IconClass", "IsAdmin", "MenuId", "Name", "Sequence" },
                values: new object[] { 4, "Index", "Admin", "Country", null, "fas fa-globe", true, 1, "Country", 1 });
        }
    }
}
