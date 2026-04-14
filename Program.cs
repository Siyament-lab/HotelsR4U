using HotelsR4U.Contexts;
using HotelsR4U.Seed;
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
                //Kör seedRunner om det
                //inte finns några hotell, rum eller gäster i databasen
                if (!dbContext.Hotels.Any () && !dbContext.Rooms.Any () && !dbContext.Guests.Any ())
                {
                    SeedRunner.Run (dbContext);
                }

                Console.WriteLine ("Programmet är klart!");
            }

            Console.ReadKey ();
        }
    }
}