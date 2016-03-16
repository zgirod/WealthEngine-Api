using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WE.Api.Objects;

namespace WE.Api
{

    public class WealthEngineApi
    {

        /// <summary>
        /// The production WealthEngine Api root
        /// </summary>
        private const string PROD_URL = "https://api.wealthengine.com/{0}/";

        /// <summary>
        /// The sandbox WealthEngine Api root
        /// </summary>
        private const string SANDBOX_URL = "https://api-sandbox.wealthengine.com/{0}/";

        /// <summary>
        /// The user's api key
        /// </summary>
        private readonly string _apiKey;

        /// <summary>
        /// The api root based upon the environment passed in
        /// </summary>
        private readonly string _apiRoot;

        /// <summary>
        /// Create an instance of the WealthEngine api
        /// </summary>
        /// <param name="apiKey">Your API key from http://dev.wealthengine.com/</param>
        /// <param name="environmentKey">The environment that should be called< (prod or sandbox)/param>
        public WealthEngineApi(string apiKey, string environmentKey, string version)
        {

            //if we don't have an environment key, throw an error
            if (string.IsNullOrWhiteSpace(environmentKey))
                throw new ArgumentNullException("environmentKey");

            //if we don't have an api key, throw an error
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException("apiKey");

            //if we don't have a version, throw an error
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException("version");

            //set the properties
            _apiKey = apiKey;
            _apiRoot = string.Format(ApiRootBasedOnEnvironment(environmentKey), version).ToLower();
            
        }

        private string ApiRootBasedOnEnvironment(string environmentKey)
        {

            //the api root to return
            string apiRoot = "";

            //set the appropriate api root based upon the environment key
            switch (environmentKey.ToLower())
            {

                case "prod":
                case "production":
                    {
                        apiRoot = PROD_URL;
                        break;
                    }
                case "sandbox":
                case "sand":
                case "test":
                    {
                        apiRoot = SANDBOX_URL;
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Unexpected environment key passed in");
                    }
            }

            //return the api root
            return apiRoot;

        }

