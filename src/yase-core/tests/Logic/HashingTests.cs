using NUnit.Framework;
using yase_core.Logic;
using System;

namespace yase_core.Logic.Tests 
{
    [TestFixture]
    public class HashingTests 
    {
        private const string TEST_URL = "http://test.com";

        private Hashing sut;

        [SetUp]
        public void SetUp()
        {
            sut = new Hashing();
        }

        [Test]
        public void Should_be_identity () 
        {
            Assert.AreEqual ("http://test.com/", sut.Create(new Uri(TEST_URL)).Shortener.AbsoluteUri);
        }
    }
}