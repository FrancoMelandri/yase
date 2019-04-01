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
        private IHashing _hashing;
        private IStorageServiceWrapper _storageServiceWrapper;

        public EngineController(IHashing hashing,
                                IStorageServiceWrapper storageServiceWrapper) 
        {
            _hashing = hashing;
            _storageServiceWrapper = storageServiceWrapper;
        }

        [HttpGet]
        public ActionResult<HashingModel> Get(string url)
        {
            return _hashing.Create(new Uri(url));
        }

        [HttpPost]
        public ActionResult Tiny([FromBody]HashRequest url)
        {
            var hased = _hashing.Create(new Uri(url.Url));
            return _storageServiceWrapper
                        .GetOrInsert(hased.To())
                        .Match<ActionResult>(_ => new JsonResult(_), 
                                             () => new NotFoundResult() );
        }
    }
}
