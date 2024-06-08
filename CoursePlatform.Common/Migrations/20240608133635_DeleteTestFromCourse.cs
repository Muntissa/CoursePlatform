using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursePlatform.Common.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTestFromCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Course_CourseId",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Test_CourseId",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Test");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Test",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Test_CourseId",
                table: "Test",
                column: "CourseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Course_CourseId",
                table: "Test",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");
        }
    }
}
