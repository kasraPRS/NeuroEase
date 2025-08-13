using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuroEase.Core.Migrations
{
    /// <inheritdoc />
    public partial class companyDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "companyDatas",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revenue = table.Column<double>(type: "float", nullable: false),
                    Expenses = table.Column<double>(type: "float", nullable: false),
                    ProfitMargin = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companyDatas", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "companyDatas");
        }
    }
}
