using HotelsR4U.Contexts;
using HotelsR4U.Entities;
using HotelsR4U.Enums;


namespace HotelsR4U.Services
{
    public class RoomPriceService
    {
        private readonly HotelDbContext _dbContext;
        private readonly RoomService _roomService;
        private readonly RelationGuardService _relationGuardService;

        public RoomPriceService ( HotelDbContext dbContext )
        {
            _dbContext = dbContext;
            _roomService = new RoomService (dbContext);
        }
        //Hämta alla priser för alla rum
        public List<RoomPrice> GetCurrentRoomPrices ()
        {
            return _dbContext.RoomPrices.ToList ();
        }
        //Hämta rumpriser med RoomPriceID
        public RoomPrice GetRoomPriceByID ( int roomPriceID )
        {
            return _dbContext.RoomPrices.FirstOrDefault
                (rp => rp.RoomPriceID == roomPriceID)
                ?? throw new Exception ("Rumspris saknas.");
        }
        //Hämta rumpriser med RoomID
        public RoomPrice GetRoomPriceByRoomID ( int roomID )
        {
            return _dbContext.RoomPrices.FirstOrDefault
                (rp => rp.RoomID == roomID)
                ?? throw new Exception ("Rumspris saknas för detta rum.");
        }
        //Lägg till rumpris
        public RoomPrice AddRoomPrice ( RoomPrice roomPrice )
        {
            var roomExists = _dbContext.Rooms.Any
                (r => r.RoomID == roomPrice.RoomID);
            if (!roomExists)
                throw new Exception ("Rummet finns inte.");

            if (roomPrice.PricePerNight < 0)
                throw new Exception ("Pris per natt kan inte vara negativt.");

            if (roomPrice.ExtraBedPrice < 0)
                throw new Exception ("Pris för extrasäng kan inte vara negativt.");

            if (roomPrice.ValidTo <= roomPrice.ValidFrom)
                throw new Exception ("ValidTo måste vara senare än ValidFrom.");

            _dbContext.RoomPrices.Add (roomPrice);
            _dbContext.SaveChanges ();

            return roomPrice;

        }
        //Uppdatera rumpriser
        public RoomPrice UpdateRoomPrice ( RoomPrice roomPrice )
        {
            var existingRoomPrice = _dbContext.RoomPrices.FirstOrDefault
                (rp => rp.RoomPriceID == roomPrice.RoomPriceID)
                ?? throw new Exception ("Rumspris finns inte.");

            var roomExists = _dbContext.Rooms.Any
                (r => r.RoomID == roomPrice.RoomID);
            if (!roomExists)
                throw new Exception ("Rummet finns inte.");

            if (roomPrice.PricePerNight < 0)
                throw new Exception ("Pris per natt kan inte vara negativt.");

            if (roomPrice.ExtraBedPrice < 0)
                throw new Exception ("Pris för extrasäng kan inte vara negativt.");

            if (roomPrice.ValidTo <= roomPrice.ValidFrom)
                throw new Exception ("ValidTo måste vara senare än ValidFrom.");

            existingRoomPrice.RoomID = roomPrice.RoomID;
            existingRoomPrice.PricePerNight = roomPrice.PricePerNight;
            existingRoomPrice.ExtraBedPrice = roomPrice.ExtraBedPrice;
            existingRoomPrice.ValidFrom = roomPrice.ValidFrom;
            existingRoomPrice.ValidTo = roomPrice.ValidTo;

            _dbContext.SaveChanges ();

            return existingRoomPrice;
        }
        //Radera ett rumpris (onödigt men finns för att CRUD ska va komplett)
        //borttagning hindras
        public void DeleteRoomPrice ( int roomPriceId )
        {
            var roomPrice = _dbContext.RoomPrices.FirstOrDefault
                (rp => rp.RoomPriceID == roomPriceId)
                ?? throw new Exception ("Rumspris finns inte.");

            //Kontrollera och förhindra borttagning om bokning med det priset finns
            _relationGuardService.EnsureRoomPriceCanBeDeleted (roomPriceId);

            _dbContext.RoomPrices.Remove (roomPrice);
            _dbContext.SaveChanges ();
        }

        //Beräkna totalen för en bokning
        public decimal CalculateBookingAmount ( Booking booking )
        {
            var roomPrice = _dbContext.RoomPrices.FirstOrDefault 
                (rp => rp.RoomPriceID == booking.RoomPriceID)
                ?? throw new Exception ("Rumspris saknas för bokningen.");

            var numberOfNights = (booking.CheckOutDate.Date - booking.CheckInDate.Date).Days;

            if (numberOfNights <= 0)
                throw new Exception ("Antal nätter måste vara minst 1.");

            var totalAmount =
                (numberOfNights * roomPrice.PricePerNight) +
                (booking.ExtraBedRequested * roomPrice.ExtraBedPrice);

            return totalAmount;
        }

    }
}
