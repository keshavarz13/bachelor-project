using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Social.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<long>(type: "bigint", nullable: true),
                    BookCategory = table.Column<int>(type: "int", nullable: false),
                    Summery = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Follow",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Follower = table.Column<long>(type: "bigint", nullable: false),
                    Followed = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostType = table.Column<int>(type: "int", nullable: false),
                    RelatedPost = table.Column<long>(type: "bigint", nullable: false),
                    RelatedBook = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Read",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    Score = table.Column<short>(type: "smallint", nullable: false),
                    Page = table.Column<int>(type: "int", nullable: false),
                    ReadStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Read", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Follow");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Read");
        }
    }
}
