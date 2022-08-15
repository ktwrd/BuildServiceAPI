using kate.shared.Helpers;
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

namespace BuildServiceAPI.DesktopClient
{
    public partial class TextBoxListItem : UserControl
    {
        public TextBoxListItem()
        {
            InitializeComponent();

            richTextBox1.KeyDown += RichTextBox1_KeyDown;
            richTextBox1.KeyPress += RichTextBox1_KeyPress;
            richTextBox1.KeyUp += RichTextBox1_KeyUp;
            buttonAdd.Click += ButtonAdd_Click;
            buttonRemove.Click += ButtonRemove_Click;
            richTextBox1.TextChanged += RichTextBox1_TextChanged;
            richTextBox1.Enter += RichTextBox1_Enter;
            richTextBox1.GotFocus += RichTextBox1_GotFocus;
        }

        private void RichTextBox1_GotFocus(object sender, EventArgs e) => TextBoxGotFocus?.Invoke(sender, e);

        private void RichTextBox1_Enter(object sender, EventArgs e) => TextBoxEnter?.Invoke(sender, e);

        private void RichTextBox1_TextChanged(object sender, EventArgs e) => TextChanged?.Invoke(sender, e);

        private void ButtonRemove_Click(object sender, EventArgs e) => RemoveButtonClick?.Invoke();

        private void ButtonAdd_Click(object sender, EventArgs e) => AddButtonClick?.Invoke();

        private void RichTextBox1_KeyUp(object sender, KeyEventArgs e) => TextBoxKeyUp?.Invoke(sender, e);

        private void RichTextBox1_KeyPress(object sender, KeyPressEventArgs e) => TextBoxKeyPress?.Invoke(sender, e);

        private void RichTextBox1_KeyDown(object sender, KeyEventArgs e) => TextBoxKeyDown?.Invoke(sender, e);

        public object ObjectData = null;

        public bool ButtonAddEnable
        {
            get { return buttonAdd.Enabled; }
            set { buttonAdd.Enabled = value; }
        }
        public bool ButtonRemoveEnable
        {
            get { return buttonRemove.Enabled; }
            set { buttonRemove.Enabled = value; }
        }

        [Localizable(true)]
        [RefreshProperties(RefreshProperties.All)]
        public new string Text
        {
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }
        [Category("CatAppearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MergableProperty(false)]
        [Localizable(true)]
        [Description("TextBoxLinesDescr")]
        [Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string[] Lines
        {
            get { return richTextBox1.Lines; }
            set { richTextBox1.Lines = value; }
        }
        private bool _readOnly = false;
        [Category("CatBehavior")]
        [DefaultValue(false)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("TextBoxReadOnlyDescr")]
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                richTextBox1.ReadOnly = true;
            }
        }

        public event KeyEventHandler TextBoxKeyDown;
        public event KeyPressEventHandler TextBoxKeyPress;
        public event KeyEventHandler TextBoxKeyUp;

        public event VoidDelegate AddButtonClick;
        public event VoidDelegate RemoveButtonClick;

        public new event EventHandler TextChanged;
        public event EventHandler TextBoxEnter;
        public event EventHandler TextBoxGotFocus;
    }
}
