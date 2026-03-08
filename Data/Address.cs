using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsR4U.Data
{
    public class Address
    {
        public int AddressID { get; set; }
        [StringLength(50)]
        public string StreetName { get; set; }
        [StringLength(10)]
        public string StreetNumber { get; set; }
        [StringLength(10)]
        public string PostalCode { get; set; }
        [StringLength(20)]
        public string City { get; set; }
        [StringLength(20)]
        public string Country { get; set; }

    }
}
