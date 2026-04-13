using HotelsR4U.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace HotelsR4U.Contexts

{
    //Skapar vår app databas(Db)kontext som är en klass som ärver från "DbContext" från Entity Framework Core.
    public class HotelDbContext : DbContext
    {
        //Skapar databas sett (Tabeller i SQL server) för våra modeller/klasser som vill ha i databasen.
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomPrice> RoomPrices { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        #region Anteckningar viktigt
        
        /// *---Viktigt------Viktigt------Viktigt------Viktigt---
        ///Tom konstruktor, konstruktor med "options" och metoden "OnConfiguring"
        ///är mycket användbara för att kunna konfigurera och ansluta till databasen på olika sätt.
        
        //EN tom konstruktor behövs för att kunna ha möjlighet till migrering
        //till databasen. Alltså du får möjlighet att skapa databasen stegvis.
        //Konstruktor med alternativ (Options) som tar in inställningar
        //Från appens konfiguration, som i sin tur kan innehålla anslutningssträngar och andra inställningar.
        //Metoden "ONConfiguring" används första gången applikationen körs för att
        //Koppla databasen till rätt server.
        
        #endregion
        public HotelDbContext ()
        { 
        }
        //Konstruktor med Options
        public HotelDbContext (DbContextOptions<HotelDbContext> options) 
            : base(options)
        {
        }
        //Metod för att begränsa borttagning av pågående bokningar om man väljer ta bort ett hotel/kund/rum som har bokningar.
        //Detta görs genom att ändra "DeleteBehavior" till "Restrict" för alla relationer i modellen.

        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            // Konvertera enum "BookingStatus" till int i databasen.
            modelBuilder.Entity<Booking>()
                .Property(b => b.Status)
                .HasConversion<int>();
        }

        // Metoden "OnConfiguring".
        protected override void OnConfiguring ( DbContextOptionsBuilder optionsBuilder )
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder ()
                    .SetBasePath (Directory.GetCurrentDirectory ())
                    .AddJsonFile ("appsettings.json")
                    .Build ();

                var connectionString = configuration.GetConnectionString ("DefaultConnection");
                optionsBuilder.UseSqlServer (connectionString);
            }
        }
    }
}
