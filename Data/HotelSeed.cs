using HotelsR4U.Entities;
using HotelsR4U.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelsR4U.Data
{
    public static class HotelSeed
    {
        public static List<Hotel> GetHotels ( List<Address> addresses )
        {
            var hotelAddress = addresses
                .FirstOrDefault(a => a.AddressType == AddressType.Hotel);
                
            return new List<Hotel>
            {
                new Hotel
                {
                   
                    HotelName = "HotelsR4U",
                    Email = "info@hotelsr4u.com",
                    Phone = "+46812345678",
                    AddressID = hotelAddress.AddressID

                }
            };
        }
    }   
}
