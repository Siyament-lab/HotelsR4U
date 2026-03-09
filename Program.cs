using HotelsR4U.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelsR4U
{
    internal class Program
    {
        static void Main ( string[] args )
        {
            //Skapar instans av DbContextOptionsBuilder och hämtar konfigurationen från DataBaseConfig-klassen.
            var options = DataBaseConfig.GetOptions ();

            using (var dbContext = new ApplicationDbContext (options))
            {
                dbContext.Database.Migrate ();

                //Anropar metoder från klasserna och skapar entiteter i databasen om de inte finns.
                //Logiken sköts innuti klasserna.
                Hotel.OurHotels (dbContext);      
                Room.OurRooms (dbContext);       
                RoomPrice.OurRoomPrices (dbContext);


            }
        }
    }
}

