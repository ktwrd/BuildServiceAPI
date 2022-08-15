namespace BuildServiceAPI.DesktopClient
{
    partial class ReleaseEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReleaseEditForm));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxLabelName = new BuildServiceAPI.DesktopClient.TextBoxLabel();
            this.textBoxLabelProductName = new BuildServiceAPI.DesktopClient.TextBoxLabel();
            this.textBoxLabelAppID = new BuildServiceAPI.DesktopClient.TextBoxLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxLabelVersion = new BuildServiceAPI.DesktopClient.TextBoxLabel();
            this.textBoxLabelCommitHash = new BuildServiceAPI.DesktopClient.TextBoxLabel();
            this.comboBoxLabelReleaseType = new BuildServiceAPI.DesktopClient.ComboBoxLabel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(794, 404);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.flowLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 154);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Information";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.textBoxLabelName);
            this.flowLayoutPanel2.Controls.Add(this.textBoxLabelProductName);
            this.flowLayoutPanel2.Controls.Add(this.textBoxLabelAppID);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(156, 135);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // textBoxLabelName
            // 
            this.textBoxLabelName.AutoSize = true;
            this.textBoxLabelName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textBoxLabelName.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLabelName.LabelText = "Name";
            this.textBoxLabelName.Location = new System.Drawing.Point(3, 3);
            this.textBoxLabelName.MinimumSize = new System.Drawing.Size(150, 39);
            this.textBoxLabelName.MultiLine = true;
            this.textBoxLabelName.Name = "textBoxLabelName";
            this.textBoxLabelName.ReadOnly = false;
            this.textBoxLabelName.Size = new System.Drawing.Size(150, 39);
            this.textBoxLabelName.TabIndex = 2;
            this.textBoxLabelName.TextboxContent = "";
            this.textBoxLabelName.TextboxLines = new string[0];
            // 
            // textBoxLabelProductName
            // 
            this.textBoxLabelProductName.AutoSize = true;
            this.textBoxLabelProductName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textBoxLabelProductName.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLabelProductName.LabelText = "ProductName";
            this.textBoxLabelProductName.Location = new System.Drawing.Point(3, 48);
            this.textBoxLabelProductName.MinimumSize = new System.Drawing.Size(150, 39);
            this.textBoxLabelProductName.MultiLine = true;
            this.textBoxLabelProductName.Name = "textBoxLabelProductName";
            this.textBoxLabelProductName.ReadOnly = true;
            this.textBoxLabelProductName.Size = new System.Drawing.Size(150, 39);
            this.textBoxLabelProductName.TabIndex = 3;
            this.textBoxLabelProductName.TextboxContent = "";
            this.textBoxLabelProductName.TextboxLines = new string[0];
            // 
            // textBoxLabelAppID
            // 
            this.textBoxLabelAppID.AutoSize = true;
            this.textBoxLabelAppID.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textBoxLabelAppID.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLabelAppID.LabelText = "App ID";
            this.textBoxLabelAppID.Location = new System.Drawing.Point(3, 93);
            this.textBoxLabelAppID.MinimumSize = new System.Drawing.Size(150, 39);
            this.textBoxLabelAppID.MultiLine = true;
            this.textBoxLabelAppID.Name = "textBoxLabelAppID";
            this.textBoxLabelAppID.ReadOnly = true;
            this.textBoxLabelAppID.Size = new System.Drawing.Size(150, 39);
            this.textBoxLabelAppID.TabIndex = 2;
            this.textBoxLabelAppID.TextboxContent = "";
            this.textBoxLabelAppID.TextboxLines = new string[0];
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.flowLayoutPanel4);
            this.groupBox2.Location = new System.Drawing.Point(171, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(162, 154);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Version Details";
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.Controls.Add(this.textBoxLabelVersion);
            this.flowLayoutPanel4.Controls.Add(this.textBoxLabelCommitHash);
            this.flowLayoutPanel4.Controls.Add(this.comboBoxLabelReleaseType);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(156, 135);
            this.flowLayoutPanel4.TabIndex = 0;
            // 
            // textBoxLabelVersion
            // 
            this.textBoxLabelVersion.AutoSize = true;
            this.textBoxLabelVersion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textBoxLabelVersion.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLabelVersion.LabelText = "Version";
            this.textBoxLabelVersion.Location = new System.Drawing.Point(3, 3);
            this.textBoxLabelVersion.MinimumSize = new System.Drawing.Size(150, 39);
            this.textBoxLabelVersion.MultiLine = true;
            this.textBoxLabelVersion.Name = "textBoxLabelVersion";
            this.textBoxLabelVersion.ReadOnly = false;
            this.textBoxLabelVersion.Size = new System.Drawing.Size(150, 39);
            this.textBoxLabelVersion.TabIndex = 2;
            this.textBoxLabelVersion.TextboxContent = "";
            this.textBoxLabelVersion.TextboxLines = new string[0];
            // 
            // textBoxLabelCommitHash
            // 
            this.textBoxLabelCommitHash.AutoSize = true;
            this.textBoxLabelCommitHash.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textBoxLabelCommitHash.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLabelCommitHash.LabelText = "Commit Hash";
            this.textBoxLabelCommitHash.Location = new System.Drawing.Point(3, 48);
            this.textBoxLabelCommitHash.MinimumSize = new System.Drawing.Size(150, 39);
            this.textBoxLabelCommitHash.MultiLine = true;
            this.textBoxLabelCommitHash.Name = "textBoxLabelCommitHash";
            this.textBoxLabelCommitHash.ReadOnly = false;
            this.textBoxLabelCommitHash.Size = new System.Drawing.Size(150, 39);
            this.textBoxLabelCommitHash.TabIndex = 3;
            this.textBoxLabelCommitHash.TextboxContent = "";
            this.textBoxLabelCommitHash.TextboxLines = new string[0];
            // 
            // comboBoxLabelReleaseType
            // 
            this.comboBoxLabelReleaseType.AutoSize = true;
            this.comboBoxLabelReleaseType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.comboBoxLabelReleaseType.DataSource = null;
            this.comboBoxLabelReleaseType.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxLabelReleaseType.LabelText = "Release Type";
            this.comboBoxLabelReleaseType.Location = new System.Drawing.Point(3, 93);
            this.comboBoxLabelReleaseType.MaxDropDownItems = 8;
            this.comboBoxLabelReleaseType.MinimumSize = new System.Drawing.Size(150, 39);
            this.comboBoxLabelReleaseType.Name = "comboBoxLabelReleaseType";
            this.comboBoxLabelReleaseType.ReadOnly = true;
            this.comboBoxLabelReleaseType.SelectedIndex = -1;
            this.comboBoxLabelReleaseType.SelectedItem = null;
            this.comboBoxLabelReleaseType.SelectedText = "";
            this.comboBoxLabelReleaseType.SelectionLength = 0;
            this.comboBoxLabelReleaseType.Size = new System.Drawing.Size(150, 39);
            this.comboBoxLabelReleaseType.TabIndex = 4;
            // 
            // buttonSave
            // 
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(3, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.buttonSave);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 413);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(794, 34);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // ReleaseEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ReleaseEditForm";
            this.Text = "Edit Release Info";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private TextBoxLabel textBoxLabelVersion;
        private TextBoxLabel textBoxLabelName;
        private TextBoxLabel textBoxLabelProductName;
        private TextBoxLabel textBoxLabelAppID;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private TextBoxLabel textBoxLabelCommitHash;
        private ComboBoxLabel comboBoxLabelReleaseType;
    }
}