using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
            var groupList = new List<string>();
            var dict = new Dictionary<string, string[]>()
            {
                { Account.Username, groupList.ToArray() }
            };
            var targetURL = Endpoint.UserGroupSet(AdminForm.Token.Token, Account.Username);
            var sendContentObject = new ObjectResponse<Dictionary<string, string[]>>()
            {
                Success = true,
                Data = dict
            };

            AdminForm.httpClient.PostAsync(targetURL, new StringContent(JsonSerializer.Serialize(sendContentObject, Program.serializerOptions))).Wait();
            AdminForm.RefreshAccounts();
            AdminForm.RefreshAccountListView();
        }
    }
}
