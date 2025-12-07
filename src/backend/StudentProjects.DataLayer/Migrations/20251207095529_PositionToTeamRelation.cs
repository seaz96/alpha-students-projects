using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentProjects.DataLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class PositionToTeamRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentPosition_PositionId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamStudents_StudentPosition_PositionId",
                table: "TeamStudents");

            migrationBuilder.DropIndex(
                name: "IX_Students_PositionId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Students");

            migrationBuilder.AlterColumn<Guid>(
                name: "PositionId",
                table: "TeamStudents",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamStudents_StudentPosition_PositionId",
                table: "TeamStudents",
                column: "PositionId",
                principalTable: "StudentPosition",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamStudents_StudentPosition_PositionId",
                table: "TeamStudents");

            migrationBuilder.AlterColumn<Guid>(
                name: "PositionId",
                table: "TeamStudents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "Students",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_PositionId",
                table: "Students",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentPosition_PositionId",
                table: "Students",
                column: "PositionId",
                principalTable: "StudentPosition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamStudents_StudentPosition_PositionId",
                table: "TeamStudents",
                column: "PositionId",
                principalTable: "StudentPosition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
