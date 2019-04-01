using System;

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

        public static HashingModel To(this ShortUrl model, string baseUrl)
        {
            return new HashingModel 
            {
                OriginalUrl = new Uri(model.OriginalUrl),
                TinyUrl = new Uri(string.Format("{0}/{1}", baseUrl, model.TinyUrl)),
                HashedUrl = model.TinyUrl
            };
        }
    }
}