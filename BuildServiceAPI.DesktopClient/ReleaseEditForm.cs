using BuildServiceCommon.AutoUpdater;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildServiceAPI.DesktopClient
{
    public partial class ReleaseEditForm : Form
    {
        public AdminForm AdminForm;
        public ReleaseInfo ReleaseInfo;
        public int ReleaseIndex;
        public ReleaseEditForm(ReleaseInfo releaseInfo, int index)
        {
            InitializeComponent();
            this.ReleaseInfo = releaseInfo;
            ReleaseIndex = index;
            comboBoxLabelReleaseType.Items.Add(ReleaseType.Stable);
            comboBoxLabelReleaseType.Items.Add(ReleaseType.Beta);
            comboBoxLabelReleaseType.Items.Add(ReleaseType.Nightly);
            comboBoxLabelReleaseType.Items.Add(ReleaseType.Other);

            Text = $"Release Edit Form - {ReleaseInfo.commitHash}";
            LoadContent();
        }

        private void ResetChanges()
        {
            ReleaseInfo = AdminForm.ContentManagerAlias.ReleaseInfoContent[ReleaseIndex];
            LoadContent();
        }

        private void LoadContent()
        {
            textBoxLabelName.TextboxContent = ReleaseInfo.name;
            textBoxLabelProductName.TextboxContent = ReleaseInfo.productName;
            textBoxLabelAppID.TextboxContent = ReleaseInfo.appID;
            textBoxLabelVersion.TextboxContent = ReleaseInfo.version;
            textBoxLabelCommitHash.TextboxContent = ReleaseInfo.commitHash;
            comboBoxLabelReleaseType.SelectedIndex = (int)ReleaseInfo.releaseType;

            textBoxListGroupWhitelist.MinimumItems = 0;
            textBoxListGroupBlacklist.MinimumItems = 0;
            textBoxListGroupWhitelist.RemoveAllItems();
            textBoxListGroupBlacklist.RemoveAllItems();
            foreach (var item in ReleaseInfo.groupWhitelist)
            {
                textBoxListGroupWhitelist.AddItem(new TextBoxListItem()
                {
                    Text = item
                });
            }
            foreach (var item in ReleaseInfo.groupBlacklist)
            {
                textBoxListGroupBlacklist.AddItem(new TextBoxListItem()
                {
                    Text = item
                });
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ReleaseInfo.groupWhitelist.Clear();
            ReleaseInfo.groupBlacklist.Clear();
            foreach (var item in textBoxListGroupWhitelist.Items)
            {
                if (item.Text.Length < 1) continue;
                ReleaseInfo.groupWhitelist.Add(item.Text);
            }
            foreach (var item in textBoxListGroupBlacklist.Items)
            {
                if (item.Text.Length < 1) continue;
                ReleaseInfo.groupBlacklist.Add(item.Text);
            }
            ReleaseInfo.releaseType = (ReleaseType)comboBoxLabelReleaseType.SelectedIndex;
            ReleaseInfo.name = textBoxLabelName.TextboxContent;
            ReleaseInfo.version = textBoxLabelVersion.TextboxContent;
            ReleaseInfo.commitHash = textBoxLabelCommitHash.TextboxContent;
            ReleaseInfo.commitHashShort = textBoxLabelCommitHash.TextboxContent.Substring(0, 7);
            AdminForm.ContentManagerAlias.ReleaseInfoContent[ReleaseIndex] = ReleaseInfo;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetChanges();
        }
    }
}