        private HttpClient SetupHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "APIKey " + _apiKey);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "WealthEngine .NET SDK");
            return httpClient;
        }

        private async Task<ApiResponse<TMatch>> BuildApiResponseAsync<TMatch>(HttpResponseMessage httpResponse)
        {

            //setup the api response to be returned
            var apiResponse = new ApiResponse<TMatch>();

            //set the status code
            apiResponse.StatusCode = (int)httpResponse.StatusCode;

            //set the raw contents
            apiResponse.RawContent = await httpResponse.Content.ReadAsStringAsync();

            //if we had a successful request, deserialize the response
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {

                //deserialize the response into a profile match
                apiResponse.ProfileMatch = JsonConvert.DeserializeObject<TMatch>(apiResponse.RawContent);

            }

            //return the api response
            return apiResponse;

        }

        /// <summary>
        /// Look up a WealthEngine profile by email and name (optional)
        /// </summary>
        /// <param name="email">The email of the profile you want to look up</param>
        /// <param name="firstName">The first name of the profile you want to look up</param>
        /// <param name="lastName">The last name of the profile you want to look up</param>
        /// <returns></returns>
        public async Task<ApiResponse<TMatch>> GetProfileByEmailAsync<TMatch>(string email, string firstName, string lastName)
        {

            //check to make sure an email was supplied
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            //check to make sure the email is in a valid format
            //TODO: check for valid email

            //setup the http client
            var httpClient = SetupHttpClient();

            //setup the post data
            var postData = new Dictionary<string, string>()
            {
                { "email", email },
                { "first_name", firstName },
                { "last_name", lastName }
            };

            //get the endpoint
            var endpoint = "profile/find_one/by_email/basic";
            if (typeof(TMatch) == typeof(FullProfileMatch))
                endpoint = "profile/find_one/by_email/full";

            //make the post call
            HttpResponseMessage response = await httpClient.PostAsync(_apiRoot + endpoint, 
                new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json"));

            //build the response
            return await BuildApiResponseAsync<TMatch>(response);
            
        }

        /// <summary>
        /// Look up a WealthEngine profile by name and address
        /// </summary>
        /// <param name="firstName">The first name of the profile you want to look up</param>
        /// <param name="lastName">The last name of the profile you want to look up</param>
        /// <param name="address1">The address of the profile you want to look up</param>
        /// <param name="city">The city of the profile you want to look up</param>
        /// <param name="state">The state of the profile you want to look up</param>
        /// <param name="zip">The zip code of the profile you want to look up</param>
        /// <returns></returns>
        public async Task<ApiResponse<TMatch>> GetProfileByNameAndAddressAsync<TMatch>(string firstName, 
            string lastName, string address_line1, string address_line2, string city, string state, string zip)
        {

            //check to make sure a first name was supplied
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException("firstName");

            //check to make sure a last name was supplied
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException("lastName");

            //check to make sure an address1 was supplied
            if (string.IsNullOrWhiteSpace(address_line1))
                throw new ArgumentNullException("address_line1");

            //check to make sure a city was supplied
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException("city");

            //check to make sure a state was supplied
            //TODO: validate 2 letters
            if (string.IsNullOrWhiteSpace(state))
                throw new ArgumentNullException("state");
            else if (state.Trim().Length != 2)
                throw new ArgumentException("state must be 2 digits");

            //check to make sure zip code is 5 digits
            int zipOut;
            if (string.IsNullOrWhiteSpace(zip))
                throw new ArgumentNullException("zip");
            else if (zip.Trim().Length != 5)
                throw new ArgumentException("zip code 5 digits");
            else if (int.TryParse(zip.Trim(), out zipOut) == false)
                throw new ArgumentException("zip code 5 digits");

            //setup the http client
            var httpClient = SetupHttpClient();

            //setup the post data
            var postData = new Dictionary<string, string>()
            {
                { "first_name", firstName },
                { "last_name", lastName },
                { "address_line1", address_line1 },
                { "address_line2", address_line2 },
                { "city", city },
                { "state", state },
                { "zip", zip },
            };

            //get the endpoint
            var endpoint = "profile/find_one/by_address/basic";
            if (typeof(TMatch) == typeof(FullProfileMatch))
                endpoint = "profile/find_one/by_address/full";

            //make the post call
            HttpResponseMessage response = await httpClient.PostAsync(_apiRoot + endpoint,
                new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json"));

            //build the response
            return await BuildApiResponseAsync<TMatch>(response);

        }

        /// <summary>
        /// Look up a WealthEngine profile by phone number and name
        /// </summary>
        /// <param name="phone">The phone number of the profile you want to look up</param>
        /// <param name="firstName">The first name of the profile you want to look up</param>
        /// <param name="lastName">The last name of the profile you want to look up</param>
        /// <returns></returns>
        public async Task<ApiResponse<TMatch>> GetProfileByPhoneAsync<TMatch>(string phone, string firstName, string lastName)
        {

            long phoneOut;
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentNullException("phone");
            else if (long.TryParse(phone.Trim(), out phoneOut) == false)
                throw new ArgumentException("phone must be all digits, no special characters");

            //setup the http client
            var httpClient = SetupHttpClient();

            //setup the post data
            var postData = new Dictionary<string, string>()
            {
                { "phone", phone },
                { "first_name", firstName },
                { "last_name", lastName }
            };

            //get the endpoint
            var endpoint = "profile/find_one/by_phone/basic";
            if (typeof(TMatch) == typeof(FullProfileMatch))
                endpoint = "profile/find_one/by_phone/full";

            //make the post call
            HttpResponseMessage response = await httpClient.PostAsync(_apiRoot + endpoint,
                new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json"));

            //build the response
            return await BuildApiResponseAsync<TMatch>(response);

        }

        /// <summary>
        /// Score a profile based upon an existing model
        /// </summary>
        /// <param name="email">The email of the profile you want to look up</param>
        /// <param name="firstName">The first name of the profile you want to look up</param>
        /// <param name="lastName">The last name of the profile you want to look up</param>
        /// <param name="modelCode">The Api model code for the model you want to score against</param>
        /// <returns></returns>
        public async Task<ApiResponse<ScoreResponse>> ScoreByEmailAsync(string email, string firstName, string lastName, string modelCode)
        {

            //check to make sure the valid parameters were supplied
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            if (string.IsNullOrWhiteSpace(modelCode))
                throw new ArgumentNullException("modelCode");

            //check to make sure the email is in a valid format
            //TODO: check for valid email

            //setup the http client
            var httpClient = SetupHttpClient();

            //setup the post data
            var postData = new Dictionary<string, string>()
            {
                { "email", email },
                { "first_name", firstName },
                { "last_name", lastName },
                { "model", modelCode }
            };

            //make the post call
            HttpResponseMessage response = await httpClient.PostAsync(_apiRoot + "score/score_one/by_email",
                new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json"));

            //build the response
            return await BuildApiResponseAsync<ScoreResponse>(response);

        }

        /// <summary>
        /// Look up a WealthEngine profile by name and address
        /// </summary>
        /// <param name="firstName">The first name of the profile you want to look up</param>
        /// <param name="lastName">The last name of the profile you want to look up</param>
        /// <param name="address1">The address of the profile you want to look up</param>
        /// <param name="city">The city of the profile you want to look up</param>
        /// <param name="state">The state of the profile you want to look up</param>
        /// <param name="zip">The zip code of the profile you want to look up</param>
        /// <param name="modelCode">The Api model code for the model you want to score against</param>
        /// <returns></returns>
        public async Task<ApiResponse<ScoreResponse>> ScoreByNameAndAddressAsync(string firstName,
            string lastName, string address_line1, string address_line2, string city, string state, string zip, string modelCode)
        {

            //check to make sure a first name was supplied
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException("firstName");

            //check to make sure a last name was supplied
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException("lastName");

            //check to make sure an address1 was supplied
            if (string.IsNullOrWhiteSpace(address_line1))
                throw new ArgumentNullException("address_line1");

            //check to make sure a city was supplied
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException("city");

            //check to make sure a state was supplied
            //TODO: validate 2 letters
            if (string.IsNullOrWhiteSpace(state))
                throw new ArgumentNullException("state");
            else if (state.Trim().Length != 2)
                throw new ArgumentException("state must be 2 digits");

            //check to make sure zip code is 5 digits
            int zipOut;
            if (string.IsNullOrWhiteSpace(zip))
                throw new ArgumentNullException("zip");
            else if (zip.Trim().Length != 5)
                throw new ArgumentException("zip code 5 digits");
            else if (int.TryParse(zip.Trim(), out zipOut) == false)
                throw new ArgumentException("zip code 5 digits");

            //make sure there is a model code
            if (string.IsNullOrWhiteSpace(modelCode))
                throw new ArgumentNullException("modelCode");

            //setup the http client
            var httpClient = SetupHttpClient();

            //setup the post data
            var postData = new Dictionary<string, string>()
            {
                { "first_name", firstName },
                { "last_name", lastName },
                { "address_line1", address_line1 },
                { "address_line2", address_line2 },
                { "city", city },
                { "state", state },
                { "zip", zip },
                { "model", modelCode }
            };

            //make the post call
            HttpResponseMessage response = await httpClient.PostAsync(_apiRoot + "score/score_one/by_address",
                new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json"));

            //build the response
            return await BuildApiResponseAsync<ScoreResponse>(response);

        }

        /// <summary>
        /// Look up a WealthEngine profile by phone number and name
        /// </summary>
        /// <param name="phone">The phone number of the profile you want to look up</param>
        /// <param name="firstName">The first name of the profile you want to look up</param>
        /// <param name="lastName">The last name of the profile you want to look up</param>
        /// <param name="modelCode">The Api model code for the model you want to score against</param>
        /// <returns></returns>
        public async Task<ApiResponse<ScoreResponse>> ScoreByPhoneAsync(string phone, string firstName, string lastName, string modelCode)
        {

            //make sure we have a proper phone number
            long phoneOut;
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentNullException("phone");
            else if (long.TryParse(phone.Trim(), out phoneOut) == false)
                throw new ArgumentException("phone must be all digits, no special characters");

            //make sure model code is supplied
            if (string.IsNullOrWhiteSpace(modelCode))
                throw new ArgumentNullException("modelCode");

            //setup the http client
            var httpClient = SetupHttpClient();

            //setup the post data
            var postData = new Dictionary<string, string>()
            {
                { "phone", phone },
                { "first_name", firstName },
                { "last_name", lastName },
                { "model", modelCode }
            };

            //make the post call
            HttpResponseMessage response = await httpClient.PostAsync(_apiRoot + "score/score_one/by_phone",
                new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json"));

            //build the response
            return await BuildApiResponseAsync<ScoreResponse>(response);

        }

    }

}
 