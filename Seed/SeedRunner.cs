using HotelsR4U.Contexts;
using HotelsR4U.Data;

namespace HotelsR4U.Seed
{
    public static class SeedRunner
    {
        public static void Run ( HotelDbContext dbContext )
        {
            //Om det redan finns data i någon av tabellerna,
            //seedas inte de igen, annars seedas enligt ordningennedan
            if (dbContext.Addresses.Any () ||
                dbContext.Hotels.Any () ||
                dbContext.Guests.Any () ||
                dbContext.Rooms.Any () ||
                dbContext.RoomPrices.Any ())
            {
                
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

            //Hämtar sparade rum efter vi har sparat ändringar för entiteten Room.
            var savedRooms = dbContext.Rooms.ToList ();
            
            // 4. RoomPrice
            var roomPrices = RoomPriceSeed.ActualRoomPrices (savedRooms);
            dbContext.RoomPrices.AddRange (roomPrices);
            dbContext.SaveChanges ();

            //Hämta sparade rum-priser efter sparandet.
            var savedRoomPrices = dbContext.RoomPrices.ToList ();
            // 5. Guest
            var guests = GuestSeed.GetGuests (addresses);
            dbContext.Guests.AddRange (guests);
            dbContext.SaveChanges ();

            //Hämta sparade gäster efter sparandet.
            var savedGuests = dbContext.Guests.ToList ();


            Console.WriteLine ("Seed klar!");
        }
    }
}