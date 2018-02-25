using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    blobkedIdsJson = table.Column<string>(nullable: false),
                    displayName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    firebaseToken = table.Column<string>(nullable: false),
                    fullName = table.Column<string>(nullable: true),
                    invitationsJson = table.Column<string>(nullable: false),
                    langCode = table.Column<string>(nullable: true),
                    language = table.Column<string>(nullable: true),
                    passwordHash = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
