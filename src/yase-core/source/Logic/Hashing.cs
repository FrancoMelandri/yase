using System;
using System.Net;
using yase_core.Models;

namespace yase_core.Logic 
{
    public interface IHashing
    {
        HashingModel Create(Uri url);
    }

    public class Hashing : IHashing
    {
        ISettings _settings;
        IHash _hash;
        ITimeToLive _timeToLive;

        public Hashing(ISettings settings,
                       IHash hash,
                       ITimeToLive timeToLive) 
        {
            _settings = settings;
            _hash = hash; 
            _timeToLive = timeToLive;
        }        

        public HashingModel Create(Uri url)
        {           
            var hashed = _hash.Get(url.AbsolutePath, 
                                   _settings.Length);

            var tinyUrl = new Uri(string.Format("{0}/{1}", 
                                                _settings.BaseUrl,
                                                hashed));
            return new HashingModel 
            {
                TinyUrl = tinyUrl,
                OriginalUrl = url,
                HashedUrl = hashed,
                ttl = _timeToLive.Get(_settings.ttl),
                Hitted = 0
            };
        }
    }
}