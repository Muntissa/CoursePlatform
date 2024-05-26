using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoursePlatform.Common.Migrations
{
    /// <inheritdoc />
    public partial class TestEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Test_TestId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Question");

            migrationBuilder.AlterColumn<long>(
                name: "TestId",
                table: "Question",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnswerContent = table.Column<string>(type: "text", nullable: false),
                    AnswerType = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Test_TestId",
                table: "Question",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Test_TestId",
                table: "Question");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.AlterColumn<long>(
                name: "TestId",
                table: "Question",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Question",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Test_TestId",
                table: "Question",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
