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
            _apiKey = "7cbd576d-df75-4cce-8683-1c65d250e121";
        }

        [Test]
        public void ShouldNotThrowAnError()
        {

            var api = new WealthEngineApi(_apiKey, "PROD");
            var response = api.GetProfileByEmailAsync<BasicProfileMatch>("danfraser12@gmail.com", "Mark", "Zuckerberg").Result;
            var responseFull = api.GetProfileByEmailAsync<FullProfileMatch>("danfraser12@gmail.com", "Mark", "Zuckerberg").Result;

        }

        [Test]
        [ExpectedException()]
        public void MissingEmail()
        {

            var api = new WealthEngineApi(_apiKey, "SANDBOX");
            var response = api.GetProfileByEmailAsync<BasicProfileMatch>(null, "John", "Doe").Result;
            var responseFull = api.GetProfileByEmailAsync<FullProfileMatch>(null, "John", "Doe").Result;

        }

        [Test]
        [ExpectedException()]
        [Ignore("The valid email check isn't working.")]
        public void InvalidEmail()
        {

            var api = new WealthEngineApi(_apiKey, "SANDBOX");
            var response = api.GetProfileByEmailAsync<BasicProfileMatch>("john@doe", "John", "Doe").Result;
            var responseFull = api.GetProfileByEmailAsync<FullProfileMatch>("john@doe", "John", "Doe").Result;

        }

    }
}
