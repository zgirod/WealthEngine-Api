using System;
using WE.Api.Objects;
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

            var api = new WealthEngineApi(_apiKey, "SANDBOX", "v1");
            var response = api.GetProfileByEmailAsync<BasicProfileMatch>("JohnDoe@gmail.com", "John", "Doe").Result;
            var responseFull = api.GetProfileByEmailAsync<FullProfileMatch>("JohnDoe@gmail.com", "John", "Doe").Result;

        }

        [Test]
        [ExpectedException()]
        public void MissingEmail()
        {

            var api = new WealthEngineApi(_apiKey, "SANDBOX", "v1");
            var response = api.GetProfileByEmailAsync<BasicProfileMatch>(null, "John", "Doe").Result;
            var responseFull = api.GetProfileByEmailAsync<FullProfileMatch>(null, "John", "Doe").Result;

        }

        [Test]
        [ExpectedException()]
        [Ignore("The valid email check isn't working.")]
        public void InvalidEmail()
        {

            var api = new WealthEngineApi(_apiKey, "SANDBOX", "v1");
            var response = api.GetProfileByEmailAsync<BasicProfileMatch>("john@doe", "John", "Doe").Result;
            var responseFull = api.GetProfileByEmailAsync<FullProfileMatch>("john@doe", "John", "Doe").Result;

        }

    }
}
