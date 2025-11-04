using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentProjects.Dal.Data.Migrations
{
    /// <inheritdoc />
    public partial class VirtualAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cases_AuthorId",
                table: "Cases",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Users_AuthorId",
                table: "Cases",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Users_AuthorId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_AuthorId",
                table: "Cases");
        }
    }
}
