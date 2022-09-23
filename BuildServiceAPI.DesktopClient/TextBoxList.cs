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
            AddItem(new TextBoxListItem());
        }
        public int MinimumItems = 1;
        public int MaximumItems = int.MaxValue;
        public List<TextBoxListItem> Items = new List<TextBoxListItem>();

        public event TextBoxListItemDelegate ItemAdd;
        public event TextBoxListDelegate ItemRemove;
        public delegate void TextBoxListItemChangedDelegate(object sender, TextBoxListItem textBoxListItem);
        public new event TextBoxListItemChangedDelegate TextChanged;
        public void OnItemAdd(TextBoxListItem item)
        {
            ItemAdd?.Invoke(this, item);
            UpdateButtonState();
        }
        public void OnItemRemove()
        {
            ItemRemove?.Invoke(this);
            UpdateButtonState();
        }

        public void AddItem(TextBoxListItem item)
        {
            item.Dock = DockStyle.Top;
            item.AutoSize = true;
            item.AutoSizeMode = AutoSizeMode.GrowOnly;
            item.Visible = true;
            item.Width = flowLayoutPanel.Size.Width;
            item.TextChanged += Item_TextChanged;
            item.AddButtonClick += Item_AddButtonClick;
            item.RemoveButtonClick += Item_RemoveButtonClick;
            Items.Add(item);
            flowLayoutPanel.Controls.Add(item);
            OnItemAdd(item);
        }
        public void AddItem(string content) => AddItem(new TextBoxListItem()
        {
            Text = content
        });

        private void UpdateButtonState()
        {
            foreach (var item in Items)
            {
                item.ButtonAddEnable = Items.Count < MaximumItems;
                item.ButtonRemoveEnable = Items.Count > MinimumItems;
                item.Width = flowLayoutPanel.Size.Width;
            }
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
        public void RemoveAllItems()
        {
            foreach (var item in Items.ToArray())
            {
                RemoveItem(item);
            }
            for (int i = 0; i < MinimumItems; i++)
            {
                AddItem(new TextBoxListItem());
            }
        }
    }
}
