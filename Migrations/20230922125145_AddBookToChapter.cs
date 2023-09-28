using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWriting.Migrations
{
    /// <inheritdoc />
    public partial class AddBookToChapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Chapters_BookID",
                table: "Chapters",
                column: "BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Books_BookID",
                table: "Chapters",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Books_BookID",
                table: "Chapters");

            migrationBuilder.DropIndex(
                name: "IX_Chapters_BookID",
                table: "Chapters");
        }
    }
}
