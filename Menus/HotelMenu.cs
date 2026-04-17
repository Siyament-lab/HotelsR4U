using HotelsR4U.Entities;
using HotelsR4U.Services;


namespace HotelsR4U.Menus
{
    public class HotelMenu : MenuBase
    {
        private readonly HotelService _hotelService;
        private readonly AddressService _addressService;

        public HotelMenu ( HotelService hotelService, AddressService addressService )
        {
            _hotelService = hotelService;
            _addressService = addressService;
        }

        protected override string GetMenuTitle () => "--- Hotellmeny ---";

        protected override void ShowAll ()
        {
            Console.Clear ();
            var hotels = _hotelService.GetAllHotels();

            foreach (var hotel in hotels)
            {
                Console.WriteLine ($"{hotel.HotelID}: {hotel.HotelName}, {hotel.Email}, {hotel.Phone}");
            }

            Pause ();
        }

        protected override void Add ()
        {
            Console.Clear ();

            Console.Write ("Ange hotellnamn: ");
            var hotelName = Console.ReadLine ();

            Console.Write ("Ange email: ");
            var email = Console.ReadLine ();

            Console.Write ("Ange telefon: ");
            var phone = Console.ReadLine ();

            Console.WriteLine ("\n--- Lägg till adress för hotellet ---");
            var address = PromptAddress (HotelsR4U.Enums.AddressType.Hotel);

            var createdAddress = _addressService.AddAddress (address);

            var hotel = new Hotel
            {
                HotelName = hotelName!,
                Email = email!,
                Phone = phone!,
                AddressID = createdAddress.AddressID
            };

            _hotelService.AddHotel (hotel);

            Console.WriteLine ("Nytt Hotell och adress tillaga.");
            Pause ();
        }

        protected override void Update ()
        {
            Console.Clear ();

            Console.Write ("Ange HotelID att uppdatera: ");
            int hotelId = int.Parse (Console.ReadLine ()!);

            Console.Write ("Ange nytt hotellnamn: ");
            var hotelName = Console.ReadLine ();

            Console.Write ("Ange ny email: ");
            var email = Console.ReadLine ();

            Console.Write ("Ange ny telefon: ");
            var phone = Console.ReadLine ();

            //Console.Write ("Ange nytt AddressID: ");
            //int addressId = int.Parse (Console.ReadLine ()!);

            var hotel = new Hotel
            {
                HotelID = hotelId,
                HotelName = hotelName!,
                Email = email!,
                Phone = phone!,
                //ddressID = addressId
            };

            _hotelService.UpdateHotel (hotel);

            Console.WriteLine ("Hotell uppdaterat.");
            Pause ();
        }

        protected override void Delete ()
        {
            Console.Clear ();

            Console.Write ("Ange HotelID att ta bort: ");
            int hotelId = int.Parse (Console.ReadLine ()!);

            _hotelService.DeleteHotel (hotelId);

            Console.WriteLine ("Hotell borttaget.");
            Pause ();
        }
    }
}
