using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentProjects.DataLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class CaseStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaseStatus",
                table: "Cases",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Cases",
                newName: "CaseStatus");
        }
    }
}
