using HotelsR4U.Entities;
using HotelsR4U.Enums;
using HotelsR4U.Services;

namespace HotelsR4U.Menus
{
    public class AddressMenu : MenuBase
    {
        private readonly AddressService _addressService;
        private readonly RelationGuardService _relationGuardService;

        public AddressMenu ( AddressService addressService )
        {
            _addressService = addressService;
        }

        protected override string GetMenuTitle () => "--- Adressmeny ---";

        protected override void ShowAll ()
        {
            Console.Clear ();

            var addresses = _addressService.GetAllAddresses ();

            foreach (var a in addresses)
            {
                Console.WriteLine ($"{a.AddressID}: {a.StreetName}, {a.StreetNumber}, {a.PostalCode} {a.City}, {a.Country}");
            }

            Pause ();
        }

        protected override void Add ()
        {
            Console.Clear ();

            Console.Write ("Gata: ");
            var streetName = Console.ReadLine ();

            Console.Write ("Gatunummer: ");
            var streetNumber = Console.ReadLine ();

            Console.Write ("Postnummer: ");
            var postalCode = Console.ReadLine ();

            Console.Write ("Stad: ");
            var city = Console.ReadLine ();

            Console.Write ("Land: ");
            var country = Console.ReadLine ();

            Console.Write ("Vilken typ av adress? (1: Hotel, 2: Gäst): ");
            var addressType = Console.ReadLine ();

            var address = new Address
            {
                StreetName = streetName!,
                StreetNumber = streetNumber!,
                PostalCode = postalCode!,
                City = city!,
                Country = country!,
                AddressType = addressType == "1" ? AddressType.Hotel : AddressType.Guest
            };

            _addressService.AddAddress (address);

            Console.WriteLine ("Adress tillagd.");
            Pause ();
        }

        protected override void Update ()
        {
            Console.Clear ();

            Console.Write ("Ange AddressID att uppdatera: ");
            int id = int.Parse (Console.ReadLine ()!);

            Console.Write ("Ny gata: ");
            var street = Console.ReadLine ();

            Console.Write ("Nytt postnummer: ");
            var postalCode = Console.ReadLine ();

            Console.Write ("Ny stad: ");
            var city = Console.ReadLine ();

            Console.Write ("Nytt land: ");
            var country = Console.ReadLine ();

            var address = new Address
            {
                AddressID = id,
                StreetName = street!,
                PostalCode = postalCode!,
                City = city!,
                Country = country!
            };

            _addressService.UpdateAddress (address);

            Console.WriteLine ("Adress uppdaterad.");
            Pause ();
        }

        protected override void Delete ()
        {
            Console.Clear ();

            Console.Write ("Ange AddressID att ta bort: ");
            int id = int.Parse (Console.ReadLine ()!);

            _addressService.DeleteAddress (id);

            Console.WriteLine ("Adress borttagen.");
            Pause ();
        }
    }
}