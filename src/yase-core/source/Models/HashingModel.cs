using System;

namespace yase_core.Models 
{
    public class HashingModel
    {
        public Uri TinyUrl { get; set; }
        public Uri OriginalUrl { get; set; }
        public string HashedUrl { get; set; }
        public long ttl { get; set; }
        public int Hitted { get; set; }
    }
}