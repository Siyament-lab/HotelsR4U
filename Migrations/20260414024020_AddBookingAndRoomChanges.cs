using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelsR4U.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingAndRoomChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraBed",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "MaxExtraBeds",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExtraBedRequested",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxExtraBeds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ExtraBedRequested",
                table: "Bookings");

            migrationBuilder.AddColumn<bool>(
                name: "ExtraBed",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
