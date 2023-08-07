using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class ApprovedResponseUpdateFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "PendingWithUserType",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PendingFrom",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<int>(
                name: "FinalStatus",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<int>(
                name: "BusinessSegmentId",
                table: "ApprovalRequests",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<int>(
                name: "CustomerSegmentId",
                table: "ApprovalRequests",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<int>(
                name: "GeographicPresenceId",
                table: "ApprovalRequests",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<int>(
                name: "RiskTypeId",
                table: "ApprovalRequests",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<int>(
                name: "StageId",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessSegmentId",
                table: "ApprovalRequests");

            migrationBuilder.DropColumn(
                name: "CustomerSegmentId",
                table: "ApprovalRequests");

            migrationBuilder.DropColumn(
                name: "GeographicPresenceId",
                table: "ApprovalRequests");

            migrationBuilder.DropColumn(
                name: "RiskTypeId",
                table: "ApprovalRequests");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "ApprovalRequests");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<int>(
                name: "PendingWithUserType",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PendingFrom",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<int>(
                name: "FinalStatus",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 12);
        }
    }
}
