using NUnit.Framework;
using yase_core.Logic;
using yase_core.Models;
using yase_core.Utilities;
using yase_core.Controllers;
using System;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;


namespace yase_core.Controllers.Tests 
{
    [TestFixture]
    public class EngineControllerrTests 
    {
        const string TEST_URL = "http://www.example.com";

        private EngineController sut;   
        private Mock<ISettings> settings;
        private Mock<IHashing> hashing;
        private Mock<IStorageServiceWrapper> storageServiceWrapper;
        private Mock<IUrlHandler> urlHandler;
        private Validator validator;
        private Mock<ITimeToLive> timeToLive;


        [SetUp]
        public void SetUp()
        {
            settings = new Mock<ISettings> ();
            hashing = new Mock<IHashing>();
            storageServiceWrapper = new Mock<IStorageServiceWrapper>();
            urlHandler = new Mock<IUrlHandler>();
            timeToLive = new Mock<ITimeToLive> ();
            validator = new Validator(timeToLive.Object);
            sut = new EngineController(settings.Object,
                                       hashing.Object,
                                       urlHandler.Object,
                                       validator,
                                       storageServiceWrapper.Object);
        }

        [Test]
        public void Should_get_not_found_If_no_hash_GET () 
        {
            storageServiceWrapper
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(Option.None<ShortUrl>());
            
            var result = sut.Get(new HashRequest());
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public void Should_get_not_found_If_expired_GET () 
        {
            storageServiceWrapper
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(Option.Some<ShortUrl>(new ShortUrl 
                        {
                            ttl = 100,
                            OriginalUrl = TEST_URL,
                            TinyUrl = TEST_URL
                        }));
            urlHandler
                .Setup(m => m.GetHash(It.IsAny<string>()))
                .Returns(Option.Some<string>(""));
    
            timeToLive
                .Setup(m => m.Now())
                .Returns(101);

            var result = sut.Get(new HashRequest{
                Url = TEST_URL
            });
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public void Should_get_json_If_not_expired_GET () 
        {
            storageServiceWrapper
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(Option.Some<ShortUrl>(new ShortUrl 
                        {
                            ttl = 100,
                            OriginalUrl = TEST_URL,
                            TinyUrl = TEST_URL
                        }));
            urlHandler
                .Setup(m => m.GetHash(It.IsAny<string>()))
                .Returns(Option.Some<string>(""));
    
            timeToLive
                .Setup(m => m.Now())
                .Returns(99);

            var result = sut.Get(new HashRequest{
                Url = TEST_URL
            });
            Assert.IsInstanceOf(typeof(JsonResult), result);
        }

        [Test]
        public void Should_get_not_found_If_no_hash_POST () 
        {
            hashing
                .Setup(m => m.Create(It.IsAny<Uri>()))
                .Returns(new HashingModel{
                    TinyUrl = new Uri(TEST_URL),
                    OriginalUrl = new Uri(TEST_URL)
                });

            storageServiceWrapper
                .Setup(m => m.GetOrInsert(It.IsAny<ShortUrl>()))
                .Returns(Option.None<ShortUrl>());
            
            var result = sut.Tiny(new HashRequest{
                Url = TEST_URL
            });
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public void Should_get_not_found_If_expired_POST () 
        {
            hashing
                .Setup(m => m.Create(It.IsAny<Uri>()))
                .Returns(new HashingModel{
                    TinyUrl = new Uri(TEST_URL),
                    OriginalUrl = new Uri(TEST_URL)
                });

            storageServiceWrapper
                .Setup(m => m.GetOrInsert(It.IsAny<ShortUrl>()))
                .Returns(Option.Some<ShortUrl>(new ShortUrl 
                        {
                            ttl = 100,
                            OriginalUrl = TEST_URL,
                            TinyUrl = TEST_URL
                        }));
            urlHandler
                .Setup(m => m.GetHash(It.IsAny<string>()))
                .Returns(Option.Some<string>(""));
    
            timeToLive
                .Setup(m => m.Now())
                .Returns(101);

            var result = sut.Tiny(new HashRequest{
                Url = TEST_URL
            });
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public void Should_get_json_If_not_expired_POST () 
        {
            hashing
                .Setup(m => m.Create(It.IsAny<Uri>()))
                .Returns(new HashingModel{
                    TinyUrl = new Uri(TEST_URL),
                    OriginalUrl = new Uri(TEST_URL)
                });

            storageServiceWrapper
                .Setup(m => m.GetOrInsert(It.IsAny<ShortUrl>()))
                .Returns(Option.Some<ShortUrl>(new ShortUrl 
                        {
                            ttl = 100,
                            OriginalUrl = TEST_URL,
                            TinyUrl = TEST_URL
                        }));
            urlHandler
                .Setup(m => m.GetHash(It.IsAny<string>()))
                .Returns(Option.Some<string>(""));
    
            timeToLive
                .Setup(m => m.Now())
                .Returns(99);

            var result = sut.Tiny(new HashRequest{
                Url = TEST_URL
            });
            Assert.IsInstanceOf(typeof(JsonResult), result);
        }

        [Test]
        public void Should_get_ok_If_hash_DELETE () 
        {
            urlHandler
                .Setup(m => m.GetHash(It.IsAny<string>()))
                .Returns(Option.Some<string>(""));

            storageServiceWrapper
                .Setup(m => m.Delete(It.IsAny<string>()))
                .Returns(Option.Some<bool>(true));
            
            var result = sut.Delete(new HashRequest{
                Url = TEST_URL
            });
            Assert.IsInstanceOf(typeof(OkResult), result);
        }

        [Test]
        public void Should_get_not_found_If_no_hash_DELETE () 
        {
            urlHandler
                .Setup(m => m.GetHash(It.IsAny<string>()))
                .Returns(Option.Some<string>(""));

            storageServiceWrapper
                .Setup(m => m.Delete(It.IsAny<string>()))
                .Returns(Option.None<bool>());
            
            var result = sut.Delete(new HashRequest{
                Url = TEST_URL
            });
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }
    }
}