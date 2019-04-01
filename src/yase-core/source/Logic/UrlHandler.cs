using System;
using System.Net;
using yase_core.Models;

namespace yase_core.Logic 
{
    public interface IUrlHandler
    {
        Option<string> GetHash(string url);
    }

    public class UrlHandler : IUrlHandler
    {
        ISettings _settings;

        public UrlHandler(ISettings settings) 
        {
            _settings = settings;
        }        

        public Option<string> GetHash(string url)
        {
            try {
                var uri = new Uri(url);
                var baseUrl = string.Format("{0}://{1}", uri.Scheme, uri.Host);
                if (baseUrl != _settings.BaseUrl)
                    return Option.None<string>();
                var path = uri.AbsolutePath.Substring(1);
                if (path.Length != _settings.Length)
                    return Option.None<string>();
                return path.ToOption();           
            }
            catch {
                return Option.None<string>();
            }
        }
    }
}