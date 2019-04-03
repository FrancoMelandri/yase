using System;

namespace yase_core.Models
{    
    public class ShortUrl 
    {
        public string OriginalUrl { get; set; }
        public string TinyUrl { get; set; }
        public long ttl { get; set; }
    }
}