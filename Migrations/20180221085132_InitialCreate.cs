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
                    displayName = table.Column<string>(nullable: false),
                    firebaseToken = table.Column<string>(nullable: false),
                    invitationsJson = table.Column<string>(nullable: false),
                    langCode = table.Column<string>(nullable: false),
                    language = table.Column<string>(nullable: false),
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
