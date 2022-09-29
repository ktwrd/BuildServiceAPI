using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildServiceAPI.DesktopClient
{
    public partial class AccountGroupPowertool : Form
    {
        public AccountGroupPowertool()
        {
            InitializeComponent();
        }

        public AdminForm AdminForm;
        public void Init(AdminForm adminForm)
        {
            AdminForm = adminForm;

            checkedListBoxAccountRemove.Items.Clear();
            checkedListBoxAccountJoin.Items.Clear();
            foreach (var account in Program.LocalContent.AccountListing)
            {
                checkedListBoxAccountRemove.Items.Add(account.Username);
                checkedListBoxAccountJoin.Items.Add(account.Username);
            }
        }

        public string[] SelectedUsernamesToRemove = Array.Empty<string>();
        public string[] SelectedUsernamesToJoin = Array.Empty<string>();

        public string[] SelectedGroupsToRemove
        {
            get
            {
                return textBoxListAccountRemove.Items.Select(v => v.Text).Where(v => v.Length > 0).ToArray();
            }
        }
        public string[] SelectedGroupsToJoin
        {
            get
            {
                return textBoxListAccountJoin.Items.Select(v => v.Text).Where(v => v.Length > 0).ToArray();
            }
        }

        public void AutoselectItems(string[] usernames, bool type_remove=false, bool type_join = true)
        {
            if (type_remove)
            {
                SelectedUsernamesToRemove = usernames;
                for (int i = 0; i < checkedListBoxAccountRemove.Items.Count; i++)
                {
                    checkedListBoxAccountRemove.SetItemChecked(i, SelectedUsernamesToRemove.Contains(checkedListBoxAccountRemove.Items[i].ToString()));
                }
            }
            if (type_join)
            {
                SelectedUsernamesToJoin = usernames;
                for (int i = 0; i < checkedListBoxAccountJoin.Items.Count; i++)
                {
                    checkedListBoxAccountJoin.SetItemChecked(i, SelectedUsernamesToJoin.Contains(checkedListBoxAccountJoin.Items[i].ToString()));
                }
            }
        }

        public string openTextFileDialog()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Text files(*.txt)|*.txt",
                Title = "Open text file"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return "";
        }
        public string openCSVFileDialog()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "CSV File(*.csv)|*.csv",
                Title = "Open CSV file"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return "";
        }

        #region Import Group Button
        private void buttonAccountRemove_GroupsFromTextfile_Click(object sender, EventArgs e)
        {
            var fileLocation = openTextFileDialog();
            if (fileLocation.Length < 1) return;

            var lines = File.ReadAllLines(fileLocation);
            textBoxListAccountRemove.RemoveAllItems();
            foreach (var l in lines)
                if (l.Length > 0)
                    textBoxListAccountRemove.AddItem(l);
        }

        private void buttonAccountJoin_GroupsFromTextfile_Click(object sender, EventArgs e)
        {
            var fileLocation = openTextFileDialog();
            if (fileLocation.Length < 1) return;

            var lines = File.ReadAllLines(fileLocation);
            textBoxListAccountJoin.RemoveAllItems();
            foreach (var l in lines)
                if (l.Length > 0)
                    textBoxListAccountJoin.AddItem(l);
        }
        #endregion

        public class AccountGroupState
        {
            public string Username { get; set; } = "";
            public string[] GroupJoin { get; set; }
            public string[] GroupRemove { get; set; }
        }
        public List<AccountGroupState> ParseCSVToDictionary(string[] lines)
        {
            var list = new List<AccountGroupState>();

            for (int i = 1; i < lines.Length; i++)
            {
                var splitted = lines[i].Split(',');
                var instance = new AccountGroupState()
                {
                    Username = splitted[0],
                    GroupJoin = splitted[0].Split(' '),
                    GroupRemove = splitted[0].Split(' ')
                };
                list.Add(instance);
            }

            return list;
        }
        public string[] GroupStateListToCSV(List<AccountGroupState> list)
        {
            var lines = new List<string>();
            lines.Add(@"Username,Groups to Join (space seperated),Groups to Remove from (space seperated)");
            foreach (var item in list)
            {
                lines.Add($"{item.Username},{string.Join(" ", item.GroupJoin)},{string.Join(" ", item.GroupRemove)}");
            }
            return lines.ToArray();
        }

        private void buttonPush_Click(object sender, EventArgs e)
        {
            SelectedUsernamesToRemove = checkedListBoxAccountRemove.Items.Cast<string>().ToArray();
            SelectedUsernamesToJoin = checkedListBoxAccountJoin.Items.Cast<string>().ToArray();

            var requestDictionary = new Dictionary<string, string[]>();
            foreach (var account in Program.LocalContent.AccountListing)
            {
                var newGroups = new List<string>(account.Groups);
                if (SelectedUsernamesToRemove.Contains(account.Username))
                {
                    newGroups = newGroups.Where(v => !SelectedGroupsToRemove.Contains(v)).ToList();
                }
                if (SelectedUsernamesToJoin.Contains(account.Username))
                {
                    newGroups = newGroups.Concat(SelectedGroupsToJoin).Distinct().ToList();
                }
                requestDictionary.Add(account.Username, newGroups.ToArray());
            }

            var objectContent = new ObjectResponse<Dictionary<string, string[]>>()
            {
                Success = true,
                Data = requestDictionary
            };

            var request = Program.AuthClient.HttpClient.PostAsync(Endpoint.UserGroupSet(Program.AuthClient.Token), new StringContent(JsonSerializer.Serialize(objectContent, Program.serializerOptions))).Result;
            var stringResponse = request.Content.ReadAsStringAsync().Result;
            var dynamicResponse = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(stringResponse, Program.serializerOptions);
            if (!dynamicResponse.Success)
            {
                var exceptionContent = JsonSerializer.Deserialize<ObjectResponse<HttpException>>(stringResponse, Program.serializerOptions);
                MessageBox.Show($"({exceptionContent.Data.Code}) {exceptionContent.Data.Message}\n{exceptionContent.Data.Exception}", $"Failed to set groups");
            }

            Close();
            Program.LocalContent.PullAccounts();
        }

        private void buttonAccountRemoveSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxAccountRemove.Items.Count; i++)
            {
                checkedListBoxAccountRemove.SetItemChecked(i, true);
            }
        }

        private void buttonAccountAddSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxAccountJoin.Items.Count; i++)
            {
                checkedListBoxAccountJoin.SetItemChecked(i, true);
            }
        }

        private void buttonAccountRemoveSelectInverse_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxAccountRemove.Items.Count; i++)
            {
                checkedListBoxAccountRemove.SetItemChecked(i, !checkedListBoxAccountRemove.GetItemChecked(i));
            }
        }

        private void buttonAccountAddSelectInverse_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxAccountJoin.Items.Count; i++)
            {
                checkedListBoxAccountJoin.SetItemChecked(i, !checkedListBoxAccountJoin.GetItemChecked(i));
            }
        }
    }
}
