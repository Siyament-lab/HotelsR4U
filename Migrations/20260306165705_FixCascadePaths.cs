using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelsR4U.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Customer_CustomerID",
                table: "Invoice");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_CustomerID",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Invoice");

            migrationBuilder.RenameTable(
                name: "Invoice",
                newName: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Invoices",
                newName: "InvoiceDate");

            migrationBuilder.AddColumn<int>(
                name: "BookingServiceID",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "InvoiceID");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtraBed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    GuestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.GuestID);
                    table.ForeignKey(
                        name: "FK_Guests_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelID);
                    table.ForeignKey(
                        name: "FK_Hotels_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomPrices",
                columns: table => new
                {
                    RoomPriceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricePerNight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ExtraBedPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomPrices", x => x.RoomPriceID);
                    table.ForeignKey(
                        name: "FK_RoomPrices_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingService",
                columns: table => new
                {
                    BookingServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    GuestID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomPriceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingService", x => x.BookingServiceID);
                    table.ForeignKey(
                        name: "FK_BookingService_Guests_GuestID",
                        column: x => x.GuestID,
                        principalTable: "Guests",
                        principalColumn: "GuestID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingService_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingService_RoomPrices_RoomPriceID",
                        column: x => x.RoomPriceID,
                        principalTable: "RoomPrices",
                        principalColumn: "RoomPriceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingService_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BookingServiceID",
                table: "Invoices",
                column: "BookingServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingService_GuestID",
                table: "BookingService",
                column: "GuestID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingService_HotelID",
                table: "BookingService",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingService_RoomID",
                table: "BookingService",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingService_RoomPriceID",
                table: "BookingService",
                column: "RoomPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_AddressID",
                table: "Guests",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_AddressID",
                table: "Hotels",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPrices_RoomID",
                table: "RoomPrices",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_BookingService_BookingServiceID",
                table: "Invoices",
                column: "BookingServiceID",
                principalTable: "BookingService",
                principalColumn: "BookingServiceID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_BookingService_BookingServiceID",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "BookingService");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "RoomPrices");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_BookingServiceID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BookingServiceID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Invoices");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "Invoice");

            migrationBuilder.RenameColumn(
                name: "InvoiceDate",
                table: "Invoice",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Invoice",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "InvoiceID");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerID",
                table: "Invoice",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Customer_CustomerID",
                table: "Invoice",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID");
        }
    }
}
