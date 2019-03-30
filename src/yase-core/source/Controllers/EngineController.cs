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

        public EngineController(IHashing hashing) 
        {
            _hashing = hashing;
        }

        [HttpGet]
        public ActionResult<HashingModel> Get(string url)
        {
            return _hashing.Create(new Uri(url));
        }
    }
}
