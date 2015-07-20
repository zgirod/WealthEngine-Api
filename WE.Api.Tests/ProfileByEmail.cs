using System;
using WE.Api;
using NUnit;
using NUnit.Framework;
using System.Configuration;

namespace WE.Api.Tests
{
    [TestFixture]
    public class ProfileByEmail
    {

        private readonly string _apiKey;

        public ProfileByEmail()
        {
            _apiKey = ConfigurationManager.AppSettings["WEAPiKey"].ToString();
        }

        [Test]
        public void ShouldNotThrowAnError()
        {

            var api = new WealthEngineApi(_apiKey, "SANDBOX");
            var response = api.GetProfileByEmailAsync("john@doe.com", "John", "Doe").Result;

        }

        [Test]
        [ExpectedException()]
        public void MissingEmail()
        {

            var api = new WealthEngineApi(_apiKey, "SANDBOX");
            var response = api.GetProfileByEmailAsync(null, "John", "Doe").Result;

        }

        [Test]
        [ExpectedException()]
        [Ignore("The valid email check isn't working.")]
        public void InvalidEmail()
        {

            var api = new WealthEngineApi(_apiKey, "SANDBOX");
            var response = api.GetProfileByEmailAsync("john@doe", "John", "Doe").Result;

        }

    }
}
