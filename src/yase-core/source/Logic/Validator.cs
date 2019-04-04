using System;
using System.Net;
using yase_core.Models;

namespace yase_core.Logic 
{
    public interface IValidator
    {
        TResult Validate<TResult>(ShortUrl request,
                                  Func<ShortUrl, TResult> onValid,
                                  Func<ShortUrl, TResult> onInvalid);
    }

    public class Validator : IValidator
    {
        ITimeToLive _timeToLive;

        public Validator(ITimeToLive timeToLive)
        {
            _timeToLive = timeToLive;
        }        

        public TResult Validate<TResult>(ShortUrl request,
                                         Func<ShortUrl, TResult> onValid,
                                         Func<ShortUrl, TResult> onInvalid)
        {
            if (_timeToLive.Now() > request.ttl)                
                return onInvalid(request);
            return onValid(request);
        }
    }
}