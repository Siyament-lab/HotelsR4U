using HotelsR4U.Entities;
using HotelsR4U.Enums;


namespace HotelsR4U.Data
{
    public static class AddressSeed
    {
        public static List<Address> GetAddresses ()
        {
            return new List<Address>
            {
                new Address
                {
                    
                    StreetName = "Main Street",
                    StreetNumber = "123",
                    PostalCode = "12345",
                    City = "Anytown",
                    Country = "USA",
                    AddressType = AddressType.Hotel
                },
                new Address
                {
                   
                    StreetName = "Second Street",
                    StreetNumber = "456",
                    PostalCode = "67890",
                    City = "Othertown",
                    Country = "USA",
                    AddressType = AddressType.Guest
                },
                new Address
                {
                   
                    StreetName = "Third Street",
                    StreetNumber = "789",
                    PostalCode = "54321",
                    City = "Sometown",
                    Country = "USA",
                    AddressType = AddressType.Guest
                },
                new Address
                {
                    
                    StreetName = "Fourth Street",
                    StreetNumber = "101",
                    PostalCode = "98765",
                    City = "Anycity",
                    Country = "USA",
                    AddressType = AddressType.Guest
                },
                  new Address
                  {
                      
                      StreetName = "Fifth Street",
                      StreetNumber = "202",
                      PostalCode = "56789",
                      City = "Othercity",
                      Country = "USA",
                      AddressType = AddressType.Guest
                  }
            };
        }
    }
}