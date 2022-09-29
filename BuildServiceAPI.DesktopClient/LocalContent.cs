using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace BuildServiceAPI.DesktopClient
{
    public enum ContentField
    {
        All,
        Account,
        Announcement,
        ContentManager
    }
    public class LocalContent
    {
        public void Pull()
        {
            if (!Auth.ValidateToken())
            {
                MessageBox.Show($"Token is not valid", @"Cannot pull from server.");
                Trace.WriteLine($"[LocalContent->Pull] Cannot pull from server, token is invalid");
                return;
            }
            OnPullBefore?.Invoke(ContentField.All);
            var taskArray = new Task[]
            {
                new Task(delegate { PullAccounts(false); }),
                new Task(delegate { PullAnnouncements(false); }),
                new Task(delegate { PullContentManager(false); }),
                new Task(delegate { Auth.FetchAccountDetails(); }),
                new Task(delegate { Auth.FetchServerDetails(); })
            };

            foreach (var i in taskArray)
                i.Start();
            Task.WhenAll(taskArray).Wait();
            OnPull?.Invoke(ContentField.All);
        }
        public void Push()
        {
            if (!Auth.ValidateToken())
            {
                MessageBox.Show($"Token is not valid", @"Cannot push to server.");
                Trace.WriteLine($"[LocalContent->Push] Cannot push to server, token is invalid");
                return;
            }
            OnPushBefore?.Invoke(ContentField.All);
            var taskArray = new Task[]
            {
                new Task(delegate { PushAnnouncements(false); }),
                new Task(delegate { PushContentManager(false); })
            };

            foreach (var i in taskArray)
                i.Start();
            Task.WhenAll(taskArray).Wait();
            OnPush?.Invoke(ContentField.All);
        }
        public AuthClient Auth => Program.AuthClient;
        public AdminForm AdminForm => Program.AdminForm;

        public delegate void ContentFieldDelegate(ContentField field);
        public event ContentFieldDelegate OnPull;
        public event ContentFieldDelegate OnPullBefore;
        public event ContentFieldDelegate OnPush;
        public event ContentFieldDelegate OnPushBefore;

        #region Account
        public List<AccountDetailsResponse> AccountListing = new List<AccountDetailsResponse>();
        public void PullAccounts(bool emit=true)
        {
            AccountListing.Clear();
            var targetURL = Endpoint.UserList(Auth.Token);
            var response = Auth.HttpClient.GetAsync(targetURL).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            var content = JsonSerializer.Deserialize<ObjectResponse<AccountDetailsResponse[]>>(stringContent, Program.serializerOptions);
            if (!dynamicContent.Success || content == null)
            {
                MessageBox.Show($"{stringContent}", $"Failed to pull accounts");
                Trace.WriteLine($"[LocalContent->PullAccounts] Failed to fetch account listings\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }
            AccountListing = new List<AccountDetailsResponse>(content.Data);
            if (emit)
                OnPull?.Invoke(ContentField.Account);
        }
        #endregion

        #region Announcements
        public class SystemAnnouncementSummaryAsList : SystemAnnouncementSummary
        {
            public new List<SystemAnnouncementEntry> Entries { get; set; }
            public SystemAnnouncementSummaryAsList()
                : base()
            {
                Entries = new List<SystemAnnouncementEntry>();
            }
        }
        public SystemAnnouncementSummaryAsList AnnouncementSummary = new SystemAnnouncementSummaryAsList();
        public void PullAnnouncements(bool emit = true)
        {
            AnnouncementSummary = new SystemAnnouncementSummaryAsList();
            var targetURL = Endpoint.AnnouncementSummary(Auth.Token);
            Trace.WriteLine($"[LocalContent->RefreshAnnouncements] Fetching Response of {targetURL}");

            var response = Auth.HttpClient.GetAsync(targetURL).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            var content = JsonSerializer.Deserialize<ObjectResponse<SystemAnnouncementSummaryAsList>>(stringContent, Program.serializerOptions);
            if (content == null || dynamicContent.Success == false)
            {
                MessageBox.Show($"{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}", $"Failed to refresh announcements");
                Trace.WriteLine($"[LocalContent->RefreshAnnouncements] Failed to fetch announcements\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }

            AnnouncementSummary = content.Data;
            if (emit)
                OnPull?.Invoke(ContentField.Announcement);
        }
        public void PushAnnouncements(bool emit = true)
        {
            if (AnnouncementSummary == null)
            {
                Trace.WriteLine($"[LocalContent->PushAnnouncements] Cannot push since AnnouncementSummary is null.");
                MessageBox.Show($"AnnouncementSummary is null ;w;", $"Failed to push announcements");
                return;
            }

            if (AnnouncementSummary.Entries.Count < 1)
                AnnouncementSummary.Active = false;
            int activeCount = 0;
            foreach (var item in AnnouncementSummary.Entries)
                if (item.Active)
                    activeCount++;
            if (activeCount < 1)
                AnnouncementSummary.Active = false;

            var arraySummary = new SystemAnnouncementSummary()
            {
                Active = AnnouncementSummary.Active,
                Entries = AnnouncementSummary.Entries.ToArray()
            };

            var targetURL = Endpoint.AnnouncementSetData(Auth.Token, arraySummary);
            var response = Auth.HttpClient.GetAsync(targetURL).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            var content = JsonSerializer.Deserialize<ObjectResponse<SystemAnnouncementSummary>>(stringContent, Program.serializerOptions);
            if (!dynamicContent.Success || content == null)
            {
                MessageBox.Show($"{stringContent}", $"Failed to push announcements");
                Trace.WriteLine($"[LocalContent->PushAnnouncements] Failed to push announcements\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }
            AnnouncementSummary = new SystemAnnouncementSummaryAsList()
            {
                Active = content.Data.Active,
                Entries = new List<SystemAnnouncementEntry>(content.Data.Entries)
            };
            if (emit)
                OnPush?.Invoke(ContentField.Announcement);
        }
        #endregion

        #region Content Manager
        public AllDataResult ContentManagerAlias = null;
        public void PullContentManager(bool emit = true)
        {
            var targetURL = Endpoint.DumpDataFetch(Auth.Token, DataType.All);
            var response = Auth.HttpClient.GetAsync(targetURL).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            var content = JsonSerializer.Deserialize<ObjectResponse<AllDataResult>>(stringContent, Program.serializerOptions);
            if (!dynamicContent.Success || content == null)
            {
                MessageBox.Show($"{stringContent}", $"Failed to refresh content manager");
                Trace.WriteLine($"[LocalContent->PullContentManager] Failed to fetch content manager\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }
            ContentManagerAlias = content.Data;
            if (emit)
                OnPull?.Invoke(ContentField.ContentManager);
        }
        public void PushContentManager(bool emit = true)
        {
            if (ContentManagerAlias == null)
            {
                Trace.WriteLine($"[LocalContent->PushContentManager] Cannot push since ContentManagerAlias is null");
                MessageBox.Show("ContentManagerAlias is null ;w;", "Failed to push Content Manager");
                return;
            }

            ContentManagerAlias.Releases = ReleaseHelper.TransformReleaseList(ContentManagerAlias.ReleaseInfoContent.ToArray());

            var targetURL = Endpoint.DumpSetData(Auth.Token, DataType.All);
            var pushContent = new ObjectResponse<AllDataResult>()
            {
                Success = true,
                Data = ContentManagerAlias
            };
            var _strcon = new StringContent(JsonSerializer.Serialize(pushContent, Program.serializerOptions));
            var response = Auth.HttpClient.PostAsync(targetURL, _strcon).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            if (dynamicContent.Success == false)
            {
                MessageBox.Show($"{stringContent}", $"Failed to push content manager");
                Trace.WriteLine($"[LocalContent->PushContentManager] Failed to push content manager\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }
            var content = JsonSerializer.Deserialize<ObjectResponse<object>>(stringContent, Program.serializerOptions);
            if (!dynamicContent.Success || content == null)
            {
                MessageBox.Show($"{stringContent}", $"Failed to push content manager");
                Trace.WriteLine($"[LocalContent->PushContentManager] Failed to push content manager\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }
            var contentTyped = JsonSerializer.Deserialize<ObjectResponse<AllDataResult>>(stringContent, Program.serializerOptions);
            ContentManagerAlias = contentTyped.Data;
            if (emit)
                OnPush?.Invoke(ContentField.ContentManager);
        }
        #endregion
    }

}
