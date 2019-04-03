using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace yase_storage.Models
{
    public class ShortUrlRequest
    {
        public string OriginalUrl { get; set; }
        public string TinyUrl { get; set; }
        public long ttl { get; set; }
    }
}