using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursePlatform.Common.Migrations
{
    /// <inheritdoc />
    public partial class Comeplexity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Complexity",
                table: "Course",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complexity",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "User",
                type: "integer",
                nullable: true);
        }
    }
}
