using HotelsR4U.Contexts;
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

                Console.WriteLine ("Databasen skapad!");
            }

            Console.ReadKey ();
        }
    }
}