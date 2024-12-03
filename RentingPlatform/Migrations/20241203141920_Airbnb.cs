using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentingPlatform.Migrations
{
    public partial class AddAirbnbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airbnbs",
                columns: table => new
                {
                    AirbnbId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxGuests = table.Column<int>(type: "int", nullable: false),
                    Amenities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rooms = table.Column<int>(type: "int", nullable: false),
                    Beds = table.Column<int>(type: "int", nullable: false),
                    Bathrooms = table.Column<int>(type: "int", nullable: false),
                    ImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: true) // Handle list of image URLs as a string (may need JSON storage depending on DB)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airbnbs", x => x.AirbnbId);
                    table.ForeignKey(
                        name: "FK_Airbnbs_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    AirbnbId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_Airbnbs_AirbnbId",
                        column: x => x.AirbnbId,
                        principalTable: "Airbnbs",
                        principalColumn: "AirbnbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AirbnbBookings",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AirbnbId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirbnbBookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_AirbnbBookings_Airbnbs_AirbnbId",
                        column: x => x.AirbnbId,
                        principalTable: "Airbnbs",
                        principalColumn: "AirbnbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airbnbs_OwnerId",
                table: "Airbnbs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_AirbnbId",
                table: "Ratings",
                column: "AirbnbId");

            migrationBuilder.CreateIndex(
                name: "IX_AirbnbBookings_AirbnbId",
                table: "AirbnbBookings",
                column: "AirbnbId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirbnbBookings");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Airbnbs");
        }
    }
}
