using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using yase_storage.Logic;
using yase_storage.Models;

namespace yase_storage.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private IMongoWrapper _mongoWrapper;

        public StorageController(IMongoWrapper mongoWrapper)
        {
            _mongoWrapper = mongoWrapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShortUrlModel>> Get()
        {
            return _mongoWrapper.GetUrls();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
