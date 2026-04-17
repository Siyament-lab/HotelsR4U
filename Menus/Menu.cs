using HotelsR4U.Services;

namespace HotelsR4U.Menus
{
    public class Menu
    {
        private readonly HotelMenu _hotelMenu;
        private readonly GuestMenu _guestMenu;
        private readonly AddressMenu _addressMenu;
        private readonly RoomMenu _roomMenu;
        private readonly BookingMenu _bookingMenu;
        //private HotelService _HotelSevice;
        //private GuestService _GuestService;
        //private AddressService _AddressService;
        //private RoomService _RoomService;
        //private BookingService _BookingService;
        

        //public Menu ( HotelService hotelSevice, GuestService guestService, AddressService addressService, RoomService roomService, BookingService bookingService )
        //{
        //    _HotelSevice = hotelSevice;
        //    _GuestService = guestService;
        //    _AddressService = addressService;
        //    _RoomService = roomService;
        //    _BookingService = bookingService;
        //}

        public Menu (
            HotelService hotelService,
            GuestService guestService,
            AddressService addressService,
            RoomPriceService roomPriceService,
            RoomService roomService,
            BookingService bookingService )
        {
            _hotelMenu = new HotelMenu (hotelService, addressService);
            _guestMenu = new GuestMenu (guestService, addressService);
            _addressMenu = new AddressMenu (addressService);
            _roomMenu = new RoomMenu (roomService, roomPriceService);
            _bookingMenu = new BookingMenu (bookingService);
        }

        public void ShowMainMenu ()
        {
            bool mainMenuRunning = true;

            while (mainMenuRunning)
            {
                Console.Clear ();
                Console.WriteLine ("=== HotelsR4U ===");
                Console.WriteLine ("1. Hantera hotell");
                Console.WriteLine ("2. Hantera gäster");
                Console.WriteLine ("3. Hantera adresser");
                Console.WriteLine ("4. Hantera rum");
                Console.WriteLine ("5. Hantera bokningar");
                Console.WriteLine ("0. Avsluta");
                Console.Write ("Välj: ");

                var choice = Console.ReadLine ();

                switch (choice)
                {
                    case "1":
                        _hotelMenu.ShowMenu ();
                        break;
                    case "2":
                        _guestMenu.ShowMenu ();
                        break;
                    case "3":
                        _addressMenu.ShowMenu ();
                        break;

                    case "4":
                        _roomMenu.ShowMenu ();
                        break;
                    case "5":
                        _bookingMenu.ShowMenu ();
                        break;
                    case "0":
                        mainMenuRunning = false;
                        break;
                    default:
                        Console.WriteLine ("Ogiltigt val. Tryck valfri tangent för att fortssätta!");
                        break;
                }
            }
        }
    }
}