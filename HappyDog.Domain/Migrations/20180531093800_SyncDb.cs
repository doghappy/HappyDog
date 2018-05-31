using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HappyDog.Domain.Migrations
{
    public partial class SyncDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BingWallpapers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BingWallpapers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Copyright = table.Column<string>(type: "nvarchar", maxLength: 400, nullable: false),
                    CopyrightLink = table.Column<string>(maxLength: 100, nullable: false),
                    Url = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BingWallpapers", x => x.Id);
                });
        }
    }
}
