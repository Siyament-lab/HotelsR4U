using HotelsR4U.Contexts;
using HotelsR4U.Entities;
/// <summary>
/// RelationGuardService används för att säkerställa att relationer i databasen
/// inte bryts när man försöker radera en entitet som är kopplad till andra entiteter
/// via relationer /FK. Detta för att hålla ren filer med logik i controllers och services, 
/// och istället samla all logik som har med relationer att göra i denna service.
/// </summary>
public class RelationGuardService
{
    private readonly HotelDbContext _dbContext;

    public RelationGuardService ( HotelDbContext dbContext )
    {
        _dbContext = dbContext;
    }
    //Hotel knuten till rum
    public void EnsureHotelCanBeDeleted ( int hotelID )
    {
        if (_dbContext.Rooms.Any (r => r.HotelID == hotelID))
            throw new Exception ("Hotellet får ej raderas, det är kopplad till andra entiteter.");
    }
    //Address knuten till hotell eller gäst
    public void EnsureAddressCanBeDeleted ( int addressID )
    {
        if (_dbContext.Hotels.Any (h => h.AddressID == addressID) 
            || _dbContext.Guests.Any (g => g.AddressID == addressID))
            throw new Exception ("Adressen får ej raderas,kopplad till andra entiteter.");
    }
    //Room knuten till bokning
    public void EnsureRoomCanBeDeleted ( int roomId )
    {
        if (_dbContext.Bookings.Any (b => b.RoomID == roomId))
            throw new Exception ("Rummet får ej raderas, används i bokning.");

    }
    //RoomPrice knuten till bokning
    public void EnsureRoomPriceCanBeDeleted ( int roomPriceId )
    {
        if (_dbContext.Bookings.Any (b => b.RoomPriceID == roomPriceId))
            throw new Exception ("Rumspriset får ej raderas, används i bokning.");
    }
    //Guest knuten till bokning
    public void EnsureGuestCanBeDeleted ( int guestId )
    {
        if (_dbContext.Bookings.Any (b => b.GuestID == guestId))
            throw new Exception ("Gästen får ej raderas, används i bokning.");
    }
    
}