using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using BuildServiceCommon.AutoUpdater;
using kate.shared.Helpers;
using Nini.Config;
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
        public static LocalContent LocalContent => Program.LocalContent;
        public AdminForm()
        {
            InitializeComponent();
            
            textBoxLabelUsername.TextboxContent = UserConfig.GetString("Authentication", "Username", "");
            textBoxLabelPassword.TextboxContent = UserConfig.GetString("Authentication", "Password", "");
            textBoxLabelEndpoint.TextboxContent = UserConfig.GetString("Authentication", "Endpoint", "");
            showLatestReleaseToolStripMenuItem.Checked = UserConfig.GetBoolean("General", "ShowLatestRelease", true);
            checkBoxAuthAutoLogin.Checked = UserConfig.GetBoolean("Authentication", "AutoLogin", false);
            SelectedReleasesChange += AdminForm_SelectedReleasesChange;
            toolStripSplitButtonReleaseDelete.Enabled = false;
            toolStripButtonReleaseEdit.Enabled = false;

            LocalContent.OnPullBefore += LocalContent_OnPullBefore;
            LocalContent.OnPull += LocalContent_OnPull;

            LocalContent.OnPushBefore += LocalContent_OnPushBefore;
            LocalContent.OnPush += LocalContent_OnPush;

            if (LocalContent.Auth != null && LocalContent.Auth.ServerDetails != null)
            {
                toolStripLabelServerVersion.Text = $"Server: {LocalContent.Auth.ServerDetails.Version}";
            }
        }


        private void LocalContent_OnPushBefore(ContentField field)
        {
            toolStripAnnouncement.Enabled = false;
            listViewAnnouncement.Enabled = false;
        }
        private void LocalContent_OnPush(ContentField field)
        {
            toolStripAnnouncement.Enabled = true;
            listViewAnnouncement.Enabled = true;
            if (field == ContentField.Announcement || field == ContentField.All)
            {
                RefreshAnnouncementList();
            }
            if (field == ContentField.ContentManager || field == ContentField.All)
            {
                RefreshReleaseTree();
                RefreshReleaseListView();
            }
            toolStripButtonAnnouncementEnforce.Enabled = !LocalContent.AnnouncementSummary.Active;
            toolStripButtonAnnouncementsDisable.Enabled = LocalContent.AnnouncementSummary.Active;
            toolStripLabelServerVersion.Text = $"Server: {LocalContent.Auth.ServerDetails.Version}";
        }

        private void LocalContent_OnPullBefore(ContentField field)
        {
            toolStripAnnouncement.Enabled = false;
            listViewAnnouncement.Enabled = false;
            tabPageAccountMan.Enabled = false;
            tabPageAnnouncementManagement.Enabled = false;
            tabPageReleaseDetails.Enabled = false;

            listBoxSettingsPermissions.Items.Clear();
        }

        private void LocalContent_OnPull(ContentField field)
        {
            Enabled = true;
            toolStripAnnouncement.Enabled = true;
            listViewAnnouncement.Enabled = true;
            toolStripButtonAnnouncementEnforce.Enabled = !LocalContent.AnnouncementSummary.Active;
            toolStripButtonAnnouncementsDisable.Enabled = LocalContent.AnnouncementSummary.Active;
            toolStripLabelServerVersion.Text = $"Server: {LocalContent.Auth.ServerDetails.Version}";

            if (LocalContent.Auth.AccountDetails != null)
            {
                bool admin = LocalContent.Auth.AccountDetails.Permissions.Contains(AccountPermission.ADMINISTRATOR);
                tabPageAccountMan.Enabled = admin || LocalContent.Auth.AccountDetails.Permissions.Contains(AccountPermission.USER_LIST);
                tabPageAnnouncementManagement.Enabled = admin || LocalContent.Auth.AccountDetails.Permissions.Contains(AccountPermission.ANNOUNCEMENT_MANAGE);
                tabPageReleaseDetails.Enabled = admin;
                listBoxSettingsPermissions.Items.Clear();
                foreach (var perm in LocalContent.Auth.AccountDetails.Permissions)
                    listBoxSettingsPermissions.Items.Add(perm.ToString());
            }
            else
            {
                listBoxSettingsPermissions.Items.Add(@"None");
            }

            if (field == ContentField.Announcement || field == ContentField.All)
            {
                RefreshAnnouncementList();
            }
            if (field == ContentField.ContentManager || field == ContentField.All)
            {
                RefreshReleaseTree();
                RefreshReleaseListView();
            }
            if (field == ContentField.Account || field == ContentField.All)
            {
                RefreshAccountListView();
            }
        }

        private void AdminForm_SelectedReleasesChange()
        {
            toolStripSplitButtonReleaseDelete.Enabled = false;
            toolStripButtonReleaseEdit.Enabled = false;
            if (SelectedReleases.Count > 0)
            {
                toolStripSplitButtonReleaseDelete.Enabled = true;
                toolStripButtonReleaseEdit.Enabled = true;
            }
            if (SelectedReleases.Count > 1)
            {
                toolStripButtonReleaseEdit.Enabled = false;
            }
        }

        public void FetchToken()
        {
            UserConfig.Set("Authentication", "Username", textBoxLabelUsername.TextboxContent);
            UserConfig.Set("Authentication", "Password", textBoxLabelPassword.TextboxContent);
            UserConfig.Set("Authentication", "Endpoint", textBoxLabelEndpoint.TextboxContent);
            Program.Save();

            // Fetch token
            Enabled = false;
            try
            {
                LocalContent.Auth.FetchToken();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.ToString(), $"Failed to fetch token", MessageBoxButtons.OK);
                Trace.WriteLine(except);
            }
            Enabled = true;
        }


        #region Account Management
        /// <summary>
        /// Nullable
        /// </summary>
        public AccountDetailsResponse SelectedAccountEntry = null;
        public AccountDetailsResponse[] SelectedAcccountEntry_Arr = Array.Empty<AccountDetailsResponse>();
        public void RefreshAccountListView()
        {
            listViewAccount.Items.Clear();
            foreach (var item in LocalContent.AccountListing)
            {
                var permissionString = new List<string>();
                foreach (var p in item.Permissions)
                    permissionString.Add(p.ToString());
                var lvitem = new ListViewItem(new String[]
                {
                    item.Username,
                    item.Enabled.ToString(),
                    string.Join(", ", item.Groups),
                    string.Join(", ", permissionString)
                });
                lvitem.Name = item.Username;
                listViewAccount.Items.Add(lvitem);
            }
        }
        public void UpdateSelectedAccountItem()
        {
            toolStripButtonAccountBlockAdd.Enabled = false;
            toolStripButtonAccountBlockDel.Enabled = false;
            toolStripButtonAccountBlockEdit.Enabled = false;
            toolStripButtonAccountGroupMan.Enabled = false;
            toolStripButtonAccountPermission.Enabled = false;
            toolStripButtonUserModify.Enabled = false;
            toolStripButtonAccountGroupPowertool.Enabled = false;
            if (LocalContent.Auth.AccountDetails != null)
            {
                bool admin = LocalContent.Auth.AccountDetails.Permissions.Contains(AccountPermission.ADMINISTRATOR);
                toolStripButtonAccountGroupPowertool.Enabled = admin || LocalContent.Auth.AccountDetails.Permissions.Contains(AccountPermission.USER_GROUP_MODIFY);
            }
            SelectedAccountEntry = null;
            var selectedList = new List<AccountDetailsResponse>();
            foreach (var account in LocalContent.AccountListing)
            {
                foreach (ListViewItem selectedItem in listViewAccount.SelectedItems)
                {
                    if (account.Username == selectedItem.Name)
                    {
                        selectedList.Add(account);
                    }
                }
            }
            SelectedAcccountEntry_Arr = selectedList.ToArray();
            if (listViewAccount.SelectedItems.Count < 1) return;
            if (listViewAccount.SelectedItems.Count > 1) return;
            foreach (var item in LocalContent.AccountListing)
            {
                if (item.Username == listViewAccount.SelectedItems[0].Name)
                {
                    #if DEBUG
                    toolStripButtonAccountBlockAdd.Enabled = true;
                    toolStripButtonAccountBlockDel.Enabled = true;
                    toolStripButtonAccountBlockEdit.Enabled = true;
                    toolStripButtonUserModify.Enabled = true;
                    #endif
                    if (LocalContent.Auth.AccountDetails != null)
                    {
                        bool admin = LocalContent.Auth.AccountDetails.Permissions.Contains(AccountPermission.ADMINISTRATOR);
                        toolStripButtonAccountGroupMan.Enabled = admin || LocalContent.Auth.AccountDetails.Permissions.Contains(AccountPermission.USER_GROUP_MODIFY);
                        toolStripButtonAccountPermission.Enabled = admin || LocalContent.Auth.AccountDetails.Permissions.Contains(AccountPermission.USER_PERMISSION_MODIFY);
                    }
                    SelectedAccountEntry = item;
                    break;
                }
            }
        }
        private void listViewAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedAccountItem();
        }
        private void toolStripButtonAccountPermission_Click(object sender, EventArgs e)
        {
            if (SelectedAccountEntry == null) return;
            var form = new AccountPermissionForm(SelectedAccountEntry, this);
            form.Show();
            form.MdiParent = this.MdiParent;
        }
        private void toolStripButtonAccountRefresh_Click(object sender, EventArgs e)
        {
            Enabled = false;
            LocalContent.PullAccounts();
            RefreshAccountListView();
            Enabled = true;
        }
        public AccountGroupForm AccountGroupForm;
        private void toolStripButtonAccountGroupMan_Click(object sender, EventArgs e)
        {
            if (AccountGroupForm == null || AccountGroupForm.IsDisposed)
                AccountGroupForm = new AccountGroupForm();
            AccountGroupForm.MdiParent = this.MdiParent;
            AccountGroupForm.Init(SelectedAccountEntry, this);
            AccountGroupForm.Show();
        }

        public AccountGroupPowertool AccountGroupPowertool;
        private void toolStripButtonAccountGroupPowertool_Click(object sender, EventArgs e)
        {
            if (AccountGroupPowertool == null || AccountGroupPowertool.IsDisposed)
                AccountGroupPowertool = new AccountGroupPowertool();
            AccountGroupPowertool.MdiParent = this.MdiParent;
            AccountGroupPowertool.Init(this);
            AccountGroupPowertool.Show();
        }
        #endregion

        #region Announcements
        public SystemAnnouncementEntry SelectedAnnouncementEntry = null;

        public void RefreshAnnouncementList()
        {
            UpdateSelectedAnnouncementItem();
            listViewAnnouncement.Items.Clear();
            foreach (var item in LocalContent.AnnouncementSummary.Entries)
            {
                var lvitem = new ListViewItem(new string[]
                {
                    item.Message,
                    item.Active.ToString(),
                    item.Timestamp.ToString()
                });
                lvitem.Name = LocalContent.AnnouncementSummary.Entries.IndexOf(item).ToString();
                listViewAnnouncement.Items.Add(lvitem);
            }
            UpdateSelectedAnnouncementItem();
            toolStripButtonAnnouncementEnforce.Enabled = !LocalContent.AnnouncementSummary.Active;
            toolStripButtonAnnouncementsDisable.Enabled = LocalContent.AnnouncementSummary.Active;
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
                if (index > LocalContent.AnnouncementSummary.Entries.Count || index < 0) return;
                toolStripButtonAnnouncementDelete.Enabled = true;
                toolStripButtonAnnouncementEdit.Enabled = true;
                SelectedAnnouncementEntry = LocalContent.AnnouncementSummary.Entries[index];
            }
            catch (Exception) { }
        }
        public void SetAnnouncementContent(SystemAnnouncementEntry entry)
        {
            if (LocalContent.AnnouncementSummary.Entries.Contains(entry))
            {
                int index = LocalContent.AnnouncementSummary.Entries.IndexOf(entry);
                LocalContent.AnnouncementSummary.Entries[index] = entry;
            }
            else
            {
                LocalContent.AnnouncementSummary.Entries.Add(entry);
            }
            RefreshAnnouncementList();
        }

        private void toolStripButtonAnnouncementRefresh_Click(object sender, EventArgs e)
        {
            LocalContent.PullAnnouncements();
            RefreshAnnouncementList();
            UpdateSelectedAnnouncementItem();
        }
        private void toolStripButtonAnnouncementPushChanges_Click(object sender, EventArgs e)
        {
            LocalContent.PushAnnouncements();
        }

        private void listViewAnnouncement_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) => UpdateSelectedAnnouncementItem();

        private void toolStripButtonAnnouncementDelete_Click(object sender, EventArgs e)
        {
            if (SelectedAnnouncementEntry == null) return;
            var newEntries = new List<SystemAnnouncementEntry>();
            foreach (var item in LocalContent.AnnouncementSummary.Entries)
                if (item != SelectedAnnouncementEntry)
                    newEntries.Add(item);
            LocalContent.AnnouncementSummary.Entries = newEntries;
            RefreshAnnouncementList();
        }
        private void toolStripButtonAnnouncementEnforce_Click(object sender, EventArgs e)
        {
            LocalContent.AnnouncementSummary.Active = true;
            toolStripButtonAnnouncementEnforce.Enabled = !LocalContent.AnnouncementSummary.Active;
            toolStripButtonAnnouncementsDisable.Enabled = LocalContent.AnnouncementSummary.Active;
        }

        private void toolStripButtonAnnouncementsDisable_Click(object sender, EventArgs e)
        {
            LocalContent.AnnouncementSummary.Active = false;
            toolStripButtonAnnouncementEnforce.Enabled = !LocalContent.AnnouncementSummary.Active;
            toolStripButtonAnnouncementsDisable.Enabled = LocalContent.AnnouncementSummary.Active;
        }

        private void toolStripButtonAnnouncementAdd_Click(object sender, EventArgs e)
        {
            var announcement = new SystemAnnouncementEntry();
            LocalContent.AnnouncementSummary.Entries.Add(announcement);

            var popup = new AnnouncementEditModal(announcement);
            popup.Show();
            popup.MdiParent = MdiParent;
            popup.AdminForm = this;
        }

        private void listViewAnnouncement_SelectedIndexChanged(object sender, EventArgs e) => UpdateSelectedAnnouncementItem();


        private void toolStripButtonAnnouncementEdit_Click(object sender, EventArgs e)
        {
            if (SelectedAnnouncementEntry == null) return;
            var popup = new AnnouncementEditModal(SelectedAnnouncementEntry);
            popup.Show();
            popup.MdiParent = MdiParent;
            popup.AdminForm = this;
        }
        #endregion


        #region Releases
        public List<ReleaseInfo> SelectedReleases = new List<ReleaseInfo>();
        public event VoidDelegate SelectedReleasesChange;
        public void RefreshReleaseTree()
        {
            treeViewReleaseProduct.Nodes.Clear();
            if (LocalContent.ContentManagerAlias == null)
            {
                Trace.WriteLine($"[AdminForm->RefreshReleaseTree] ContentManagerAlias is null");
                return;
            }

            Dictionary<string, List<ReleaseInfo>> releaseInfoDict = new Dictionary<string, List<ReleaseInfo>>();
            foreach (var release in LocalContent.ContentManagerAlias.ReleaseInfoContent)
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
            listViewReleases_SelectedIndexChanged(null, null);
        }
        public void RefreshReleaseListView()
        {
            listViewReleases.Items.Clear();
            if (LocalContent.ContentManagerAlias == null)
            {
                Trace.WriteLine($"[AdminForm->RefreshReleaseTree] ContentManagerAlias is null");
                return;
            }
            if (treeViewReleaseProduct.SelectedNode == null) return;
            var targetReleaseList = LocalContent.ContentManagerAlias.ReleaseInfoContent
                .Where(v => v.appID == treeViewReleaseProduct.SelectedNode.Text)
                .OrderByDescending(s => s.timestamp).ToList();
            if (UserConfig.GetBoolean("General", "ShowLatestRelease", true))
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
                lvitem.Name = LocalContent.ContentManagerAlias.ReleaseInfoContent.IndexOf(item).ToString();
                listViewReleases.Items.Add(lvitem);
            }
        }

        private void toolStripButtonReleasePush_Click(object sender, EventArgs e)
        {
            Enabled = false;
            LocalContent.PushContentManager();
            Enabled = true;
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
            UserConfig.Set("General", "ShowLatestRelease", showLatestReleaseToolStripMenuItem.Checked);
            Program.Save();
            RefreshReleaseListView();
        }

        private void listViewReleases_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedReleases.Clear();
            for (int i = 0; i < listViewReleases.SelectedItems.Count; i++)
            {
                var item = listViewReleases.SelectedItems[i];
                int attemptedIndex = int.Parse(item.Name);
                if (attemptedIndex >= 0)
                {
                    SelectedReleases.Add(LocalContent.ContentManagerAlias.ReleaseInfoContent[attemptedIndex]);
                }
            }
            if (SelectedReleasesChange != null)
                SelectedReleasesChange?.Invoke();
        }

        private void toolStripButtonReleaseEdit_Click(object sender, EventArgs e)
        {
            var form = new ReleaseEditForm(SelectedReleases[0], LocalContent.ContentManagerAlias.ReleaseInfoContent.IndexOf(SelectedReleases[0]), this);
            form.Show();
            form.MdiParent = MdiParent;
            form.AdminForm = this;
        }

        private void toolStripSplitButtonReleaseDelete_ButtonClick(object sender, EventArgs e)
        {
            foreach (var selected in SelectedReleases)
            {
                RemoveRelease(selected);
            }
            RefreshReleaseListView();
            RefreshReleaseTree();
        }
        public bool RemoveRelease(ReleaseInfo releaseInfo) => LocalContent.ContentManagerAlias.ReleaseInfoContent.Remove(releaseInfo);
        public void RemoveReleaseBySignature(string signature)
        {
            var newReleaseInfoList = new List<ReleaseInfo>();
            foreach (var item in LocalContent.ContentManagerAlias.ReleaseInfoContent)
            {
                if (item.remoteLocation != signature)
                    newReleaseInfoList.Add(item);
            }
            LocalContent.ContentManagerAlias.ReleaseInfoContent = newReleaseInfoList;
        }

        private void toolStripMenuItemDeleteRemoteSignature_Click(object sender, EventArgs e)
        {
            foreach (var selected in SelectedReleases)
            {
                RemoveReleaseBySignature(selected.remoteLocation);
            }
            RefreshReleaseListView();
            RefreshReleaseTree();
        }

        private void listViewReleases_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) => listViewReleases_SelectedIndexChanged(null, null);

        private void listViewReleases_Click(object sender, EventArgs e) => listViewReleases_SelectedIndexChanged(null, null);
        #endregion

        private void toolStripButtonMainPull_Click(object sender, EventArgs e)
        {
            LocalContent.Pull();
        }
        private void toolStripButtonMainPushChanges_Click(object sender, EventArgs e)
        {
            LocalContent.Push();
        }
        private void buttonConnectionTokenFetch_Click(object sender, EventArgs e)
        {
            LocalContent.Auth.FetchToken();
        }

        private void checkBoxAuthAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            UserConfig.Set("Authentication", "AutoLogin", checkBoxAuthAutoLogin.Checked);
            UserConfig.Save();
        }

    }
}
