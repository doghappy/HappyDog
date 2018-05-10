using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HappyDog.Domain.Migrations
{
    public partial class CategoryAddFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Categories",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Categories",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IconClass",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Categories",
                newName: "Name");
        }
    }
}
