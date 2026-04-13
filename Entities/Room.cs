using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsR4U.Entities
{
    public class Room
    {
        public int RoomID { get; set; }

        public int HotelID { get; set; }

        [ForeignKey (nameof (HotelID))]
        public virtual Hotel Hotel { get; set; } = null!;

        [StringLength (20)]
        public string RoomNumber { get; set; } = null!;

        [StringLength (20)]
        public string RoomType { get; set; } = null!;

        [StringLength (20)]
        public string RoomSize { get; set; } = null!;

        public bool ExtraBed { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking> ();
        public ICollection<RoomPrice> RoomPrices { get; set; } = new List<RoomPrice> ();
    }
}