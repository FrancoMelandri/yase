using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using yase_core.Models;
using yase_core.Utilities;

namespace yase_core.Logic
{
    public interface IStorageServiceWrapper
    {
        Option<ShortUrl> Get(string hash);
        Option<ShortUrl> GetOrInsert(ShortUrl shortUrl);
    }

    class StorageServiceWrapper : IStorageServiceWrapper
    {
        private const string STORAGE_BASE_URL = "StorageService";
        private const string STORAGE_HOST = "YASE_STORAGE_SERVICE_HOST";
        private const string STORAGE_PORT = "YASE_STORAGE_SERVICE_PORT";
        private IHttpRequests _httpRequests;
        private string _storageBaseUrl;

        public StorageServiceWrapper(IConfiguration config,
                                     IHttpRequests httpRequests)
        {
            _httpRequests = httpRequests;
            _storageBaseUrl = config.GetConnectionString(STORAGE_BASE_URL);

            var hostStorage = System.Environment.GetEnvironmentVariable(STORAGE_HOST);
            var portStorage = System.Environment.GetEnvironmentVariable(STORAGE_PORT);
            if (string.IsNullOrEmpty(hostStorage) || string.IsNullOrEmpty(portStorage))
                _storageBaseUrl = string.Format("http://{0}:{1}", hostStorage, portStorage);
            System.Console.WriteLine(">>>>>>>>>>> " + _storageBaseUrl);
        }

        public Option<ShortUrl> Get(string hash)
        {
            var requesturl = string.Format ("{0}/Storage/{1}",  
                                            _storageBaseUrl,
                                            hash);
            return _httpRequests
                        .Get(requesturl)
                        .FromJson<ShortUrl>()
                        .ToOption();
        }

        public Option<ShortUrl> GetOrInsert(ShortUrl tiny)
        {
            var requesturl = string.Format ("{0}/Storage",  
                                            _storageBaseUrl);
            return _httpRequests
                        .Post(requesturl,
                              tiny.ToJson())
                        .FromJson<ShortUrl>()
                        .ToOption();
        }
    }
}