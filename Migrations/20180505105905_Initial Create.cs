using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MultiLang.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    AdminId = table.Column<string>(nullable: false),
                    members = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    blobkedIdsJson = table.Column<string>(nullable: false),
                    displayName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    firebaseToken = table.Column<string>(nullable: false),
                    fullName = table.Column<string>(nullable: true),
                    invitationsJson = table.Column<string>(nullable: false),
                    langCode = table.Column<string>(nullable: true),
                    language = table.Column<string>(nullable: true),
                    passwordHash = table.Column<string>(maxLength: 64, nullable: false),
                    translationEngine = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
