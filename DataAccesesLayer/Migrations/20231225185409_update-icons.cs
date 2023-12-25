using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateicons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconTitle",
                table: "Icons");

            migrationBuilder.AddColumn<string>(
                name: "IIcon",
                table: "Icons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Icons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IIcon",
                table: "Icons");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Icons");

            migrationBuilder.AddColumn<string>(
                name: "IconTitle",
                table: "Icons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
