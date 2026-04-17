using HotelsR4U.Services;
using HotelsR4U.Entities;


namespace HotelsR4U.Menus
{
    public class GuestMenu : MenuBase
    {
        private readonly GuestService _guestService;
        private readonly RelationGuardService _relationGuardService;
        private readonly AddressService _addressService;

        public GuestMenu ( GuestService guestService, AddressService addressService )
        {
            _guestService = guestService;
            _addressService = addressService;
        }

        protected override string GetMenuTitle () => "--- Gästmeny ---";

        protected override void ShowAll ()
        {
            Console.Clear ();
            var guests = _guestService.GetAllGuests ();

            foreach (var guest in guests)
            {
                Console.WriteLine ($"{guest.GuestID}: {guest.FirstName} {guest.LastName}, {guest.Email}, {guest.Phone}");
            }

            Pause ();
        }

        protected override void Add ()
        {
            Console.Clear ();

            Console.Write ("Förnamn: ");
            var firstName = Console.ReadLine ();

            Console.Write ("Efternamn: ");
            var lastName = Console.ReadLine ();

            Console.Write ("Email: ");
            var email = Console.ReadLine ();

            Console.Write ("Telefon: ");
            var phone = Console.ReadLine ();

            Console.WriteLine ("\n--- Lägg till adress för gästen ---");
            var address = PromptAddress (HotelsR4U.Enums.AddressType.Guest);

            var createdAddress = _addressService.AddAddress (address);

            var guest = new Guest
            {
                FirstName = firstName!,
                LastName = lastName!,
                Email = email!,
                Phone = phone!,
                AddressID = createdAddress.AddressID
            };

            _guestService.AddGuest (guest);

            Console.WriteLine ("Ny gäst och adress tillagd.");
            Pause ();
        }

        protected override void Update ()
        {
            Console.Clear ();

            Console.Write ("Ange GuestID att uppdatera: ");
            int guestId = int.Parse (Console.ReadLine ()!);

            Console.Write ("Nytt förnamn: ");
            var firstName = Console.ReadLine ();

            Console.Write ("Nytt efternamn: ");
            var lastName = Console.ReadLine ();

            Console.Write ("Ny email: ");
            var email = Console.ReadLine ();

            Console.Write ("Ny telefon: ");
            var phone = Console.ReadLine ();

            Console.Write ("Nytt AddressID: ");
            int addressId = int.Parse (Console.ReadLine ()!);

            var guest = new Guest
            {
                GuestID = guestId,
                FirstName = firstName!,
                LastName = lastName!,
                Email = email!,
                Phone = phone!,
                AddressID = addressId
            };

            _guestService.UpdateGuest (guest);

            Console.WriteLine ("Gäst uppdaterad.");
            Pause ();
        }

        protected override void Delete ()
        {
            Console.Clear ();

            Console.Write ("Ange GuestID att ta bort: ");
            int guestId = int.Parse (Console.ReadLine ()!);

            _guestService.DeleteGuest (guestId);

            Console.WriteLine ("Gäst borttagen.");
            Pause ();
        }
    }
}