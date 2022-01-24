using HotelServices.Models;
using MongoDB.Driver;

namespace HotelServices.Services
{
    public class AdditionalServices : IHotelService
    {
        private readonly IMongoCollection<HotelService> _hotelServices;

        public AdditionalServices(IHotelServiceDataBaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DataBaseName);
            _hotelServices = database.GetCollection<HotelService>(settings.HotelServicesCollectionName);
        }

        HotelService IHotelService.AddHotelService(HotelService hotelService)
        {
            _hotelServices.InsertOne(hotelService);
            return hotelService;
        }

        void IHotelService.DeleteHotelService(string id)
        {
            _hotelServices.DeleteOne(hotelService => hotelService.Id == id);
        }

        HotelService IHotelService.GetHotelService(string id)
        {
            return _hotelServices.Find(hotelService => hotelService.Id == id).FirstOrDefault();
        }

        List<HotelService> IHotelService.GetHotelServices()
        {
            return _hotelServices.Find(hotelService => true).ToList();
        }

        void IHotelService.UpdateHotelService(string id, HotelService hotelService)
        {
            _hotelServices.ReplaceOne(hotelService => hotelService.Id == id, hotelService);
        }
    }
}
