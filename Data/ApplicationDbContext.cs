using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsR4U.Data
{
    //Skapar vår app databas(Db)kontext som är en klass som ärver från "DbContext" i Entity Framework Core.
    public class ApplicationDbContext : DbContext
    {
        //Skapar databas sett (Tabeller i SQL server) för våra modeller/klasser som vill ha i databasen.
        public DbSet<Guest> Guests { get; set; }
        public DbSet<BookingService> BookingService { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomPrice> RoomPrices { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        //****Anteckningar****
        /// <summary>
        /// *---Viktigt------Viktigt------Viktigt------Viktigt---
        ///Tom konstruktor, konstruktor med "options" och metoden "OnConfiguring"
        ///är mycket användbara för att kunna konfigurera och ansluta till databasen på olika sätt.
        /// </summary>
        //EN tom konstruktor behövs för att kunna ha möjlighet till migrering
        //till databasen. Alltså du får möjlighet att skapa databasen stegvis.
        //Konstruktor med alternativ (Options) som tar in inställningar
        //Från appens konfiguration, som i sin tur kan innehålla anslutningssträngar och andra inställningar.
        //Metoden "ONConfiguring" används första gången applikationen körs för att
        //Koppla databasen till rätt server.

        public ApplicationDbContext ()
        { 
        }
        //Konstruktor med Options
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) 
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
        }

        // Metoden "OnConfiguring".
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                //Behövde lägga till "TrustedServerCertificate=True" för att förbise SQL;s nya säkerhetsrutiner. Nu är min lokala server pålitlig.
                optionsBuilder.UseSqlServer(@"Server=.;Database=hotelsR4U;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}
