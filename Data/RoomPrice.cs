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
    }
}
