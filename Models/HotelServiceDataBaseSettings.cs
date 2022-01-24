namespace HotelServices.Models
{
    public class HotelServiceDataBaseSettings : IHotelServiceDataBaseSettings
    {
        public string HotelServicesCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DataBaseName { get; set; } = string.Empty;
    }
}
