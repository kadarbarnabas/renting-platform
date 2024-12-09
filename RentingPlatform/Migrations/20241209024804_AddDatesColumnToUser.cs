using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddDatesColumnToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "dates",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dates",
                table: "User");
        }
    }
}
