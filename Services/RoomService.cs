using HotelsR4U.Contexts;
using HotelsR4U.Entities;

namespace HotelsR4U.Services
{
    public class RoomService
    {
        private readonly HotelDbContext _DbContext;
        private readonly RelationGuardService _relationGuardService;

        public RoomService ( HotelDbContext dbContext )
        {
            _DbContext = dbContext;
            _relationGuardService = new RelationGuardService (dbContext);
        }
        //Hämta alla rum
        public List<Room> GetAllRooms ()
        {
            return _DbContext.Rooms.ToList ();
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

        // Sätter max antal extrasängar för ett rum baserat på dess typ och storlek
        public void SetMaxExtraBeds ( Room room )
        {
            var roomSize = ParseRoomSize (room.RoomSize);

            if (room.RoomType == "Single")
            {
                room.MaxExtraBeds = 0;
                return;
            }

            if ((room.RoomType == "Double" || room.RoomType == "Suite") && roomSize >= 40)
            {
                room.MaxExtraBeds = 2;
                return;
            }

            if ((room.RoomType == "Double" || room.RoomType == "Suite") && roomSize >= 30)
            {
                room.MaxExtraBeds = 1;
                return;
            }

            room.MaxExtraBeds = 0;
        }

        // Lägger till ett nytt rum i databasen och sätter max antal extrasängar innan sparning
        public Room AddRoom ( Room room )
        {
            SetMaxExtraBeds (room);

            _DbContext.Rooms.Add (room);
            _DbContext.SaveChanges ();
            return room;
        }

        // Uppdaterar ett befintligt rum &
        // sätter max antal extrasängar baserad på storlek & typ
        public void UpdateRoom ( Room room )
        {
            var existingRoom = _DbContext.Rooms.FirstOrDefault (r => r.RoomID == room.RoomID);
            if (existingRoom == null)
                return;
            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.RoomType = room.RoomType;
            existingRoom.RoomSize = room.RoomSize;
            existingRoom.HotelID = room.HotelID;

            SetMaxExtraBeds (existingRoom);

            _DbContext.SaveChanges ();
            return;
        }
        // Radera ett rum
        public void DeleteRoom ( int roomId )
        {
            var room = _DbContext.Rooms.FirstOrDefault (r => r.RoomID == roomId);
            if (room == null)
                throw new Exception ("Rummet finns inte.");

            //Förhindra borttagning av rum som har bokningar kopplade till sig
            _relationGuardService.EnsureRoomCanBeDeleted (roomId);

            //Ta bort först knutna rum-priser innan rummet tas bort
            //för att undvika FK-relation problem
            var roomPrices = _DbContext.RoomPrices.Where (rp => rp.RoomID == roomId).ToList ();
            if(roomPrices.Any ())
            {
                _DbContext.RoomPrices.RemoveRange (roomPrices);
            }


            _DbContext.Rooms.Remove (room);
            _DbContext.SaveChanges ();
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