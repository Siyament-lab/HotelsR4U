using HotelsR4U.Contexts;
using HotelsR4U.Entities;

namespace HotelsR4U.Services
{
    public class HotelService ( HotelDbContext dbContext )
    {
        private readonly HotelDbContext _dbContext = dbContext;
        private readonly RelationGuardService _relationGuardService;

        // Hämtar alla hotell från databasen
        public List<Hotel> GetAllHotels ()
        {
            return _dbContext.Hotels.ToList ();
        }
        // Lägger till ett nytt hotell i databasen
        public bool AddHotel ( Hotel hotel )
        {
            _dbContext.Hotels.Add (hotel);
            _dbContext.SaveChanges ();
            return true;
        }
        // Uppdaterar ett befintligt hotell i databasen
        public bool UpdateHotel ( Hotel hotel )
        {
            var existingHotel = _dbContext.Hotels.Find (hotel.HotelID);
            if (existingHotel == null)
                return false;
            existingHotel.HotelName = hotel.HotelName;
            existingHotel.Email = hotel.Email;
            existingHotel.Phone = hotel.Phone;
            existingHotel.AddressID = hotel.AddressID;
            _dbContext.SaveChanges ();
            return true;
        }
        // Tar bort ett hotell från databasen efter att ha kontrollerat relationer
        public bool DeleteHotel ( int hotelID )
        {
            var hotel = _dbContext.Hotels.Find (hotelID);
            if (hotel == null)
                return false;

            _relationGuardService.EnsureHotelCanBeDeleted (hotelID);

            _dbContext.Hotels.Remove (hotel);
            _dbContext.SaveChanges ();
            return true;
        }
    }
}