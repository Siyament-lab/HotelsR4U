using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsR4U.Data
{
    public class Hotel
    {
        #region Properties
        public int HotelID { get; set; }
        [StringLength (50)]
        public string HotelName { get; set; }
        [StringLength (50)]
        public string Email { get; set; }
        [StringLength (20)]
        public string Phone { get; set; }
        public int AddressID { get; set; }
        [ForeignKey (nameof (AddressID))]
        public virtual Address Address { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room> ();
        #endregion
        //navigeringsegenskap för bokningar som hör till hotellet
        public virtual ICollection<BookingService> Bookings { get; set; }

        public static List<Hotel> OurHotels ()
        {
            return new List<Hotel>
            {
                new Hotel
                {
                    HotelName= "3Star Hotel",
                    Email ="info@trestar.com",
                    Phone = "0823456789",
                    Address = new Address
                    {
                        StreetName = "TreStar Street",
                        StreetNumber = "1",
                        PostalCode = "12345",
                        City = "Stockholm",
                        Country = "Sweden"

                    }

                },
                new Hotel
                {
                    HotelName = "Hotel 5Star",
                    Email = "info@FiveStar.com",
                    Phone = "0317654321",
                    Address = new Address
                    {
                        StreetName ="FiveStar Street",
                        StreetNumber = "55",
                        PostalCode = "54321",
                        City ="Gothenburg",
                        Country = "Sweden"
                    }
                }
            };

        }
        //SKickar och sparar Hotell-listan i SQL servern
        public static void OurHotels ( ApplicationDbContext dbContext )
        {
            //Om databasen är tom, lägg till hotell i databasen, från de obj som finns i Hotel-klassen.
            // Annars hämtas de hotellen som finns
            if (!dbContext.Hotels.Any ())
            {
                var hotels = Hotel.OurHotels ();

                //Lägger till Hotellen och rummen i dbContext och sparar ändringarna i databasen.
                dbContext.Hotels.AddRange (hotels);
                dbContext.SaveChanges ();
            }

        }
    }
}
