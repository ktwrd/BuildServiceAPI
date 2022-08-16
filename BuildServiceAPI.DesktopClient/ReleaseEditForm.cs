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
        public ReleaseEditForm(ReleaseInfo releaseInfo)
        {
            InitializeComponent();
            this.ReleaseInfo = releaseInfo;
            comboBoxLabelReleaseType.Items.Add(ReleaseType.Nightly);
            comboBoxLabelReleaseType.Items.Add(ReleaseType.Beta);
            comboBoxLabelReleaseType.Items.Add(ReleaseType.Stable);
            comboBoxLabelReleaseType.Items.Add(ReleaseType.Other);

            Text = $"Release Edit Form - {ReleaseInfo.commitHash}";
        }
    }
}
