using HotelsR4U.Services;
using HotelsR4U.Entities;
using HotelsR4U.Enums;

namespace HotelsR4U.Menus
{
    public class BookingMenu : MenuBase
    {
        private readonly BookingService _bookingService;
        private readonly RelationGuardService _relationGuardService;

        public BookingMenu ( BookingService bookingService )
        {
            _bookingService = bookingService;
        }

        protected override string GetMenuTitle () => "--- Bokningsmeny ---";

        protected override void ShowAll ()
        {
            Console.Clear ();
            var bookings = _bookingService.GetAllBookings ();

            foreach (var booking in bookings)
            {
                Console.WriteLine ($"{booking.BookingID}: Guest {booking.GuestID}, Room {booking.RoomID}, {booking.CheckInDate:d} - {booking.CheckOutDate:d}");
            }

            Pause ();
        }

        protected override void Add ()
        {
            Console.Clear ();

            try
            {
                Console.Write ("GuestID: ");
                int guestId = int.Parse (Console.ReadLine ()!);

                Console.Write ("RoomID: ");
                int roomId = int.Parse (Console.ReadLine ()!);

                Console.Write ("CheckInDate (yyyy-mm-dd): ");
                DateTime checkInDate = DateTime.Parse (Console.ReadLine ()!);

                Console.Write ("CheckOutDate (yyyy-mm-dd): ");
                DateTime checkOutDate = DateTime.Parse (Console.ReadLine ()!);

                Console.Write ("Antal extrasängar: ");
                int extraBeds = int.Parse (Console.ReadLine ()!);

                Console.Write ("Ange status (1=Pending, 2=Paid, 3=Cancelled): ");
                BookingStatus status = (BookingStatus)int.Parse (Console.ReadLine ()!);

                _bookingService.AddBooking (guestId, roomId, checkInDate, checkOutDate, extraBeds);

                Console.WriteLine ("Bokning skapad.");
            }
            //Se över och skapa relevant msg detaljer.
            catch (Exception ex)
            {
                Console.WriteLine (ex.Message);
            }

            Pause ();
        }

        protected override void Update ()
        {
            Console.Clear ();
            Console.WriteLine ("Uppdatering av bokning är inte implementerad ännu.");
            Pause ();
        }

        protected override void Delete ()
        {
            Console.Clear ();

            Console.Write ("Ange BookingID att ta bort: ");
            int bookingId = int.Parse (Console.ReadLine ()!);

            _bookingService.CancelBooking (bookingId);

            Console.WriteLine ("Bokning borttagen.");
            Pause ();
        }
    }
}