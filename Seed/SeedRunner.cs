using HotelsR4U.Contexts;
using HotelsR4U.Data;
using System.Linq;

namespace HotelsR4U.Seed
{
    public static class SeedRunner
    {
        public static void Run ( HotelDbContext dbContext )
        {
            if (dbContext.Addresses.Any () ||
                dbContext.Hotels.Any () ||
                dbContext.Guests.Any () ||
                dbContext.Rooms.Any () ||
                dbContext.RoomPrices.Any ())
            {
                //Om det redan finns data i någon av tabellerna, seedas inte de igen, annars seedas enligt ordningennedan
                return;
            }

            // 1. Address
            var addresses = AddressSeed.GetAddresses ();
            dbContext.Addresses.AddRange (addresses);
            dbContext.SaveChanges ();

            // 2. Hotel
            var hotels = HotelSeed.GetHotels (addresses);
            dbContext.Hotels.AddRange (hotels);
            dbContext.SaveChanges ();

            // 3. Room
            var rooms = RoomSeed.HotelRooms (hotels);
            dbContext.Rooms.AddRange (rooms);
            dbContext.SaveChanges ();

            // 4. RoomPrice
            var savedRooms = dbContext.Rooms.ToList ();
            var roomPrices = RoomPriceSeed.ActualRoomPrices (savedRooms);
            dbContext.RoomPrices.AddRange (roomPrices);
            dbContext.SaveChanges ();

            // 5. Guest
            var guests = GuestSeed.GetGuests (addresses);
            dbContext.Guests.AddRange (guests);
            dbContext.SaveChanges ();

            // Fyller på med Booking & Invoice senare

            Console.WriteLine ("Seed klar!");
        }
    }
}