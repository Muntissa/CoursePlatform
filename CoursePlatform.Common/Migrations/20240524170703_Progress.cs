using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursePlatform.Common.Migrations
{
    /// <inheritdoc />
    public partial class Progress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollment_Progress_ProgressId",
                table: "CourseEnrollment");

            migrationBuilder.DropIndex(
                name: "IX_CourseEnrollment_ProgressId",
                table: "CourseEnrollment");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "ProgressId",
                table: "CourseEnrollment");

            migrationBuilder.AlterColumn<int>(
                name: "CompletionStatus",
                table: "Progress",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "CourseEnrollmentId",
                table: "Progress",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Progress_CourseEnrollmentId",
                table: "Progress",
                column: "CourseEnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_CourseEnrollment_CourseEnrollmentId",
                table: "Progress",
                column: "CourseEnrollmentId",
                principalTable: "CourseEnrollment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_CourseEnrollment_CourseEnrollmentId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_CourseEnrollmentId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "CourseEnrollmentId",
                table: "Progress");

            migrationBuilder.AddColumn<long>(
                name: "VideoId",
                table: "Video",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "CompletionStatus",
                table: "Progress",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Progress",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Progress",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProgressId",
                table: "CourseEnrollment",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollment_ProgressId",
                table: "CourseEnrollment",
                column: "ProgressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollment_Progress_ProgressId",
                table: "CourseEnrollment",
                column: "ProgressId",
                principalTable: "Progress",
                principalColumn: "Id");
        }
    }
}
