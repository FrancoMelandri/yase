namespace yase_core.Models
{
    public static class ShortUrlModelExtension
    {
        public static ShortUrl To(this HashingModel model)
        {
            return new ShortUrl 
            {
                OriginalUrl = model.OriginalUrl.AbsoluteUri,
                TinyUrl = model.HashedUrl
            };
        }
    }
}