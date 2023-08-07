using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class AddValuepropertyinModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskScoreProductVolumRatingResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AddColumn<decimal>(
                name: "Values",
                table: "RiskScoreProductVolumRatingResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Values",
                table: "RiskScoreProductVolumRatingResponse");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskScoreProductVolumRatingResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 11);
        }
    }
}
