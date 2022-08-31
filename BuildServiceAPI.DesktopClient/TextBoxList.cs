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
    public delegate void TextBoxListItemDelegate(TextBoxList list, TextBoxListItem item);
    public delegate void TextBoxListDelegate(TextBoxList list);
    public partial class TextBoxList : UserControl
    {
        public TextBoxList()
        {
            InitializeComponent();
            if (MinimumItems < Items.Count)
            {
                AddItem(new TextBoxListItem());
            }
        }
        public int MinimumItems = 1;
        public List<TextBoxListItem> Items = new List<TextBoxListItem>();

        public event TextBoxListItemDelegate ItemAdd;
        public event TextBoxListDelegate ItemRemove;
        public delegate void TextBoxListItemChangedDelegate(object sender, TextBoxListItem textBoxListItem);
        public new event TextBoxListItemChangedDelegate TextChanged;
        public void OnItemAdd(TextBoxListItem item)
        {
            ItemAdd?.Invoke(this, item);
        }
        public void OnItemRemove()
        {
            ItemRemove?.Invoke(this);
        }

        public void AddItem(TextBoxListItem item)
        {
            Items.Add(item);
            flowLayoutPanel.Controls.Add(item);
            OnItemAdd(item);
            item.TextChanged += Item_TextChanged;
            item.AddButtonClick += Item_AddButtonClick;
            item.RemoveButtonClick += Item_RemoveButtonClick;
            item.Dock = DockStyle.Top;
            item.AutoSize = true;
            item.AutoSizeMode = AutoSizeMode.GrowOnly;
        }

        private void Item_TextChanged(object sender, TextBoxListItem e) => TextChanged?.Invoke(sender, e);
        private void Item_AddButtonClick(object sender, TextBoxListItem e)
        {
            AddItem(new TextBoxListItem());
        }
        private void Item_RemoveButtonClick(object sender, TextBoxListItem e)
        {
            int index = Items.IndexOf(e);
            if (index >= 0)
                RemoveItem(e);
        }

        public bool RemoveItem(TextBoxListItem item)
        {
            bool success = Items.Remove(item);
            if (success)
            {
                flowLayoutPanel.Controls.Remove(item);
                item.TextChanged -= Item_TextChanged;
                item.AddButtonClick -= Item_AddButtonClick;
                item.RemoveButtonClick -= Item_RemoveButtonClick;
                OnItemRemove();
            }
            return success;
        }
    }
}
