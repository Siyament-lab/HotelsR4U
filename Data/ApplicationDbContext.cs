using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsR4U.Data
{
    //Skapar vår app databas(Db)kontext som är en klass som ärver från "DbContext" i Entity Framework Core.
    internal class ApplicationDbContext : DbContext
    {
        //Skapar databas sett (Tabeller i SQL server) för våra modeller/klasser som vill ha i databasen.
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Invoice> Invoice { get; set; }

        /// <summary>
        /// *---Viktigt------Viktigt------Viktigt------Viktigt---
        ///Tom konstruktor, konstruktor med "options" och metoden "OnConfiguring"
        ///är mycket användbara för att kunna konfigurera och ansluta till databasen på olika sätt.
        /// </summary>

        //EN tom konstruktor behövs för att kunna ha möjlighet till migrering
        //till databasen. Alltså du får möjlighet att skapa databasen stegvis.
        public ApplicationDbContext ()
        { 
        }

        //Konstruktor med alternativ (Options) som tar in inställningar
        //Från appens konfiguration, som i sin tur kan innehålla anslutningssträngar och andra inställningar.
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        //Metoden "ONConfiguring" används första gången applikationen kors för att
        //Koppla databasen till rätt server.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=hotelsR4U;Trusted_Connection=True;");
            }
        }
    }
}
