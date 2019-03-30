using NUnit.Framework;
using yase_core.Logic;
using System;

namespace yase_core.Logic.Tests 
{
    [TestFixture]
    public class SettingsTests 
    {
        private SettingsBuilder sut;

        [SetUp]
        public void SetUp()
        {
            sut = new SettingsBuilder();
        }

        [Test]
        public void Should_create_settings_in_right_way () 
        {
            var settings = sut
                            .WithBase("https://sample.com")
                            .WithLength(10)
                            .Build();
            Assert.AreEqual ("https://sample.com", settings.BaseUrl);
        }
    }
}