using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddPictureColumnToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "User");
        }
    }
}
