using HotelsR4U.Entities;
using HotelsR4U.Enums;
using System.Linq;

namespace HotelsR4U.Data
{
    public static class BookingSeed
    {
        // Skapar exempelbokningar baserat på sparade gäster, rum och rumspriser
        public static List<Booking> GetBookings (
            List<Guest> guests,
            List<Room> rooms,
            List<RoomPrice> roomPrices )
        {
            //SKapar listan
            var bookings = new List<Booking> ();

            if (guests.Count < 4 || rooms.Count == 0 || roomPrices.Count == 0)
                return bookings;
            //Definerar variabler
            var guest1 = guests[0];
            var guest2 = guests[1];
            var guest3 = guests[2];
            var guest4 = guests[3];

            var singleRoom = rooms.First (r => r.RoomType == "Single");
            var doubleRoom = rooms.First (r => r.RoomType == "Double");
            var suiteRoom = rooms.First (r => r.RoomType == "Suite");
           

            var singleRoomPrice = roomPrices.First (rp => rp.RoomID == singleRoom.RoomID);
            var doubleRoomPrice = roomPrices.First (rp => rp.RoomID == doubleRoom.RoomID);
            var suiteRoomPrice = roomPrices.First (rp => rp.RoomID == suiteRoom.RoomID);

            //SKapar bokningarna
            bookings.Add (new Booking
            {
                GuestID = guest1.GuestID,
                RoomID = singleRoom.RoomID,
                RoomPriceID = singleRoomPrice.RoomPriceID,
                CheckInDate = DateTime.Now.AddDays (2),
                CheckOutDate = DateTime.Now.AddDays (5),
                BookingDate = DateTime.Now,
                ExtraBedRequested = 0,
                PaymentDate = null,
                Status = BookingStatus.Pending
            });

            bookings.Add (new Booking
            {
                GuestID = guest2.GuestID,
                RoomID = doubleRoom.RoomID,
                RoomPriceID = doubleRoomPrice.RoomPriceID,
                CheckInDate = DateTime.Now.AddDays (7),
                CheckOutDate = DateTime.Now.AddDays (10),
                BookingDate = DateTime.Now.AddDays (-1),
                ExtraBedRequested = doubleRoom.MaxExtraBeds > 0 ? 1 : 0,
                PaymentDate = DateTime.Now,
                Status = BookingStatus.Paid
            });

            bookings.Add (new Booking
            {
                GuestID = guest3.GuestID,
                RoomID = suiteRoom.RoomID,
                RoomPriceID = suiteRoomPrice.RoomPriceID,
                CheckInDate = DateTime.Now.AddDays (14),
                CheckOutDate = DateTime.Now.AddDays (18),
                BookingDate = DateTime.Now.AddDays (-12),
                ExtraBedRequested = suiteRoom.MaxExtraBeds,
                PaymentDate = null,
                Status = BookingStatus.Cancelled
            });
            bookings.Add (new Booking
            {
                GuestID = guest4.GuestID,
                RoomID = singleRoom.RoomID,
                RoomPriceID = singleRoomPrice.RoomPriceID,
                CheckInDate = DateTime.Now.AddDays (20),
                CheckOutDate = DateTime.Now.AddDays (25),
                BookingDate = DateTime.Now.AddDays (-5),
                ExtraBedRequested = 0,
                PaymentDate = null,
                Status = BookingStatus.Pending
            });
            //Returnerar listan med bokningar
            return bookings;
        }
    }
}