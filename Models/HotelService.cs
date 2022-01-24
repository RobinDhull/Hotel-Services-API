using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace HotelServices.Models
{
    [BsonIgnoreExtraElements]
    public class HotelService
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;
        [BsonElement("price")]
        public string Price { get; set; } = string.Empty;
        [BsonElement("photos")]
        public List<byte[]> Photos { get; set; } = new List<byte[]>();
        [BsonElement("availability")]
        public int Availability { get; set; } = 0;
    }
}
