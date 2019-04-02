using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using yase_core.Logic;
using yase_core.Models;

namespace yase_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EngineController : ControllerBase
    {
        private ISettings _settings;
        private IHashing _hashing;
        private IStorageServiceWrapper _storageServiceWrapper;
        private IUrlHandler _urlHandler;

        public EngineController(ISettings settings,
                                IHashing hashing,
                                IUrlHandler urlHandler,
                                IStorageServiceWrapper storageServiceWrapper) 
        {
            _settings = settings;
            _hashing = hashing;
            _urlHandler = urlHandler;
            _storageServiceWrapper = storageServiceWrapper;
        }

        [HttpPost]
        public ActionResult Get(HashRequest url)
        {
            var hash = _urlHandler
                        .GetHash(url.Url)
                        .Match(_ => _,
                               () => string.Empty);
            return _storageServiceWrapper
                        .Get(hash)
                        .Match<ActionResult>(_ => new JsonResult(_.To(_settings.BaseUrl)), 
                                             () => new NotFoundResult() );
        }

        [HttpPut]
        public ActionResult Tiny([FromBody]HashRequest url)
        {
            var hased = _hashing.Create(new Uri(url.Url));
            return _storageServiceWrapper
                        .GetOrInsert(hased.To())
                        .Match<ActionResult>(_ => new JsonResult(_.To(_settings.BaseUrl)), 
                                             () => new NotFoundResult() );
        }
    }
}
