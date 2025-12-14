using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentProjects.DataLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentPosRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamStudents_StudentPosition_PositionId",
                table: "TeamStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentPosition",
                table: "StudentPosition");

            migrationBuilder.RenameTable(
                name: "StudentPosition",
                newName: "StudentPositions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentPositions",
                table: "StudentPositions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamStudents_StudentPositions_PositionId",
                table: "TeamStudents",
                column: "PositionId",
                principalTable: "StudentPositions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamStudents_StudentPositions_PositionId",
                table: "TeamStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentPositions",
                table: "StudentPositions");

            migrationBuilder.RenameTable(
                name: "StudentPositions",
                newName: "StudentPosition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentPosition",
                table: "StudentPosition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamStudents_StudentPosition_PositionId",
                table: "TeamStudents",
                column: "PositionId",
                principalTable: "StudentPosition",
                principalColumn: "Id");
        }
    }
}
