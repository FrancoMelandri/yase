using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OpenTracing;
using Prometheus.Client;

using yase_core.Logic;
using yase_core.Models;

namespace yase_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EngineController : ControllerBase
    {
        private const string JAEGER_GET_TINY = "GET_TINY";
        private const string JAEGER_ADD_TINY = "ADD_TINY";
        private const string JAEGER_DELETE_TINY = "DELETE_TINY";

        private ISettings _settings;
        private IHashing _hashing;
        private IStorageServiceWrapper _storageServiceWrapper;
        private IUrlHandler _urlHandler;
        private IValidator _validator;
        private ITracer _tracer;

        // private readonly Counter _counter = Metrics.CreateCounter("myCounter", "some help about this");
        
        public EngineController(ISettings settings,
                                IHashing hashing,
                                IUrlHandler urlHandler,
                                IValidator validator,
                                IStorageServiceWrapper storageServiceWrapper,
                                ITracer tracer) 
        {
            _settings = settings;
            _hashing = hashing;
            _urlHandler = urlHandler;
            _validator = validator;
            _storageServiceWrapper = storageServiceWrapper;
            _tracer = tracer;
        }

        [HttpPost]
        public ActionResult Get(HashRequest url)
        {
            using (IScope scope = _tracer.BuildSpan(JAEGER_GET_TINY).StartActive(finishSpanOnDispose: true))
            {
                // System.Console.WriteLine ("GET: " + url.Url.ToString());
                // _counter.Inc();
                var hash = _urlHandler
                            .GetHash(url.Url)
                            .Match(_ => _,
                               () => string.Empty);
                return _storageServiceWrapper
                            .Get(hash)
                            .Match<ActionResult>(_ => _validator.Validate<ActionResult>(_,
                                                                __ =>  new JsonResult(__.To(_settings.BaseUrl)),
                                                                __ => {
                                                                        Delete(new HashRequest {Url = __.TinyUrl });
                                                                        return new NotFoundResult();
                                                                      } ) ,
                                                 () => new NotFoundResult() );
            }
        }

        [HttpPut]
        public ActionResult Tiny(HashRequest url)
        {
            using (IScope scope = _tracer.BuildSpan(JAEGER_ADD_TINY).StartActive(finishSpanOnDispose: true))
            {
                var hased = _hashing.Create(new Uri(url.Url));
                return _storageServiceWrapper
                            .GetOrInsert(hased.To())
                            .Match<ActionResult>(_ => _validator.Validate<ActionResult>(_,
                                                                __ =>  new JsonResult(__.To(_settings.BaseUrl)),
                                                                __ => {
                                                                        Delete(new HashRequest {Url = __.TinyUrl });
                                                                        return new NotFoundResult();
                                                                      } ) ,
                                                 () => new NotFoundResult() );
            }
        }

        [HttpDelete]
        public ActionResult Delete(HashRequest url)
        {
            using (IScope scope = _tracer.BuildSpan(JAEGER_DELETE_TINY).StartActive(finishSpanOnDispose: true))
            {
                var hash = _urlHandler
                            .GetHash(url.Url)
                            .Match(_ => _,
                                   () => string.Empty);
                return _storageServiceWrapper
                            .Delete(hash)
                            .Match<ActionResult>(_ => new OkResult(), 
                                                 () => new NotFoundResult() );
            }
        }
    }
}
