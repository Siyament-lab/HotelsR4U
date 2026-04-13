using System.ComponentModel.DataAnnotations.Schema;
using HotelsR4U.Enums;


namespace HotelsR4U.Entities
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int GuestID { get; set; }
        [ForeignKey(nameof(GuestID))]
        public Guest Guest { get; set; }= null!;
        public int RoomID { get; set; }
        [ForeignKey(nameof(RoomID))]
        public Room Room { get; set; }= null!;
        public int RoomPriceID { get; set; }
        [ForeignKey (nameof (RoomPriceID))]
        public RoomPrice RoomPrice { get; set; } = null!;

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public DateTime? PaymentDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;




    }
}
