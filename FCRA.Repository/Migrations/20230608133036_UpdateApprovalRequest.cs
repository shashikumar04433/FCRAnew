using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class UpdateApprovalRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VersionName",
                table: "ApprovalRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PendingFrom",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AddColumn<int>(
                name: "PendingWithUser",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 11);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "ApprovalHistorys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovalHistorys",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<int>(
                name: "PendingWithUser",
                table: "ApprovalHistorys",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "ApprovalHistorys",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PendingWithUser",
                table: "ApprovalRequests");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "ApprovalRequests");

            migrationBuilder.DropColumn(
                name: "PendingWithUser",
                table: "ApprovalHistorys");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "ApprovalHistorys");

            migrationBuilder.AlterColumn<string>(
                name: "VersionName",
                table: "ApprovalRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PendingFrom",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "ApprovalHistorys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovalHistorys",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 8);
        }
    }
}
