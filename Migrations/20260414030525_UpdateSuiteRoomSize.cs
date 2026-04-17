using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelsR4U.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSuiteRoomSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Rooms
                Set RoomSize = '60sqm'
                WHERE RoomType = 'Suite'
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Rooms
                Set RoomSize = '30sqm'
                WHERE RoomType = 'Suite'
            ");

        }
    }
}
