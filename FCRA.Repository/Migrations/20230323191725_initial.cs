using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultScale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScaleType = table.Column<int>(type: "int", nullable: false),
                    RankType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultScale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessSegment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CustomerSegmentId = table.Column<int>(type: "int", nullable: false),
                    ScaleRange2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ScaleRange5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSegment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ScaleType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLocation",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address4 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLocation", x => new { x.CustomerId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_CustomerLocation_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerLocation_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerScaleLabels",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ScaleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerScaleLabels", x => new { x.CustomerId, x.ScaleId });
                    table.ForeignKey(
                        name: "FK_CustomerScaleLabels_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerScaleLabels_DefaultScale_ScaleId",
                        column: x => x.ScaleId,
                        principalTable: "DefaultScale",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerForm",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    FormName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerForm", x => new { x.CustomerId, x.FormId });
                    table.ForeignKey(
                        name: "FK_CustomerForm_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerSegment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GeographicPresenceId = table.Column<int>(type: "int", nullable: false),
                    ScaleRange2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ScaleRange5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ExcludeChildCategory = table.Column<bool>(type: "bit", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSegment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSegment_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormControlMaster",
                columns: table => new
                {
                    FormId = table.Column<int>(type: "int", nullable: false),
                    ControlId = table.Column<int>(type: "int", nullable: false),
                    ControlName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormControlMaster", x => new { x.FormId, x.ControlId });
                });

            migrationBuilder.CreateTable(
                name: "FormControlRoleMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    ControlId = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormControlRoleMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Area = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Controller = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeographicPresence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RiskTypeId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ScaleRange2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ScaleRange5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ExcludeChildCategory = table.Column<bool>(type: "bit", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeographicPresence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeographicPresence_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GeographicPresence_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GeographyRisk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskRating = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeographyRisk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeographyRisk_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GeographyRisk_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MenuMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreDefinedRiskParameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreDefinedRiskParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreDefinedRiskParameter_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductRiskCriteriaMapping",
                columns: table => new
                {
                    RiskSubFactorId = table.Column<int>(type: "int", nullable: false),
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RiskCriteriaId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRiskCriteriaMapping", x => new { x.CustomerId, x.RiskFactorId, x.RiskSubFactorId, x.ProductId, x.RiskCriteriaId });
                    table.ForeignKey(
                        name: "FK_ProductRiskCriteriaMapping_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductService_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Scale1Value = table.Column<int>(type: "int", nullable: false),
                    Scale2Value = table.Column<int>(type: "int", nullable: false),
                    Scale3Value = table.Column<int>(type: "int", nullable: false),
                    Scale4Value = table.Column<int>(type: "int", nullable: true),
                    Scale5Value = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Questions_ProductService_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductService",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionsRiskCriteriaMapping",
                columns: table => new
                {
                    RiskSubFactorId = table.Column<int>(type: "int", nullable: false),
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RiskCriteriaId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsRiskCriteriaMapping", x => new { x.CustomerId, x.RiskFactorId, x.RiskSubFactorId, x.ProductId, x.RiskCriteriaId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_QuestionsRiskCriteriaMapping_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionsRiskCriteriaMapping_ProductService_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductService",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionsRiskCriteriaMapping_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskCriteria_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskFactor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    RiskTypeId = table.Column<int>(type: "int", nullable: true),
                    GeographicPresenceId = table.Column<int>(type: "int", nullable: true),
                    CustomerSegmentId = table.Column<int>(type: "int", nullable: true),
                    BusinessSegmentId = table.Column<int>(type: "int", nullable: true),
                    WeightPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ScaleRange5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskFactor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskFactor_BusinessSegment_BusinessSegmentId",
                        column: x => x.BusinessSegmentId,
                        principalTable: "BusinessSegment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskFactor_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskFactor_CustomerSegment_CustomerSegmentId",
                        column: x => x.CustomerSegmentId,
                        principalTable: "CustomerSegment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskFactor_GeographicPresence_GeographicPresenceId",
                        column: x => x.GeographicPresenceId,
                        principalTable: "GeographicPresence",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskFactorProductServiceMapping",
                columns: table => new
                {
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    RiskSubFactorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ScaleRange2 = table.Column<int>(type: "int", nullable: false),
                    ScaleRange3 = table.Column<int>(type: "int", nullable: false),
                    ScaleRange4 = table.Column<int>(type: "int", nullable: true),
                    ScaleRange5 = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskFactorProductServiceMapping", x => new { x.CustomerId, x.RiskFactorId, x.RiskSubFactorId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_RiskFactorProductServiceMapping_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskFactorProductServiceMapping_ProductService_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductService",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskFactorProductServiceMapping_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskFactorResponse",
                columns: table => new
                {
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    TotalWeightedScore = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskFactorResponse", x => x.RiskFactorId);
                    table.ForeignKey(
                        name: "FK_RiskFactorResponse_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskFactorResponse_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskScoreProductVolumRatingResponse",
                columns: table => new
                {
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    RiskSubFactorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TotalScore = table.Column<int>(type: "int", nullable: true),
                    FinalScore = table.Column<int>(type: "int", nullable: true),
                    RiskRating = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskScoreProductVolumRatingResponse", x => new { x.CustomerId, x.RiskFactorId, x.RiskSubFactorId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_RiskScoreProductVolumRatingResponse_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskScoreProductVolumRatingResponse_ProductService_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductService",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskScoreProductVolumRatingResponse_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskScoreResponse",
                columns: table => new
                {
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    RiskSubFactorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RiskCriteriaId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    QuestionIds = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Answers = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskScoreResponse", x => new { x.CustomerId, x.RiskFactorId, x.RiskSubFactorId, x.ProductId, x.RiskCriteriaId });
                    table.ForeignKey(
                        name: "FK_RiskScoreResponse_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskScoreResponse_ProductService_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductService",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskScoreResponse_RiskCriteria_RiskCriteriaId",
                        column: x => x.RiskCriteriaId,
                        principalTable: "RiskCriteria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskScoreResponse_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskSubFactor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    IsExcludedInRisk = table.Column<bool>(type: "bit", nullable: false),
                    RiskRangeParameter = table.Column<int>(type: "int", nullable: false),
                    LowRiskPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MediumRiskMinPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MediumRiskMaxPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    HighRiskPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    LowRiskPreDefinedParameterId = table.Column<int>(type: "int", nullable: true),
                    MediumRiskPreDefinedParameterId = table.Column<int>(type: "int", nullable: true),
                    HighRiskPreDefinedParameterId = table.Column<int>(type: "int", nullable: true),
                    LowRiskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediumRiskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighRiskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LowRiskVolume = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MediumRiskVolume = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    HighRiskVolume = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    RiskWeightage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskSubFactor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskSubFactor_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactor_PreDefinedRiskParameter_HighRiskPreDefinedParameterId",
                        column: x => x.HighRiskPreDefinedParameterId,
                        principalTable: "PreDefinedRiskParameter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactor_PreDefinedRiskParameter_LowRiskPreDefinedParameterId",
                        column: x => x.LowRiskPreDefinedParameterId,
                        principalTable: "PreDefinedRiskParameter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactor_PreDefinedRiskParameter_MediumRiskPreDefinedParameterId",
                        column: x => x.MediumRiskPreDefinedParameterId,
                        principalTable: "PreDefinedRiskParameter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactor_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskSubFactorResponse",
                columns: table => new
                {
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    RiskSubFactorId = table.Column<int>(type: "int", nullable: false),
                    RiskRangeParameter = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Assumptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PreDefinedParameterId = table.Column<int>(type: "int", nullable: true),
                    ResponseDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskSubFactorResponse", x => new { x.CustomerId, x.RiskFactorId, x.RiskSubFactorId });
                    table.ForeignKey(
                        name: "FK_RiskSubFactorResponse_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactorResponse_PreDefinedRiskParameter_PreDefinedParameterId",
                        column: x => x.PreDefinedParameterId,
                        principalTable: "PreDefinedRiskParameter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactorResponse_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactorResponse_RiskSubFactor_RiskSubFactorId",
                        column: x => x.RiskSubFactorId,
                        principalTable: "RiskSubFactor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskSubFactorVolumeResponse",
                columns: table => new
                {
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    RiskSubFactorId = table.Column<int>(type: "int", nullable: false),
                    LowRiskScore = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MediumRiskScore = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HighRiskScore = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LowRiskVolume = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MediumRiskVolume = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HighRiskVolume = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LowRiskWeight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MediumRiskWeight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HighRiskWeight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LowRiskWeightedScore = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MediumRiskWeightedScore = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HighRiskWeightedScore = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Countries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryWiseRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryWiseVolume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskSubFactorVolumeResponse", x => new { x.CustomerId, x.RiskFactorId, x.RiskSubFactorId });
                    table.ForeignKey(
                        name: "FK_RiskSubFactorVolumeResponse_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactorVolumeResponse_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactorVolumeResponse_RiskSubFactor_RiskSubFactorId",
                        column: x => x.RiskSubFactorId,
                        principalTable: "RiskSubFactor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    ScaleRange2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ScaleRange5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ExcludeChildCategory = table.Column<bool>(type: "bit", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskType_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    UserTypeId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMaster_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    FormName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    View = table.Column<bool>(type: "bit", nullable: false),
                    Add = table.Column<bool>(type: "bit", nullable: false),
                    Edit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.FormId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_FormMaster_FormId",
                        column: x => x.FormId,
                        principalTable: "FormMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_RoleMaster_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMaster_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMaster_RoleMaster_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMaster_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMaster_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ScaleRange2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ScaleRange4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ScaleRange5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ExcludeChildCategory = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stages_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stages_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stages_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_RoleMaster_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRoles_UserMaster_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "DefaultScale",
                columns: new[] { "Id", "Name", "RankType", "ScaleType" },
                values: new object[,]
                {
                    { 1, "Weak", 1, 3 },
                    { 2, "Adequate", 2, 3 },
                    { 3, "Needs Improvement", 3, 3 },
                    { 4, "Weak", 1, 4 },
                    { 5, "Adequate", 2, 4 },
                    { 6, "Needs Improvement", 3, 4 },
                    { 7, "Strong", 4, 4 },
                    { 8, "Absent", 1, 5 },
                    { 9, "Weak", 2, 5 },
                    { 10, "Adequate", 3, 5 },
                    { 11, "Needs Improvement", 4, 5 },
                    { 12, "Strong", 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "FormMaster",
                columns: new[] { "Id", "Action", "Area", "Controller", "Description", "IconClass", "IsAdmin", "MenuId", "Name", "Sequence" },
                values: new object[] { 20, "Index", "", "RiskAssessment", null, "fas fa-diagnoses", false, null, "Risk Assessment", 1 });

            migrationBuilder.InsertData(
                table: "UserMaster",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "CustomerId", "Email", "IsActive", "Name", "Password", "RoleId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@admin.com", true, "Super Admin", "JbaCWJnsulrNRTnw1k2Ayw==", null, null, null });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Super Admin" },
                    { 2, "Admin" },
                    { 3, "User" }
                });

            migrationBuilder.InsertData(
                table: "MenuMaster",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "IconClass", "IsActive", "IsAdmin", "Name", "Sequence", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "fas fa-layer-group", true, true, "Masters", 1, null, null });

            migrationBuilder.InsertData(
                table: "MenuMaster",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "IconClass", "IsActive", "IsAdmin", "Name", "Sequence", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 2, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "fas fa-users-cog", true, true, "User Management", 2, null, null });

            migrationBuilder.InsertData(
                table: "RoleMaster",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "IsActive", "Name", "UpdatedBy", "UpdatedOn", "UserTypeId" },
                values: new object[] { 1, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "Super Admin", null, null, 1 });

            migrationBuilder.InsertData(
                table: "FormMaster",
                columns: new[] { "Id", "Action", "Area", "Controller", "Description", "IconClass", "IsAdmin", "MenuId", "Name", "Sequence" },
                values: new object[,]
                {
                    { 1, "Index", "Admin", "Roles", null, "fas fa-building", true, 2, "Role", 1 },
                    { 2, "Index", "Admin", "RolePermissions", null, "fas fa-building", true, 2, "Role Permission", 2 },
                    { 3, "Index", "Admin", "User", null, "fas fa-user-tie", true, 2, "User", 3 },
                    { 4, "Index", "Admin", "Country", null, "fas fa-globe", true, 1, "Country", 1 },
                    { 5, "Index", "Admin", "Customer", null, "fas fa-user-friends", true, 1, "Customer", 2 },
                    { 6, "Index", "Admin", "GeographyRisk", null, "fas fa-user-friends", true, 1, "Geography Risk - Country Risk Rating", 2 },
                    { 7, "Index", "Admin", "Stage", null, "fas fa-user-friends", true, 1, "Stage", 3 },
                    { 8, "Index", "Admin", "RiskType", null, "fas fa-asterisk", true, 1, "Risk Type", 4 },
                    { 9, "Index", "Admin", "GeographicPresence", null, "fas fa-user-friends", true, 1, "Geographic Presence", 5 },
                    { 10, "Index", "Admin", "CustomerSegment", null, "fas fa-user-friends", true, 1, "Customer Segment", 6 },
                    { 11, "Index", "Admin", "BusinessSegment", null, "fas fa-briefcase", true, 1, "Business Segment", 7 },
                    { 12, "Index", "Admin", "RiskFactor", null, "fas fa-industry", true, 1, "Risk Factor", 8 },
                    { 13, "Index", "Admin", "RiskSubFactor", null, "fa fa-industry", true, 1, "Risk Sub Factor", 9 },
                    { 14, "Index", "Admin", "PreDefinedRiskParameter", null, "fas fa-list-ul", true, 1, "Pre DefinedRisk Parameter", 10 },
                    { 15, "Index", "Admin", "ProductService", null, "fas fa-credit-card", true, 1, "Product Service", 11 },
                    { 16, "Index", "Admin", "Questions", null, "fas fa-question-circle", true, 1, "Question", 12 },
                    { 17, "Index", "Admin", "RiskCriteria", null, "fas fa-paw", true, 1, "Risk Criteria", 13 },
                    { 18, "Index", "Admin", "ProductServiceMapping", null, "fas fa-sitemap", true, 1, "Risk Factor Product Service Mapping", 14 },
                    { 19, "Index", "Admin", "ProductRiskCriteriaMapping", null, "fas fa-sitemap", true, 1, "Product Risk Criteria Mapping", 15 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "FormId", "RoleId", "Add", "Edit", "FormName", "View" },
                values: new object[] { 20, 1, true, true, "Risk Assessment", true });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "FormId", "RoleId", "Add", "Edit", "FormName", "View" },
                values: new object[,]
                {
                    { 1, 1, true, true, "Role", true },
                    { 2, 1, true, true, "Role Permission", true },
                    { 3, 1, true, true, "User", true },
                    { 4, 1, true, true, "Country", true },
                    { 5, 1, true, true, "Customer", true },
                    { 6, 1, true, true, "Geography Risk - Country Risk Rating", true },
                    { 7, 1, true, true, "Stage", true },
                    { 8, 1, true, true, "Risk Type", true },
                    { 9, 1, true, true, "Geographic Presence", true },
                    { 10, 1, true, true, "Customer Segment", true },
                    { 11, 1, true, true, "Business Segment", true },
                    { 12, 1, true, true, "Risk Factor", true },
                    { 13, 1, true, true, "Risk Sub Factor", true },
                    { 14, 1, true, true, "Pre DefinedRisk Parameter", true },
                    { 15, 1, true, true, "Product Service", true },
                    { 16, 1, true, true, "Question", true },
                    { 17, 1, true, true, "Risk Criteria", true },
                    { 18, 1, true, true, "Risk Factor Product Service Mapping", true },
                    { 19, 1, true, true, "Product Risk Criteria Mapping", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSegment_CreatedBy",
                table: "BusinessSegment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSegment_CustomerId",
                table: "BusinessSegment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSegment_CustomerSegmentId",
                table: "BusinessSegment",
                column: "CustomerSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSegment_UpdatedBy",
                table: "BusinessSegment",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CreatedBy",
                table: "Country",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Country_UpdatedBy",
                table: "Country",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CreatedBy",
                table: "Customer",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UpdatedBy",
                table: "Customer",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerForm_FormId",
                table: "CustomerForm",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLocation_CountryId",
                table: "CustomerLocation",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerScaleLabels_ScaleId",
                table: "CustomerScaleLabels",
                column: "ScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSegment_CreatedBy",
                table: "CustomerSegment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSegment_CustomerId",
                table: "CustomerSegment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSegment_GeographicPresenceId",
                table: "CustomerSegment",
                column: "GeographicPresenceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSegment_UpdatedBy",
                table: "CustomerSegment",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FormControlRoleMaster_CreatedBy",
                table: "FormControlRoleMaster",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FormControlRoleMaster_FormId",
                table: "FormControlRoleMaster",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormControlRoleMaster_RoleId",
                table: "FormControlRoleMaster",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_FormControlRoleMaster_UpdatedBy",
                table: "FormControlRoleMaster",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FormMaster_MenuId",
                table: "FormMaster",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_GeographicPresence_CountryId",
                table: "GeographicPresence",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_GeographicPresence_CreatedBy",
                table: "GeographicPresence",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GeographicPresence_CustomerId",
                table: "GeographicPresence",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_GeographicPresence_RiskTypeId",
                table: "GeographicPresence",
                column: "RiskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GeographicPresence_UpdatedBy",
                table: "GeographicPresence",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GeographyRisk_CountryId",
                table: "GeographyRisk",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_GeographyRisk_CreatedBy",
                table: "GeographyRisk",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GeographyRisk_CustomerId",
                table: "GeographyRisk",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_GeographyRisk_UpdatedBy",
                table: "GeographyRisk",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MenuMaster_CreatedBy",
                table: "MenuMaster",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MenuMaster_UpdatedBy",
                table: "MenuMaster",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PreDefinedRiskParameter_CreatedBy",
                table: "PreDefinedRiskParameter",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PreDefinedRiskParameter_CustomerId",
                table: "PreDefinedRiskParameter",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PreDefinedRiskParameter_UpdatedBy",
                table: "PreDefinedRiskParameter",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRiskCriteriaMapping_CreatedBy",
                table: "ProductRiskCriteriaMapping",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRiskCriteriaMapping_ProductId",
                table: "ProductRiskCriteriaMapping",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRiskCriteriaMapping_RiskCriteriaId",
                table: "ProductRiskCriteriaMapping",
                column: "RiskCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRiskCriteriaMapping_RiskFactorId",
                table: "ProductRiskCriteriaMapping",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRiskCriteriaMapping_RiskSubFactorId",
                table: "ProductRiskCriteriaMapping",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRiskCriteriaMapping_UpdatedBy",
                table: "ProductRiskCriteriaMapping",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductService_CreatedBy",
                table: "ProductService",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductService_CustomerId",
                table: "ProductService",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductService_UpdatedBy",
                table: "ProductService",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatedBy",
                table: "Questions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CustomerId",
                table: "Questions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ProductId",
                table: "Questions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UpdatedBy",
                table: "Questions",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsRiskCriteriaMapping_CreatedBy",
                table: "QuestionsRiskCriteriaMapping",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsRiskCriteriaMapping_ProductId",
                table: "QuestionsRiskCriteriaMapping",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsRiskCriteriaMapping_QuestionId",
                table: "QuestionsRiskCriteriaMapping",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsRiskCriteriaMapping_RiskCriteriaId",
                table: "QuestionsRiskCriteriaMapping",
                column: "RiskCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsRiskCriteriaMapping_RiskFactorId",
                table: "QuestionsRiskCriteriaMapping",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsRiskCriteriaMapping_RiskSubFactorId",
                table: "QuestionsRiskCriteriaMapping",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsRiskCriteriaMapping_UpdatedBy",
                table: "QuestionsRiskCriteriaMapping",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteria_CreatedBy",
                table: "RiskCriteria",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteria_CustomerId",
                table: "RiskCriteria",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteria_UpdatedBy",
                table: "RiskCriteria",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactor_BusinessSegmentId",
                table: "RiskFactor",
                column: "BusinessSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactor_CreatedBy",
                table: "RiskFactor",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactor_CustomerId",
                table: "RiskFactor",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactor_CustomerSegmentId",
                table: "RiskFactor",
                column: "CustomerSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactor_GeographicPresenceId",
                table: "RiskFactor",
                column: "GeographicPresenceId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactor_RiskTypeId",
                table: "RiskFactor",
                column: "RiskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactor_StageId",
                table: "RiskFactor",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactor_UpdatedBy",
                table: "RiskFactor",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactorProductServiceMapping_CreatedBy",
                table: "RiskFactorProductServiceMapping",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactorProductServiceMapping_ProductId",
                table: "RiskFactorProductServiceMapping",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactorProductServiceMapping_RiskFactorId",
                table: "RiskFactorProductServiceMapping",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactorProductServiceMapping_RiskSubFactorId",
                table: "RiskFactorProductServiceMapping",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactorProductServiceMapping_UpdatedBy",
                table: "RiskFactorProductServiceMapping",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactorResponse_CreatedBy",
                table: "RiskFactorResponse",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactorResponse_CustomerId",
                table: "RiskFactorResponse",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskFactorResponse_UpdatedBy",
                table: "RiskFactorResponse",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScoreProductVolumRatingResponse_CreatedBy",
                table: "RiskScoreProductVolumRatingResponse",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScoreProductVolumRatingResponse_ProductId",
                table: "RiskScoreProductVolumRatingResponse",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScoreProductVolumRatingResponse_RiskFactorId",
                table: "RiskScoreProductVolumRatingResponse",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScoreProductVolumRatingResponse_RiskSubFactorId",
                table: "RiskScoreProductVolumRatingResponse",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScoreProductVolumRatingResponse_UpdatedBy",
                table: "RiskScoreProductVolumRatingResponse",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScoreResponse_CreatedBy",
                table: "RiskScoreResponse",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScoreResponse_ProductId",
                table: "RiskScoreResponse",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScoreResponse_RiskCriteriaId",
                table: "RiskScoreResponse",
                column: "RiskCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScoreResponse_RiskFactorId",
                table: "RiskScoreResponse",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScoreResponse_RiskSubFactorId",
                table: "RiskScoreResponse",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScoreResponse_UpdatedBy",
                table: "RiskScoreResponse",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactor_CreatedBy",
                table: "RiskSubFactor",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactor_CustomerId",
                table: "RiskSubFactor",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactor_HighRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                column: "HighRiskPreDefinedParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactor_LowRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                column: "LowRiskPreDefinedParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactor_MediumRiskPreDefinedParameterId",
                table: "RiskSubFactor",
                column: "MediumRiskPreDefinedParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactor_RiskFactorId",
                table: "RiskSubFactor",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactor_UpdatedBy",
                table: "RiskSubFactor",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorResponse_CreatedBy",
                table: "RiskSubFactorResponse",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorResponse_PreDefinedParameterId",
                table: "RiskSubFactorResponse",
                column: "PreDefinedParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorResponse_RiskFactorId",
                table: "RiskSubFactorResponse",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorResponse_RiskSubFactorId",
                table: "RiskSubFactorResponse",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorResponse_UpdatedBy",
                table: "RiskSubFactorResponse",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorVolumeResponse_CreatedBy",
                table: "RiskSubFactorVolumeResponse",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorVolumeResponse_RiskFactorId",
                table: "RiskSubFactorVolumeResponse",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorVolumeResponse_RiskSubFactorId",
                table: "RiskSubFactorVolumeResponse",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorVolumeResponse_UpdatedBy",
                table: "RiskSubFactorVolumeResponse",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskType_CreatedBy",
                table: "RiskType",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskType_CustomerId",
                table: "RiskType",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskType_StageId",
                table: "RiskType",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskType_UpdatedBy",
                table: "RiskType",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMaster_CreatedBy",
                table: "RoleMaster",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMaster_UpdatedBy",
                table: "RoleMaster",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMaster_UserTypeId",
                table: "RoleMaster",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_FormId",
                table: "RolePermissions",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_CreatedBy",
                table: "Stages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_CustomerId",
                table: "Stages",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_UpdatedBy",
                table: "Stages",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserMaster_CreatedBy",
                table: "UserMaster",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserMaster_CustomerId",
                table: "UserMaster",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMaster_RoleId",
                table: "UserMaster",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMaster_UpdatedBy",
                table: "UserMaster",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessSegment_Customer_CustomerId",
                table: "BusinessSegment",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessSegment_CustomerSegment_CustomerSegmentId",
                table: "BusinessSegment",
                column: "CustomerSegmentId",
                principalTable: "CustomerSegment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessSegment_UserMaster_CreatedBy",
                table: "BusinessSegment",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessSegment_UserMaster_UpdatedBy",
                table: "BusinessSegment",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_UserMaster_CreatedBy",
                table: "Country",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_UserMaster_UpdatedBy",
                table: "Country",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_UserMaster_CreatedBy",
                table: "Customer",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_UserMaster_UpdatedBy",
                table: "Customer",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerForm_FormMaster_FormId",
                table: "CustomerForm",
                column: "FormId",
                principalTable: "FormMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSegment_GeographicPresence_GeographicPresenceId",
                table: "CustomerSegment",
                column: "GeographicPresenceId",
                principalTable: "GeographicPresence",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSegment_UserMaster_CreatedBy",
                table: "CustomerSegment",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSegment_UserMaster_UpdatedBy",
                table: "CustomerSegment",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormControlMaster_FormMaster_FormId",
                table: "FormControlMaster",
                column: "FormId",
                principalTable: "FormMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormControlRoleMaster_FormMaster_FormId",
                table: "FormControlRoleMaster",
                column: "FormId",
                principalTable: "FormMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormControlRoleMaster_RoleMaster_RoleId",
                table: "FormControlRoleMaster",
                column: "RoleId",
                principalTable: "RoleMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormControlRoleMaster_UserMaster_CreatedBy",
                table: "FormControlRoleMaster",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormControlRoleMaster_UserMaster_UpdatedBy",
                table: "FormControlRoleMaster",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormMaster_MenuMaster_MenuId",
                table: "FormMaster",
                column: "MenuId",
                principalTable: "MenuMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeographicPresence_RiskType_RiskTypeId",
                table: "GeographicPresence",
                column: "RiskTypeId",
                principalTable: "RiskType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeographicPresence_UserMaster_CreatedBy",
                table: "GeographicPresence",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeographicPresence_UserMaster_UpdatedBy",
                table: "GeographicPresence",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeographyRisk_UserMaster_CreatedBy",
                table: "GeographyRisk",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeographyRisk_UserMaster_UpdatedBy",
                table: "GeographyRisk",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuMaster_UserMaster_CreatedBy",
                table: "MenuMaster",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuMaster_UserMaster_UpdatedBy",
                table: "MenuMaster",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreDefinedRiskParameter_UserMaster_CreatedBy",
                table: "PreDefinedRiskParameter",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreDefinedRiskParameter_UserMaster_UpdatedBy",
                table: "PreDefinedRiskParameter",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRiskCriteriaMapping_ProductService_ProductId",
                table: "ProductRiskCriteriaMapping",
                column: "ProductId",
                principalTable: "ProductService",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRiskCriteriaMapping_RiskCriteria_RiskCriteriaId",
                table: "ProductRiskCriteriaMapping",
                column: "RiskCriteriaId",
                principalTable: "RiskCriteria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRiskCriteriaMapping_RiskFactor_RiskFactorId",
                table: "ProductRiskCriteriaMapping",
                column: "RiskFactorId",
                principalTable: "RiskFactor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRiskCriteriaMapping_RiskSubFactor_RiskSubFactorId",
                table: "ProductRiskCriteriaMapping",
                column: "RiskSubFactorId",
                principalTable: "RiskSubFactor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRiskCriteriaMapping_UserMaster_CreatedBy",
                table: "ProductRiskCriteriaMapping",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRiskCriteriaMapping_UserMaster_UpdatedBy",
                table: "ProductRiskCriteriaMapping",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductService_UserMaster_CreatedBy",
                table: "ProductService",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductService_UserMaster_UpdatedBy",
                table: "ProductService",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_UserMaster_CreatedBy",
                table: "Questions",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_UserMaster_UpdatedBy",
                table: "Questions",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsRiskCriteriaMapping_RiskCriteria_RiskCriteriaId",
                table: "QuestionsRiskCriteriaMapping",
                column: "RiskCriteriaId",
                principalTable: "RiskCriteria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsRiskCriteriaMapping_RiskFactor_RiskFactorId",
                table: "QuestionsRiskCriteriaMapping",
                column: "RiskFactorId",
                principalTable: "RiskFactor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsRiskCriteriaMapping_RiskSubFactor_RiskSubFactorId",
                table: "QuestionsRiskCriteriaMapping",
                column: "RiskSubFactorId",
                principalTable: "RiskSubFactor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsRiskCriteriaMapping_UserMaster_CreatedBy",
                table: "QuestionsRiskCriteriaMapping",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsRiskCriteriaMapping_UserMaster_UpdatedBy",
                table: "QuestionsRiskCriteriaMapping",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskCriteria_UserMaster_CreatedBy",
                table: "RiskCriteria",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskCriteria_UserMaster_UpdatedBy",
                table: "RiskCriteria",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskFactor_RiskType_RiskTypeId",
                table: "RiskFactor",
                column: "RiskTypeId",
                principalTable: "RiskType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskFactor_Stages_StageId",
                table: "RiskFactor",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskFactor_UserMaster_CreatedBy",
                table: "RiskFactor",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskFactor_UserMaster_UpdatedBy",
                table: "RiskFactor",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskFactorProductServiceMapping_RiskSubFactor_RiskSubFactorId",
                table: "RiskFactorProductServiceMapping",
                column: "RiskSubFactorId",
                principalTable: "RiskSubFactor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskFactorProductServiceMapping_UserMaster_CreatedBy",
                table: "RiskFactorProductServiceMapping",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskFactorProductServiceMapping_UserMaster_UpdatedBy",
                table: "RiskFactorProductServiceMapping",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskFactorResponse_UserMaster_CreatedBy",
                table: "RiskFactorResponse",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskFactorResponse_UserMaster_UpdatedBy",
                table: "RiskFactorResponse",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskScoreProductVolumRatingResponse_RiskSubFactor_RiskSubFactorId",
                table: "RiskScoreProductVolumRatingResponse",
                column: "RiskSubFactorId",
                principalTable: "RiskSubFactor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskScoreProductVolumRatingResponse_UserMaster_CreatedBy",
                table: "RiskScoreProductVolumRatingResponse",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskScoreProductVolumRatingResponse_UserMaster_UpdatedBy",
                table: "RiskScoreProductVolumRatingResponse",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskScoreResponse_RiskSubFactor_RiskSubFactorId",
                table: "RiskScoreResponse",
                column: "RiskSubFactorId",
                principalTable: "RiskSubFactor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskScoreResponse_UserMaster_CreatedBy",
                table: "RiskScoreResponse",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskScoreResponse_UserMaster_UpdatedBy",
                table: "RiskScoreResponse",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactor_UserMaster_CreatedBy",
                table: "RiskSubFactor",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactor_UserMaster_UpdatedBy",
                table: "RiskSubFactor",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactorResponse_UserMaster_CreatedBy",
                table: "RiskSubFactorResponse",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactorResponse_UserMaster_UpdatedBy",
                table: "RiskSubFactorResponse",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactorVolumeResponse_UserMaster_CreatedBy",
                table: "RiskSubFactorVolumeResponse",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskSubFactorVolumeResponse_UserMaster_UpdatedBy",
                table: "RiskSubFactorVolumeResponse",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskType_Stages_StageId",
                table: "RiskType",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskType_UserMaster_CreatedBy",
                table: "RiskType",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskType_UserMaster_UpdatedBy",
                table: "RiskType",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleMaster_UserMaster_CreatedBy",
                table: "RoleMaster",
                column: "CreatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleMaster_UserMaster_UpdatedBy",
                table: "RoleMaster",
                column: "UpdatedBy",
                principalTable: "UserMaster",
                principalColumn: "Id");

            migrationBuilder.UpdateSQLObjects();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMaster_Customer_CustomerId",
                table: "UserMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleMaster_UserMaster_CreatedBy",
                table: "RoleMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleMaster_UserMaster_UpdatedBy",
                table: "RoleMaster");

            migrationBuilder.DropTable(
                name: "CustomerForm");

            migrationBuilder.DropTable(
                name: "CustomerLocation");

            migrationBuilder.DropTable(
                name: "CustomerScaleLabels");

            migrationBuilder.DropTable(
                name: "FormControlMaster");

            migrationBuilder.DropTable(
                name: "FormControlRoleMaster");

            migrationBuilder.DropTable(
                name: "GeographyRisk");

            migrationBuilder.DropTable(
                name: "ProductRiskCriteriaMapping");

            migrationBuilder.DropTable(
                name: "QuestionsRiskCriteriaMapping");

            migrationBuilder.DropTable(
                name: "RiskFactorProductServiceMapping");

            migrationBuilder.DropTable(
                name: "RiskFactorResponse");

            migrationBuilder.DropTable(
                name: "RiskScoreProductVolumRatingResponse");

            migrationBuilder.DropTable(
                name: "RiskScoreResponse");

            migrationBuilder.DropTable(
                name: "RiskSubFactorResponse");

            migrationBuilder.DropTable(
                name: "RiskSubFactorVolumeResponse");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "DefaultScale");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "RiskCriteria");

            migrationBuilder.DropTable(
                name: "RiskSubFactor");

            migrationBuilder.DropTable(
                name: "FormMaster");

            migrationBuilder.DropTable(
                name: "ProductService");

            migrationBuilder.DropTable(
                name: "PreDefinedRiskParameter");

            migrationBuilder.DropTable(
                name: "RiskFactor");

            migrationBuilder.DropTable(
                name: "MenuMaster");

            migrationBuilder.DropTable(
                name: "BusinessSegment");

            migrationBuilder.DropTable(
                name: "CustomerSegment");

            migrationBuilder.DropTable(
                name: "GeographicPresence");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "RiskType");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "UserMaster");

            migrationBuilder.DropTable(
                name: "RoleMaster");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
