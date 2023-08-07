using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class ChangeRiskSubFactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_HighRiskPreDefinedParameterId",
                table: "RiskSubFactor");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_LowRiskPreDefinedParameterId",
                table: "RiskSubFactor");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_MediumRiskPreDefinedParameterId",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "RiskRangeParameter",
                table: "RiskSubFactorResponse");

            migrationBuilder.RenameColumn(
                name: "MediumRiskWeightedScore",
                table: "RiskSubFactorVolumeResponse",
                newName: "WeightedScore5");

            migrationBuilder.RenameColumn(
                name: "MediumRiskWeight",
                table: "RiskSubFactorVolumeResponse",
                newName: "WeightedScore4");

            migrationBuilder.RenameColumn(
                name: "MediumRiskVolume",
                table: "RiskSubFactorVolumeResponse",
                newName: "WeightedScore3");

            migrationBuilder.RenameColumn(
                name: "MediumRiskScore",
                table: "RiskSubFactorVolumeResponse",
                newName: "WeightedScore2");

            migrationBuilder.RenameColumn(
                name: "LowRiskWeightedScore",
                table: "RiskSubFactorVolumeResponse",
                newName: "WeightedScore1");

            migrationBuilder.RenameColumn(
                name: "LowRiskWeight",
                table: "RiskSubFactorVolumeResponse",
                newName: "Weight5");

            migrationBuilder.RenameColumn(
                name: "LowRiskVolume",
                table: "RiskSubFactorVolumeResponse",
                newName: "Weight4");

            migrationBuilder.RenameColumn(
                name: "LowRiskScore",
                table: "RiskSubFactorVolumeResponse",
                newName: "Weight3");

            migrationBuilder.RenameColumn(
                name: "HighRiskWeightedScore",
                table: "RiskSubFactorVolumeResponse",
                newName: "Weight2");

            migrationBuilder.RenameColumn(
                name: "HighRiskWeight",
                table: "RiskSubFactorVolumeResponse",
                newName: "Weight1");

            migrationBuilder.RenameColumn(
                name: "HighRiskVolume",
                table: "RiskSubFactorVolumeResponse",
                newName: "Volume5");

            migrationBuilder.RenameColumn(
                name: "HighRiskScore",
                table: "RiskSubFactorVolumeResponse",
                newName: "Volume4");

            migrationBuilder.RenameColumn(
                name: "MediumRiskVolume",
                table: "RiskSubFactor",
                newName: "RiskVolume5");

            migrationBuilder.RenameColumn(
                name: "MediumRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                newName: "PreDefinedParameter5Id");

            migrationBuilder.RenameColumn(
                name: "MediumRiskMinPercentage",
                table: "RiskSubFactor",
                newName: "RiskVolume4");

            migrationBuilder.RenameColumn(
                name: "MediumRiskMaxPercentage",
                table: "RiskSubFactor",
                newName: "RiskVolume3");

            migrationBuilder.RenameColumn(
                name: "MediumRiskDescription",
                table: "RiskSubFactor",
                newName: "RiskDescription5");

            migrationBuilder.RenameColumn(
                name: "LowRiskVolume",
                table: "RiskSubFactor",
                newName: "RiskVolume2");

            migrationBuilder.RenameColumn(
                name: "LowRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                newName: "PreDefinedParameter4Id");

            migrationBuilder.RenameColumn(
                name: "LowRiskPercentage",
                table: "RiskSubFactor",
                newName: "RiskVolume1");

            migrationBuilder.RenameColumn(
                name: "LowRiskDescription",
                table: "RiskSubFactor",
                newName: "RiskDescription4");

            migrationBuilder.RenameColumn(
                name: "HighRiskVolume",
                table: "RiskSubFactor",
                newName: "Percentage5");

            migrationBuilder.RenameColumn(
                name: "HighRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                newName: "PreDefinedParameter3Id");

            migrationBuilder.RenameColumn(
                name: "HighRiskPercentage",
                table: "RiskSubFactor",
                newName: "Percentage4");

            migrationBuilder.RenameColumn(
                name: "HighRiskDescription",
                table: "RiskSubFactor",
                newName: "RiskDescription3");

            migrationBuilder.RenameIndex(
                name: "IX_RiskSubFactor_MediumRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                newName: "IX_RiskSubFactor_PreDefinedParameter5Id");

            migrationBuilder.RenameIndex(
                name: "IX_RiskSubFactor_LowRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                newName: "IX_RiskSubFactor_PreDefinedParameter4Id");

            migrationBuilder.RenameIndex(
                name: "IX_RiskSubFactor_HighRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                newName: "IX_RiskSubFactor_PreDefinedParameter3Id");

            migrationBuilder.AlterColumn<string>(
                name: "CountryWiseVolume",
                table: "RiskSubFactorVolumeResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 27)
                .OldAnnotation("Relational:ColumnOrder", 19);

            migrationBuilder.AlterColumn<string>(
                name: "CountryWiseRating",
                table: "RiskSubFactorVolumeResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 26)
                .OldAnnotation("Relational:ColumnOrder", 18);

            migrationBuilder.AlterColumn<string>(
                name: "Countries",
                table: "RiskSubFactorVolumeResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 25)
                .OldAnnotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskSubFactorVolumeResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 28)
                .OldAnnotation("Relational:ColumnOrder", 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightedScore5",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 24)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightedScore4",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 23)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightedScore3",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 22)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightedScore2",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 21)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightedScore1",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 20)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight5",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 19)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight4",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 18)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight3",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 17)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight1",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<decimal>(
                name: "Volume5",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Volume4",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<decimal>(
                name: "Score1",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<decimal>(
                name: "Score2",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<decimal>(
                name: "Score3",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<decimal>(
                name: "Score4",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<decimal>(
                name: "Score5",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume1",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 10);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume2",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 11);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume3",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "RiskSubFactorResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "ResponseDescription",
                table: "RiskSubFactorResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Response",
                table: "RiskSubFactorResponse",
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
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<int>(
                name: "PreDefinedParameterId",
                table: "RiskSubFactorResponse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "Assumptions",
                table: "RiskSubFactorResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<int>(
                name: "ScaleResponse",
                table: "RiskSubFactorResponse",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<int>(
                name: "Sequence",
                table: "RiskSubFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 28)
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
                .Annotation("Relational:ColumnOrder", 27)
                .OldAnnotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskSubFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 29)
                .OldAnnotation("Relational:ColumnOrder", 19);

            migrationBuilder.AlterColumn<decimal>(
                name: "RiskVolume5",
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
                .Annotation("Relational:ColumnOrder", 22)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<int>(
                name: "PreDefinedParameter5Id",
                table: "RiskSubFactor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<decimal>(
                name: "RiskVolume4",
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
                .Annotation("Relational:ColumnOrder", 21)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "RiskVolume3",
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
                .Annotation("Relational:ColumnOrder", 20)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "RiskDescription5",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 17)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<decimal>(
                name: "RiskVolume2",
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
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<int>(
                name: "PreDefinedParameter4Id",
                table: "RiskSubFactor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "RiskVolume1",
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
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "RiskDescription4",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 16)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage5",
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
                .OldAnnotation("Relational:ColumnOrder", 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage4",
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
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<string>(
                name: "RiskDescription3",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AddColumn<decimal>(
                name: "Number2",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 23);

            migrationBuilder.AddColumn<decimal>(
                name: "Number3",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 24);

            migrationBuilder.AddColumn<decimal>(
                name: "Number4",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 25);

            migrationBuilder.AddColumn<decimal>(
                name: "Number5",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 26);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage2",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage3",
                table: "RiskSubFactor",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<int>(
                name: "PreDefinedParameter1Id",
                table: "RiskSubFactor",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "PreDefinedParameter2Id",
                table: "RiskSubFactor",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddColumn<string>(
                name: "RiskDescription1",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 13);

            migrationBuilder.AddColumn<string>(
                name: "RiskDescription2",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 14);

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactor_PreDefinedParameter1Id",
                table: "RiskSubFactor",
                column: "PreDefinedParameter1Id");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactor_PreDefinedParameter2Id",
                table: "RiskSubFactor",
                column: "PreDefinedParameter2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_PreDefinedParameter1Id",
                table: "RiskSubFactor",
                column: "PreDefinedParameter1Id",
                principalTable: "PreDefinedRiskParameter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_PreDefinedParameter2Id",
                table: "RiskSubFactor",
                column: "PreDefinedParameter2Id",
                principalTable: "PreDefinedRiskParameter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_PreDefinedParameter3Id",
                table: "RiskSubFactor",
                column: "PreDefinedParameter3Id",
                principalTable: "PreDefinedRiskParameter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_PreDefinedParameter4Id",
                table: "RiskSubFactor",
                column: "PreDefinedParameter4Id",
                principalTable: "PreDefinedRiskParameter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_PreDefinedParameter5Id",
                table: "RiskSubFactor",
                column: "PreDefinedParameter5Id",
                principalTable: "PreDefinedRiskParameter",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_PreDefinedParameter1Id",
                table: "RiskSubFactor");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_PreDefinedParameter2Id",
                table: "RiskSubFactor");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_PreDefinedParameter3Id",
                table: "RiskSubFactor");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_PreDefinedParameter4Id",
                table: "RiskSubFactor");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_PreDefinedParameter5Id",
                table: "RiskSubFactor");

            migrationBuilder.DropIndex(
                name: "IX_RiskSubFactor_PreDefinedParameter1Id",
                table: "RiskSubFactor");

            migrationBuilder.DropIndex(
                name: "IX_RiskSubFactor_PreDefinedParameter2Id",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "Score1",
                table: "RiskSubFactorVolumeResponse");

            migrationBuilder.DropColumn(
                name: "Score2",
                table: "RiskSubFactorVolumeResponse");

            migrationBuilder.DropColumn(
                name: "Score3",
                table: "RiskSubFactorVolumeResponse");

            migrationBuilder.DropColumn(
                name: "Score4",
                table: "RiskSubFactorVolumeResponse");

            migrationBuilder.DropColumn(
                name: "Score5",
                table: "RiskSubFactorVolumeResponse");

            migrationBuilder.DropColumn(
                name: "Volume1",
                table: "RiskSubFactorVolumeResponse");

            migrationBuilder.DropColumn(
                name: "Volume2",
                table: "RiskSubFactorVolumeResponse");

            migrationBuilder.DropColumn(
                name: "Volume3",
                table: "RiskSubFactorVolumeResponse");

            migrationBuilder.DropColumn(
                name: "ScaleResponse",
                table: "RiskSubFactorResponse");

            migrationBuilder.DropColumn(
                name: "Number2",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "Number3",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "Number4",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "Number5",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "Percentage2",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "Percentage3",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "PreDefinedParameter1Id",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "PreDefinedParameter2Id",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "RiskDescription1",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "RiskDescription2",
                table: "RiskSubFactor");

            migrationBuilder.RenameColumn(
                name: "WeightedScore5",
                table: "RiskSubFactorVolumeResponse",
                newName: "MediumRiskWeightedScore");

            migrationBuilder.RenameColumn(
                name: "WeightedScore4",
                table: "RiskSubFactorVolumeResponse",
                newName: "MediumRiskWeight");

            migrationBuilder.RenameColumn(
                name: "WeightedScore3",
                table: "RiskSubFactorVolumeResponse",
                newName: "MediumRiskVolume");

            migrationBuilder.RenameColumn(
                name: "WeightedScore2",
                table: "RiskSubFactorVolumeResponse",
                newName: "MediumRiskScore");

            migrationBuilder.RenameColumn(
                name: "WeightedScore1",
                table: "RiskSubFactorVolumeResponse",
                newName: "LowRiskWeightedScore");

            migrationBuilder.RenameColumn(
                name: "Weight5",
                table: "RiskSubFactorVolumeResponse",
                newName: "LowRiskWeight");

            migrationBuilder.RenameColumn(
                name: "Weight4",
                table: "RiskSubFactorVolumeResponse",
                newName: "LowRiskVolume");

            migrationBuilder.RenameColumn(
                name: "Weight3",
                table: "RiskSubFactorVolumeResponse",
                newName: "LowRiskScore");

            migrationBuilder.RenameColumn(
                name: "Weight2",
                table: "RiskSubFactorVolumeResponse",
                newName: "HighRiskWeightedScore");

            migrationBuilder.RenameColumn(
                name: "Weight1",
                table: "RiskSubFactorVolumeResponse",
                newName: "HighRiskWeight");

            migrationBuilder.RenameColumn(
                name: "Volume5",
                table: "RiskSubFactorVolumeResponse",
                newName: "HighRiskVolume");

            migrationBuilder.RenameColumn(
                name: "Volume4",
                table: "RiskSubFactorVolumeResponse",
                newName: "HighRiskScore");

            migrationBuilder.RenameColumn(
                name: "RiskVolume5",
                table: "RiskSubFactor",
                newName: "MediumRiskVolume");

            migrationBuilder.RenameColumn(
                name: "RiskVolume4",
                table: "RiskSubFactor",
                newName: "MediumRiskMinPercentage");

            migrationBuilder.RenameColumn(
                name: "RiskVolume3",
                table: "RiskSubFactor",
                newName: "MediumRiskMaxPercentage");

            migrationBuilder.RenameColumn(
                name: "RiskVolume2",
                table: "RiskSubFactor",
                newName: "LowRiskVolume");

            migrationBuilder.RenameColumn(
                name: "RiskVolume1",
                table: "RiskSubFactor",
                newName: "LowRiskPercentage");

            migrationBuilder.RenameColumn(
                name: "RiskDescription5",
                table: "RiskSubFactor",
                newName: "MediumRiskDescription");

            migrationBuilder.RenameColumn(
                name: "RiskDescription4",
                table: "RiskSubFactor",
                newName: "LowRiskDescription");

            migrationBuilder.RenameColumn(
                name: "RiskDescription3",
                table: "RiskSubFactor",
                newName: "HighRiskDescription");

            migrationBuilder.RenameColumn(
                name: "PreDefinedParameter5Id",
                table: "RiskSubFactor",
                newName: "MediumRiskPreDefinedParameterId");

            migrationBuilder.RenameColumn(
                name: "PreDefinedParameter4Id",
                table: "RiskSubFactor",
                newName: "LowRiskPreDefinedParameterId");

            migrationBuilder.RenameColumn(
                name: "PreDefinedParameter3Id",
                table: "RiskSubFactor",
                newName: "HighRiskPreDefinedParameterId");

            migrationBuilder.RenameColumn(
                name: "Percentage5",
                table: "RiskSubFactor",
                newName: "HighRiskVolume");

            migrationBuilder.RenameColumn(
                name: "Percentage4",
                table: "RiskSubFactor",
                newName: "HighRiskPercentage");

            migrationBuilder.RenameIndex(
                name: "IX_RiskSubFactor_PreDefinedParameter5Id",
                table: "RiskSubFactor",
                newName: "IX_RiskSubFactor_MediumRiskPreDefinedParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskSubFactor_PreDefinedParameter4Id",
                table: "RiskSubFactor",
                newName: "IX_RiskSubFactor_LowRiskPreDefinedParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskSubFactor_PreDefinedParameter3Id",
                table: "RiskSubFactor",
                newName: "IX_RiskSubFactor_HighRiskPreDefinedParameterId");

            migrationBuilder.AlterColumn<string>(
                name: "CountryWiseVolume",
                table: "RiskSubFactorVolumeResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 19)
                .OldAnnotation("Relational:ColumnOrder", 27);

            migrationBuilder.AlterColumn<string>(
                name: "CountryWiseRating",
                table: "RiskSubFactorVolumeResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 18)
                .OldAnnotation("Relational:ColumnOrder", 26);

            migrationBuilder.AlterColumn<string>(
                name: "Countries",
                table: "RiskSubFactorVolumeResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 17)
                .OldAnnotation("Relational:ColumnOrder", 25);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskSubFactorVolumeResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 20)
                .OldAnnotation("Relational:ColumnOrder", 28);

            migrationBuilder.AlterColumn<decimal>(
                name: "MediumRiskWeightedScore",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 24);

            migrationBuilder.AlterColumn<decimal>(
                name: "MediumRiskWeight",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 23);

            migrationBuilder.AlterColumn<decimal>(
                name: "MediumRiskVolume",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 22);

            migrationBuilder.AlterColumn<decimal>(
                name: "MediumRiskScore",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 21);

            migrationBuilder.AlterColumn<decimal>(
                name: "LowRiskWeightedScore",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "LowRiskWeight",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 19);

            migrationBuilder.AlterColumn<decimal>(
                name: "LowRiskVolume",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 18);

            migrationBuilder.AlterColumn<decimal>(
                name: "LowRiskScore",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<decimal>(
                name: "HighRiskWeight",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<decimal>(
                name: "HighRiskVolume",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<decimal>(
                name: "HighRiskScore",
                table: "RiskSubFactorVolumeResponse",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "RiskSubFactorResponse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "ResponseDescription",
                table: "RiskSubFactorResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<decimal>(
                name: "Response",
                table: "RiskSubFactorResponse",
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
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "PreDefinedParameterId",
                table: "RiskSubFactorResponse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "Assumptions",
                table: "RiskSubFactorResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<int>(
                name: "RiskRangeParameter",
                table: "RiskSubFactorResponse",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "Sequence",
                table: "RiskSubFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 18)
                .OldAnnotation("Relational:ColumnOrder", 28);

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
                .OldAnnotation("Relational:ColumnOrder", 27);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskSubFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 19)
                .OldAnnotation("Relational:ColumnOrder", 29);

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
                .OldAnnotation("Relational:ColumnOrder", 22);

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
                .OldAnnotation("Relational:ColumnOrder", 21);

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
                .OldAnnotation("Relational:ColumnOrder", 20);

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
                .OldAnnotation("Relational:ColumnOrder", 19);

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
                .OldAnnotation("Relational:ColumnOrder", 18);

            migrationBuilder.AlterColumn<string>(
                name: "MediumRiskDescription",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<string>(
                name: "LowRiskDescription",
                table: "RiskSubFactor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 16);

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
                name: "MediumRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<int>(
                name: "LowRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8)
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
                .Annotation("Relational:ColumnOrder", 16)
                .OldAnnotation("Relational:ColumnOrder", 7);

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
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_HighRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                column: "HighRiskPreDefinedParameterId",
                principalTable: "PreDefinedRiskParameter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_LowRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                column: "LowRiskPreDefinedParameterId",
                principalTable: "PreDefinedRiskParameter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactor_PreDefinedRiskParameter_MediumRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                column: "MediumRiskPreDefinedParameterId",
                principalTable: "PreDefinedRiskParameter",
                principalColumn: "Id");
        }
    }
}
