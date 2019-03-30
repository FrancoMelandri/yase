using NUnit.Framework;
using yase_core.Logic;
using System;
using Moq;

namespace yase_core.Logic.Tests 
{
    [TestFixture]
    public class HashingTests 
    {
        private const string TEST_URL = "http://test.com";

        private Hashing sut;   
        private Mock<ISettings> settings;

        [SetUp]
        public void SetUp()
        {
            settings = new Mock<ISettings> ();
            sut = new Hashing(settings.Object);
        }

        [Test]
        public void Should_long_url_be_identity () 
        {
            Assert.AreEqual ("http://test.com/", sut.Create(new Uri(TEST_URL)).LongUrl.AbsoluteUri);
        }
    }
}