using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsR4U.Data
{
    public static class DataBaseConfig
    {
        public static DbContextOptions<ApplicationDbContext> GetOptions ()
        {
            var builder = new ConfigurationBuilder ()
                        .AddJsonFile ("appsettings.json", true, true);
            var config = builder.Build ();

            //SKapar en DbOptionsBuilder som hjälper till att konfigurera  och ansluta till databasen.
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext> ();

            //Hämtar anslutningssträngen från "Appsettings.json".
            var connectionString = config.GetConnectionString ("DefaultConnection");

            //Sedan använder ansl.strängen för att konfigurera SQL server som databas för ApplicationDbContext.
            optionsBuilder.UseSqlServer (connectionString);

            return optionsBuilder.Options;
        }
    }
}
