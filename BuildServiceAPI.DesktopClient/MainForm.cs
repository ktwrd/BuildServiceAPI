using AutoUpdaterDotNET;
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
    public partial class MainForm : Form
    {
        public AdminForm AdminForm = null;
        public MainForm()
        {
            InitializeComponent();

            Program.LocalContent = new LocalContent();
            Program.LocalContent.OnPull += LocalContent_OnPull;
            Program.LocalContent.OnPush += LocalContent_OnPush;

            AdminForm = new AdminForm();
            AdminForm.MdiParent = this;
            AdminForm.Show();
            AdminForm.Shown += AdminForm_Shown;
            AdminForm.FormClosing += AdminForm_FormClosing;

#if false
            var componentZoo = new ComponentZoo();
            componentZoo.MdiParent = this;
            componentZoo.Show();
#endif
            AutoUpdater.Start("https://raw.githubusercontent.com/ktwrd/BuildServiceAPI/main/AutoUpdate.DesktopClient.xml");
            AutoUpdater.Mandatory = true;
        }

        private void LocalContent_OnPush(ContentField field)
        {
            toolStripLabelServerVersion.Text = $"Server Version: Unknown";
            if (Program.LocalContent.Auth.ServerDetails != null)
            {
                toolStripLabelServerVersion.Text = $"Server Version: v{Program.LocalContent.Auth.ServerDetails.Version}";
            }
        }

        private void LocalContent_OnPull(ContentField field)
        {
            toolStripLabelServerVersion.Text = $"Server Version: Unknown";
            if (Program.LocalContent.Auth.ServerDetails != null)
            {
                toolStripLabelServerVersion.Text = $"Server Version: v{Program.LocalContent.Auth.ServerDetails.Version}";
            }
        }

        private void AdminForm_Shown(object sender, EventArgs e)
        {
            Program.AuthClient = new AuthClient();
            var gotten = UserConfig.Get();
            if (UserConfig.GetBoolean("Authentication", "AutoLogin", false))
                Program.AuthClient.ValidateOrFetch();
            if (Program.AuthClient.TokenData != null)
                Program.LocalContent.Pull();
            AdminForm.Shown -= AdminForm_Shown;
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                Close();
        }
    }
}
