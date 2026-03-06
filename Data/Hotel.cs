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
        public int HotelID { get; set; }
        [StringLength(50)]
        public string HotelName { get; set; }
        [StringLength (50)]
        public string Email { get; set; }
        [StringLength (20)]
        public string Phone { get; set; }
        public int AddressID { get; set;}
        [ForeignKey (nameof(AddressID))]
        public virtual Address Address { get; set; }

        //navigeringsegenskap för bokningar som hör till hotellet
        public virtual ICollection<BookingService> Bookings { get; set; }

    }
}
