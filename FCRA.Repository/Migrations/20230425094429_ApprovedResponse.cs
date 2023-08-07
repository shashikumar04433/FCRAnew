using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class ApprovedResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovedRiskFactorResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_ApprovedRiskFactorResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedRiskFactorResponse_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskFactorResponse_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskFactorResponse_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskFactorResponse_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovedRiskScoreProductVolumRatingResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    RiskSubFactorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TotalScore = table.Column<int>(type: "int", nullable: true),
                    FinalScore = table.Column<int>(type: "int", nullable: true),
                    RiskRating = table.Column<int>(type: "int", nullable: false),
                    Values = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedRiskScoreProductVolumRatingResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreProductVolumRatingResponse_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreProductVolumRatingResponse_ProductService_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductService",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreProductVolumRatingResponse_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreProductVolumRatingResponse_RiskSubFactor_RiskSubFactorId",
                        column: x => x.RiskSubFactorId,
                        principalTable: "RiskSubFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreProductVolumRatingResponse_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreProductVolumRatingResponse_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovedRiskScoreResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_ApprovedRiskScoreResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreResponse_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreResponse_ProductService_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductService",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreResponse_RiskCriteria_RiskCriteriaId",
                        column: x => x.RiskCriteriaId,
                        principalTable: "RiskCriteria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreResponse_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreResponse_RiskSubFactor_RiskSubFactorId",
                        column: x => x.RiskSubFactorId,
                        principalTable: "RiskSubFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreResponse_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskScoreResponse_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovedRiskSubFactorAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskSubFactorId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedRiskSubFactorAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorAttachment_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorAttachment_RiskSubFactor_RiskSubFactorId",
                        column: x => x.RiskSubFactorId,
                        principalTable: "RiskSubFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorAttachment_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorAttachment_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovedRiskSubFactorResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    RiskSubFactorId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Assumptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PreDefinedParameterId = table.Column<int>(type: "int", nullable: true),
                    ResponseDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScaleResponse = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedRiskSubFactorResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorResponse_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorResponse_PreDefinedRiskParameter_PreDefinedParameterId",
                        column: x => x.PreDefinedParameterId,
                        principalTable: "PreDefinedRiskParameter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorResponse_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorResponse_RiskSubFactor_RiskSubFactorId",
                        column: x => x.RiskSubFactorId,
                        principalTable: "RiskSubFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorResponse_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorResponse_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovedRiskSubFactorVolumeResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskFactorId = table.Column<int>(type: "int", nullable: false),
                    RiskSubFactorId = table.Column<int>(type: "int", nullable: false),
                    Score1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Score2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Score3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Score4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Score5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Volume1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Volume2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Volume3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Volume4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Volume5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Weight1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Weight5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    WeightedScore1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WeightedScore2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WeightedScore3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WeightedScore4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    WeightedScore5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
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
                    table.PrimaryKey("PK_ApprovedRiskSubFactorVolumeResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorVolumeResponse_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorVolumeResponse_RiskFactor_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalTable: "RiskFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorVolumeResponse_RiskSubFactor_RiskSubFactorId",
                        column: x => x.RiskSubFactorId,
                        principalTable: "RiskSubFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorVolumeResponse_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApprovedRiskSubFactorVolumeResponse_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ApprovalStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Approved" });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskFactorResponse_CreatedBy",
                table: "ApprovedRiskFactorResponse",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskFactorResponse_CustomerId",
                table: "ApprovedRiskFactorResponse",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskFactorResponse_RiskFactorId",
                table: "ApprovedRiskFactorResponse",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskFactorResponse_UpdatedBy",
                table: "ApprovedRiskFactorResponse",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreProductVolumRatingResponse_CreatedBy",
                table: "ApprovedRiskScoreProductVolumRatingResponse",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreProductVolumRatingResponse_CustomerId",
                table: "ApprovedRiskScoreProductVolumRatingResponse",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreProductVolumRatingResponse_ProductId",
                table: "ApprovedRiskScoreProductVolumRatingResponse",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreProductVolumRatingResponse_RiskFactorId",
                table: "ApprovedRiskScoreProductVolumRatingResponse",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreProductVolumRatingResponse_RiskSubFactorId",
                table: "ApprovedRiskScoreProductVolumRatingResponse",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreProductVolumRatingResponse_UpdatedBy",
                table: "ApprovedRiskScoreProductVolumRatingResponse",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreResponse_CreatedBy",
                table: "ApprovedRiskScoreResponse",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreResponse_CustomerId",
                table: "ApprovedRiskScoreResponse",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreResponse_ProductId",
                table: "ApprovedRiskScoreResponse",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreResponse_RiskCriteriaId",
                table: "ApprovedRiskScoreResponse",
                column: "RiskCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreResponse_RiskFactorId",
                table: "ApprovedRiskScoreResponse",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreResponse_RiskSubFactorId",
                table: "ApprovedRiskScoreResponse",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskScoreResponse_UpdatedBy",
                table: "ApprovedRiskScoreResponse",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorAttachment_CreatedBy",
                table: "ApprovedRiskSubFactorAttachment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorAttachment_CustomerId",
                table: "ApprovedRiskSubFactorAttachment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorAttachment_RiskSubFactorId",
                table: "ApprovedRiskSubFactorAttachment",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorAttachment_UpdatedBy",
                table: "ApprovedRiskSubFactorAttachment",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorResponse_CreatedBy",
                table: "ApprovedRiskSubFactorResponse",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorResponse_CustomerId",
                table: "ApprovedRiskSubFactorResponse",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorResponse_PreDefinedParameterId",
                table: "ApprovedRiskSubFactorResponse",
                column: "PreDefinedParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorResponse_RiskFactorId",
                table: "ApprovedRiskSubFactorResponse",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorResponse_RiskSubFactorId",
                table: "ApprovedRiskSubFactorResponse",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorResponse_UpdatedBy",
                table: "ApprovedRiskSubFactorResponse",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorVolumeResponse_CreatedBy",
                table: "ApprovedRiskSubFactorVolumeResponse",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorVolumeResponse_CustomerId",
                table: "ApprovedRiskSubFactorVolumeResponse",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorVolumeResponse_RiskFactorId",
                table: "ApprovedRiskSubFactorVolumeResponse",
                column: "RiskFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorVolumeResponse_RiskSubFactorId",
                table: "ApprovedRiskSubFactorVolumeResponse",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRiskSubFactorVolumeResponse_UpdatedBy",
                table: "ApprovedRiskSubFactorVolumeResponse",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovedRiskFactorResponse");

            migrationBuilder.DropTable(
                name: "ApprovedRiskScoreProductVolumRatingResponse");

            migrationBuilder.DropTable(
                name: "ApprovedRiskScoreResponse");

            migrationBuilder.DropTable(
                name: "ApprovedRiskSubFactorAttachment");

            migrationBuilder.DropTable(
                name: "ApprovedRiskSubFactorResponse");

            migrationBuilder.DropTable(
                name: "ApprovedRiskSubFactorVolumeResponse");

            migrationBuilder.DeleteData(
                table: "ApprovalStatus",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
