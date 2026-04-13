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
                SeedRunner.Run (dbContext);

                Console.WriteLine ();
            }

            Console.ReadKey ();
        }
    }
}