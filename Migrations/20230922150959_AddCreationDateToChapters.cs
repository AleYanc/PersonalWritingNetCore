using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWriting.Migrations
{
    /// <inheritdoc />
    public partial class AddCreationDateToChapters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChapterNumber",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Chapters",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChapterNumber",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Chapters");
        }
    }
}
