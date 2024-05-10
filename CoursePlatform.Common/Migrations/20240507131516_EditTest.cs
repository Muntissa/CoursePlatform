using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursePlatform.Common.Migrations
{
    /// <inheritdoc />
    public partial class EditTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Test_LectureId",
                table: "Test");

            migrationBuilder.CreateIndex(
                name: "IX_Test_LectureId",
                table: "Test",
                column: "LectureId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Test_LectureId",
                table: "Test");

            migrationBuilder.CreateIndex(
                name: "IX_Test_LectureId",
                table: "Test",
                column: "LectureId");
        }
    }
}
