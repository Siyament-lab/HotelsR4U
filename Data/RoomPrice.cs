using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsR4U.Data
{
    public class RoomPrice
    {
        public int RoomPriceID { get; set; }

        [Column (TypeName = "decimal(10,2)")]
        public decimal PricePerNight { get; set; }

        [Column (TypeName = "decimal(10,2)")]
        public decimal ExtraBedPrice { get; set; }

        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        //Kopplar ihop med rummet
        public int RoomID { get; set; }
        [ForeignKey(nameof(RoomID))]
        public virtual Room Room { get; set; }

        //Skapar en prislista baserad på rum-listan
        public static List<RoomPrice> ActualRoomPrices ( List<Room> rooms )
        {
            return rooms.Select(r => new RoomPrice
            {
                RoomID = r.RoomID,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddYears(1),

                // Sätter priset baserat på rumstypen, och extra säng tillägg
                PricePerNight = r.RoomType == "Suite" ? 2000m :
                r.RoomType == "Double" ? 1200m:900m,
                ExtraBedPrice = r.ExtraBed ? 300m : 0m,

            }).ToList ();
        }
        //SKickar och sparar priser i SQL servern
        public static void OurRoomPrices ( ApplicationDbContext dbContext )
        {
            if (!dbContext.RoomPrices.Any ())
            {
                var allRooms = dbContext.Rooms.ToList ();
                var prices = ActualRoomPrices (allRooms);
                dbContext.RoomPrices.AddRange (prices);
                dbContext.SaveChanges ();

            }
        }
    }
}
