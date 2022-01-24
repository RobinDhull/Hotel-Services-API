namespace HotelServices.Models
{
    public interface IHotelServiceDataBaseSettings
    {
        string HotelServicesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DataBaseName { get; set; }
    }
}
