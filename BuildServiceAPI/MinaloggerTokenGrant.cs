using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace BuildServiceAPI.Minalyze
{
    public class MinaloggerTokenGrant : ITokenGranter
    {
        private class pLoginResponse
        {
            public string message { get; set; }
        }
        public HttpClient httpClient { get; private set; }
        public bool Grant(string username, string password)
        {
            var response = httpClient.GetAsync($"https://minalogger.com/api/login?email={HttpUtility.UrlEncode(username)}&password={HttpUtility.UrlEncode(password)}").Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;

            pLoginResponse loginResponse = JsonSerializer.Deserialize<pLoginResponse>(stringContent, MainClass.serializerOptions);
            if (loginResponse == null || response.StatusCode == (HttpStatusCode)401)
            {
                return false;
            }
            return loginResponse.message == "success";
        }
        public MinaloggerTokenGrant()
        {
            httpClient = new HttpClient();
        }
    }
}
