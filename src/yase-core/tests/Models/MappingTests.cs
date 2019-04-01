using NUnit.Framework;
using System;
using yase_core.Models;

namespace yase_core.Models.Tests 
{
    [TestFixture]
    public class MappingTests 
    {
        [Test]
        public void Should_map_HasingModel_to_ShortUrl()
        {
            var model = new HashingModel
            {
                OriginalUrl = new Uri("https://www.example.com?param1=1&param2=2"),
                TinyUrl = new Uri("http://aa.com/aAbB123"),
                HashedUrl = "aAbB123"
            };

            var mapped = model.To();

            Assert.AreEqual ("https://www.example.com/?param1=1&param2=2", mapped.OriginalUrl);
            Assert.AreEqual ("aAbB123", mapped.TinyUrl);
        }
    }
}