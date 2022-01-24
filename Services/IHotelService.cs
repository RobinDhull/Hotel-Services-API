using HotelServices.Models;

namespace HotelServices.Services
{
    public interface IHotelService
    {
        List<HotelService> GetHotelServices();
        HotelService GetHotelService(string id);
        HotelService AddHotelService(HotelService hotelService);
        void UpdateHotelService(string id, HotelService hotelService);
        void DeleteHotelService(string id);
    }
}
