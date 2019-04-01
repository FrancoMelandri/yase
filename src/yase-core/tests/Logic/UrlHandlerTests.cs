
using NUnit.Framework;
using yase_core.Logic;
using System;
using Moq;

namespace yase_core.Logic.Tests 
{
    [TestFixture]
    public class UrlHandlerTests 
    {
        private UrlHandler sut;   
        private Mock<ISettings> settings;

        [SetUp]
        public void SetUp()
        {
            settings = new Mock<ISettings> ();
            sut = new UrlHandler(settings.Object);
            settings
                .Setup(m => m.Length)
                .Returns(7);
            settings
                .Setup(m => m.BaseUrl)
                .Returns("http://aa.com");
        }

        [Test]
        public void Should_extract_hash_from_request_url () 
        {
            const string REQUEST = "http://aa.com/aAbB123";
            sut
                .GetHash(REQUEST)
                .Match(_ => Assert.AreEqual ("aAbB123", _),
                       () => Assert.Fail());
        }

        [Test]
        public void Should_get_none_if_no_url_formatted () 
        {
            const string REQUEST = "aAbB123";
            var result = sut.GetHash(REQUEST);

            Assert.AreEqual (false, result.HasValue);
        }

        [TestCase("aAbB1234")]
        [TestCase("aAbB12")]
        public void Should_get_none_if_hash_length_wrong (string hash) 
        {
            var REQUEST = string.Format("http://aa.com/{0}", hash);
            var result = sut.GetHash(REQUEST);

            Assert.AreEqual (false, result.HasValue);
        }

        [TestCase("https://aa.com")]
        [TestCase("http://bb.com")]
        public void Should_get_none_if_baseurl_wrong (string url) 
        {
            var REQUEST = string.Format("{0}/aAbB123", url);
            var result = sut.GetHash(REQUEST);

            Assert.AreEqual (false, result.HasValue);
        }
    }
}