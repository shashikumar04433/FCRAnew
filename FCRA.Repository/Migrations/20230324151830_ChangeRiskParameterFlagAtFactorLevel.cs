using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class ChangeRiskParameterFlagAtFactorLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExcludedInRisk",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "RiskRangeParameter",
                table: "RiskSubFactor");

            migrationBuilder.AlterColumn<int>(
                name: "Sequence",
                table: "RiskSubFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 18)
                .OldAnnotation("Relational:ColumnOrder", 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "RiskWeightage",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 17)
                .OldAnnotation("Relational:ColumnOrder", 19);

            migrationBuilder.AlterColumn<decimal>(
                name: "MediumRiskVolume",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<int>(
                name: "MediumRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<decimal>(
                name: "MediumRiskMinPercentage",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "MediumRiskMaxPercentage",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "MediumRiskDescription",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<decimal>(
                name: "LowRiskVolume",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 16);

            migrationBuilder.AlterColumn<int>(
                name: "LowRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "LowRiskPercentage",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "LowRiskDescription",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<decimal>(
                name: "HighRiskVolume",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 16)
                .OldAnnotation("Relational:ColumnOrder", 18);

            migrationBuilder.AlterColumn<int>(
                name: "HighRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<decimal>(
                name: "HighRiskPercentage",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "HighRiskDescription",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskSubFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 19)
                .OldAnnotation("Relational:ColumnOrder", 21);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 16)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AddColumn<bool>(
                name: "IsExcludedInRisk",
                table: "RiskFactor",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 14);

            migrationBuilder.AddColumn<int>(
                name: "RiskRangeParameter",
                table: "RiskFactor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 15);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExcludedInRisk",
                table: "RiskFactor");

            migrationBuilder.DropColumn(
                name: "RiskRangeParameter",
                table: "RiskFactor");

            migrationBuilder.AlterColumn<int>(
                name: "Sequence",
                table: "RiskSubFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 20)
                .OldAnnotation("Relational:ColumnOrder", 18);

            migrationBuilder.AlterColumn<decimal>(
                name: "RiskWeightage",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 19)
                .OldAnnotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<decimal>(
                name: "MediumRiskVolume",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 17)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<int>(
                name: "MediumRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<decimal>(
                name: "MediumRiskMinPercentage",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MediumRiskMaxPercentage",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "MediumRiskDescription",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<decimal>(
                name: "LowRiskVolume",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 16)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<int>(
                name: "LowRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "LowRiskPercentage",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "LowRiskDescription",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<decimal>(
                name: "HighRiskVolume",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 18)
                .OldAnnotation("Relational:ColumnOrder", 16);

            migrationBuilder.AlterColumn<int>(
                name: "HighRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "HighRiskPercentage",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<string>(
                name: "HighRiskDescription",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskSubFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 21)
                .OldAnnotation("Relational:ColumnOrder", 19);

            migrationBuilder.AddColumn<bool>(
                name: "IsExcludedInRisk",
                table: "RiskSubFactor",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<int>(
                name: "RiskRangeParameter",
                table: "RiskSubFactor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 16);
        }
    }
}
