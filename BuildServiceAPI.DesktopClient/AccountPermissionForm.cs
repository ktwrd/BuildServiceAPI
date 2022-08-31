using BuildServiceCommon.Authorization;
using kate.shared.Helpers;
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
    public partial class AccountPermissionForm : Form
    {
        public AccountDetailsResponse Account;
        public AdminForm AdminForm;
        public AccountPermissionForm(AccountDetailsResponse account, AdminForm adminForm)
        {
            InitializeComponent();
            Account = account;
            AdminForm = adminForm;

            UpdatePermissionCheckboxList();

            for (int i = 0; i < checkedListBoxPermissions.Items.Count; i++)
            {
                var perm = (AccountPermission)checkedListBoxPermissions.Items[i];
                if (new List<AccountPermission>(Account.Permissions).Contains(perm))
                {
                    checkedListBoxPermissions.SetItemChecked(i, true);    
                }
            }
        }

        public void UpdatePermissionCheckboxList()
        {
            //checkedListBoxPermissions
            checkedListBoxPermissions.Items.Clear();
            foreach (var item in GeneralHelper.GetEnumList<AccountPermission>())
            {
                checkedListBoxPermissions.Items.Add(item);
            }
        }

        private void buttonPush_Click(object sender, EventArgs e)
        {
            Enabled = false;
            AdminForm.Enabled = false;
            var revokeList = new List<AccountPermission>();
            var grantList = new List<AccountPermission>();
            for (int i = 0; i < checkedListBoxPermissions.Items.Count; i++)
            {
                var target = (AccountPermission)checkedListBoxPermissions.Items[i];
                bool is_checked = checkedListBoxPermissions.GetItemChecked(i);
                bool previousState = (new List<AccountPermission>(Account.Permissions).Contains((AccountPermission)checkedListBoxPermissions.Items[i]));
                if (is_checked && previousState == false)
                {
                    grantList.Add(target);
                }
                else if (is_checked == false && previousState == true)
                {
                    revokeList.Add(target);
                }
            }

            var taskList = new List<Task>();
            foreach (var perm in revokeList)
            {
                taskList.Add(new Task(delegate
                {
                    var targetURL = Endpoint.UserPermissionRevoke(AdminForm.Token.Token, Account.Username, perm);
                    AdminForm.httpClient.GetAsync(targetURL).Wait();
                }));
            }
            foreach (var perm in grantList)
            {
                taskList.Add(new Task(delegate
                {
                    var targetURL = Endpoint.UserPermissionGrant(AdminForm.Token.Token, Account.Username, perm);
                    AdminForm.httpClient.GetAsync(targetURL).Wait();
                }));
            }
            foreach (var t in taskList)
                t.Start();
            Task.WhenAll(taskList);
            Enabled = true;
            AdminForm.RefreshAccounts();
            AdminForm.RefreshAccountListView();
            AdminForm.Enabled = true;
            Close();
        }
    }
}
