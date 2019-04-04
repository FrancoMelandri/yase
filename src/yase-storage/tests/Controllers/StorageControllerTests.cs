using NUnit.Framework;
using System;
using yase_storage.Models;
using yase_storage.Controllers;
using yase_storage.Logic;
using Moq;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace yase_storage.Controllers.Tests 
{
    [TestFixture]
    public class StorageControllerTests 
    {
        private StorageController sut;
        private Mock<IMongoWrapper> mongoWrapper;
        private Mock<ITracer> tracer;

        [SetUp]
        public void SetUp() 
        {
            mongoWrapper = new Mock<IMongoWrapper>();
            tracer = new Mock<ITracer>();
            sut = new StorageController(mongoWrapper.Object,
                                        tracer.Object);

            tracer
                .Setup (m => m.BuildSpan(It.IsAny<string>()))
                .Returns(new Mock<ISpanBuilder>().Object);
        }

        [Test]
        public void Should_Get_NotFound_Response() 
        {
            mongoWrapper
                .Setup(m => m.GetUrl(It.IsAny<string>()))
                .Returns(Option.None<ShortUrlModel>());

            var result = sut.Get("tiny");
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public void Should_Get_Ok_Response() 
        {
            mongoWrapper
                .Setup(m => m.GetUrl(It.IsAny<string>()))
                .Returns(Option.Some<ShortUrlModel>(new ShortUrlModel()));

            var result = sut.Get("tiny");
            Assert.IsInstanceOf(typeof(JsonResult), result);
        }        
        [Test]
        public void Should_Delete_Conflict_Response() 
        {
            mongoWrapper
                .Setup(m => m.DeleteUrl(It.IsAny<string>()))
                .Returns(Option.None<long>());

            var result = sut.Delete("tiny");
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public void Should_Delete_Ok_Response() 
        {
            mongoWrapper
                .Setup(m => m.DeleteUrl(It.IsAny<string>()))
                .Returns(Option.Some<long>(1));

            var result = sut.Delete("tiny");
            Assert.IsInstanceOf(typeof(OkResult), result);
        }                
    }
}