namespace lStore
{
    partial class lStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(lStore));
            this.profilepic = new System.Windows.Forms.PictureBox();
            this.uname = new System.Windows.Forms.Label();
            this.linkChangeImage = new System.Windows.Forms.LinkLabel();
            this.search = new System.Windows.Forms.TextBox();
            this.submitSearch = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.backbutton = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.internetstateImg = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape3 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.label1 = new System.Windows.Forms.Label();
            this.countOnline = new System.Windows.Forms.Label();
            this.onlineUsers = new System.Windows.Forms.ListBox();
            this.filterUser = new System.Windows.Forms.TextBox();
            this.bottombar_label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bgw_internetstate = new System.ComponentModel.BackgroundWorker();
            this.selectCategories = new System.Windows.Forms.ComboBox();
            this.bottombar_label2 = new System.Windows.Forms.Label();
            this.notifICO = new System.Windows.Forms.NotifyIcon(this.components);
            this.lv_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lv_menu_open = new System.Windows.Forms.ToolStripMenuItem();
            this.lv_menu_download = new System.Windows.Forms.ToolStripMenuItem();
            this.cOPYToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rATEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortbySelectBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.workspace = new System.Windows.Forms.ListView();
            this.lv_filename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_username = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_filesize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_category = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_rating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.onlineUserRetriever = new System.ComponentModel.BackgroundWorker();
            this.pingLabel = new System.Windows.Forms.Label();
            this.imageListLV = new System.Windows.Forms.ImageList(this.components);
            this.tmpLog = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.presentLocation = new System.Windows.Forms.TextBox();
            this.filterOnline = new System.ComponentModel.BackgroundWorker();
            this.refreshbutton1 = new System.Windows.Forms.Button();
            this.loader = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.profilepic)).BeginInit();
            this.lv_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loader)).BeginInit();
            this.SuspendLayout();
            // 
            // profilepic
            // 
            this.profilepic.Image = ((System.Drawing.Image)(resources.GetObject("profilepic.Image")));
            this.profilepic.Location = new System.Drawing.Point(16, 27);
            this.profilepic.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.profilepic.Name = "profilepic";
            this.profilepic.Size = new System.Drawing.Size(103, 123);
            this.profilepic.TabIndex = 0;
            this.profilepic.TabStop = false;
            // 
            // uname
            // 
            this.uname.AutoSize = true;
            this.uname.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.uname.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uname.Location = new System.Drawing.Point(16, 150);
            this.uname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.uname.Name = "uname";
            this.uname.Size = new System.Drawing.Size(108, 16);
            this.uname.TabIndex = 1;
            this.uname.Text = "Username: ";
            // 
            // linkChangeImage
            // 
            this.linkChangeImage.AutoSize = true;
            this.linkChangeImage.Location = new System.Drawing.Point(26, 166);
            this.linkChangeImage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkChangeImage.Name = "linkChangeImage";
            this.linkChangeImage.Size = new System.Drawing.Size(74, 13);
            this.linkChangeImage.TabIndex = 7;
            this.linkChangeImage.TabStop = true;
            this.linkChangeImage.Text = "change image";
            this.linkChangeImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkChangeImage_LinkClicked);
            // 
            // search
            // 
            this.search.Font = new System.Drawing.Font("Lucida Sans Typewriter", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.ForeColor = System.Drawing.SystemColors.Highlight;
            this.search.Location = new System.Drawing.Point(755, 46);
            this.search.MaximumSize = new System.Drawing.Size(500, 58);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(435, 26);
            this.search.TabIndex = 12;
            this.search.Text = " Search here...";
            this.search.Click += new System.EventHandler(this.search_Click);
            this.search.TextChanged += new System.EventHandler(this.search_TextChanged);
            this.search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.search_KeyDown);
            // 
            // submitSearch
            // 
            this.submitSearch.Location = new System.Drawing.Point(1196, 45);
            this.submitSearch.Name = "submitSearch";
            this.submitSearch.Size = new System.Drawing.Size(102, 28);
            this.submitSearch.TabIndex = 13;
            this.submitSearch.Text = "Search now! ";
            this.submitSearch.UseVisualStyleBackColor = true;
            this.submitSearch.Click += new System.EventHandler(this.submitSearch_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.backbutton,
            this.internetstateImg,
            this.rectangleShape3,
            this.rectangleShape2,
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(1350, 681);
            this.shapeContainer1.TabIndex = 14;
            this.shapeContainer1.TabStop = false;
            // 
            // backbutton
            // 
            this.backbutton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backbutton.BackgroundImage")));
            this.backbutton.BorderColor = System.Drawing.Color.Transparent;
            this.backbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backbutton.FillColor = System.Drawing.Color.Transparent;
            this.backbutton.FillGradientColor = System.Drawing.Color.Transparent;
            this.backbutton.Location = new System.Drawing.Point(203, 190);
            this.backbutton.Name = "backbutton";
            this.backbutton.SelectionColor = System.Drawing.Color.Transparent;
            this.backbutton.Size = new System.Drawing.Size(40, 33);
            this.backbutton.Click += new System.EventHandler(this.backbutton_Click);
            // 
            // internetstateImg
            // 
            this.internetstateImg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("internetstateImg.BackgroundImage")));
            this.internetstateImg.BorderColor = System.Drawing.Color.Transparent;
            this.internetstateImg.Location = new System.Drawing.Point(1299, 183);
            this.internetstateImg.Name = "internetstateImg";
            this.internetstateImg.Size = new System.Drawing.Size(40, 40);
            // 
            // rectangleShape3
            // 
            this.rectangleShape3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape3.BorderColor = System.Drawing.Color.Transparent;
            this.rectangleShape3.Location = new System.Drawing.Point(-2, 690);
            this.rectangleShape3.Name = "rectangleShape3";
            this.rectangleShape3.Size = new System.Drawing.Size(2000, 40);
            // 
            // rectangleShape2
            // 
            this.rectangleShape2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.rectangleShape2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape2.BorderColor = System.Drawing.Color.Transparent;
            this.rectangleShape2.Location = new System.Drawing.Point(16, 150);
            this.rectangleShape2.Name = "rectangleShape2";
            this.rectangleShape2.Size = new System.Drawing.Size(103, 16);
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape1.BorderColor = System.Drawing.Color.Transparent;
            this.rectangleShape1.Location = new System.Drawing.Point(732, 28);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(607, 104);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Online now ";
            // 
            // countOnline
            // 
            this.countOnline.AutoSize = true;
            this.countOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countOnline.Location = new System.Drawing.Point(127, 205);
            this.countOnline.Name = "countOnline";
            this.countOnline.Size = new System.Drawing.Size(48, 18);
            this.countOnline.TabIndex = 17;
            this.countOnline.Text = "( 12 )";
            this.countOnline.UseMnemonic = false;
            // 
            // onlineUsers
            // 
            this.onlineUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.onlineUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.onlineUsers.Font = new System.Drawing.Font("Maiandra GD", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onlineUsers.FormattingEnabled = true;
            this.onlineUsers.ItemHeight = 15;
            this.onlineUsers.Items.AddRange(new object[] {
            "testItem"});
            this.onlineUsers.Location = new System.Drawing.Point(16, 249);
            this.onlineUsers.Name = "onlineUsers";
            this.onlineUsers.Size = new System.Drawing.Size(158, 420);
            this.onlineUsers.TabIndex = 18;
            this.onlineUsers.SelectedIndexChanged += new System.EventHandler(this.onlineUsers_SelectedIndexChanged);
            // 
            // filterUser
            // 
            this.filterUser.Location = new System.Drawing.Point(16, 229);
            this.filterUser.Name = "filterUser";
            this.filterUser.Size = new System.Drawing.Size(158, 20);
            this.filterUser.TabIndex = 19;
            this.filterUser.Text = "search....";
            this.filterUser.Click += new System.EventHandler(this.filterUser_Click);
            this.filterUser.MouseClick += new System.Windows.Forms.MouseEventHandler(this.filterUser_MouseClick);
            this.filterUser.TextChanged += new System.EventHandler(this.filterUser_TextChanged);
            // 
            // bottombar_label1
            // 
            this.bottombar_label1.AutoSize = true;
            this.bottombar_label1.Location = new System.Drawing.Point(15, 695);
            this.bottombar_label1.Name = "bottombar_label1";
            this.bottombar_label1.Size = new System.Drawing.Size(91, 13);
            this.bottombar_label1.TabIndex = 20;
            this.bottombar_label1.Text = "refreshing user list";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 668);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(158, 10);
            this.progressBar1.TabIndex = 21;
            // 
            // bgw_internetstate
            // 
            this.bgw_internetstate.WorkerSupportsCancellation = true;
            this.bgw_internetstate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_internetstate_DoWork);
            this.bgw_internetstate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_internetstate_RunWorkerCompleted);
            // 
            // selectCategories
            // 
            this.selectCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectCategories.FormattingEnabled = true;
            this.selectCategories.Items.AddRange(new object[] {
            "All",
            "Documents",
            "Movies",
            "Songs",
            "Games",
            "Softwares",
            "Codes"});
            this.selectCategories.Location = new System.Drawing.Point(864, 94);
            this.selectCategories.Name = "selectCategories";
            this.selectCategories.Size = new System.Drawing.Size(121, 21);
            this.selectCategories.TabIndex = 24;
            this.selectCategories.SelectedIndexChanged += new System.EventHandler(this.selectCategories_SelectedIndexChanged);
            // 
            // bottombar_label2
            // 
            this.bottombar_label2.AutoSize = true;
            this.bottombar_label2.ForeColor = System.Drawing.Color.Red;
            this.bottombar_label2.Location = new System.Drawing.Point(391, 695);
            this.bottombar_label2.Name = "bottombar_label2";
            this.bottombar_label2.Size = new System.Drawing.Size(195, 13);
            this.bottombar_label2.TabIndex = 26;
            this.bottombar_label2.Text = "User is not available or is not accessible";
            // 
            // notifICO
            // 
            this.notifICO.ContextMenuStrip = this.lv_menu;
            this.notifICO.Icon = ((System.Drawing.Icon)(resources.GetObject("notifICO.Icon")));
            this.notifICO.Text = "lStore: LAN Sharing simplified";
            this.notifICO.Visible = true;
            // 
            // lv_menu
            // 
            this.lv_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lv_menu_open,
            this.lv_menu_download,
            this.cOPYToolStripMenuItem1,
            this.rATEToolStripMenuItem});
            this.lv_menu.Name = "lv_menu";
            this.lv_menu.Size = new System.Drawing.Size(162, 92);
            // 
            // lv_menu_open
            // 
            this.lv_menu_open.Name = "lv_menu_open";
            this.lv_menu_open.Size = new System.Drawing.Size(161, 22);
            this.lv_menu_open.Text = "Open in explorer";
            this.lv_menu_open.Click += new System.EventHandler(this.lv_menu_open_Click);
            // 
            // lv_menu_download
            // 
            this.lv_menu_download.Enabled = false;
            this.lv_menu_download.Name = "lv_menu_download";
            this.lv_menu_download.Size = new System.Drawing.Size(161, 22);
            this.lv_menu_download.Text = "DOWNLOAD";
            // 
            // cOPYToolStripMenuItem1
            // 
            this.cOPYToolStripMenuItem1.Name = "cOPYToolStripMenuItem1";
            this.cOPYToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.cOPYToolStripMenuItem1.Text = "COPY";
            this.cOPYToolStripMenuItem1.Click += new System.EventHandler(this.cOPYToolStripMenuItem1_Click);
            // 
            // rATEToolStripMenuItem
            // 
            this.rATEToolStripMenuItem.Name = "rATEToolStripMenuItem";
            this.rATEToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.rATEToolStripMenuItem.Text = "RATE";
            // 
            // sortbySelectBox
            // 
            this.sortbySelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortbySelectBox.FormattingEnabled = true;
            this.sortbySelectBox.Items.AddRange(new object[] {
            "User Rating",
            "Online Rating",
            "Date modified"});
            this.sortbySelectBox.Location = new System.Drawing.Point(1077, 94);
            this.sortbySelectBox.Name = "sortbySelectBox";
            this.sortbySelectBox.Size = new System.Drawing.Size(121, 21);
            this.sortbySelectBox.TabIndex = 27;
            this.sortbySelectBox.SelectedIndexChanged += new System.EventHandler(this.sortbySelectBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(759, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 19);
            this.label2.TabIndex = 28;
            this.label2.Text = "Categories";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(1000, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 19);
            this.label3.TabIndex = 29;
            this.label3.Text = "Sort by";
            // 
            // workspace
            // 
            this.workspace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.workspace.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lv_filename,
            this.lv_username,
            this.lv_filesize,
            this.lv_category,
            this.lv_rating});
            this.workspace.ContextMenuStrip = this.lv_menu;
            this.workspace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.workspace.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workspace.ForeColor = System.Drawing.Color.Gray;
            this.workspace.FullRowSelect = true;
            this.workspace.GridLines = true;
            this.workspace.Location = new System.Drawing.Point(195, 229);
            this.workspace.MultiSelect = false;
            this.workspace.Name = "workspace";
            this.workspace.Size = new System.Drawing.Size(1143, 364);
            this.workspace.TabIndex = 30;
            this.workspace.TileSize = new System.Drawing.Size(710, 50);
            this.workspace.UseCompatibleStateImageBehavior = false;
            this.workspace.View = System.Windows.Forms.View.Details;
            this.workspace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.workspace_KeyDown);
            this.workspace.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.workspace_MouseDoubleClick);
            // 
            // lv_filename
            // 
            this.lv_filename.Text = "Filename";
            this.lv_filename.Width = 645;
            // 
            // lv_username
            // 
            this.lv_username.Text = "Owner";
            this.lv_username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lv_username.Width = 129;
            // 
            // lv_filesize
            // 
            this.lv_filesize.Text = "size";
            this.lv_filesize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lv_filesize.Width = 116;
            // 
            // lv_category
            // 
            this.lv_category.Text = "Category";
            this.lv_category.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lv_category.Width = 122;
            // 
            // lv_rating
            // 
            this.lv_rating.Text = "rating";
            this.lv_rating.Width = 100;
            // 
            // onlineUserRetriever
            // 
            this.onlineUserRetriever.WorkerReportsProgress = true;
            this.onlineUserRetriever.WorkerSupportsCancellation = true;
            this.onlineUserRetriever.DoWork += new System.ComponentModel.DoWorkEventHandler(this.onlineUserRetriever_DoWork);
            this.onlineUserRetriever.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.onlineUserRetriever_ProgressChanged);
            this.onlineUserRetriever.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.onlineUserRetriever_RunWorkerCompleted);
            // 
            // pingLabel
            // 
            this.pingLabel.AutoSize = true;
            this.pingLabel.ForeColor = System.Drawing.Color.DarkCyan;
            this.pingLabel.Location = new System.Drawing.Point(884, 695);
            this.pingLabel.Name = "pingLabel";
            this.pingLabel.Size = new System.Drawing.Size(111, 13);
            this.pingLabel.TabIndex = 31;
            this.pingLabel.Text = "talking to other users!!";
            // 
            // imageListLV
            // 
            this.imageListLV.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListLV.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListLV.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tmpLog
            // 
            this.tmpLog.AutoSize = true;
            this.tmpLog.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tmpLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tmpLog.Location = new System.Drawing.Point(757, 82);
            this.tmpLog.Name = "tmpLog";
            this.tmpLog.Size = new System.Drawing.Size(0, 13);
            this.tmpLog.TabIndex = 32;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(34, 183);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(53, 13);
            this.linkLabel2.TabIndex = 33;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "About Me";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // presentLocation
            // 
            this.presentLocation.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.presentLocation.Location = new System.Drawing.Point(249, 189);
            this.presentLocation.Name = "presentLocation";
            this.presentLocation.Size = new System.Drawing.Size(530, 33);
            this.presentLocation.TabIndex = 34;
            this.presentLocation.Text = "lStore/";
            this.presentLocation.MouseClick += new System.Windows.Forms.MouseEventHandler(this.presentLocation_MouseClick);
            // 
            // filterOnline
            // 
            this.filterOnline.WorkerReportsProgress = true;
            this.filterOnline.WorkerSupportsCancellation = true;
            this.filterOnline.DoWork += new System.ComponentModel.DoWorkEventHandler(this.filterOnline_DoWork);
            this.filterOnline.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.filterOnline_ProgressChanged);
            this.filterOnline.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.filterOnline_RunWorkerCompleted);
            // 
            // refreshbutton1
            // 
            this.refreshbutton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshbutton1.FlatAppearance.BorderSize = 0;
            this.refreshbutton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshbutton1.Image = ((System.Drawing.Image)(resources.GetObject("refreshbutton1.Image")));
            this.refreshbutton1.Location = new System.Drawing.Point(20, 204);
            this.refreshbutton1.Name = "refreshbutton1";
            this.refreshbutton1.Size = new System.Drawing.Size(20, 20);
            this.refreshbutton1.TabIndex = 35;
            this.refreshbutton1.Tag = "Refresh online users";
            this.refreshbutton1.UseVisualStyleBackColor = true;
            this.refreshbutton1.Visible = false;
            this.refreshbutton1.Click += new System.EventHandler(this.refreshbutton1_Click);
            // 
            // loader
            // 
            this.loader.BackColor = System.Drawing.Color.Transparent;
            this.loader.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.loader.Image = ((System.Drawing.Image)(resources.GetObject("loader.Image")));
            this.loader.InitialImage = ((System.Drawing.Image)(resources.GetObject("loader.InitialImage")));
            this.loader.Location = new System.Drawing.Point(788, 192);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(24, 24);
            this.loader.TabIndex = 37;
            this.loader.TabStop = false;
            this.loader.Visible = false;
            // 
            // lStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1350, 681);
            this.Controls.Add(this.onlineUsers);
            this.Controls.Add(this.loader);
            this.Controls.Add(this.presentLocation);
            this.Controls.Add(this.refreshbutton1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.tmpLog);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.search);
            this.Controls.Add(this.submitSearch);
            this.Controls.Add(this.sortbySelectBox);
            this.Controls.Add(this.selectCategories);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.workspace);
            this.Controls.Add(this.bottombar_label1);
            this.Controls.Add(this.bottombar_label2);
            this.Controls.Add(this.pingLabel);
            this.Controls.Add(this.filterUser);
            this.Controls.Add(this.countOnline);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkChangeImage);
            this.Controls.Add(this.uname);
            this.Controls.Add(this.profilepic);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(700, 395);
            this.Name = "lStore";
            this.Text = "lStore: LAN Sharing simplified !";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.lStore_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.lStore_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.profilepic)).EndInit();
            this.lv_menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepic;
        private System.Windows.Forms.Label uname;
        private System.Windows.Forms.LinkLabel linkChangeImage;
        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.Button submitSearch;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label countOnline;
        private System.Windows.Forms.ListBox onlineUsers;
        private System.Windows.Forms.TextBox filterUser;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape3;
        private System.Windows.Forms.Label bottombar_label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker bgw_internetstate;
        private System.Windows.Forms.ComboBox selectCategories;
        private System.Windows.Forms.Label bottombar_label2;
        private System.Windows.Forms.NotifyIcon notifICO;
        private System.Windows.Forms.ComboBox sortbySelectBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView workspace;
        private System.Windows.Forms.ColumnHeader lv_filename;
        private System.Windows.Forms.ColumnHeader lv_username;
        private System.Windows.Forms.ColumnHeader lv_filesize;
        private System.Windows.Forms.ColumnHeader lv_rating;
        private System.ComponentModel.BackgroundWorker onlineUserRetriever;
        private System.Windows.Forms.Label pingLabel;
        private System.Windows.Forms.ImageList imageListLV;
        private System.Windows.Forms.ContextMenuStrip lv_menu;
        private System.Windows.Forms.ToolStripMenuItem lv_menu_open;
        private System.Windows.Forms.ToolStripMenuItem lv_menu_download;
        private System.Windows.Forms.ColumnHeader lv_category;
        private System.Windows.Forms.ToolStripMenuItem cOPYToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rATEToolStripMenuItem;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape internetstateImg;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape backbutton;
        private System.Windows.Forms.Label tmpLog;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.TextBox presentLocation;
        private System.ComponentModel.BackgroundWorker filterOnline;
        private System.Windows.Forms.Button refreshbutton1;
        private System.Windows.Forms.PictureBox loader;
    }
}

