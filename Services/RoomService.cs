using HotelsR4U.Contexts;
using HotelsR4U.Entities;

namespace HotelsR4U.Services
{
    public class RoomService
    {
        private readonly HotelDbContext _DbContext;

        public RoomService ( HotelDbContext dbContext )
        {
            _DbContext = dbContext;
        }
        //Skapar lista över tillgängliga rum baserat på hotellID och datumintervall
        public List<Room> GetAvailableRooms ( int hotelID, DateTime checkInDate, DateTime checkOutDate )
        {
            return _DbContext.Rooms
                .Where (room => room.HotelID == hotelID)
                .Where (room => !_DbContext.Bookings.Any (b =>
                    b.RoomID == room.RoomID &&
                    b.Status != Enums.BookingStatus.Cancelled &&
                    b.CheckInDate < checkOutDate &&
                    b.CheckOutDate > checkInDate))
                .ToList ();
        }
        // Beräknar max antal extrasängar baserat på rumstyp och storlek
        public int CalculateMaxExtraBeds ( string roomType, int roomSize )
        {
            if (roomType == "Single")
                return 0;

            if ((roomType == "Double" || roomType == "Suite") && roomSize >= 40)
                return 2;

            if ((roomType == "Double" || roomType == "Suite") && roomSize >= 30)
                return 1;

            return 0;
        }
        // Sätter max antal extrasängar för ett rum baserat på dess typ och storlek
        public void SetMaxExtraBeds ( Room room )
        {
            room.MaxExtraBeds = CalculateMaxExtraBeds (room.RoomType, ParseRoomSize (room.RoomSize));
        }

        // Lägger till ett nytt rum i databasen och sätter max antal extrasängar innan sparning
        public bool AddRoom ( Room room )
        {
            SetMaxExtraBeds (room);

            _DbContext.Rooms.Add (room);
            _DbContext.SaveChanges ();
            return true;
        }
        
        // Uppdaterar ett befintligt rum i databasen,sedan sparar det i DB
        public bool UpdateRoom ( Room room )
        {
            var existingRoom = _DbContext.Rooms.FirstOrDefault (r => r.RoomID == room.RoomID);
            if (existingRoom == null)
                return false;

            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.RoomType = room.RoomType;
            existingRoom.RoomSize = room.RoomSize;
            existingRoom.HotelID = room.HotelID;

            SetMaxExtraBeds (existingRoom);

            _DbContext.SaveChanges ();
            return true;
        }
        //Omvandlar sträng --> int för att kunna använda det i beräkningen av max antal extrasängar
        private int ParseRoomSize ( string roomSize )
        {
            if (string.IsNullOrWhiteSpace (roomSize))
                return 0;

            var digitsOnly = new string (roomSize.Where (char.IsDigit).ToArray ());

            if (int.TryParse (digitsOnly, out int size))
                return size;

            return 0;
        }
    }
}