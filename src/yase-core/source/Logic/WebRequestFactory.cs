using System.IO;
using System.Net;

namespace yase_core.Logic
{
    public interface IWebRequestFactory
    {
        HttpWebRequest Create(string url, string verb, string json);
    }

    public class WebRequestFactory : IWebRequestFactory
    {
        private const string CONTENT_TYPE_JSON = "application/json";

        public HttpWebRequest Create(string url, string verb, string json)
        {
            var req = WebRequest.Create(url) as HttpWebRequest;
            req.Method = verb;
            if (!string.IsNullOrEmpty(json))
            {
                req.ContentType = CONTENT_TYPE_JSON;
                using (var writer = new StreamWriter(req.GetRequestStream()))
                {
                    writer.WriteLine(json);
                }
            }
            else
            {
                req.ContentLength = 0;
            }
            return req;
        }
    }
}
