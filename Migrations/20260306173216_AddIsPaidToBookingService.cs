using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelsR4U.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPaidToBookingService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "BookingService",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "BookingService");
        }
    }
}
