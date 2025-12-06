using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentProjects.DataLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Mentors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectUser",
                columns: table => new
                {
                    MentorProjectsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MentorsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => new { x.MentorProjectsId, x.MentorsId });
                    table.ForeignKey(
                        name: "FK_ProjectUser_Projects_MentorProjectsId",
                        column: x => x.MentorProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUser_Users_MentorsId",
                        column: x => x.MentorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_MentorsId",
                table: "ProjectUser",
                column: "MentorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUser");
        }
    }
}
