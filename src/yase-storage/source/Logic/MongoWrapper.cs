using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

using yase_storage.Models;

namespace yase_storage.Logic
{
    public interface IMongoWrapper
    {
        List<ShortUrlModel> GetUrls();
        Option<ShortUrlModel> GetUrl(string tiny);
    }

    public class MongoWrapper : IMongoWrapper
    {
        const string SHORTENER_CONNECTION = "ShortenerConnection";
        const string SHORTENER_DB = "ShortenerDb";
        const string SHORTENER_COLLECTION = "ShortUrls";

        private readonly IMongoCollection<ShortUrlModel> _shortUrls;

        public MongoWrapper(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString(SHORTENER_CONNECTION));
            var database = client.GetDatabase(SHORTENER_DB);
            _shortUrls = database.GetCollection<ShortUrlModel>(SHORTENER_COLLECTION);
        }

        public List<ShortUrlModel> GetUrls()
        {
            return _shortUrls
                        .Find(url => true)
                        .ToList();
        }

        public Option<ShortUrlModel> GetUrl(string tiny)
        {
            return _shortUrls
                        .Find<ShortUrlModel>(url => url.TinylUrl == tiny)
                        .FirstOrDefault()
                        .ToOption();
        }
    }
}