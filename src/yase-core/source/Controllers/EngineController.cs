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
        private IValidator _validator;

        public EngineController(ISettings settings,
                                IHashing hashing,
                                IUrlHandler urlHandler,
                                IValidator validator,
                                IStorageServiceWrapper storageServiceWrapper) 
        {
            _settings = settings;
            _hashing = hashing;
            _urlHandler = urlHandler;
            _validator = validator;
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
                        .Match<ActionResult>(_ => _validator.Validate<ActionResult>(_,
                                                            __ =>  new JsonResult(__.To(_settings.BaseUrl)),
                                                            __ => {
                                                                    Delete(new HashRequest {Url = __.TinyUrl });
                                                                    return new NotFoundResult();
                                                                  } ) ,
                                             () => new NotFoundResult() );
        }

        [HttpPut]
        public ActionResult Tiny(HashRequest url)
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

        [HttpDelete]
        public ActionResult Delete(HashRequest url)
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
