using HotelsR4U.Contexts;
using HotelsR4U.Entities;
using HotelsR4U.Enums;
using Microsoft.Identity.Client;

namespace HotelsR4U.Services
{
    public class BookingService
    {
        private readonly HotelDbContext _dbContext;
        private readonly RoomService _roomService;
        private BookingStatus _bookingStatus;

        public BookingService ( HotelDbContext dbContext )
        {
            _dbContext = dbContext;
            _roomService = new RoomService (dbContext);
            _bookingStatus = BookingStatus.Pending;
        }
        //Visa alla bokningar
        public List<Booking> GetAllBookings ()
        {
            return _dbContext.Bookings.ToList ();
        }

        public Booking AddBooking (
            int guestId,
            int roomId,
            DateTime checkInDate,
            DateTime checkOutDate,
            int extraBedRequested )
        {
            // Datumkontroll
            if (checkInDate.Date < DateTime.Today)
                throw new Exception ("Check-in datum kan inte vara i det förflutna.");

            if (checkOutDate.Date <= checkInDate.Date)
                throw new Exception ("Check-out datum måste vara efter check-in datum.");

            // Hämta data om gäst, rum och rumspris
            var guest = _dbContext.Guests.FirstOrDefault (g => g.GuestID == guestId)
                ?? throw new Exception ("Gästen finns inte.");

            var room = _dbContext.Rooms.FirstOrDefault (r => r.RoomID == roomId)
                ?? throw new Exception ("Rummet finns inte.");

            var roomPrice = _dbContext.RoomPrices.FirstOrDefault (rp => rp.RoomID == roomId)
                ?? throw new Exception ("Rumspris saknas.");

            // Kontroll tillgängliga rum via RoomService
            if (!_roomService
                .GetAvailableRooms (room.HotelID, checkInDate, checkOutDate)
                .Any (r => r.RoomID == roomId))
            {
                throw new Exception ("Rummet är inte tillgängligt under vald period.");
            }

            // Antal tillåtna extrasängar för vald rum
            if (extraBedRequested < 0 || extraBedRequested > room.MaxExtraBeds)
                throw new Exception ($"Max {room.MaxExtraBeds} extrasäng(ar) tillåts.");

            //Betalning status på bokning(!!! lägg till logik)
            


            // Bokning skapas och sparas till databasen
            var booking = new Booking
            {
                GuestID = guestId,
                RoomID = roomId,
                RoomPriceID = roomPrice.RoomPriceID,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                BookingDate = DateTime.Now,
                PaymentDate = null,
                ExtraBedRequested = extraBedRequested,
                //Status = status //Hanteras senare
            };

            _dbContext.Bookings.Add (booking);
            _dbContext.SaveChanges ();

            return booking;
        }
        //Annulera eller ta bort bokning
        public bool CancelBooking ( int bookingId )
        {

            var booking = _dbContext.Bookings.FirstOrDefault (b => b.BookingID == bookingId);
            if (booking == null || _bookingStatus == BookingStatus.Pending)
                return false;

            booking.Status = BookingStatus.Cancelled;
            _dbContext.SaveChanges ();
            return true;
        }
    }
}