using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsR4U.Data
{
    public class BookingService
    {
        public int BookingServiceID { get; set; }
        public int HotelID { get; set; }
        [ForeignKey(nameof(HotelID))]
        public virtual Hotel Hotel { get; set; }
        public int GuestID { get; set; }
        [ForeignKey(nameof(GuestID))]
        public virtual Guest Guest { get; set; }
        public int RoomID { get; set; }
        [ForeignKey(nameof(RoomID))]
        public virtual Room Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int RoomPriceID { get; set; }
        [ForeignKey(nameof(RoomPriceID))]
        public virtual RoomPrice AppliedPrice { get; set; }

        public bool IsPaid { get; set; }




    }
}
