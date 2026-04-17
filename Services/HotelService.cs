using HotelsR4U.Contexts;
using HotelsR4U.Entities;

namespace HotelsR4U.Services
{

    public class HotelService
    {
        private readonly HotelDbContext _dbContext;
        private readonly RelationGuardService _relationGuardService;

        public HotelService ( HotelDbContext dbContext )
        {
            _dbContext = dbContext;
            _relationGuardService = new RelationGuardService (dbContext);
        }

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
        public void UpdateHotel ( Hotel hotel )
        {
            var existingHotel = _dbContext.Hotels.FirstOrDefault (h => h.HotelID == hotel.HotelID);
            if (existingHotel == null)
                return;
            existingHotel.HotelName = hotel.HotelName;
            existingHotel.Email = hotel.Email;
            existingHotel.Phone = hotel.Phone;
            //existingHotel.AddressID = hotel.AddressID;
            _dbContext.SaveChanges ();
            return;
        }
        // Tar bort ett hotell
        public void DeleteHotel ( int hotelID )
        {
            var hotel = _dbContext.Hotels.Find (hotelID);
            if (hotel == null)
                throw new Exception ("Hotellet finns inte.");

            // Stoppa om det finns beroenden
            _relationGuardService.EnsureHotelCanBeDeleted (hotelID);

            var addressId = hotel.AddressID;

            // 1. Tar bort hotellet först
            _dbContext.Hotels.Remove (hotel);
            _dbContext.SaveChanges ();


            var addressDelete = _dbContext.Hotels
         .Where (h => h.AddressID == addressId)
         .ToList ();

            if (!addressDelete.Any ())
            {
                var address = _dbContext.Addresses.FirstOrDefault (a => a.AddressID == addressId);
                if (address != null)
                {
                    _dbContext.Addresses.Remove (address);
                    _dbContext.SaveChanges ();
                }
            }
        }
    }
}