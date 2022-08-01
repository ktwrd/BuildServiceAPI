using BuildServiceAPI.Minalyze;
using kate.shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuildServiceCommon
{
    public class AuthenticatedUser
    {
        public static JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            IncludeFields = true
        };

        public bool IsAdmin { get; private set; }
        private List<string> availableServices = new List<string>();
        public string AvailableServices => string.Join(" ", availableServices.ToArray());

        private HttpClient httpClient;
        private HttpClientHandler httpClientHandler;
        private string credentialHash = "";
        private string name = "";
        private string username = "";
        private string password = "";
        public string Username => username;
        public string Name => name;
        public string Token { get; private set; }
        private void recalculateCredentialHash()
        {
            SHA256 sha256Instance = SHA256.Create();
            var computedHash = sha256Instance.ComputeHash(Encoding.UTF8.GetBytes($"{username}{password}"));
            credentialHash = GeneralHelper.Base62Encode(computedHash);
        }
        public AuthenticatedUser(string username, string password)
        {
            this.username = username;
            this.password = password;
            Token = GeneralHelper.GenerateToken(32);
            recalculateCredentialHash();

            httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = new System.Net.CookieContainer()
            };
            httpClient = new HttpClient(httpClientHandler);

            RefreshFlags();
        }
        private CookieContainer Cookies
        {
            get { return httpClientHandler.CookieContainer; }
            set { httpClientHandler.CookieContainer = value; }
        }
        public MinaloggerTokenGrant tokenGranter = new MinaloggerTokenGrant();
        public bool IsCredentialsValid()
        {
            var isvalid = tokenGranter.Grant(Username, password);
            if (isvalid)
            {
                httpClient.GetAsync($"https://minalogger.com/api/login?email={WebUtility.UrlEncode(username)}&password={WebUtility.UrlEncode(password)}").Wait();
            }
            return isvalid;
        }
        public void RefreshFlags()
        {
            var taskList = new List<Task>();

            availableServices.Clear();

            var cnt = IsCredentialsValid();
            if (!cnt) return;

            // Check admin
            taskList.Add(new Task(new Action(delegate
            {
                var response = httpClient.GetAsync($"https://minalogger.com/api/IsUserAdmin").Result;
                var stringContent = response.Content.ReadAsStringAsync().Result;
                var deserialized = JsonSerializer.Deserialize<resIsAdmin>(stringContent, serializerOptions);
                if (deserialized != null)
                    IsAdmin = deserialized.isAdmin;
                else
                    IsAdmin = false;
            })));

            taskList.Add(new Task(new Action(delegate
            {
                var response = httpClient.GetAsync($"https://minalogger.com/api/hasSubscription?id=4").Result;
                var stringContent = response.Content.ReadAsStringAsync().Result;
                var deserialized = JsonSerializer.Deserialize<resIsSubscribed>(stringContent, serializerOptions);
                if (deserialized != null && deserialized.isSubscribed)
                    availableServices.Add("ml2");
            })));

            // Check Geolog
            taskList.Add(new Task(new Action(delegate
            {
                var response = httpClient.GetAsync("https://minalogger.com/api/IsUserSubscribedToGeoLog").Result;
                var stringContent = response.Content.ReadAsStringAsync().Result;
                var deserialized = JsonSerializer.Deserialize<resIsSubscribed>(stringContent, serializerOptions);
                if (deserialized != null && deserialized.isSubscribed)
                    availableServices.Add("geolog");
            })));

            // Check GeoTech
            taskList.Add(new Task(new Action(delegate
            {
                var response = httpClient.GetAsync("https://minalogger.com/api/IsUserSubscribedToGeoTech").Result;
                var stringContent = response.Content.ReadAsStringAsync().Result;
                var deserialized = JsonSerializer.Deserialize<resIsSubscribed>(stringContent, serializerOptions);
                if (deserialized != null && deserialized.isSubscribed)
                    availableServices.Add("geotech");
            })));

            foreach (var i in taskList)
                i.Start();
            Task.WaitAll(taskList.ToArray());
        }
        #region Validation
        public bool ValidateHash(string hash)
        {
            recalculateCredentialHash();
            return credentialHash == hash;
        }
        public bool ValidateToken(string token)
        {
            recalculateCredentialHash();
            return Token == token;
        }
        #endregion

        #region Private Classes
        private class resIsAdmin
        {
            public bool isAdmin = false;
        }
        private class resIsSubscribed
        {
            public bool isSubscribed = false;
        }
        #endregion
    }
}
