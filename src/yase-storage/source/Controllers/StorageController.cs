using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

using yase_storage.Logic;
using yase_storage.Models;

namespace yase_storage.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private const string JAEGER_GET_TINY = "GET_TINY";
        private const string JAEGER_POST_TINY = "ADD_TINY";
        private const string JAEGER_DELETE_TINY = "DELETE_TINY";

        private IMongoWrapper _mongoWrapper;
        private ITracer _tracer;

        public StorageController(IMongoWrapper mongoWrapper,
                                 ITracer tracer)
        {
            _mongoWrapper = mongoWrapper;
            _tracer = tracer;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShortUrlModel>> Get()
        {
            return _mongoWrapper.GetUrls();
        }

        [HttpGet("{tiny}")]
        public ActionResult Get(string tiny)
        {
            using (IScope scope = _tracer.BuildSpan(JAEGER_GET_TINY).StartActive(finishSpanOnDispose: true))
            {
                return _mongoWrapper
                            .GetUrl(tiny)
                            .Match<ActionResult>(_ => new JsonResult(_.To()), 
                                                 () => new NotFoundResult() );
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]ShortUrlRequest value)
        {
            using (IScope scope = _tracer.BuildSpan(JAEGER_POST_TINY).StartActive(finishSpanOnDispose: true))
            {
                var mapped = value.To();
                return _mongoWrapper
                            .GetOrUpdateUrl<ActionResult>(mapped,
                                            _ => new JsonResult(_.To()), 
                                            _ => new ConflictResult() );
            }
        }

        [HttpDelete("{tiny}")]
        public ActionResult Delete(string tiny)
        {
            using (IScope scope = _tracer.BuildSpan(JAEGER_DELETE_TINY).StartActive(finishSpanOnDispose: true))
            {
                return _mongoWrapper
                            .DeleteUrl(tiny)
                            .Match<ActionResult>(_ => new OkResult(), 
                                                 () => new NotFoundResult() );
            }
        }
    }
}
