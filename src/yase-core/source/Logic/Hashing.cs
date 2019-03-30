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

        public Hashing(ISettings settings) 
        {
            _settings = settings;
        }

        public HashingModel Create(Uri url)
        {
            return new HashingModel 
            {
                Shortener = url,
                Hitted = 0
            };
        }
    }
}