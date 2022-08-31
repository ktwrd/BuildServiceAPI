using BuildServiceCommon;
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
    public partial class AnnouncementEditModal : Form
    {
        public SystemAnnouncementEntry AnnouncementEntry;
        public AdminForm AdminForm;
        public AnnouncementEditModal(SystemAnnouncementEntry entry)
        {
            InitializeComponent();
            AnnouncementEntry = entry;

            textBoxMessage.Lines = entry.Message.Split(new string[] { "\n" }, StringSplitOptions.None);
            checkBoxActive.Checked = entry.Active;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            AnnouncementEntry.Active = checkBoxActive.Checked;
            AnnouncementEntry.Message = string.Join("\n", textBoxMessage.Lines);

            AdminForm.SetAnnouncementContent(AnnouncementEntry);
            Close();
        }
    }
}
