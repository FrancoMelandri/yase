using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace yase_storage.Models
{
    public class ShortUrlModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("originalUrl")]
        public string OriginalUrl { get; set; }

        [BsonElement("tinyUrl")]
        public string TinyUrl { get; set; }

        [BsonElement("ttl")]
        public long Ttl { get; set; }
    }
}