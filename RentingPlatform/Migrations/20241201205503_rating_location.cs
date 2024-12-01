using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class rating_location : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Airbnb",
                newName: "ImageUrls");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Airbnb",
                newName: "Country");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Cars",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Cars",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "Airbnb",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Bathrooms",
                table: "Airbnb",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Beds",
                table: "Airbnb",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rooms",
                table: "Airbnb",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AirbnbBookings",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AirbnbId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AirbnbsAirbnbId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirbnbBookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_AirbnbBookings_Airbnb_AirbnbsAirbnbId",
                        column: x => x.AirbnbsAirbnbId,
                        principalTable: "Airbnb",
                        principalColumn: "AirbnbId");
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RatingValue = table.Column<int>(type: "INTEGER", nullable: false),
                    AirbnbsAirbnbId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_Airbnb_AirbnbsAirbnbId",
                        column: x => x.AirbnbsAirbnbId,
                        principalTable: "Airbnb",
                        principalColumn: "AirbnbId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirbnbBookings_AirbnbsAirbnbId",
                table: "AirbnbBookings",
                column: "AirbnbsAirbnbId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_AirbnbsAirbnbId",
                table: "Ratings",
                column: "AirbnbsAirbnbId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirbnbBookings");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Airbnb");

            migrationBuilder.DropColumn(
                name: "Bathrooms",
                table: "Airbnb");

            migrationBuilder.DropColumn(
                name: "Beds",
                table: "Airbnb");

            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "Airbnb");

            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "Airbnb",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Airbnb",
                newName: "CreatedAt");
        }
    }
}
