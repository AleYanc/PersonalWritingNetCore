using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWriting.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToWB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorldBuildingDetail",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorldBuildingDetail");
        }
    }
}
