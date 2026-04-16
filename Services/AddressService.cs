using HotelsR4U.Contexts;
using HotelsR4U.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsR4U.Services
{
    public class AddressService
    {
        private readonly HotelDbContext _dbContext;
        private readonly RelationGuardService _relationGuardService;

        public AddressService ( HotelDbContext dbContext )
        {
            _dbContext = dbContext;
            _relationGuardService = new RelationGuardService (dbContext);
        }
            
        public List<Address> GetAllAddresses ()
        {
            return _dbContext.Addresses.ToList ();
        }
        // Visa alla adresser, inklusive relaterade hotell och gäster
        public List<Address> GetAllAddressesWithRelations ()
        {
            return _dbContext.Addresses
                .Include (a => a.Hotels)
                .Include (a => a.Guests)
                .ToList ();
        }
        public Address AddAddress ( Address address )
        {
            _dbContext.Addresses.Add (address);
            _dbContext.SaveChanges ();
            return address;
        }

        public bool UpdateAddress ( Address address )
        {
            var existing = _dbContext.Addresses.FirstOrDefault (a => a.AddressID == address.AddressID);
            if(existing == null)
                return false;

            existing.StreetName = address.StreetName;
            existing.StreetNumber = address.StreetNumber;
            existing.PostalCode = address.PostalCode;
            existing.City = address.City;
            existing.Country = address.Country;

            _dbContext.SaveChanges ();
            return true;
        }

        public void DeleteAddress ( int addressId )
        {
            var address = _dbContext.Addresses.FirstOrDefault (a => a.AddressID == addressId);
            if(address == null)
                throw new Exception ("Adressen finns inte.");

            // Skydda relationer (hotell/gäster)
           _relationGuardService.EnsureAddressCanBeDeleted (addressId);

            _dbContext.Addresses.Remove (address);
            _dbContext.SaveChanges ();
        }
    }
}