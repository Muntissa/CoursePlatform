using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursePlatform.Common.Migrations
{
    /// <inheritdoc />
    public partial class ProgressList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecture_Progress_ProgressId",
                table: "Lecture");

            migrationBuilder.DropIndex(
                name: "IX_Lecture_ProgressId",
                table: "Lecture");

            migrationBuilder.DropColumn(
                name: "ProgressId",
                table: "Lecture");

            migrationBuilder.AddColumn<long>(
                name: "LectureId",
                table: "Progress",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progress_LectureId",
                table: "Progress",
                column: "LectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Lecture_LectureId",
                table: "Progress",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Lecture_LectureId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_LectureId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "LectureId",
                table: "Progress");

            migrationBuilder.AddColumn<long>(
                name: "ProgressId",
                table: "Lecture",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lecture_ProgressId",
                table: "Lecture",
                column: "ProgressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecture_Progress_ProgressId",
                table: "Lecture",
                column: "ProgressId",
                principalTable: "Progress",
                principalColumn: "Id");
        }
    }
}
