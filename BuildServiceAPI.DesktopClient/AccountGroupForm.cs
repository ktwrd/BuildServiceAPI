using BuildServiceCommon.Authorization;
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
    public partial class AccountGroupForm : Form
    {
        public AccountDetailsResponse Account;
        public AdminForm AdminForm;
        public AccountGroupForm(AccountDetailsResponse account, AdminForm adminForm)
        {
            InitializeComponent();

            AdminForm = adminForm;
            Account = account;
            textBoxList1.MinimumItems = 0;
            foreach (var i in textBoxList1.Items)
                textBoxList1.RemoveItem(i);
        }

        private void buttonPush_Click(object sender, EventArgs e)
        {

        }
    }
}
