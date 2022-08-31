namespace BuildServiceAPI.DesktopClient
{
    partial class AccountPermissionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBoxPermissions = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonPush = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBoxPermissions
            // 
            this.checkedListBoxPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxPermissions.FormattingEnabled = true;
            this.checkedListBoxPermissions.Location = new System.Drawing.Point(3, 3);
            this.checkedListBoxPermissions.Name = "checkedListBoxPermissions";
            this.checkedListBoxPermissions.Size = new System.Drawing.Size(258, 145);
            this.checkedListBoxPermissions.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.checkedListBoxPermissions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonPush, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(264, 201);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // buttonPush
            // 
            this.buttonPush.Location = new System.Drawing.Point(3, 154);
            this.buttonPush.Name = "buttonPush";
            this.buttonPush.Size = new System.Drawing.Size(75, 23);
            this.buttonPush.TabIndex = 2;
            this.buttonPush.Text = "Push";
            this.buttonPush.UseVisualStyleBackColor = true;
            this.buttonPush.Click += new System.EventHandler(this.buttonPush_Click);
            // 
            // AccountPermissionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 201);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(280, 200);
            this.Name = "AccountPermissionForm";
            this.Text = "Edit Account Permissions";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxPermissions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonPush;
    }
}