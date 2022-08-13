using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using BuildServiceCommon.AutoUpdater;
using kate.shared.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildServiceAPI.DesktopClient
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            textBoxLabelUsername.TextboxContent = (string)Properties.Settings.Default["Username"];
            textBoxLabelPassword.TextboxContent = (string)Properties.Settings.Default["Password"];
            textBoxLabelEndpoint.TextboxContent = (string)Properties.Settings.Default["ServerEndpoint"];
            httpClient = new HttpClient();
            Refresh += AdminForm_Refresh;
            PushChanges += AdminForm_PushChanges;
        }

        private void AdminForm_PushChanges()
        {
            buttonPushAll_Click(null, null);
        }

        private void AdminForm_Refresh()
        {
            buttonRefresh_Click(null, null);
        }

        private void buttonConnectionTokenFetch_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["Username"] = textBoxLabelUsername.TextboxContent;
            Properties.Settings.Default["Password"] = textBoxLabelPassword.TextboxContent;
            Properties.Settings.Default["ServerEndpoint"] = textBoxLabelEndpoint.TextboxContent;
            Program.Save();

            // Fetch token
            Enabled = false;
            UpdateToken();
            Enabled = true;
        }

        private static HttpClient httpClient;

        public void UpdateToken()
        {
            var targetURL = Endpoint.TokenGrant(Properties.Settings.Default["Username"].ToString(), Properties.Settings.Default["Password"].ToString());
            Trace.WriteLine($"[AdminForm->UpdateToken] Fetching Response of {targetURL}");

            var response = httpClient.GetAsync(targetURL).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            var deserialized = JsonSerializer.Deserialize<ObjectResponse<GrantTokenResponse>>(stringContent, Program.serializerOptions);
            if (deserialized == null || deserialized.DataType != "BuildServiceCommon.Authorization.GrantTokenResponse" || deserialized.Success == false)
            {
                MessageBox.Show($"{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}", $"Failed to refresh announcements");
                Trace.WriteLine($"[AdminForm->RefreshAnnouncements] Failed to fetch announcements\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }

            if (deserialized.Data.Success && dynamicContent.Success)
            {
                // We got the token!~
                Token = deserialized.Data.Token;
            }
            else
            {
                MessageBox.Show($"{deserialized.Data.Message}\n {JsonSerializer.Serialize(deserialized, Program.serializerOptions)}", $"Failed to fetch token");
                Token = null;
            }
            OnRefresh();
        }

        public event VoidDelegate PushChanges;
        public void OnPushChanges()
        {
            if (PushChanges != null)
                PushChanges?.Invoke();
        }

        public event VoidDelegate Refresh;
        public void OnRefresh()
        {
            if (Refresh != null)
                Refresh?.Invoke();
        }

        public AccountToken Token;

        public SystemAnnouncementSummary AnnouncementSummary = new SystemAnnouncementSummary();
        public SystemAnnouncementEntry SelectedAnnouncementEntry = null;
        public AllDataResult ContentManagerAlias = null;

        public void RefreshAnnouncements()
        {
            AnnouncementSummary = new SystemAnnouncementSummary();
            if (Token == null)
            {
                Trace.WriteLine($"[AdminForm->RefreshAnnouncements] Token is null");
                return;
            }
            var targetURL = Endpoint.AnnouncementSummary(Token.Token);
            Trace.WriteLine($"[AdminForm->RefreshAnnouncements] Fetching Response of {targetURL}");

            var response = httpClient.GetAsync(targetURL).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            var content = JsonSerializer.Deserialize<ObjectResponse<SystemAnnouncementSummary>>(stringContent, Program.serializerOptions);
            if (content == null || dynamicContent.Success == false)
            {
                MessageBox.Show($"{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}", $"Failed to refresh announcements");
                Trace.WriteLine($"[AdminForm->RefreshAnnouncements] Failed to fetch announcements\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }

            AnnouncementSummary = content.Data;
        }
        public void PushAnnouncements()
        {
            if (AnnouncementSummary == null)
            {
                Trace.WriteLine($"[AdminForm->PushAnnouncements] Cannot push since AnnouncementSummary is null.");
                MessageBox.Show($"AnnouncementSummary is null ;w;", $"Failed to push announcements");
                return;
            }

            if (AnnouncementSummary.Entries.Length < 1)
                AnnouncementSummary.Active = false;
            int activeCount = 0;
            foreach (var item in AnnouncementSummary.Entries)
                if (item.Active)
                    activeCount++;
            if (activeCount < 1)
                AnnouncementSummary.Active = false;

            var targetURL = Endpoint.AnnouncementSetData(Token.Token, AnnouncementSummary);
            var response = httpClient.GetAsync(targetURL).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            var content = JsonSerializer.Deserialize<ObjectResponse<SystemAnnouncementSummary>>(stringContent, Program.serializerOptions);
            if (!dynamicContent.Success || content == null)
            {
                MessageBox.Show($"{stringContent}", $"Failed to push announcements");
                Trace.WriteLine($"[AdminForm->PushAnnouncements] Failed to push announcements\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }
            AnnouncementSummary = content.Data;
        }
        public void RefreshContentManager()
        {
            var targetURL = Endpoint.DumpDataFetch(Token.Token, DataType.All);
            var response = httpClient.GetAsync(targetURL).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            var content = JsonSerializer.Deserialize<ObjectResponse<AllDataResult>>(stringContent, Program.serializerOptions);
            if (!dynamicContent.Success || content == null)
            {
                MessageBox.Show($"{stringContent}", $"Failed to refresh content manager");
                Trace.WriteLine($"[AdminForm->RefreshContentManager] Failed to fetch content manager\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }
            ContentManagerAlias = content.Data;
        }
        public void PushContentManager()
        {
            if (ContentManagerAlias == null)
            {
                Trace.WriteLine($"[AdminForm->PushContentManager] Cannot push since ContentManagerAlias is null");
                MessageBox.Show("ContentManagerAlias is null ;w;", "Failed to push Content Manager");
                return;
            }

            var targetURL = Endpoint.DumpSetData(Token.Token, DataType.All, ContentManagerAlias);
            var response = httpClient.GetAsync(targetURL).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            var content = JsonSerializer.Deserialize<ObjectResponse<AllDataResult>>(stringContent, Program.serializerOptions);
            if (!dynamicContent.Success || content == null)
            {
                MessageBox.Show($"{stringContent}", $"Failed to push content manager");
                Trace.WriteLine($"[AdminForm->PushContentManager] Failed to push content manager\n--------\n{JsonSerializer.Serialize(dynamicContent, Program.serializerOptions)}\n--------\n");
                return;
            }
            ContentManagerAlias = content.Data;
        }

        public void RefreshAnnouncementList()
        {
            UpdateSelectedAnnouncementItem();
            listViewAnnouncement.Items.Clear();
            foreach (var item in AnnouncementSummary.Entries)
            {
                var lvitem = new ListViewItem(new string[]
                {
                    item.Message,
                    item.Active.ToString(),
                    item.Timestamp.ToString()
                });
                lvitem.Name = Array.IndexOf(AnnouncementSummary.Entries, item).ToString();
                listViewAnnouncement.Items.Add(lvitem);
            }
            UpdateSelectedAnnouncementItem();
        }
        public void UpdateSelectedAnnouncementItem()
        {
            toolStripButtonAnnouncementDelete.Enabled = false;
            toolStripButtonAnnouncementEdit.Enabled = false;
            SelectedAnnouncementEntry = null;
            if (listViewAnnouncement.SelectedItems.Count < 1) return;
            try
            {
                int index = int.Parse(listViewAnnouncement.SelectedItems[0].Name);
                if (index > AnnouncementSummary.Entries.Length || index < 0) return;
                toolStripButtonAnnouncementDelete.Enabled = true;
                toolStripButtonAnnouncementEdit.Enabled = true;
                SelectedAnnouncementEntry = AnnouncementSummary.Entries[index];
            }
            catch (Exception) { }
        }

        private void toolStripButtonAnnouncementRefresh_Click(object sender, EventArgs e)
        {
            RefreshAnnouncements();
            RefreshAnnouncementList();
            UpdateSelectedAnnouncementItem();
        }

        private void listViewAnnouncement_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) => UpdateSelectedAnnouncementItem();

        private void toolStripButtonAnnouncementDelete_Click(object sender, EventArgs e)
        {
            if (SelectedAnnouncementEntry == null) return;
            var newEntries = new List<SystemAnnouncementEntry>();
            foreach (var item in AnnouncementSummary.Entries)
                if (item != SelectedAnnouncementEntry)
                    newEntries.Add(item);
            AnnouncementSummary.Entries = newEntries.ToArray();
            RefreshAnnouncementList();
        }

        private void buttonPushAll_Click(object sender, EventArgs e)
        {
            toolStripAnnouncement.Enabled = false;
            listViewAnnouncement.Enabled = false;
            var taskArray = new Task[]
            {
                new Task(delegate { PushAnnouncements(); }),
                new Task(delegate { PushContentManager(); })
            };
            foreach (var i in taskArray)
                i.Start();
            Task.WhenAll(taskArray).Wait();
            toolStripAnnouncement.Enabled = true;
            listViewAnnouncement.Enabled = true;
            RefreshAnnouncementList();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            toolStripAnnouncement.Enabled = false;
            listViewAnnouncement.Enabled = false;
            var taskArray = new Task[]
            {
                new Task(delegate { RefreshAnnouncements(); }),
                new Task(delegate { RefreshContentManager(); })
            };
            foreach (var i in taskArray)
                i.Start();
            Task.WhenAll(taskArray).Wait();
            Enabled = true;
            toolStripAnnouncement.Enabled = true;
            listViewAnnouncement.Enabled = true;
            RefreshAnnouncementList();
            RefreshReleaseTree();
            RefreshReleaseListView();
        }

        private void toolStripButtonAnnouncementPushChanges_Click(object sender, EventArgs e)
        {
            PushAnnouncements();
        }

        private void listViewAnnouncement_SelectedIndexChanged(object sender, EventArgs e) => UpdateSelectedAnnouncementItem();
    
        public void RefreshReleaseTree()
        {
            treeViewReleaseProduct.Nodes.Clear();
            if (ContentManagerAlias == null)
            {
                Trace.WriteLine($"[AdminForm->RefreshReleaseTree] ContentManagerAlias is null");
                return;
            }

            Dictionary<string, List<ReleaseInfo>> releaseInfoDict = new Dictionary<string, List<ReleaseInfo>>();
            foreach (var release in ContentManagerAlias.ReleaseInfoContent)
            {
                if (release.appID.Length < 1) continue;
                if (!releaseInfoDict.ContainsKey(release.appID))
                    releaseInfoDict.Add(release.appID, new List<ReleaseInfo>());
                releaseInfoDict[release.appID].Add(release);
            }
            foreach (var pair in releaseInfoDict)
            {
                treeViewReleaseProduct.Nodes.Add($"{pair.Key}");
            }
        }
        public void RefreshReleaseListView()
        {
            listViewReleases.Items.Clear();
            if (ContentManagerAlias == null)
            {
                Trace.WriteLine($"[AdminForm->RefreshReleaseTree] ContentManagerAlias is null");
                return;
            }
            if (treeViewReleaseProduct.SelectedNode == null) return;
            var targetReleaseList = ContentManagerAlias.ReleaseInfoContent
                .Where(v => v.appID == treeViewReleaseProduct.SelectedNode.Text)
                .OrderBy(s => s.timestamp).ToList();
            if ((bool)Properties.Settings.Default["ShowLatestRelease"])
            {
                targetReleaseList = targetReleaseList.GroupBy(v => v.remoteLocation)
                    .Select(v => v.First()).ToList();
            }
            foreach (var item in targetReleaseList)
            {
                var lvitem = new ListViewItem(
                    new string[]
                    {
                        item.commitHashShort,
                        item.remoteLocation,
                        DateTimeOffset.FromUnixTimeMilliseconds(item.timestamp).ToString()
                    });
                lvitem.Name = ContentManagerAlias.ReleaseInfoContent.IndexOf(item).ToString();
                listViewReleases.Items.Add(lvitem);
            }
        }

        private void toolStripButtonReleaseRefresh_Click(object sender, EventArgs e)
        {
            RefreshReleaseTree();
            RefreshReleaseListView();
        }

        private void treeViewReleaseProduct_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshReleaseListView();
        }

        private void showLatestReleaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["ShowLatestRelease"] = showLatestReleaseToolStripMenuItem.Checked;
            Program.Save();
            RefreshReleaseListView();
        }
    }
}
