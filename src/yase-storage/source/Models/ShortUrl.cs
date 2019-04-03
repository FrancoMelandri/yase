namespace yase_storage.Models
{
    public class ShortUrl 
    {
        public string OriginalUrl { get; set; }
        public string TinyUrl { get; set; }
        public long Ttl { get; set; }
    }
}