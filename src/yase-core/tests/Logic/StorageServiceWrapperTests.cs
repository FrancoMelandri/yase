using NUnit.Framework;
using yase_core.Logic;
using yase_core.Models;
using yase_core.Utilities;
using System;
using Moq;
using Microsoft.Extensions.Configuration;


namespace yase_core.Logic.Tests 
{
    [TestFixture]
    public class StorageServiceWrapperTests 
    {
        private StorageServiceWrapper sut;   
        private Mock<IConfiguration> configuration;
        private Mock<IHttpRequests> httpRequests;

        [SetUp]
        public void SetUp()
        {
            configuration = new Mock<IConfiguration> ();
            httpRequests = new Mock<IHttpRequests>();
            sut = new StorageServiceWrapper(configuration.Object,
                                            httpRequests.Object);
        }

        [Test]
        public void Should_get_url_from_storage () 
        {
            var reponse = new ShortUrl();
            httpRequests
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(reponse.ToJson());
           
            Assert.IsTrue(sut.Get("hash").HasValue);
        }

        [Test]
        public void Should_not_get_url_from_storage () 
        {
            var reponse = new ShortUrl();
            httpRequests
                .Setup(m => m.Get(It.IsAny<string>()))
                .Throws(new Exception());
           
            Assert.IsFalse(sut.Get("hash").HasValue);
        }

        [Test]
        public void Should_post_url_to_storage () 
        {
            var reponse = new ShortUrl();
            httpRequests
                .Setup(m => m.Post(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(reponse.ToJson());
           
            Assert.IsTrue(sut.GetOrInsert(new ShortUrl()).HasValue);
        }

        [Test]
        public void Should_not_post_url_to_storage () 
        {
            var reponse = new ShortUrl();
            httpRequests
                .Setup(m => m.Post(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());
           
            Assert.IsFalse(sut.GetOrInsert(new ShortUrl()).HasValue);
        }

        [Test]
        public void Should_delete_url_in_storage () 
        {
            var reponse = new ShortUrl();
            httpRequests
                .Setup(m => m.Delete(It.IsAny<string>()))
                .Returns(reponse.ToJson());
           
            Assert.IsTrue(sut.Delete("hash").HasValue);
        }

        [Test]
        public void Should_not_deletet_url_in_storage () 
        {
            var reponse = new ShortUrl();
            httpRequests
                .Setup(m => m.Delete(It.IsAny<string>()))
                .Throws(new Exception());
           
            Assert.IsFalse(sut.Delete("hash").HasValue);
        }
    }
}