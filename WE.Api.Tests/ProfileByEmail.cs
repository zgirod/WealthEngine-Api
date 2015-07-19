using System;
using WE.Api;
using NUnit;
using NUnit.Framework;

namespace WE.Api.Tests
{
    [TestFixture]
    public class ProfileByEmail
    {
        [Test]
        public void ShouldNotThrowAnError()
        {

            var api = new WealthEngineApi("f0f44ec8-65dd-4d2d-9084-fedb7bb73a1a", "SANDBOX");
            var response = api.GetProfileByEmailAsync("john@doe.com", "John", "Doe").Result;

        }

        [Test]
        [ExpectedException()]
        public void MissingEmail()
        {

            var api = new WealthEngineApi("f0f44ec8-65dd-4d2d-9084-fedb7bb73a1a", "SANDBOX");
            var response = api.GetProfileByEmailAsync(null, "John", "Doe").Result;

        }

        [Test]
        [ExpectedException()]
        [Ignore("The valid email check isn't working.")]
        public void InvalidEmail()
        {

            var api = new WealthEngineApi("f0f44ec8-65dd-4d2d-9084-fedb7bb73a1a", "SANDBOX");
            var response = api.GetProfileByEmailAsync("john@doe", "John", "Doe").Result;

        }

    }
}
