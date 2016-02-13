using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WE.Api.Objects;

namespace WE.Api.Tests
{

    [TestFixture]
    public class ProfileByNameAndAddress
    {

        private readonly string _apiKey;

        public ProfileByNameAndAddress()
        {
            _apiKey = ConfigurationManager.AppSettings["WEAPiKey"].ToString();
        }

    }

}
