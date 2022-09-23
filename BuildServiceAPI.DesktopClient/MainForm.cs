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

            AdminForm = new AdminForm();
            AdminForm.MdiParent = this;
            AdminForm.Show();
            AdminForm.FormClosing += AdminForm_FormClosing;

#if false
            var componentZoo = new ComponentZoo();
            componentZoo.MdiParent = this;
            componentZoo.Show();
#endif
            AutoUpdater.Start("https://raw.githubusercontent.com/ktwrd/BuildServiceAPI/main/AutoUpdate.DesktopClient.xml");
            AutoUpdater.Mandatory = true;
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                Close();
        }
    }
}
