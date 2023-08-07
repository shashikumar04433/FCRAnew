using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class ApprovedResponseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovedRiskSubFactorVolumeResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 29)
                .OldAnnotation("Relational:ColumnOrder", 28);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalId",
                table: "ApprovedRiskSubFactorVolumeResponse",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 28);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovedRiskSubFactorResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalId",
                table: "ApprovedRiskSubFactorResponse",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovedRiskScoreResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalId",
                table: "ApprovedRiskScoreResponse",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovedRiskScoreProductVolumRatingResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalId",
                table: "ApprovedRiskScoreProductVolumRatingResponse",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovedRiskFactorResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalId",
                table: "ApprovedRiskFactorResponse",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalId",
                table: "ApprovedRiskSubFactorVolumeResponse");

            migrationBuilder.DropColumn(
                name: "ApprovalId",
                table: "ApprovedRiskSubFactorResponse");

            migrationBuilder.DropColumn(
                name: "ApprovalId",
                table: "ApprovedRiskScoreResponse");

            migrationBuilder.DropColumn(
                name: "ApprovalId",
                table: "ApprovedRiskScoreProductVolumRatingResponse");

            migrationBuilder.DropColumn(
                name: "ApprovalId",
                table: "ApprovedRiskFactorResponse");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovedRiskSubFactorVolumeResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 28)
                .OldAnnotation("Relational:ColumnOrder", 29);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovedRiskSubFactorResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovedRiskScoreResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovedRiskScoreProductVolumRatingResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ApprovedRiskFactorResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 5);
        }
    }
}
