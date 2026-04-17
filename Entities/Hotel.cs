
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HotelsR4U.Entities
{
    public class Hotel
    {
        #region Properties
        public int HotelID { get; set; }
        [StringLength (50)]
        public string HotelName { get; set; }
        [StringLength (50)]
        public string Email { get; set; }
        [StringLength (20)]
        public string Phone { get; set; }
        public int AddressID { get; set; }
        [ForeignKey (nameof (AddressID))]
        public virtual Address Address { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room> ();
        #endregion
       

        
    }
}
