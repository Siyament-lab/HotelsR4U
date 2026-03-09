using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsR4U.Data
{
    public class Room
    {
        public int RoomID { get; set; }

        public int HotelID { get; set; }
        [ForeignKey (nameof (HotelID))]
        public virtual Hotel Hotel { get; set; }

        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string RoomSize { get; set; }
        public bool ExtraBed { get; set; }

        //Navigeringsegenskap för bokningar som hör till rummet
        public virtual ICollection<BookingService> Bookings { get; set; }

        // Metod som skapar 4 rum per hotell
        public static List<Room> HotelRooms ( List<Hotel> hotels )
        {
            var allRooms = new List<Room> ();

            foreach (var hotel in hotels)
            {
                for (int i = 1; i <= 6; i++)
                {
                    //Hotellen ska ha 3 rumtyper  (Singel,dubbell och Svit) med olika storlekar.
                    //Använder modulo 3 för de 3 typerna
                    var typeIndex = i % 3;

                    allRooms.Add (new Room
                    {
                        // Skapar rumsnummer 101-104 för första hotellet, 201-204 för nästa
                        RoomNumber = $"{(hotels.IndexOf (hotel) + 1)}0{i}",
                        // Bestämmer rumstyp baserat på typeIndex (1 = Single, 2 = Double, 0 = Svit)
                        RoomType = (typeIndex == 1) ? "Single" : (typeIndex == 2) ? "Double" : "Suite",
                        RoomSize = (typeIndex == 1) ? "18sqm" : (typeIndex == 2) ? "25sqm" : "30sqm",
                        ExtraBed = (typeIndex != 1), // Dubbelrum & Svit kan få en extrabädd, singelrum är exkluderat
                        Hotel = hotel      // Detta skapar kopplingen i SQL
                    });
                }
            }
            return allRooms;
        }
        //SKickar och sparar Rum-listan i SQL servern
        public static void OurRooms (ApplicationDbContext dbContext )
        {
            if (!dbContext.Rooms.Any ())
            {
                // Om det inte finns några rum i databasen, skapa och lägg till rum baserat på de hotell som redan finns i databasen.
                if (!dbContext.Rooms.Any ())
                {
                    // Hämta befintliga hotell från SQL med ToList()
                    var hotelsInDb = dbContext.Hotels.ToList ();

                    // Skapa listan med alla 12 rum (6 per hotell)
                    var allRooms = Room.HotelRooms (hotelsInDb);

                    // Lägger till rummen i dbContext
                    allRooms.ForEach (room => dbContext.Rooms.Add (room));
                    //Sparar ändringarna i db
                    dbContext.SaveChanges ();
                }
            }
        }
    }
}
