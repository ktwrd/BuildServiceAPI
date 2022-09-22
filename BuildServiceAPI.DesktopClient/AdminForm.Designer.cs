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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAccountMan = new System.Windows.Forms.TabPage();
            this.listViewAccount = new System.Windows.Forms.ListView();
            this.columnHeaderUsername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderGroups = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPermissions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.toolStripButtonAnnouncementPushChanges = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAnnouncementEnforce = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAnnouncementsDisable = new System.Windows.Forms.ToolStripButton();
            this.tabPageReleaseDetails = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonReleaseRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButtonReleaseFilter = new System.Windows.Forms.ToolStripDropDownButton();
            this.showLatestReleaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButtonReleaseDelete = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemDeleteRemoteSignature = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonReleaseEdit = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeViewReleaseProduct = new System.Windows.Forms.TreeView();
            this.listViewReleases = new System.Windows.Forms.ListView();
            this.columnHeaderCommitHashShort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderReleaseSignature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderReleaseTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxLabelUsername = new BuildServiceAPI.DesktopClient.TextBoxLabel();
            this.textBoxLabelPassword = new BuildServiceAPI.DesktopClient.MaskedTextBoxLabel();
            this.textBoxLabelEndpoint = new BuildServiceAPI.DesktopClient.TextBoxLabel();
            this.checkBoxAuthAutoLogin = new System.Windows.Forms.CheckBox();
            this.buttonConnectionTokenFetch = new System.Windows.Forms.Button();
            this.imageListTabs = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonMainPushChanges = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMainPull = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.tabPageAccountMan.SuspendLayout();
            this.toolStripAccountMan.SuspendLayout();
            this.tabPageAnnouncementManagement.SuspendLayout();
            this.toolStripAnnouncement.SuspendLayout();
            this.tabPageReleaseDetails.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageAccountMan);
            this.tabControl1.Controls.Add(this.tabPageAnnouncementManagement);
            this.tabControl1.Controls.Add(this.tabPageReleaseDetails);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageListTabs;
            this.tabControl1.Location = new System.Drawing.Point(3, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1258, 410);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageAccountMan
            // 
            this.tabPageAccountMan.Controls.Add(this.listViewAccount);
            this.tabPageAccountMan.Controls.Add(this.toolStripAccountMan);
            this.tabPageAccountMan.ImageIndex = 1;
            this.tabPageAccountMan.Location = new System.Drawing.Point(4, 23);
            this.tabPageAccountMan.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageAccountMan.Name = "tabPageAccountMan";
            this.tabPageAccountMan.Size = new System.Drawing.Size(1250, 383);
            this.tabPageAccountMan.TabIndex = 0;
            this.tabPageAccountMan.Text = "Account";
            this.tabPageAccountMan.UseVisualStyleBackColor = true;
            // 
            // listViewAccount
            // 
            this.listViewAccount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderUsername,
            this.columnHeaderEnabled,
            this.columnHeaderGroups,
            this.columnHeaderPermissions});
            this.listViewAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAccount.HideSelection = false;
            this.listViewAccount.Location = new System.Drawing.Point(0, 25);
            this.listViewAccount.Name = "listViewAccount";
            this.listViewAccount.Size = new System.Drawing.Size(1250, 358);
            this.listViewAccount.TabIndex = 1;
            this.listViewAccount.UseCompatibleStateImageBehavior = false;
            this.listViewAccount.View = System.Windows.Forms.View.Details;
            this.listViewAccount.SelectedIndexChanged += new System.EventHandler(this.listViewAccount_SelectedIndexChanged);
            // 
            // columnHeaderUsername
            // 
            this.columnHeaderUsername.Text = "Username";
            this.columnHeaderUsername.Width = 219;
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
            // columnHeaderPermissions
            // 
            this.columnHeaderPermissions.Text = "Permissions";
            this.columnHeaderPermissions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderPermissions.Width = 233;
            // 
            // toolStripAccountMan
            // 
            this.toolStripAccountMan.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripAccountMan.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonUserModify,
            this.toolStripSeparator2,
            this.toolStripButtonAccountBlockAdd,
            this.toolStripButtonAccountBlockEdit,
            this.toolStripButtonAccountBlockDel,
            this.toolStripSeparator3,
            this.toolStripButtonAccountPermission,
            this.toolStripButtonAccountGroupMan});
            this.toolStripAccountMan.Location = new System.Drawing.Point(0, 0);
            this.toolStripAccountMan.Name = "toolStripAccountMan";
            this.toolStripAccountMan.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripAccountMan.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripAccountMan.Size = new System.Drawing.Size(1250, 25);
            this.toolStripAccountMan.TabIndex = 0;
            this.toolStripAccountMan.Text = "Account Management";
            // 
            // toolStripButtonUserModify
            // 
            this.toolStripButtonUserModify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUserModify.Enabled = false;
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
            this.toolStripButtonAccountBlockAdd.Enabled = false;
            this.toolStripButtonAccountBlockAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAccountBlockAdd.Image")));
            this.toolStripButtonAccountBlockAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAccountBlockAdd.Name = "toolStripButtonAccountBlockAdd";
            this.toolStripButtonAccountBlockAdd.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAccountBlockAdd.Text = "Add Block Reason";
            // 
            // toolStripButtonAccountBlockEdit
            // 
            this.toolStripButtonAccountBlockEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAccountBlockEdit.Enabled = false;
            this.toolStripButtonAccountBlockEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAccountBlockEdit.Image")));
            this.toolStripButtonAccountBlockEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAccountBlockEdit.Name = "toolStripButtonAccountBlockEdit";
            this.toolStripButtonAccountBlockEdit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAccountBlockEdit.Text = "Edit Block Reason(s)";
            // 
            // toolStripButtonAccountBlockDel
            // 
            this.toolStripButtonAccountBlockDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAccountBlockDel.Enabled = false;
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
            this.toolStripButtonAccountPermission.Enabled = false;
            this.toolStripButtonAccountPermission.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAccountPermission.Image")));
            this.toolStripButtonAccountPermission.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAccountPermission.Name = "toolStripButtonAccountPermission";
            this.toolStripButtonAccountPermission.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAccountPermission.Text = "Permissions";
            this.toolStripButtonAccountPermission.Click += new System.EventHandler(this.toolStripButtonAccountPermission_Click);
            // 
            // toolStripButtonAccountGroupMan
            // 
            this.toolStripButtonAccountGroupMan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAccountGroupMan.Enabled = false;
            this.toolStripButtonAccountGroupMan.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAccountGroupMan.Image")));
            this.toolStripButtonAccountGroupMan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAccountGroupMan.Name = "toolStripButtonAccountGroupMan";
            this.toolStripButtonAccountGroupMan.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAccountGroupMan.Text = "Group Management";
            this.toolStripButtonAccountGroupMan.Click += new System.EventHandler(this.toolStripButtonAccountGroupMan_Click);
            // 
            // tabPageAnnouncementManagement
            // 
            this.tabPageAnnouncementManagement.Controls.Add(this.listViewAnnouncement);
            this.tabPageAnnouncementManagement.Controls.Add(this.toolStripAnnouncement);
            this.tabPageAnnouncementManagement.ImageIndex = 2;
            this.tabPageAnnouncementManagement.Location = new System.Drawing.Point(4, 23);
            this.tabPageAnnouncementManagement.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageAnnouncementManagement.Name = "tabPageAnnouncementManagement";
            this.tabPageAnnouncementManagement.Size = new System.Drawing.Size(1250, 383);
            this.tabPageAnnouncementManagement.TabIndex = 1;
            this.tabPageAnnouncementManagement.Text = "Announcement";
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
            this.listViewAnnouncement.Location = new System.Drawing.Point(0, 25);
            this.listViewAnnouncement.Name = "listViewAnnouncement";
            this.listViewAnnouncement.Size = new System.Drawing.Size(1250, 358);
            this.listViewAnnouncement.TabIndex = 1;
            this.listViewAnnouncement.UseCompatibleStateImageBehavior = false;
            this.listViewAnnouncement.View = System.Windows.Forms.View.Details;
            this.listViewAnnouncement.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewAnnouncement_ItemSelectionChanged);
            this.listViewAnnouncement.SelectedIndexChanged += new System.EventHandler(this.listViewAnnouncement_SelectedIndexChanged);
            // 
            // columnHeaderContent
            // 
            this.columnHeaderContent.Text = "Message";
            this.columnHeaderContent.Width = 242;
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
            this.toolStripAnnouncement.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripAnnouncement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAnnouncementAdd,
            this.toolStripButtonAnnouncementEdit,
            this.toolStripButtonAnnouncementDelete,
            this.toolStripSeparator1,
            this.toolStripButtonAnnouncementRefresh,
            this.toolStripButtonAnnouncementPushChanges,
            this.toolStripSeparator5,
            this.toolStripButtonAnnouncementEnforce,
            this.toolStripButtonAnnouncementsDisable});
            this.toolStripAnnouncement.Location = new System.Drawing.Point(0, 0);
            this.toolStripAnnouncement.Name = "toolStripAnnouncement";
            this.toolStripAnnouncement.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripAnnouncement.Size = new System.Drawing.Size(1250, 25);
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
            this.toolStripButtonAnnouncementAdd.Click += new System.EventHandler(this.toolStripButtonAnnouncementAdd_Click);
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
            this.toolStripButtonAnnouncementEdit.Click += new System.EventHandler(this.toolStripButtonAnnouncementEdit_Click);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAnnouncementEnforce
            // 
            this.toolStripButtonAnnouncementEnforce.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAnnouncementEnforce.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAnnouncementEnforce.Image")));
            this.toolStripButtonAnnouncementEnforce.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAnnouncementEnforce.Name = "toolStripButtonAnnouncementEnforce";
            this.toolStripButtonAnnouncementEnforce.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAnnouncementEnforce.Text = "Enable Announcements";
            this.toolStripButtonAnnouncementEnforce.Click += new System.EventHandler(this.toolStripButtonAnnouncementEnforce_Click);
            // 
            // toolStripButtonAnnouncementsDisable
            // 
            this.toolStripButtonAnnouncementsDisable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAnnouncementsDisable.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAnnouncementsDisable.Image")));
            this.toolStripButtonAnnouncementsDisable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAnnouncementsDisable.Name = "toolStripButtonAnnouncementsDisable";
            this.toolStripButtonAnnouncementsDisable.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAnnouncementsDisable.Text = "Disable Announcements";
            this.toolStripButtonAnnouncementsDisable.Click += new System.EventHandler(this.toolStripButtonAnnouncementsDisable_Click);
            // 
            // tabPageReleaseDetails
            // 
            this.tabPageReleaseDetails.Controls.Add(this.toolStrip1);
            this.tabPageReleaseDetails.Controls.Add(this.splitContainer2);
            this.tabPageReleaseDetails.ImageIndex = 0;
            this.tabPageReleaseDetails.Location = new System.Drawing.Point(4, 23);
            this.tabPageReleaseDetails.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageReleaseDetails.Name = "tabPageReleaseDetails";
            this.tabPageReleaseDetails.Size = new System.Drawing.Size(1250, 383);
            this.tabPageReleaseDetails.TabIndex = 2;
            this.tabPageReleaseDetails.Text = "Release";
            this.tabPageReleaseDetails.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonReleaseRefresh,
            this.toolStripDropDownButtonReleaseFilter,
            this.toolStripSeparator4,
            this.toolStripSplitButtonReleaseDelete,
            this.toolStripButtonReleaseEdit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1250, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonReleaseRefresh
            // 
            this.toolStripButtonReleaseRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReleaseRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReleaseRefresh.Image")));
            this.toolStripButtonReleaseRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReleaseRefresh.Name = "toolStripButtonReleaseRefresh";
            this.toolStripButtonReleaseRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonReleaseRefresh.Text = "Refresh";
            this.toolStripButtonReleaseRefresh.Click += new System.EventHandler(this.toolStripButtonReleaseRefresh_Click);
            // 
            // toolStripDropDownButtonReleaseFilter
            // 
            this.toolStripDropDownButtonReleaseFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonReleaseFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLatestReleaseToolStripMenuItem});
            this.toolStripDropDownButtonReleaseFilter.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonReleaseFilter.Image")));
            this.toolStripDropDownButtonReleaseFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonReleaseFilter.Name = "toolStripDropDownButtonReleaseFilter";
            this.toolStripDropDownButtonReleaseFilter.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButtonReleaseFilter.Text = "Filter";
            // 
            // showLatestReleaseToolStripMenuItem
            // 
            this.showLatestReleaseToolStripMenuItem.CheckOnClick = true;
            this.showLatestReleaseToolStripMenuItem.Name = "showLatestReleaseToolStripMenuItem";
            this.showLatestReleaseToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.showLatestReleaseToolStripMenuItem.Text = "Show Latest Release";
            this.showLatestReleaseToolStripMenuItem.Click += new System.EventHandler(this.showLatestReleaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButtonReleaseDelete
            // 
            this.toolStripSplitButtonReleaseDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonReleaseDelete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteRemoteSignature});
            this.toolStripSplitButtonReleaseDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonReleaseDelete.Image")));
            this.toolStripSplitButtonReleaseDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonReleaseDelete.Name = "toolStripSplitButtonReleaseDelete";
            this.toolStripSplitButtonReleaseDelete.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButtonReleaseDelete.Text = "Delete Selected";
            this.toolStripSplitButtonReleaseDelete.ToolTipText = "Delete Selected";
            this.toolStripSplitButtonReleaseDelete.ButtonClick += new System.EventHandler(this.toolStripSplitButtonReleaseDelete_ButtonClick);
            // 
            // toolStripMenuItemDeleteRemoteSignature
            // 
            this.toolStripMenuItemDeleteRemoteSignature.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemDeleteRemoteSignature.Image")));
            this.toolStripMenuItemDeleteRemoteSignature.Name = "toolStripMenuItemDeleteRemoteSignature";
            this.toolStripMenuItemDeleteRemoteSignature.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItemDeleteRemoteSignature.Text = "Remote Signature";
            this.toolStripMenuItemDeleteRemoteSignature.ToolTipText = "Delete all Release Streams that have the same Remote Signature";
            this.toolStripMenuItemDeleteRemoteSignature.Click += new System.EventHandler(this.toolStripMenuItemDeleteRemoteSignature_Click);
            // 
            // toolStripButtonReleaseEdit
            // 
            this.toolStripButtonReleaseEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReleaseEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReleaseEdit.Image")));
            this.toolStripButtonReleaseEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReleaseEdit.Name = "toolStripButtonReleaseEdit";
            this.toolStripButtonReleaseEdit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonReleaseEdit.Text = "Edit";
            this.toolStripButtonReleaseEdit.Click += new System.EventHandler(this.toolStripButtonReleaseEdit_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeViewReleaseProduct);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listViewReleases);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.splitContainer2.Size = new System.Drawing.Size(1250, 383);
            this.splitContainer2.SplitterDistance = 271;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeViewReleaseProduct
            // 
            this.treeViewReleaseProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewReleaseProduct.Location = new System.Drawing.Point(0, 25);
            this.treeViewReleaseProduct.Name = "treeViewReleaseProduct";
            this.treeViewReleaseProduct.Size = new System.Drawing.Size(271, 358);
            this.treeViewReleaseProduct.TabIndex = 0;
            this.treeViewReleaseProduct.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewReleaseProduct_AfterSelect);
            // 
            // listViewReleases
            // 
            this.listViewReleases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCommitHashShort,
            this.columnHeaderReleaseSignature,
            this.columnHeaderReleaseTimestamp});
            this.listViewReleases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewReleases.FullRowSelect = true;
            this.listViewReleases.GridLines = true;
            this.listViewReleases.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewReleases.HideSelection = false;
            this.listViewReleases.Location = new System.Drawing.Point(0, 25);
            this.listViewReleases.Name = "listViewReleases";
            this.listViewReleases.Size = new System.Drawing.Size(975, 358);
            this.listViewReleases.TabIndex = 0;
            this.listViewReleases.UseCompatibleStateImageBehavior = false;
            this.listViewReleases.View = System.Windows.Forms.View.Details;
            this.listViewReleases.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewReleases_ItemSelectionChanged);
            this.listViewReleases.SelectedIndexChanged += new System.EventHandler(this.listViewReleases_SelectedIndexChanged);
            this.listViewReleases.Click += new System.EventHandler(this.listViewReleases_Click);
            // 
            // columnHeaderCommitHashShort
            // 
            this.columnHeaderCommitHashShort.Text = "Hash";
            this.columnHeaderCommitHashShort.Width = 80;
            // 
            // columnHeaderReleaseSignature
            // 
            this.columnHeaderReleaseSignature.Text = "Signature";
            this.columnHeaderReleaseSignature.Width = 150;
            // 
            // columnHeaderReleaseTimestamp
            // 
            this.columnHeaderReleaseTimestamp.Text = "Timestamp";
            this.columnHeaderReleaseTimestamp.Width = 160;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.groupBox1);
            this.tabPageSettings.ImageIndex = 3;
            this.tabPageSettings.Location = new System.Drawing.Point(4, 23);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPageSettings.Size = new System.Drawing.Size(1250, 383);
            this.tabPageSettings.TabIndex = 3;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 247);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Authentication";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.textBoxLabelUsername);
            this.flowLayoutPanel2.Controls.Add(this.textBoxLabelPassword);
            this.flowLayoutPanel2.Controls.Add(this.textBoxLabelEndpoint);
            this.flowLayoutPanel2.Controls.Add(this.checkBoxAuthAutoLogin);
            this.flowLayoutPanel2.Controls.Add(this.buttonConnectionTokenFetch);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(314, 228);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // textBoxLabelUsername
            // 
            this.textBoxLabelUsername.AutoSize = true;
            this.textBoxLabelUsername.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textBoxLabelUsername.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLabelUsername.LabelText = "Username";
            this.textBoxLabelUsername.Location = new System.Drawing.Point(3, 3);
            this.textBoxLabelUsername.MinimumSize = new System.Drawing.Size(308, 39);
            this.textBoxLabelUsername.MultiLine = true;
            this.textBoxLabelUsername.Name = "textBoxLabelUsername";
            this.textBoxLabelUsername.ReadOnly = false;
            this.textBoxLabelUsername.Size = new System.Drawing.Size(308, 39);
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
            this.textBoxLabelPassword.MinimumSize = new System.Drawing.Size(308, 39);
            this.textBoxLabelPassword.MultiLine = false;
            this.textBoxLabelPassword.Name = "textBoxLabelPassword";
            this.textBoxLabelPassword.ReadOnly = false;
            this.textBoxLabelPassword.Size = new System.Drawing.Size(308, 39);
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
            this.textBoxLabelEndpoint.MinimumSize = new System.Drawing.Size(308, 39);
            this.textBoxLabelEndpoint.MultiLine = true;
            this.textBoxLabelEndpoint.Name = "textBoxLabelEndpoint";
            this.textBoxLabelEndpoint.ReadOnly = false;
            this.textBoxLabelEndpoint.Size = new System.Drawing.Size(308, 39);
            this.textBoxLabelEndpoint.TabIndex = 2;
            this.textBoxLabelEndpoint.TextboxContent = "";
            this.textBoxLabelEndpoint.TextboxLines = new string[0];
            // 
            // checkBoxAuthAutoLogin
            // 
            this.checkBoxAuthAutoLogin.AutoSize = true;
            this.checkBoxAuthAutoLogin.Location = new System.Drawing.Point(3, 138);
            this.checkBoxAuthAutoLogin.Name = "checkBoxAuthAutoLogin";
            this.checkBoxAuthAutoLogin.Size = new System.Drawing.Size(117, 17);
            this.checkBoxAuthAutoLogin.TabIndex = 4;
            this.checkBoxAuthAutoLogin.Text = "Automatically Login";
            this.checkBoxAuthAutoLogin.UseVisualStyleBackColor = true;
            this.checkBoxAuthAutoLogin.CheckedChanged += new System.EventHandler(this.checkBoxAuthAutoLogin_CheckedChanged);
            // 
            // buttonConnectionTokenFetch
            // 
            this.buttonConnectionTokenFetch.Image = ((System.Drawing.Image)(resources.GetObject("buttonConnectionTokenFetch.Image")));
            this.buttonConnectionTokenFetch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonConnectionTokenFetch.Location = new System.Drawing.Point(3, 161);
            this.buttonConnectionTokenFetch.Name = "buttonConnectionTokenFetch";
            this.buttonConnectionTokenFetch.Size = new System.Drawing.Size(105, 23);
            this.buttonConnectionTokenFetch.TabIndex = 3;
            this.buttonConnectionTokenFetch.Text = "Reload Token";
            this.buttonConnectionTokenFetch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonConnectionTokenFetch.UseVisualStyleBackColor = true;
            this.buttonConnectionTokenFetch.Click += new System.EventHandler(this.buttonConnectionTokenFetch_Click);
            // 
            // imageListTabs
            // 
            this.imageListTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTabs.ImageStream")));
            this.imageListTabs.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTabs.Images.SetKeyName(0, "box.png");
            this.imageListTabs.Images.SetKeyName(1, "user.png");
            this.imageListTabs.Images.SetKeyName(2, "megaphone.png");
            this.imageListTabs.Images.SetKeyName(3, "gear.png");
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonMainPushChanges,
            this.toolStripButtonMainPull});
            this.toolStrip2.Location = new System.Drawing.Point(3, 0);
            this.toolStrip2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(1258, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButtonMainPushChanges
            // 
            this.toolStripButtonMainPushChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMainPushChanges.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMainPushChanges.Image")));
            this.toolStripButtonMainPushChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMainPushChanges.Name = "toolStripButtonMainPushChanges";
            this.toolStripButtonMainPushChanges.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonMainPushChanges.Text = "Push Changes";
            this.toolStripButtonMainPushChanges.Click += new System.EventHandler(this.toolStripButtonMainPushChanges_Click);
            // 
            // toolStripButtonMainPull
            // 
            this.toolStripButtonMainPull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMainPull.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMainPull.Image")));
            this.toolStripButtonMainPull.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMainPull.Name = "toolStripButtonMainPull";
            this.toolStripButtonMainPull.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonMainPull.Text = "Pull Changes";
            this.toolStripButtonMainPull.Click += new System.EventHandler(this.toolStripButtonMainPull_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1264, 441);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 441);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1280, 480);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Server";
            this.Shown += new System.EventHandler(this.AdminForm_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPageAccountMan.ResumeLayout(false);
            this.tabPageAccountMan.PerformLayout();
            this.toolStripAccountMan.ResumeLayout(false);
            this.toolStripAccountMan.PerformLayout();
            this.tabPageAnnouncementManagement.ResumeLayout(false);
            this.tabPageAnnouncementManagement.PerformLayout();
            this.toolStripAnnouncement.ResumeLayout(false);
            this.toolStripAnnouncement.PerformLayout();
            this.tabPageReleaseDetails.ResumeLayout(false);
            this.tabPageReleaseDetails.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private BuildServiceAPI.DesktopClient.TextBoxLabel textBoxLabelUsername;
        private BuildServiceAPI.DesktopClient.MaskedTextBoxLabel textBoxLabelPassword;
        private BuildServiceAPI.DesktopClient.TextBoxLabel textBoxLabelEndpoint;
        private System.Windows.Forms.Button buttonConnectionTokenFetch;
        private System.Windows.Forms.ColumnHeader columnHeaderGroups;
        private System.Windows.Forms.ToolStripButton toolStripButtonAnnouncementPushChanges;
        private System.Windows.Forms.TabPage tabPageReleaseDetails;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeViewReleaseProduct;
        private System.Windows.Forms.ListView listViewReleases;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonReleaseRefresh;
        private System.Windows.Forms.ColumnHeader columnHeaderCommitHashShort;
        private System.Windows.Forms.ColumnHeader columnHeaderReleaseSignature;
        private System.Windows.Forms.ColumnHeader columnHeaderReleaseTimestamp;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonReleaseFilter;
        private System.Windows.Forms.ToolStripMenuItem showLatestReleaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonReleaseDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteRemoteSignature;
        private System.Windows.Forms.ToolStripButton toolStripButtonReleaseEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonAnnouncementEnforce;
        private System.Windows.Forms.ToolStripButton toolStripButtonAnnouncementsDisable;
        private System.Windows.Forms.ColumnHeader columnHeaderPermissions;
        private System.Windows.Forms.ImageList imageListTabs;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonMainPushChanges;
        private System.Windows.Forms.ToolStripButton toolStripButtonMainPull;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBoxAuthAutoLogin;
    }
}