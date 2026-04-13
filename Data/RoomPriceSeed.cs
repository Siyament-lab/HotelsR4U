using HotelsR4U.Entities;


namespace HotelsR4U.Data
{
    public static class RoomPriceSeed
    {
        //Skapar en prislista baserad på rum-listan
        public static List<RoomPrice> ActualRoomPrices ( List<Room> rooms )
        {
            return rooms.Select (r => new RoomPrice
            {
                RoomID = r.RoomID,
                ValidFrom = new DateTime(2026,1,1),
                ValidTo = new DateTime(2026,12,31),

                // Sätter priset baserat på rumstypen, och extra säng tillägg
                PricePerNight = r.RoomType
                switch
                {
                    "Suite" => 2000m,
                    "Double" => 1200m,
                    "Single" => 900m,
                    _ => 0m
                },

                ExtraBedPrice = r.ExtraBed ? 300m : 0m,

            }).ToList ();
        }
    }
}
