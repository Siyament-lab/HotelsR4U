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
        [Required][StringLength(50)]
        public string FirstName { get; set; }
        [Required][StringLength (50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }

        public int AddressID { get; set; }
        [ForeignKey(nameof(AddressID))]
        public virtual Address Address { get; set; }

        public virtual ICollection<BookingService> Bookings { get; set; } = new List<BookingService> ();
    }
}
