using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class rating_location2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Ratings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "Cars",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CarId",
                table: "Ratings",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Cars_CarId",
                table: "Ratings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Cars_CarId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_CarId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Cars");
        }
    }
}
