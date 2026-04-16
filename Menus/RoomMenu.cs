using HotelsR4U.Entities;
using HotelsR4U.Services;

namespace HotelsR4U.Menus
{
    public class RoomMenu : MenuBase
    {
        private readonly RoomService _roomService;
        private readonly RoomPriceService _roomPriceService;

        public RoomMenu ( RoomService roomService, RoomPriceService roomPriceService )
        {
            _roomService = roomService;
            _roomPriceService = roomPriceService;
        }

        protected override string GetMenuTitle () => "--- Rumsmeny ---";

        protected override void ShowAll ()
        {
            Console.Clear ();
            var rooms = _roomService.GetAllRooms ();

            foreach (var room in rooms)
            {
                Console.WriteLine ($"{room.RoomID}: Nr {room.RoomNumber}, {room.RoomType}, {room.RoomSize}, ExtraBeds: {room.MaxExtraBeds}");
            }

            Pause ();
        }

        //Skapa rum och priser
        protected override void Add ()
        {
            Console.Clear ();

            Console.Write ("RoomNumber: ");
            var roomNumber = Console.ReadLine ();

            Console.Write ("RoomType (Single/Double/Suite): ");
            var roomType = Console.ReadLine ();

            Console.Write ("RoomSize (t.ex. 30sqm): ");
            var roomSize = Console.ReadLine ();

            Console.Write ("HotelID: ");
            int hotelId = int.Parse (Console.ReadLine ()!);

            var room = new Room
            {
                RoomNumber = roomNumber!,
                RoomType = roomType!,
                RoomSize = roomSize!,
                HotelID = hotelId
            };

            var createdRoom = _roomService.AddRoom (room);

            Console.Write ("Pris per natt: ");
            decimal pricePerNight = decimal.Parse (Console.ReadLine ()!);

            Console.Write ("Pris per extrasäng: ");
            decimal extraBedPrice = decimal.Parse (Console.ReadLine ()!);

            var roomPrice = new RoomPrice
            {
                //RumID angens auto av db, så vi behöver inte sätta det här
                PricePerNight = pricePerNight,
                ExtraBedPrice = extraBedPrice,
                ValidFrom = DateTime.Today,
                ValidTo = DateTime.Today.AddYears (1)
            };

            _roomPriceService.AddRoomPrice (roomPrice);

            Console.WriteLine ("Rum och rumspris tillagda.");
            Pause ();
        }

        protected override void Update ()
        {
            Console.Clear ();

            Console.Write ("Ange RoomID att uppdatera: ");
            int roomId = int.Parse (Console.ReadLine ()!);

            Console.Write ("Nytt RoomNumber: ");
            var roomNumber = Console.ReadLine ();

            Console.Write ("Ny RoomType: ");
            var roomType = Console.ReadLine ();

            Console.Write ("Ny RoomSize: ");
            var roomSize = Console.ReadLine ();

            Console.Write ("Välj Hotell-ID för befintlig hotell: ");
            int hotelId = int.Parse (Console.ReadLine ()!);

            var room = new Room
            {
                //RoomID = roomId,
                RoomNumber = roomNumber!,
                RoomType = roomType!,
                RoomSize = roomSize!,
                HotelID = hotelId
            };

            _roomService.UpdateRoom (room);

            Console.WriteLine ("Rum uppdaterat.");
            Pause ();
        }

        protected override void Delete ()
        {
            Console.Clear ();

            Console.Write ("Ange RoomID att ta bort: ");
            int roomId = int.Parse (Console.ReadLine ()!);

            _roomService.DeleteRoom (roomId);

            Console.WriteLine ("Rum borttaget.");
            Pause ();
        }
    }
}