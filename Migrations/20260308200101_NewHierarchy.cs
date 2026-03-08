using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelsR4U.Migrations
{
    /// <inheritdoc />
    public partial class NewHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelID",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "HotelID",
                table: "BookingService",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelID",
                table: "Rooms",
                column: "HotelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelID",
                table: "Rooms",
                column: "HotelID",
                principalTable: "Hotels",
                principalColumn: "HotelID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelID",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HotelID",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HotelID",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "HotelID",
                table: "BookingService",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
