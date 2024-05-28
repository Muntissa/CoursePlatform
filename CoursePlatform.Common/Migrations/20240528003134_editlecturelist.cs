using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursePlatform.Common.Migrations
{
    /// <inheritdoc />
    public partial class editlecturelist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalFile_Lecture_LectureId",
                table: "AdditionalFile");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureMaterial_Lecture_LectureId",
                table: "LectureMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Lecture_LectureId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_LectureId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_LectureMaterial_LectureId",
                table: "LectureMaterial");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalFile_LectureId",
                table: "AdditionalFile");

            migrationBuilder.AlterColumn<long>(
                name: "LectureId",
                table: "Video",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "LectureId",
                table: "LectureMaterial",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "LectureId",
                table: "AdditionalFile",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Video_LectureId",
                table: "Video",
                column: "LectureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LectureMaterial_LectureId",
                table: "LectureMaterial",
                column: "LectureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalFile_LectureId",
                table: "AdditionalFile",
                column: "LectureId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalFile_Lecture_LectureId",
                table: "AdditionalFile",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureMaterial_Lecture_LectureId",
                table: "LectureMaterial",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Lecture_LectureId",
                table: "Video",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalFile_Lecture_LectureId",
                table: "AdditionalFile");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureMaterial_Lecture_LectureId",
                table: "LectureMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Lecture_LectureId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_LectureId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_LectureMaterial_LectureId",
                table: "LectureMaterial");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalFile_LectureId",
                table: "AdditionalFile");

            migrationBuilder.AlterColumn<long>(
                name: "LectureId",
                table: "Video",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LectureId",
                table: "LectureMaterial",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LectureId",
                table: "AdditionalFile",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Video_LectureId",
                table: "Video",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureMaterial_LectureId",
                table: "LectureMaterial",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalFile_LectureId",
                table: "AdditionalFile",
                column: "LectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalFile_Lecture_LectureId",
                table: "AdditionalFile",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureMaterial_Lecture_LectureId",
                table: "LectureMaterial",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Lecture_LectureId",
                table: "Video",
                column: "LectureId",
                principalTable: "Lecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
