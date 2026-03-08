using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

    }
}
