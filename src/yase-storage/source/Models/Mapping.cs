namespace yase_storage.Models
{
    public static class ShortUrlModelExtension
    {
        public static ShortUrl To(this ShortUrlModel model)
        {
            return new ShortUrl 
            {
                OriginalUrl = model.OriginalUrl,
                TinyUrl = model.TinyUrl
            };
        }        
    }
}