using HotelsR4U.Contexts;
using HotelsR4U.Menus;
using HotelsR4U.Seed;
using HotelsR4U.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelsR4U
{
    internal class Program
    {
        static void Main ( string[] args )
        {
            using (var dbContext = new HotelDbContext ())
            {
                dbContext.Database.Migrate ();
                ////Kör seedRunner om det
                ////inte finns några hotell, rum eller gäster i databasen
                //if (!dbContext.Hotels.Any () && !dbContext.Rooms.Any () && !dbContext.Guests.Any ())
                //{
                //    SeedRunner.Run (dbContext);
                //}


                var hotelSevice = new HotelService (dbContext);
                var guestService = new GuestService (dbContext);
                var addressService = new AddressService (dbContext);
                var roomService = new RoomService (dbContext);
                var bookingService = new BookingService (dbContext);

                var menu = new Menu (
                    hotelSevice, 
                    guestService, 
                    addressService, 
                    roomService, 
                    bookingService);
                menu.ShowMainMenu ();

               // Console.WriteLine ("Programmet är klart!");
            }

            //Console.ReadKey ();
        }
    }
}