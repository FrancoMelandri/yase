using NUnit.Framework;
using yase_core.Logic;
using System;
using Moq;

namespace yase_core.Logic.Tests 
{
    [TestFixture]
    public class HashingTests 
    {
        private const string TEST_URL = "http://test.com/?type=1";

        private Hashing sut;   
        private Mock<ISettings> settings;
        private Mock<IHash> hash;

        [SetUp]
        public void SetUp()
        {
            settings = new Mock<ISettings> ();
            hash = new Mock<IHash>();
            sut = new Hashing(settings.Object,
                              hash.Object);
        }

        [Test]
        public void Should_long_url_be_identity () 
        {
            Assert.AreEqual ("http://test.com/?type=1", sut.Create(new Uri(TEST_URL)).OriginalUrl.AbsoluteUri);
        }

        [Test]
        public void Should_return_tiny_url () 
        {
            settings
                .Setup(m => m.BaseUrl)
                .Returns("http://aa.bb");

            hash
                .Setup(m => m.Get(It.IsAny<string>(), It.IsAny<int>()))
                .Returns("aAbB123");

            Assert.AreEqual ("http://aa.bb/aAbB123", sut.Create(new Uri(TEST_URL)).TinyUrl.AbsoluteUri);
        }
    }
}