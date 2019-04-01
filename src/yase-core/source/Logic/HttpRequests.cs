using System.Collections.Generic;
using System.IO;
using System.Net;

namespace yase_core.Logic
{
    public interface IHttpRequests
    {
        string Post(string url, string json);
    }

    public class HttpRequests : IHttpRequests
    {
        private const string POST = "POST";
        private IWebRequestFactory _webRequestFactory;

        public HttpRequests(IWebRequestFactory webRequestFactory)
        {
            _webRequestFactory = webRequestFactory;
        }

        public string Post(string url, string json)
        {
            var request = _webRequestFactory.Create(url, POST, json);
            return MakeRestCall(request);
        }

        private string MakeRestCall(HttpWebRequest req)
        {
            var httpWebResponse = (HttpWebResponse)req.GetResponse();
            string responseString;
            using (var reader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                responseString = reader.ReadToEnd();
            }
            httpWebResponse.Close();
            return responseString;
        }
    }
}
