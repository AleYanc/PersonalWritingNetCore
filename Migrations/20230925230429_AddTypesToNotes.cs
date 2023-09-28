using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWriting.Migrations
{
    /// <inheritdoc />
    public partial class AddTypesToNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Notes");
        }
    }
}
