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
        public AccountGroupForm()
        {
            InitializeComponent();
        }
        public AccountGroupForm(AccountDetailsResponse account, AdminForm adminForm)
        {
            InitializeComponent();

            AdminForm = adminForm;
            Account = account;
        }

        public void Reset()
        {
            textBoxList1.MinimumItems = 1;
            foreach (var i in Account.Groups)
                textBoxList1.AddItem(i);
        }

        private void buttonPush_Click(object sender, EventArgs e)
        {
            var groupList = new List<string>();
            foreach (var item in textBoxList1.Items)
            {
                if (item.Text.Length > 0)
                    groupList.Add(item.Text);
            }
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

            var response = AdminForm.httpClient.PostAsync(targetURL, new StringContent(JsonSerializer.Serialize(sendContentObject, Program.serializerOptions))).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;
            var dynamicContent = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringContent, Program.serializerOptions);
            if (!dynamicContent.Success)
            {
                var exceptionContent = JsonSerializer.Deserialize<ObjectResponse<HttpException>>(stringContent, Program.serializerOptions);
                MessageBox.Show($"({exceptionContent.Data.Code}) {exceptionContent.Data.Message}\n{exceptionContent.Data.Exception}", $"Failed to push user group");
            }

            AdminForm.RefreshAccounts();
            AdminForm.RefreshAccountListView();
        }

        private void AccountGroupForm_Shown(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
