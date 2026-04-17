using HotelsR4U.Contexts;
using HotelsR4U.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsR4U.Services
{
    public class GuestService
    {
        private readonly HotelDbContext _dbContext;
        private readonly RelationGuardService _relationGuardService;
        public GuestService ( HotelDbContext dbContext )
        {
            _dbContext = dbContext;
            _relationGuardService = new RelationGuardService (dbContext);
        }
        // Hämtar alla gäster från databasen
        public List<Guest> GetAllGuests ()
        {
            return _dbContext.Guests.ToList ();
        }
        // Lägger till en ny gäst i databasen
        public bool AddGuest ( Guest guest )
        {
            _dbContext.Guests.Add (guest);
            _dbContext.SaveChanges ();
            return true;
        }
        // Uppdaterar en befintlig gäst i databasen
        public bool UpdateGuest ( Guest guest )
        {
            var existingGuest = _dbContext.Guests.Find (guest.GuestID);
            if (existingGuest == null)
                return false;
            existingGuest.FirstName = guest.FirstName;
            existingGuest.LastName = guest.LastName;
            existingGuest.Email = guest.Email;
            existingGuest.Phone = guest.Phone;
            existingGuest.AddressID = guest.AddressID;
            _dbContext.SaveChanges ();
            return true;
        }
        // Tar bort en gäst från databasen efter att ha kontrollerat relationer
        public void DeleteGuest ( int guestID )
        {
            var guest = _dbContext.Guests.FirstOrDefault (g => g.GuestID == guestID);
            if (guest == null)
                return;
            _relationGuardService.EnsureGuestCanBeDeleted (guestID);

            var addressId = guest.AddressID;

            _dbContext.Guests.Remove (guest);
            _dbContext.SaveChanges ();
            return;

            var guestDelete = _dbContext.Guests.Where (gd => gd.GuestID == guestID).ToList ();
            if (guestDelete.Any ())
            {
                _dbContext.Guests.RemoveRange (guestDelete);
                _dbContext.SaveChanges ();

            }
        }
    }
}
