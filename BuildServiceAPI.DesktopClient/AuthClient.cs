using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BuildServiceAPI.DesktopClient
{
    public class AuthClient
    {
        internal AccountToken TokenData { get; set; } = null;
        public AccountDetailsResponse AccountDetails { get; set; } = null;
        public ServerDetailsResponse ServerDetails { get; set; } = null;
        internal string Token
        {
            get
            {
                if (TokenData != null)
                    return TokenData.Token;
                return UserConfig.GetString("Authentication", "Token", "");
            }
        }

        public HttpClient HttpClient = new HttpClient();

        public void ValidateOrFetch()
        {
            bool fetchToken = true;
            if (UserConfig.GetString("Authentication", "Token", "").Length > 0)
            {
                fetchToken = !ValidateToken();
            }
            if (fetchToken)
                FetchToken();
            else
            {
                var taskList = new Task[]
                {
                    new Task(delegate { FetchAccountDetails(); }),
                    new Task(delegate { FetchServerDetails(); })
                };
                foreach (var i in taskList)
                    i.Start();
                Task.WhenAll(taskList).Wait();
                Program.LocalContent.Pull();
            }
        }

        public bool ValidateToken(string token, bool silent=false)
        {
            if (ServerDetails == null)
            {
                FetchServerDetails();
                if (ServerDetails == null) return false;
            }
            HttpResponseMessage response = null;
            try
            {
                response = HttpClient.GetAsync(Endpoint.TokenValidate(token)).Result;
            }
            catch (AggregateException except)
            {
                foreach (var e in except.InnerExceptions)
                {
                    if (!silent)
                        MessageBox.Show(e.Message, $"Failed to validate token", MessageBoxButtons.OK);
                    Trace.WriteLine(e);
                }
                return false;
            }
            catch (Exception except)
            {
                if (!silent)
                    MessageBox.Show(except.Message, $"Failed to validate token", MessageBoxButtons.OK);
                Trace.WriteLine(except);
                return false;
            }

            var stringContent = response.Content.ReadAsStringAsync().Result;
            var deserialized = JsonSerializer.Deserialize<ObjectResponse<bool>>(stringContent, Program.serializerOptions);
            return deserialized.Data;
        }
        public bool ValidateToken() => ValidateToken(UserConfig.GetString("Authentication", "Token", ""));

        public void FetchToken(string username, string password, bool pull=true)
        {
            var targetURL = Endpoint.TokenGrant(username, password);

            HttpResponseMessage response = null;
            try
            {
                response = HttpClient.GetAsync(targetURL).Result;
            }
            catch (AggregateException except)
            {
                foreach (var e in except.InnerExceptions)
                {
                    MessageBox.Show(e.Message, $"Failed to fetch token", MessageBoxButtons.OK);
                    Trace.WriteLine(e);
                }
                return;
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, $"Failed to fetch token", MessageBoxButtons.OK);
                Trace.WriteLine(except);
                return;
            }
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            var deserialized = JsonSerializer.Deserialize<ObjectResponse<GrantTokenResponse>>(stringContent, Program.serializerOptions);
            if (deserialized == null || Type.GetType(deserialized.DataType) != typeof(BuildServiceCommon.Authorization.GrantTokenResponse) || deserialized.Success == false)
            {
                MessageBox.Show($"{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}", $"Failed to refresh announcements");
                Trace.WriteLine($"[AdminForm->RefreshAnnouncements] Failed to fetch announcements\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }

            if (deserialized.Data.Success && dynamicContent.Success)
            {
                // We got the token!~
                TokenData = deserialized.Data.Token;
                UserConfig.Set("Authentication", "Token", TokenData.Token);
                var taskList = new Task[]
                {
                    new Task(delegate { FetchAccountDetails(); }),
                    new Task(delegate { FetchServerDetails(); })
                };
                foreach (var i in taskList)
                    i.Start();
                Task.WhenAll(taskList).Wait();
                if (pull)
                    Program.LocalContent.Pull();
            }
            else
            {
                MessageBox.Show($"{deserialized.Data.Message}\n {JsonSerializer.Serialize(deserialized, Program.serializerOptions)}", $"Failed to fetch token");
                TokenData = null;
            }
        }
        public void FetchToken()
        {
            FetchToken(
                UserConfig.GetString("Authentication", "Username", ""),
                UserConfig.GetString("Authentication", "Password", ""));
        }

        public void FetchAccountDetails()
        {
            var targetURL = Endpoint.AccountDetails(Token);

            var response = HttpClient.GetAsync(targetURL).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicDeserialized = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var exceptionDeserialized = JsonSerializer.Deserialize<ObjectResponse<HttpException>>(stringContent, Program.serializerOptions);
                Trace.WriteLine($"[AuthClient->FetchAccountDetails] Recieved HttpException. {exceptionDeserialized.Data.Message}");
                MessageBox.Show($"Failed to fetch account details. Reason:\n{exceptionDeserialized.Data.Message}", @"Recieved HttpException");
                return;
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Trace.WriteLine($"[AuthClient->FetchAccountDetails] Invalid status code. Content\n================\n{stringContent}\n================\n");
                MessageBox.Show(stringContent, @"Invalid Body");
                return;
            }

            var detailsDeserialized = JsonSerializer.Deserialize<ObjectResponse<AccountDetailsResponse>>(stringContent, Program.serializerOptions);
            if (detailsDeserialized == null)
            {
                Trace.WriteLine($"[AuthClient->FetchAccountDetails] Invalid body. Content\n================\n{stringContent}\n================\n");
                MessageBox.Show(stringContent, @"Invalid Body");
                return;
            }
            AccountDetails = detailsDeserialized.Data;
        }
        public void FetchServerDetails()
        {
            ServerDetails = null;
            var targetURL = Endpoint.ServerDetails;

            var response = HttpClient.GetAsync(targetURL).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Trace.WriteLine($"[AuthClient->FetchServerDetails] Invalid status code. Content\n================\n{stringContent}\n================\n");
                MessageBox.Show(stringContent, @"Response not OK");
                return;
            }

            var deserialized = JsonSerializer.Deserialize<ServerDetailsResponse>(stringContent, Program.serializerOptions);
            ServerDetails = deserialized;
        }
    }
}
