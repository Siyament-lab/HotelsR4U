using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsR4U.Data
{
    public class Guest
    {
        public int GuestID { get; set; }
        [Required][StringLength (50)]
        public string FirstName { get; set; }
        [Required][StringLength (50)]
        public string LastName { get; set; }
        [StringLength (100)]
        public string Email { get; set; }
        [Required][StringLength (15)]
        public string Phone { get; set; }

        public int AddressID { get; set; }
        [ForeignKey (nameof (AddressID))]
        public virtual Address Address { get; set; }

        public virtual ICollection<BookingService> Bookings { get; set; } = new List<BookingService> ();

        //Skapar en grund gästlista först(för test)
        public static void DefaultGusts ( ApplicationDbContext dbCOntext )
        {
            if (!dbCOntext.Guests.Any ())
            {
                var defaults = new List<Guest>
                {
                   new Guest{ FirstName = "John", LastName = "Doe", Email = "john.doe@defaultGuest.com", Phone = "1234567890",
                             Address = new Address{AddressID=1,StreetName = "Default Street", City = "Default City", PostalCode = "12345"}
                    },
                   new Guest{ FirstName = "Jane", LastName = "Smith", Email = "jane.smith@defaultGuest.com", Phone = "0987654321",
                             Address = new Address{AddressID=2,StreetName = "Default Street", City = "Default City", PostalCode = "12345"}
                    }
                };
                dbCOntext.Guests.AddRange (defaults);
                dbCOntext.SaveChanges ();
            }

            //Metod för att skapa gäst med data från användarinput
            public static Guest CreateGuest ()
            {
                var g = new Guest ();
                var addr = new HotelsR4U.Data.Address ();

                Console.WriteLine ("\n--- Registrera ny gäst ---");
                Console.Write ("Förnamn: "); g.FirstName = Console.ReadLine ();
                Console.Write ("Efternamn: "); g.LastName = Console.ReadLine ();
                Console.Write ("E-post: "); g.Email = Console.ReadLine ();
                Console.Write ("Telefon: "); g.Phone = Console.ReadLine ();

                Console.WriteLine ("\n--- Adressuppgifter ---");
                Console.Write ("Gata: "); addr.StreetName = Console.ReadLine ();
                Console.Write ("Postnummer: "); addr.PostalCode = Console.ReadLine ();
                Console.Write ("Ort: "); addr.City = Console.ReadLine ();

                // Koppla ihop adressen med gästen
                g.Address = addr;
                return g;
            }
        }
    }
}
