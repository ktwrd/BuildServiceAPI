using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ComboBox;

namespace BuildServiceAPI.DesktopClient
{
    public partial class ComboBoxLabel : UserControl
    {
        public ComboBoxLabel()
        {
            InitializeComponent();
            comboBox.DropDown += comboBox_DropDown;
            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            comboBox.SelectionChangeCommitted += comboBox_SelectionChangeCommitted;
            comboBox.DropDownClosed += comboBox_DropDownClosed;
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            DropDownClosed?.Invoke(sender, e);
        }

        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SelectionChangeCommitted?.Invoke(sender, e);
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(sender, e);
        }

        private void comboBox_DropDown(object sender, EventArgs e)
        {
            DropDown?.Invoke(sender, e);
        }

        public string LabelText
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public Font LabelFont
        {
            get { return label1.Font; }
            set { label1.Font = value; }
        }

        
        public bool ReadOnly
        {
            get { return comboBox.Enabled; }
            set { comboBox.Enabled = value; }
        }
        [Category("CatData")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Description("ComboBoxItemsDescr")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        public ObjectCollection Items => comboBox.Items;
        public int MaxDropDownItems
        {
            get { return comboBox.MaxDropDownItems; }
            set { comboBox.MaxDropDownItems = value; }
        }
        public int SelectedIndex
        {
            get { return comboBox.SelectedIndex; }
            set { comboBox.SelectedIndex = value;  }
        }
        public object SelectedItem
        {
            get { return comboBox.SelectedItem; }
            set { comboBox.SelectedItem = value; }
        }
        public string SelectedText
        {
            get { return comboBox.SelectedText; }
            set { comboBox.SelectedText = value; }
        }
        public int SelectionLength
        {
            get { return comboBox.SelectionLength; }
            set { comboBox.SelectionLength = value; }
        }
        public object DataSource
        {
            get { return comboBox.DataSource; }
            set { comboBox.DataSource = value; }
        }

        public event EventHandler DropDown;
        public event EventHandler SelectedIndexChanged;
        public event EventHandler SelectionChangeCommitted;
        public event EventHandler DropDownClosed;
    }
}
