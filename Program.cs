using HotelsR4U.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelsR4U
{
    internal class Program
    {
        static void Main ( string[] args )
        {
            var builder = new ConfigurationBuilder ()
                .AddJsonFile ("appsettings.json", true, true);
            var config = builder.Build();

            //SKapar en DbOptionsBuilder som hjälper till att konfigurera  och ansluta till databasen.
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext> ();

            //Hämstar anslutningssträngen från "Appsettings.json".
            var connectionString = config.GetConnectionString ("DefaultConnection");

            //Sedan använder ansl.strängen för att konfigurera SQL server som databas för ApplicationDbContext.
            optionsBuilder.UseSqlServer (connectionString);

            using (var dbContext = new ApplicationDbContext (optionsBuilder.Options))
            {
                dbContext.Database.Migrate ();
            }
        }
    }
}
