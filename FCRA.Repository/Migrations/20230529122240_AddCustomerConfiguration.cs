using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCRA.Repository.Migrations
{
    public partial class AddCustomerConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskSubFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 31)
                .OldAnnotation("Relational:ColumnOrder", 29);

            migrationBuilder.AddColumn<bool>(
                name: "isAttachmentApplicable",
                table: "RiskSubFactor",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 29);

            migrationBuilder.AddColumn<bool>(
                name: "isAttachmentMandatory",
                table: "RiskSubFactor",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 30);

            migrationBuilder.CreateTable(
                name: "CustomerConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldId = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerConfiguration", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CustomerConfiguration",
                columns: new[] { "Id", "FieldId", "FieldName", "Visible" },
                values: new object[,]
                {
                    { 1, 8, "Stage", false },
                    { 2, 9, "Risk Type", false },
                    { 3, 10, "Geographic Presence", false },
                    { 4, 11, "Business Segment", false },
                    { 5, 12, "Sub Unit", false }
                });

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Action", "Controller" },
                values: new object[] { "Index", "CustomerConfiguration" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerConfiguration");

            migrationBuilder.DropColumn(
                name: "isAttachmentApplicable",
                table: "RiskSubFactor");

            migrationBuilder.DropColumn(
                name: "isAttachmentMandatory",
                table: "RiskSubFactor");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RiskSubFactor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 29)
                .OldAnnotation("Relational:ColumnOrder", 31);

            migrationBuilder.UpdateData(
                table: "FormMaster",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Action", "Controller" },
                values: new object[] { "CustomerConfiguration", "Customer" });
        }
    }
}
