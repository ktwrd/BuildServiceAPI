namespace BuildServiceAPI.DesktopClient
{
    partial class AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAccountMan = new System.Windows.Forms.TabPage();
            this.listViewAccount = new System.Windows.Forms.ListView();
            this.columnHeaderUsername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderGroups = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripAccountMan = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonUserModify = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAccountBlockAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAccountBlockEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAccountBlockDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAccountPermission = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAccountGroupMan = new System.Windows.Forms.ToolStripButton();
            this.tabPageAnnouncementManagement = new System.Windows.Forms.TabPage();
            this.listViewAnnouncement = new System.Windows.Forms.ListView();
            this.columnHeaderContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEnable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripAnnouncement = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAnnouncementAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAnnouncementEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAnnouncementDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAnnouncementRefresh = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonConnectionTokenFetch = new System.Windows.Forms.Button();
            this.textBoxLabelUsername = new BuildServiceAPI.DesktopClient.TextBoxLabel();
            this.textBoxLabelPassword = new BuildServiceAPI.DesktopClient.TextBoxLabel();
            this.textBoxLabelEndpoint = new BuildServiceAPI.DesktopClient.TextBoxLabel();
            this.buttonPushAll = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.toolStripButtonAnnouncementPushChanges = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPageAccountMan.SuspendLayout();
            this.toolStripAccountMan.SuspendLayout();
            this.tabPageAnnouncementManagement.SuspendLayout();
            this.toolStripAnnouncement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageAccountMan);
            this.tabControl1.Controls.Add(this.tabPageAnnouncementManagement);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(545, 441);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageAccountMan
            // 
            this.tabPageAccountMan.Controls.Add(this.listViewAccount);
            this.tabPageAccountMan.Controls.Add(this.toolStripAccountMan);
            this.tabPageAccountMan.Location = new System.Drawing.Point(4, 22);
            this.tabPageAccountMan.Name = "tabPageAccountMan";
            this.tabPageAccountMan.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAccountMan.Size = new System.Drawing.Size(537, 415);
            this.tabPageAccountMan.TabIndex = 0;
            this.tabPageAccountMan.Text = "Account Management";
            this.tabPageAccountMan.UseVisualStyleBackColor = true;
            // 
            // listViewAccount
            // 
            this.listViewAccount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderUsername,
            this.columnHeaderEnabled,
            this.columnHeaderGroups});
            this.listViewAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAccount.HideSelection = false;
            this.listViewAccount.Location = new System.Drawing.Point(3, 28);
            this.listViewAccount.Name = "listViewAccount";
            this.listViewAccount.Size = new System.Drawing.Size(531, 384);
            this.listViewAccount.TabIndex = 1;
            this.listViewAccount.UseCompatibleStateImageBehavior = false;
            this.listViewAccount.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderUsername
            // 
            this.columnHeaderUsername.Text = "Username";
            this.columnHeaderUsername.Width = 120;
            // 
            // columnHeaderEnabled
            // 
            this.columnHeaderEnabled.Text = "Enabled";
            this.columnHeaderEnabled.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderEnabled.Width = 80;
            // 
            // columnHeaderGroups
            // 
            this.columnHeaderGroups.Text = "Groups";
            this.columnHeaderGroups.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderGroups.Width = 200;
            // 
            // toolStripAccountMan
            // 
            this.toolStripAccountMan.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonUserModify,
            this.toolStripSeparator2,
            this.toolStripButtonAccountBlockAdd,
            this.toolStripButtonAccountBlockEdit,
            this.toolStripButtonAccountBlockDel,
            this.toolStripSeparator3,
            this.toolStripButtonAccountPermission,
            this.toolStripButtonAccountGroupMan});
            this.toolStripAccountMan.Location = new System.Drawing.Point(3, 3);
            this.toolStripAccountMan.Name = "toolStripAccountMan";
            this.toolStripAccountMan.Size = new System.Drawing.Size(531, 25);
            this.toolStripAccountMan.TabIndex = 0;
            this.toolStripAccountMan.Text = "Account Management";
            // 
            // toolStripButtonUserModify
            // 
            this.toolStripButtonUserModify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUserModify.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUserModify.Image")));
            this.toolStripButtonUserModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUserModify.Name = "toolStripButtonUserModify";
            this.toolStripButtonUserModify.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUserModify.Text = "Modify";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAccountBlockAdd
            // 
            this.toolStripButtonAccountBlockAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAccountBlockAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAccountBlockAdd.Image")));
            this.toolStripButtonAccountBlockAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAccountBlockAdd.Name = "toolStripButtonAccountBlockAdd";
            this.toolStripButtonAccountBlockAdd.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAccountBlockAdd.Text = "Add Block Reason";
            // 
            // toolStripButtonAccountBlockEdit
            // 
            this.toolStripButtonAccountBlockEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAccountBlockEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAccountBlockEdit.Image")));
            this.toolStripButtonAccountBlockEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAccountBlockEdit.Name = "toolStripButtonAccountBlockEdit";
            this.toolStripButtonAccountBlockEdit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAccountBlockEdit.Text = "Edit Block Reason(s)";
            // 
            // toolStripButtonAccountBlockDel
            // 
            this.toolStripButtonAccountBlockDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAccountBlockDel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAccountBlockDel.Image")));
            this.toolStripButtonAccountBlockDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAccountBlockDel.Name = "toolStripButtonAccountBlockDel";
            this.toolStripButtonAccountBlockDel.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAccountBlockDel.Text = "Remove Account Blocks";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAccountPermission
            // 
            this.toolStripButtonAccountPermission.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAccountPermission.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAccountPermission.Image")));
            this.toolStripButtonAccountPermission.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAccountPermission.Name = "toolStripButtonAccountPermission";
            this.toolStripButtonAccountPermission.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAccountPermission.Text = "Permissions";
            // 
            // toolStripButtonAccountGroupMan
            // 
            this.toolStripButtonAccountGroupMan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAccountGroupMan.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAccountGroupMan.Image")));
            this.toolStripButtonAccountGroupMan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAccountGroupMan.Name = "toolStripButtonAccountGroupMan";
            this.toolStripButtonAccountGroupMan.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAccountGroupMan.Text = "Group Management";
            // 
            // tabPageAnnouncementManagement
            // 
            this.tabPageAnnouncementManagement.Controls.Add(this.listViewAnnouncement);
            this.tabPageAnnouncementManagement.Controls.Add(this.toolStripAnnouncement);
            this.tabPageAnnouncementManagement.Location = new System.Drawing.Point(4, 22);
            this.tabPageAnnouncementManagement.Name = "tabPageAnnouncementManagement";
            this.tabPageAnnouncementManagement.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAnnouncementManagement.Size = new System.Drawing.Size(537, 415);
            this.tabPageAnnouncementManagement.TabIndex = 1;
            this.tabPageAnnouncementManagement.Text = "Announcements";
            this.tabPageAnnouncementManagement.UseVisualStyleBackColor = true;
            // 
            // listViewAnnouncement
            // 
            this.listViewAnnouncement.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderContent,
            this.columnHeaderEnable,
            this.columnHeaderTimestamp});
            this.listViewAnnouncement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAnnouncement.HideSelection = false;
            this.listViewAnnouncement.Location = new System.Drawing.Point(3, 28);
            this.listViewAnnouncement.Name = "listViewAnnouncement";
            this.listViewAnnouncement.Size = new System.Drawing.Size(531, 384);
            this.listViewAnnouncement.TabIndex = 1;
            this.listViewAnnouncement.UseCompatibleStateImageBehavior = false;
            this.listViewAnnouncement.View = System.Windows.Forms.View.Details;
            this.listViewAnnouncement.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewAnnouncement_ItemSelectionChanged);
            this.listViewAnnouncement.SelectedIndexChanged += new System.EventHandler(this.listViewAnnouncement_SelectedIndexChanged);
            // 
            // columnHeaderContent
            // 
            this.columnHeaderContent.Text = "Message";
            this.columnHeaderContent.Width = 200;
            // 
            // columnHeaderEnable
            // 
            this.columnHeaderEnable.Text = "Enabled";
            this.columnHeaderEnable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderEnable.Width = 100;
            // 
            // columnHeaderTimestamp
            // 
            this.columnHeaderTimestamp.Text = "Timestamp";
            this.columnHeaderTimestamp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderTimestamp.Width = 130;
            // 
            // toolStripAnnouncement
            // 
            this.toolStripAnnouncement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAnnouncementAdd,
            this.toolStripButtonAnnouncementEdit,
            this.toolStripButtonAnnouncementDelete,
            this.toolStripSeparator1,
            this.toolStripButtonAnnouncementRefresh,
            this.toolStripButtonAnnouncementPushChanges});
            this.toolStripAnnouncement.Location = new System.Drawing.Point(3, 3);
            this.toolStripAnnouncement.Name = "toolStripAnnouncement";
            this.toolStripAnnouncement.Size = new System.Drawing.Size(531, 25);
            this.toolStripAnnouncement.TabIndex = 0;
            this.toolStripAnnouncement.Text = "Announcement Management";
            // 
            // toolStripButtonAnnouncementAdd
            // 
            this.toolStripButtonAnnouncementAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAnnouncementAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAnnouncementAdd.Image")));
            this.toolStripButtonAnnouncementAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAnnouncementAdd.Name = "toolStripButtonAnnouncementAdd";
            this.toolStripButtonAnnouncementAdd.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAnnouncementAdd.Text = "Create";
            // 
            // toolStripButtonAnnouncementEdit
            // 
            this.toolStripButtonAnnouncementEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAnnouncementEdit.Enabled = false;
            this.toolStripButtonAnnouncementEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAnnouncementEdit.Image")));
            this.toolStripButtonAnnouncementEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAnnouncementEdit.Name = "toolStripButtonAnnouncementEdit";
            this.toolStripButtonAnnouncementEdit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAnnouncementEdit.Text = "Edit";
            // 
            // toolStripButtonAnnouncementDelete
            // 
            this.toolStripButtonAnnouncementDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAnnouncementDelete.Enabled = false;
            this.toolStripButtonAnnouncementDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAnnouncementDelete.Image")));
            this.toolStripButtonAnnouncementDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAnnouncementDelete.Name = "toolStripButtonAnnouncementDelete";
            this.toolStripButtonAnnouncementDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAnnouncementDelete.Text = "Delete";
            this.toolStripButtonAnnouncementDelete.Click += new System.EventHandler(this.toolStripButtonAnnouncementDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAnnouncementRefresh
            // 
            this.toolStripButtonAnnouncementRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAnnouncementRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAnnouncementRefresh.Image")));
            this.toolStripButtonAnnouncementRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAnnouncementRefresh.Name = "toolStripButtonAnnouncementRefresh";
            this.toolStripButtonAnnouncementRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAnnouncementRefresh.Text = "Refresh";
            this.toolStripButtonAnnouncementRefresh.Click += new System.EventHandler(this.toolStripButtonAnnouncementRefresh_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(704, 441);
            this.splitContainer1.SplitterDistance = 155;
            this.splitContainer1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.textBoxLabelUsername);
            this.flowLayoutPanel1.Controls.Add(this.textBoxLabelPassword);
            this.flowLayoutPanel1.Controls.Add(this.textBoxLabelEndpoint);
            this.flowLayoutPanel1.Controls.Add(this.buttonConnectionTokenFetch);
            this.flowLayoutPanel1.Controls.Add(this.buttonPushAll);
            this.flowLayoutPanel1.Controls.Add(this.buttonRefresh);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(155, 441);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // buttonConnectionTokenFetch
            // 
            this.buttonConnectionTokenFetch.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonConnectionTokenFetch.Image = ((System.Drawing.Image)(resources.GetObject("buttonConnectionTokenFetch.Image")));
            this.buttonConnectionTokenFetch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonConnectionTokenFetch.Location = new System.Drawing.Point(3, 138);
            this.buttonConnectionTokenFetch.Name = "buttonConnectionTokenFetch";
            this.buttonConnectionTokenFetch.Size = new System.Drawing.Size(150, 23);
            this.buttonConnectionTokenFetch.TabIndex = 3;
            this.buttonConnectionTokenFetch.Text = "Get Token";
            this.buttonConnectionTokenFetch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonConnectionTokenFetch.UseVisualStyleBackColor = true;
            this.buttonConnectionTokenFetch.Click += new System.EventHandler(this.buttonConnectionTokenFetch_Click);
            // 
            // textBoxLabelUsername
            // 
            this.textBoxLabelUsername.AutoSize = true;
            this.textBoxLabelUsername.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textBoxLabelUsername.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLabelUsername.LabelText = "Username";
            this.textBoxLabelUsername.Location = new System.Drawing.Point(3, 3);
            this.textBoxLabelUsername.MinimumSize = new System.Drawing.Size(150, 39);
            this.textBoxLabelUsername.MultiLine = true;
            this.textBoxLabelUsername.Name = "textBoxLabelUsername";
            this.textBoxLabelUsername.ReadOnly = false;
            this.textBoxLabelUsername.Size = new System.Drawing.Size(150, 39);
            this.textBoxLabelUsername.TabIndex = 0;
            this.textBoxLabelUsername.TextboxContent = "";
            this.textBoxLabelUsername.TextboxLines = new string[0];
            // 
            // textBoxLabelPassword
            // 
            this.textBoxLabelPassword.AutoSize = true;
            this.textBoxLabelPassword.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textBoxLabelPassword.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLabelPassword.LabelText = "Password";
            this.textBoxLabelPassword.Location = new System.Drawing.Point(3, 48);
            this.textBoxLabelPassword.MinimumSize = new System.Drawing.Size(150, 39);
            this.textBoxLabelPassword.MultiLine = true;
            this.textBoxLabelPassword.Name = "textBoxLabelPassword";
            this.textBoxLabelPassword.ReadOnly = false;
            this.textBoxLabelPassword.Size = new System.Drawing.Size(150, 39);
            this.textBoxLabelPassword.TabIndex = 1;
            this.textBoxLabelPassword.TextboxContent = "";
            this.textBoxLabelPassword.TextboxLines = new string[0];
            // 
            // textBoxLabelEndpoint
            // 
            this.textBoxLabelEndpoint.AutoSize = true;
            this.textBoxLabelEndpoint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textBoxLabelEndpoint.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLabelEndpoint.LabelText = "URL";
            this.textBoxLabelEndpoint.Location = new System.Drawing.Point(3, 93);
            this.textBoxLabelEndpoint.MinimumSize = new System.Drawing.Size(150, 39);
            this.textBoxLabelEndpoint.MultiLine = true;
            this.textBoxLabelEndpoint.Name = "textBoxLabelEndpoint";
            this.textBoxLabelEndpoint.ReadOnly = false;
            this.textBoxLabelEndpoint.Size = new System.Drawing.Size(150, 39);
            this.textBoxLabelEndpoint.TabIndex = 2;
            this.textBoxLabelEndpoint.TextboxContent = "";
            this.textBoxLabelEndpoint.TextboxLines = new string[0];
            // 
            // buttonPushAll
            // 
            this.buttonPushAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonPushAll.Image = ((System.Drawing.Image)(resources.GetObject("buttonPushAll.Image")));
            this.buttonPushAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPushAll.Location = new System.Drawing.Point(3, 167);
            this.buttonPushAll.Name = "buttonPushAll";
            this.buttonPushAll.Size = new System.Drawing.Size(150, 23);
            this.buttonPushAll.TabIndex = 4;
            this.buttonPushAll.Text = "Push Changes";
            this.buttonPushAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonPushAll.UseVisualStyleBackColor = true;
            this.buttonPushAll.Click += new System.EventHandler(this.buttonPushAll_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRefresh.Location = new System.Drawing.Point(3, 196);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(150, 23);
            this.buttonRefresh.TabIndex = 5;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // toolStripButtonAnnouncementPushChanges
            // 
            this.toolStripButtonAnnouncementPushChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAnnouncementPushChanges.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAnnouncementPushChanges.Image")));
            this.toolStripButtonAnnouncementPushChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAnnouncementPushChanges.Name = "toolStripButtonAnnouncementPushChanges";
            this.toolStripButtonAnnouncementPushChanges.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAnnouncementPushChanges.Text = "Push";
            this.toolStripButtonAnnouncementPushChanges.Click += new System.EventHandler(this.toolStripButtonAnnouncementPushChanges_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Server";
            this.tabControl1.ResumeLayout(false);
            this.tabPageAccountMan.ResumeLayout(false);
            this.tabPageAccountMan.PerformLayout();
            this.toolStripAccountMan.ResumeLayout(false);
            this.toolStripAccountMan.PerformLayout();
            this.tabPageAnnouncementManagement.ResumeLayout(false);
            this.tabPageAnnouncementManagement.PerformLayout();
            this.toolStripAnnouncement.ResumeLayout(false);
            this.toolStripAnnouncement.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAccountMan;
        private System.Windows.Forms.TabPage tabPageAnnouncementManagement;
        private System.Windows.Forms.ToolStrip toolStripAnnouncement;
        private System.Windows.Forms.ToolStripButton toolStripButtonAnnouncementAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonAnnouncementEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonAnnouncementDelete;
        private System.Windows.Forms.ListView listViewAnnouncement;
        private System.Windows.Forms.ColumnHeader columnHeaderContent;
        private System.Windows.Forms.ColumnHeader columnHeaderEnable;
        private System.Windows.Forms.ColumnHeader columnHeaderTimestamp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAnnouncementRefresh;
        private System.Windows.Forms.ToolStrip toolStripAccountMan;
        private System.Windows.Forms.ListView listViewAccount;
        private System.Windows.Forms.ColumnHeader columnHeaderUsername;
        private System.Windows.Forms.ColumnHeader columnHeaderEnabled;
        private System.Windows.Forms.ToolStripButton toolStripButtonUserModify;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonAccountBlockDel;
        private System.Windows.Forms.ToolStripButton toolStripButtonAccountBlockAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonAccountBlockEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonAccountPermission;
        private System.Windows.Forms.ToolStripButton toolStripButtonAccountGroupMan;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private BuildServiceAPI.DesktopClient.TextBoxLabel textBoxLabelUsername;
        private BuildServiceAPI.DesktopClient.TextBoxLabel textBoxLabelPassword;
        private BuildServiceAPI.DesktopClient.TextBoxLabel textBoxLabelEndpoint;
        private System.Windows.Forms.Button buttonConnectionTokenFetch;
        private System.Windows.Forms.ColumnHeader columnHeaderGroups;
        private System.Windows.Forms.Button buttonPushAll;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButtonAnnouncementPushChanges;
    }
}