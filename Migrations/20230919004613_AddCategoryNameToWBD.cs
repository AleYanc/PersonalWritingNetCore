using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWriting.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryNameToWBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorldBuildingDetail_WorldBuildingCategories_WorldBuildingCategoryID",
                table: "WorldBuildingDetail");

            migrationBuilder.DropIndex(
                name: "IX_WorldBuildingDetail_WorldBuildingCategoryID",
                table: "WorldBuildingDetail");

            migrationBuilder.AddColumn<string>(
                name: "WorldBuildingCategoryName",
                table: "WorldBuildingDetail",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorldBuildingCategoryName",
                table: "WorldBuildingDetail");

            migrationBuilder.CreateIndex(
                name: "IX_WorldBuildingDetail_WorldBuildingCategoryID",
                table: "WorldBuildingDetail",
                column: "WorldBuildingCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_WorldBuildingDetail_WorldBuildingCategories_WorldBuildingCategoryID",
                table: "WorldBuildingDetail",
                column: "WorldBuildingCategoryID",
                principalTable: "WorldBuildingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
