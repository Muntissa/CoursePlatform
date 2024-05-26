﻿// <auto-generated />
using System;
using CoursePlatform.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoursePlatform.Common.Migrations
{
    [DbContext(typeof(CoursePlatformContext))]
    [Migration("20240526152131_CEStudentConnection")]
    partial class CEStudentConnection
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryCourse", b =>
                {
                    b.Property<long>("CourseCategoriesId")
                        .HasColumnType("bigint");

                    b.Property<long>("CoursesId")
                        .HasColumnType("bigint");

                    b.HasKey("CourseCategoriesId", "CoursesId");

                    b.HasIndex("CoursesId");

                    b.ToTable("CategoryCourse");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.AdditionalFile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FileType")
                        .HasColumnType("integer");

                    b.Property<long>("LectureId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.ToTable("AdditionalFile");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Certificate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("CourseEnrollmentId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseEnrollmentId")
                        .IsUnique();

                    b.ToTable("Certificate");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Complexity")
                        .HasColumnType("integer");

                    b.Property<string>("CourseDecription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CourseTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.CourseEnrollment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseEnrollment");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Lecture", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<int>("OrderInCourse")
                        .HasColumnType("integer");

                    b.Property<long?>("ProgressId")
                        .HasColumnType("bigint");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProgressId")
                        .IsUnique();

                    b.ToTable("Lecture");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.LectureMaterial", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("LectureId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.ToTable("LectureMaterial");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Profile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Progress", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int?>("CompletionStatus")
                        .HasColumnType("integer");

                    b.Property<long>("CourseEnrollmentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CourseEnrollmentId");

                    b.ToTable("Progress");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<int>("QuestionType")
                        .HasColumnType("integer");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Test", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<long?>("LectureId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CourseId")
                        .IsUnique();

                    b.HasIndex("LectureId")
                        .IsUnique();

                    b.ToTable("Test");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Video", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("LectureId")
                        .HasColumnType("bigint");

                    b.Property<string>("VideoURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.ToTable("Video");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CategoryCourse", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CourseCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoursePlatform.Common.Entities.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.AdditionalFile", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.Lecture", "Lecture")
                        .WithMany("AdditionalFiles")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Certificate", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.CourseEnrollment", "CourseEnrollment")
                        .WithOne("Certificate")
                        .HasForeignKey("CoursePlatform.Common.Entities.Certificate", "CourseEnrollmentId");

                    b.Navigation("CourseEnrollment");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Course", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.User", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("UserId");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.CourseEnrollment", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.Course", "Course")
                        .WithMany("CourseEnrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoursePlatform.Common.Entities.User", "Student")
                        .WithMany("CourseEnrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Lecture", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.Course", "Course")
                        .WithMany("Lectures")
                        .HasForeignKey("CourseId");

                    b.HasOne("CoursePlatform.Common.Entities.Progress", "Progress")
                        .WithOne("Lecture")
                        .HasForeignKey("CoursePlatform.Common.Entities.Lecture", "ProgressId");

                    b.Navigation("Course");

                    b.Navigation("Progress");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.LectureMaterial", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.Lecture", "Lecture")
                        .WithMany("LectureMaterials")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Profile", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("CoursePlatform.Common.Entities.Profile", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Progress", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.CourseEnrollment", "Enrollment")
                        .WithMany("Progreses")
                        .HasForeignKey("CourseEnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enrollment");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Question", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Test", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.Course", "Course")
                        .WithOne("FinalTest")
                        .HasForeignKey("CoursePlatform.Common.Entities.Test", "CourseId");

                    b.HasOne("CoursePlatform.Common.Entities.Lecture", "Lecture")
                        .WithOne("Test")
                        .HasForeignKey("CoursePlatform.Common.Entities.Test", "LectureId");

                    b.Navigation("Course");

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Video", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.Lecture", "Lecture")
                        .WithMany("Videos")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoursePlatform.Common.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("CoursePlatform.Common.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Course", b =>
                {
                    b.Navigation("CourseEnrollments");

                    b.Navigation("FinalTest");

                    b.Navigation("Lectures");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.CourseEnrollment", b =>
                {
                    b.Navigation("Certificate");

                    b.Navigation("Progreses");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Lecture", b =>
                {
                    b.Navigation("AdditionalFiles");

                    b.Navigation("LectureMaterials");

                    b.Navigation("Test");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Progress", b =>
                {
                    b.Navigation("Lecture")
                        .IsRequired();
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.Test", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("CoursePlatform.Common.Entities.User", b =>
                {
                    b.Navigation("CourseEnrollments");

                    b.Navigation("Courses");

                    b.Navigation("Profile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
