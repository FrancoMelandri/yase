using NUnit.Framework;
using System;
using yase_storage.Models;

namespace yase_storage.Models.Tests 
{
    [TestFixture]
    public class MappingTests 
    {
        [Test]
        public void Should_map_ShortUrlModel_to_ShortUrl()
        {
            var model = new ShortUrlModel
            {
                Id = "123",
                OriginalUrl = "OriginalUrl",
                TinyUrl = "TinyUrl"
            };

            var mapped = model.To();

            Assert.AreEqual ("OriginalUrl", mapped.OriginalUrl);
            Assert.AreEqual ("TinyUrl", mapped.TinyUrl);
        }

        [Test]
        public void Should_map_ShortUrlRequest_to_ShortUrlModel()
        {
            var model = new ShortUrlRequest
            {
                OriginalUrl = "OriginalUrl",
                TinyUrl = "TinyUrl"
            };

            var mapped = model.To();

            Assert.AreEqual (null, mapped.Id);
            Assert.AreEqual ("OriginalUrl", mapped.OriginalUrl);
            Assert.AreEqual ("TinyUrl", mapped.TinyUrl);
        }
    }
}