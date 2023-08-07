using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class AddSequenceToRiskCriterai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskCriteria",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "RiskCriteria",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "RiskCriteria");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskCriteria",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 4);
        }
    }
}
