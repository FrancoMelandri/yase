
using NUnit.Framework;
using yase_core.Logic;
using yase_core.Models;
using System;
using Moq;

namespace yase_core.Logic.Tests 
{
    [TestFixture]
    public class ValidatorTests 
    {
        private Validator sut;   
        private Mock<ITimeToLive> timeToLive;

        [SetUp]
        public void SetUp()
        {
            timeToLive = new Mock<ITimeToLive> ();
            sut = new Validator(timeToLive.Object);
        }

        [Test]
        public void Should_call_onValid_if_no_expired () 
        {
            var shortUtl = new ShortUrl 
            {
                ttl = 100
            };
            timeToLive
                .Setup(m => m.Now())
                .Returns(99);
            var result = sut
                            .Validate<bool>(shortUtl,
                                    _ => true,
                                    _ => false);
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_call_onInvalid_if_expired () 
        {
            var shortUtl = new ShortUrl 
            {
                ttl = 100
            };
            timeToLive
                .Setup(m => m.Now())
                .Returns(101);
            var result = sut
                            .Validate<bool>(shortUtl,
                                    _ => false,
                                    _ => true);
            Assert.IsTrue(result);
        }
    }
}