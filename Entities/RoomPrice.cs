
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsR4U.Entities
{
    public class RoomPrice
    {
        public int RoomPriceID { get; set; }

        [Column (TypeName = "decimal(10,2)")]
        public decimal PricePerNight { get; set; }

        [Column (TypeName = "decimal(10,2)")]
        public decimal ExtraBedPrice { get; set; }
        [Column (TypeName = "Date")]
        public DateTime ValidFrom { get; set; }
        [Column (TypeName = "Date")]
        public DateTime ValidTo { get; set; }

        //Kopplar ihop med rummet
        public int RoomID { get; set; }
        [ForeignKey (nameof (RoomID))]
        public Room Room { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking> ();

    }
}
