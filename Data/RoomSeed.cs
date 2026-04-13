using HotelsR4U.Entities;


namespace HotelsR4U.Data
{
    public static class RoomSeed
    {
        // Metod som skapar 6 rum per hotell som en lista
        public static List<Room> HotelRooms ( List<Hotel> hotels )
        {
            var allRooms = new List<Room> ();
            
            foreach (var hotel in hotels)
            {
                for (int i = 1; i <= 6; i++)
                {
                    //Hotellen ska ha 3 rumtyper  (Singel,dubbell och Svit) med olika storlekar.
                    //Använder modulo 3 för de 3 typerna
                    var typeIndex = i % 3;

                    allRooms.Add (new Room
                    {
                        // Skapar rumsnummer 101-104 för första hotellet, 201-204 för nästa
                        RoomNumber = $"{(hotel.HotelID)}0{i}",
                        // Bestämmer rumstyp baserat på typeIndex (1 = Single, 2 = Double, 0 = Svit)
                        RoomType = (typeIndex == 1) ? "Single" : (typeIndex == 2) ? "Double" : "Suite",
                        RoomSize = (typeIndex == 1) ? "18sqm" : (typeIndex == 2) ? "25sqm" : "30sqm",
                        ExtraBed = (typeIndex != 1), // Dubbelrum & Svit kan få en extrabädd, singelrum är exkluderat
                        HotelID = hotel.HotelID      // Detta skapar kopplingen mellan rum & Hotell i SQL
                    });
                }
            }
            return allRooms;
        }

    }
}
