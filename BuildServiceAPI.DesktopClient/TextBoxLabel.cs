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
    public partial class TextBoxLabel : UserControl
    {
        public TextBoxLabel()
        {
            InitializeComponent();
            
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

        public bool MultiLine
        {
            get { return richTextBox1.Multiline; }
            set { richTextBox1.Multiline = value; }
        }
        public string TextboxContent
        {
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }
        public string[] TextboxLines
        {
            get { return richTextBox1.Lines; }
            set { richTextBox1.Lines = value; }
        }
        public bool ReadOnly
        {
            get { return richTextBox1.ReadOnly; }
            set { richTextBox1.ReadOnly = value; }
        }

        public event EventHandler HideSelectionChanged
        {
            add { richTextBox1.HideSelectionChanged += value; }
            remove { richTextBox1.HideSelectionChanged -= value; }
        }
        public event EventHandler ModifiedChanged
        {
            add { richTextBox1.ModifiedChanged += value; }
            remove { richTextBox1.ModifiedChanged -= value; }
        }
        public event EventHandler MultilineChanged
        {
            add { richTextBox1.MultilineChanged += value; }
            remove { richTextBox1.MultilineChanged -= value; }
        }
        public event EventHandler ReadOnlyChanged
        {
            add { richTextBox1.ReadOnlyChanged += value; }
            remove { richTextBox1.ReadOnlyChanged -= value; }
        }
    }
}
