using HotelsR4U.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsR4U.Entities
{
    public class Address
    {
        public int AddressID { get; set; }
        [StringLength(50)]
        public string StreetName { get; set; } = null!;
        [StringLength(10)]
        public string StreetNumber { get; set; }= null!;
        [StringLength(10)]
        public string PostalCode { get; set; }= null!;
        [StringLength(20)]
        public string City { get; set; }= null!;
        [StringLength(20)]
        public string Country { get; set; }= null!;
        public AddressType AddressType { get; set; }

        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel> ();
        public ICollection<Guest> Guests { get; set; } = new List<Guest> ();

    }
}
