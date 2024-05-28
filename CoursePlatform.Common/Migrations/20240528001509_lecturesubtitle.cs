using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursePlatform.Common.Migrations
{
    /// <inheritdoc />
    public partial class lecturesubtitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Lecture",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Lecture");

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Course",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
