using HotelsR4U.Entities;
using HotelsR4U.Enums;

namespace HotelsR4U.Data
{
    public static class GuestSeed
    {
        public static List<Guest> GetGuests ( List<Address> addresses )
        {
            var guestAddresses = addresses
                .Where (a => a.AddressType == AddressType.Guest)
                .ToList ();

            return new List<Guest>
            {
                new Guest
                {
                   
                    FirstName = "Anna",
                    LastName = "Andersson",
                    Email = "anna.andersson@mail.se",
                    Phone = "0701111111",
                    AddressID = guestAddresses[0].AddressID
                },
                new Guest
                {
                   
                    FirstName = "Björn",
                    LastName = "Berg",
                    Email = "bjorn.berg@mail.se",
                    Phone = "0702222222",
                    AddressID = guestAddresses[1].AddressID
                },
                new Guest
                {
                    
                    FirstName = "Carla",
                    LastName = "Carlsson",
                    Email = "carla.carlsson@mail.se",
                    Phone = "0703333333",
                    AddressID = guestAddresses[2].AddressID
                },
                new Guest
                {
                    
                    FirstName = "David",
                    LastName = "Dahl",
                    Email = "david.dahl@mail.se",
                    Phone = "0704444444",
                    AddressID = guestAddresses[3].AddressID
                }
            };
        }
    }
}