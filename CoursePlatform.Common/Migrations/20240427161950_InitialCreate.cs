using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoursePlatform.Common.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Progress",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompletionStatus = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: true),
                    Total = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseTitle = table.Column<string>(type: "text", nullable: false),
                    CourseDecription = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseEnrollment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnrollmentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    ProgressId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEnrollment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseEnrollment_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseEnrollment_Progress_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "Progress",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseEnrollment_User_StudentId",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lecture",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: false),
                    OrderInCourse = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    ProgressId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecture_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lecture_Progress_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "Progress",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: true),
                    CourseEnrollmentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificate_CourseEnrollment_CourseEnrollmentId",
                        column: x => x.CourseEnrollmentId,
                        principalTable: "CourseEnrollment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdditionalFile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<int>(type: "integer", nullable: false),
                    LectureId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalFile_Lecture_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LectureMaterial",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    LectureId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureMaterial_Lecture_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    LectureId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Test_Lecture_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VideoId = table.Column<long>(type: "bigint", nullable: false),
                    VideoURL = table.Column<string>(type: "text", nullable: false),
                    LectureId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_Lecture_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    QuestionType = table.Column<int>(type: "integer", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    TestId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Test_TestId",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalFile_LectureId",
                table: "AdditionalFile",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CourseId",
                table: "Category",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_CourseEnrollmentId",
                table: "Certificate",
                column: "CourseEnrollmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_UserId",
                table: "Course",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollment_CourseId",
                table: "CourseEnrollment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollment_ProgressId",
                table: "CourseEnrollment",
                column: "ProgressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollment_StudentId",
                table: "CourseEnrollment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecture_CourseId",
                table: "Lecture",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecture_ProgressId",
                table: "Lecture",
                column: "ProgressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LectureMaterial_LectureId",
                table: "LectureMaterial",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_UserId",
                table: "Profile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_TestId",
                table: "Question",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_CourseId",
                table: "Test",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Test_LectureId",
                table: "Test",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_LectureId",
                table: "Video",
                column: "LectureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalFile");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "LectureMaterial");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "CourseEnrollment");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Lecture");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Progress");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
