using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursePlatform.Common.Migrations
{
    /// <inheritdoc />
    public partial class EditCourseTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Course_CourseId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_CourseId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Category");

            migrationBuilder.CreateTable(
                name: "CategoryCourse",
                columns: table => new
                {
                    CourseCategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    CoursesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCourse", x => new { x.CourseCategoriesId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_CategoryCourse_Category_CourseCategoriesId",
                        column: x => x.CourseCategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryCourse_Course_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCourse_CoursesId",
                table: "CategoryCourse",
                column: "CoursesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryCourse");

            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Category",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_CourseId",
                table: "Category",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Course_CourseId",
                table: "Category",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");
        }
    }
}
