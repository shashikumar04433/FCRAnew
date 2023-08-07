using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class SubFactorAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskSubFactorAttachment",
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
                    table.PrimaryKey("PK_RiskSubFactorAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskSubFactorAttachment_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactorAttachment_RiskSubFactor_RiskSubFactorId",
                        column: x => x.RiskSubFactorId,
                        principalTable: "RiskSubFactor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactorAttachment_UserMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RiskSubFactorAttachment_UserMaster_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorAttachment_CreatedBy",
                table: "RiskSubFactorAttachment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorAttachment_CustomerId",
                table: "RiskSubFactorAttachment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorAttachment_RiskSubFactorId",
                table: "RiskSubFactorAttachment",
                column: "RiskSubFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubFactorAttachment_UpdatedBy",
                table: "RiskSubFactorAttachment",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiskSubFactorAttachment");
        }
    }
}
