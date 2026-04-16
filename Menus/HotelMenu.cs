using HotelsR4U.Entities;
using HotelsR4U.Services;


namespace HotelsR4U.Menus
{
    public class HotelMenu : MenuBase
    {
        private readonly HotelService _hotelService;

        public HotelMenu ( HotelService hotelService )
        {
            _hotelService = hotelService;
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

            ////AddressID väljs av db. Ge möjlighet till antingen att välja
            ////en befintlig eller att skapa en ny adress i AddressMenu
            ////eller casta så användaren skapar ny address
            //Console.Write ("Ange AddressID: ");
            //int addressId = int.Parse (Console.ReadLine ()!);

            var hotel = new Hotel
            {
                HotelName = hotelName!,
                Email = email!,
                Phone = phone!,
                //AddressID = addressId
            };

            _hotelService.AddHotel (hotel);

            Console.WriteLine ("Hotell tillagt.");
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

            Console.Write ("Ange nytt AddressID: ");
            int addressId = int.Parse (Console.ReadLine ()!);

            var hotel = new Hotel
            {
                HotelID = hotelId,
                HotelName = hotelName!,
                Email = email!,
                Phone = phone!,
                AddressID = addressId
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
