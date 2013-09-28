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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(lStore));
            this.profilepic = new System.Windows.Forms.PictureBox();
            this.uname = new System.Windows.Forms.Label();
            this.nname = new System.Windows.Forms.Label();
            this.countFileSharedLabel = new System.Windows.Forms.Label();
            this.ratingLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fILEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nEWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPENToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rESETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eDITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vIEWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hELPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tOOLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sOCIALToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cHATToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bROADCASTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codeLocationLabel = new System.Windows.Forms.Label();
            this.linkChangeImage = new System.Windows.Forms.LinkLabel();
            this.codeLocation = new System.Windows.Forms.Label();
            this.rating = new System.Windows.Forms.Label();
            this.countFilesShared = new System.Windows.Forms.Label();
            this.internetState = new System.Windows.Forms.Label();
            this.search = new System.Windows.Forms.TextBox();
            this.submitSearch = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape3 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.tmpLog = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.countOnline = new System.Windows.Forms.Label();
            this.onlineUsers = new System.Windows.Forms.ListBox();
            this.filterUser = new System.Windows.Forms.TextBox();
            this.bottombar_label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bg1 = new System.ComponentModel.BackgroundWorker();
            this.bgw_internetstate = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.profilepic)).BeginInit();
            this.menuStrip1.SuspendLayout();
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
            this.uname.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.uname.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uname.Location = new System.Drawing.Point(16, 150);
            this.uname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.uname.Name = "uname";
            this.uname.Size = new System.Drawing.Size(108, 16);
            this.uname.TabIndex = 1;
            this.uname.Text = "Username: ";
            // 
            // nname
            // 
            this.nname.AutoSize = true;
            this.nname.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.nname.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nname.Location = new System.Drawing.Point(123, 27);
            this.nname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nname.Name = "nname";
            this.nname.Size = new System.Drawing.Size(148, 16);
            this.nname.TabIndex = 2;
            this.nname.Text = "Network name: ";
            // 
            // countFileSharedLabel
            // 
            this.countFileSharedLabel.AutoSize = true;
            this.countFileSharedLabel.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countFileSharedLabel.Location = new System.Drawing.Point(123, 87);
            this.countFileSharedLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.countFileSharedLabel.Name = "countFileSharedLabel";
            this.countFileSharedLabel.Size = new System.Drawing.Size(148, 16);
            this.countFileSharedLabel.TabIndex = 3;
            this.countFileSharedLabel.Text = "Files shared: ";
            // 
            // ratingLabel
            // 
            this.ratingLabel.AutoSize = true;
            this.ratingLabel.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ratingLabel.Location = new System.Drawing.Point(123, 49);
            this.ratingLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ratingLabel.Name = "ratingLabel";
            this.ratingLabel.Size = new System.Drawing.Size(88, 16);
            this.ratingLabel.TabIndex = 4;
            this.ratingLabel.Text = "Rating: ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fILEToolStripMenuItem,
            this.eDITToolStripMenuItem,
            this.vIEWToolStripMenuItem,
            this.hELPToolStripMenuItem,
            this.tOOLSToolStripMenuItem,
            this.sOCIALToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            this.fILEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nEWToolStripMenuItem,
            this.oPENToolStripMenuItem,
            this.rESETToolStripMenuItem,
            this.eXITToolStripMenuItem});
            this.fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            this.fILEToolStripMenuItem.Size = new System.Drawing.Size(40, 22);
            this.fILEToolStripMenuItem.Text = "FILE";
            // 
            // nEWToolStripMenuItem
            // 
            this.nEWToolStripMenuItem.Name = "nEWToolStripMenuItem";
            this.nEWToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.nEWToolStripMenuItem.Text = "NEW";
            // 
            // oPENToolStripMenuItem
            // 
            this.oPENToolStripMenuItem.Name = "oPENToolStripMenuItem";
            this.oPENToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.oPENToolStripMenuItem.Text = "OPEN";
            // 
            // rESETToolStripMenuItem
            // 
            this.rESETToolStripMenuItem.Name = "rESETToolStripMenuItem";
            this.rESETToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.rESETToolStripMenuItem.Text = "RESET";
            // 
            // eXITToolStripMenuItem
            // 
            this.eXITToolStripMenuItem.Name = "eXITToolStripMenuItem";
            this.eXITToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.eXITToolStripMenuItem.Text = "EXIT";
            // 
            // eDITToolStripMenuItem
            // 
            this.eDITToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.eDITToolStripMenuItem.Name = "eDITToolStripMenuItem";
            this.eDITToolStripMenuItem.Size = new System.Drawing.Size(43, 22);
            this.eDITToolStripMenuItem.Text = "EDIT";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.copyToolStripMenuItem.Text = "Copy ";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // vIEWToolStripMenuItem
            // 
            this.vIEWToolStripMenuItem.Name = "vIEWToolStripMenuItem";
            this.vIEWToolStripMenuItem.Size = new System.Drawing.Size(46, 22);
            this.vIEWToolStripMenuItem.Text = "VIEW";
            // 
            // hELPToolStripMenuItem
            // 
            this.hELPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.offlineHelpToolStripMenuItem,
            this.onlineHelpToolStripMenuItem,
            this.aboutUsToolStripMenuItem});
            this.hELPToolStripMenuItem.Name = "hELPToolStripMenuItem";
            this.hELPToolStripMenuItem.Size = new System.Drawing.Size(47, 22);
            this.hELPToolStripMenuItem.Text = "HELP";
            // 
            // offlineHelpToolStripMenuItem
            // 
            this.offlineHelpToolStripMenuItem.Name = "offlineHelpToolStripMenuItem";
            this.offlineHelpToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.offlineHelpToolStripMenuItem.Text = "Offline Help";
            // 
            // onlineHelpToolStripMenuItem
            // 
            this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
            this.onlineHelpToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.onlineHelpToolStripMenuItem.Text = "Online Help";
            // 
            // aboutUsToolStripMenuItem
            // 
            this.aboutUsToolStripMenuItem.Name = "aboutUsToolStripMenuItem";
            this.aboutUsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.aboutUsToolStripMenuItem.Text = "About Us";
            // 
            // tOOLSToolStripMenuItem
            // 
            this.tOOLSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.checkUpdatesToolStripMenuItem});
            this.tOOLSToolStripMenuItem.Name = "tOOLSToolStripMenuItem";
            this.tOOLSToolStripMenuItem.Size = new System.Drawing.Size(56, 22);
            this.tOOLSToolStripMenuItem.Text = "TOOLS";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // checkUpdatesToolStripMenuItem
            // 
            this.checkUpdatesToolStripMenuItem.Name = "checkUpdatesToolStripMenuItem";
            this.checkUpdatesToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.checkUpdatesToolStripMenuItem.Text = "Check Updates";
            // 
            // sOCIALToolStripMenuItem
            // 
            this.sOCIALToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cHATToolStripMenuItem,
            this.groupChatToolStripMenuItem,
            this.bROADCASTToolStripMenuItem});
            this.sOCIALToolStripMenuItem.Name = "sOCIALToolStripMenuItem";
            this.sOCIALToolStripMenuItem.Size = new System.Drawing.Size(59, 22);
            this.sOCIALToolStripMenuItem.Text = "SOCIAL";
            // 
            // cHATToolStripMenuItem
            // 
            this.cHATToolStripMenuItem.Name = "cHATToolStripMenuItem";
            this.cHATToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.cHATToolStripMenuItem.Text = "Chat";
            // 
            // groupChatToolStripMenuItem
            // 
            this.groupChatToolStripMenuItem.Name = "groupChatToolStripMenuItem";
            this.groupChatToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.groupChatToolStripMenuItem.Text = "Group Chat";
            // 
            // bROADCASTToolStripMenuItem
            // 
            this.bROADCASTToolStripMenuItem.Name = "bROADCASTToolStripMenuItem";
            this.bROADCASTToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.bROADCASTToolStripMenuItem.Text = "Broadcast";
            // 
            // codeLocationLabel
            // 
            this.codeLocationLabel.AutoSize = true;
            this.codeLocationLabel.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeLocationLabel.Location = new System.Drawing.Point(123, 68);
            this.codeLocationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.codeLocationLabel.Name = "codeLocationLabel";
            this.codeLocationLabel.Size = new System.Drawing.Size(148, 16);
            this.codeLocationLabel.TabIndex = 6;
            this.codeLocationLabel.Text = "Location code:";
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
            // codeLocation
            // 
            this.codeLocation.AutoSize = true;
            this.codeLocation.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeLocation.ForeColor = System.Drawing.Color.OrangeRed;
            this.codeLocation.Location = new System.Drawing.Point(261, 69);
            this.codeLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.codeLocation.Name = "codeLocation";
            this.codeLocation.Size = new System.Drawing.Size(108, 16);
            this.codeLocation.TabIndex = 8;
            this.codeLocation.Text = "loading...";
            // 
            // rating
            // 
            this.rating.AutoSize = true;
            this.rating.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rating.ForeColor = System.Drawing.Color.OrangeRed;
            this.rating.Location = new System.Drawing.Point(192, 51);
            this.rating.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rating.Name = "rating";
            this.rating.Size = new System.Drawing.Size(108, 16);
            this.rating.TabIndex = 9;
            this.rating.Text = "loading...";
            // 
            // countFilesShared
            // 
            this.countFilesShared.AutoSize = true;
            this.countFilesShared.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countFilesShared.ForeColor = System.Drawing.Color.OrangeRed;
            this.countFilesShared.Location = new System.Drawing.Point(252, 89);
            this.countFilesShared.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.countFilesShared.Name = "countFilesShared";
            this.countFilesShared.Size = new System.Drawing.Size(108, 16);
            this.countFilesShared.TabIndex = 10;
            this.countFilesShared.Text = "loading...";
            // 
            // internetState
            // 
            this.internetState.AutoSize = true;
            this.internetState.BackColor = System.Drawing.Color.Transparent;
            this.internetState.ForeColor = System.Drawing.Color.Red;
            this.internetState.Location = new System.Drawing.Point(1057, 6);
            this.internetState.Name = "internetState";
            this.internetState.Size = new System.Drawing.Size(0, 13);
            this.internetState.TabIndex = 11;
            // 
            // search
            // 
            this.search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.search.Location = new System.Drawing.Point(602, 66);
            this.search.MaximumSize = new System.Drawing.Size(500, 58);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(435, 26);
            this.search.TabIndex = 12;
            this.search.Text = "  Search here...";
            this.search.TextChanged += new System.EventHandler(this.search_TextChanged);
            // 
            // submitSearch
            // 
            this.submitSearch.Location = new System.Drawing.Point(1043, 65);
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
            this.rectangleShape3,
            this.rectangleShape2,
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(1184, 637);
            this.shapeContainer1.TabIndex = 14;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape3
            // 
            this.rectangleShape3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape3.BorderColor = System.Drawing.Color.Transparent;
            this.rectangleShape3.Location = new System.Drawing.Point(0, 617);
            this.rectangleShape3.Name = "rectangleShape3";
            this.rectangleShape3.Size = new System.Drawing.Size(2000, 20);
            // 
            // rectangleShape2
            // 
            this.rectangleShape2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.rectangleShape2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape2.BorderColor = System.Drawing.Color.Transparent;
            this.rectangleShape2.Location = new System.Drawing.Point(16, 150);
            this.rectangleShape2.Name = "rectangleShape2";
            this.rectangleShape2.Size = new System.Drawing.Size(103, 16);
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape1.BorderColor = System.Drawing.Color.Transparent;
            this.rectangleShape1.Location = new System.Drawing.Point(586, 41);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(579, 104);
            // 
            // tmpLog
            // 
            this.tmpLog.AutoSize = true;
            this.tmpLog.Location = new System.Drawing.Point(607, 98);
            this.tmpLog.Name = "tmpLog";
            this.tmpLog.Size = new System.Drawing.Size(0, 13);
            this.tmpLog.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Online now ";
            // 
            // countOnline
            // 
            this.countOnline.AutoSize = true;
            this.countOnline.Font = new System.Drawing.Font("Minion Pro", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countOnline.Location = new System.Drawing.Point(101, 204);
            this.countOnline.Name = "countOnline";
            this.countOnline.Size = new System.Drawing.Size(42, 21);
            this.countOnline.TabIndex = 17;
            this.countOnline.Text = "( 12 )";
            this.countOnline.UseMnemonic = false;
            // 
            // onlineUsers
            // 
            this.onlineUsers.Font = new System.Drawing.Font("Maiandra GD", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onlineUsers.FormattingEnabled = true;
            this.onlineUsers.ItemHeight = 15;
            this.onlineUsers.Location = new System.Drawing.Point(16, 249);
            this.onlineUsers.Name = "onlineUsers";
            this.onlineUsers.Size = new System.Drawing.Size(158, 334);
            this.onlineUsers.TabIndex = 18;
            // 
            // filterUser
            // 
            this.filterUser.Location = new System.Drawing.Point(16, 229);
            this.filterUser.Name = "filterUser";
            this.filterUser.Size = new System.Drawing.Size(158, 20);
            this.filterUser.TabIndex = 19;
            this.filterUser.Text = "search....";
            this.filterUser.Click += new System.EventHandler(this.filterUser_Click);
            this.filterUser.TextChanged += new System.EventHandler(this.filterUser_TextChanged);
            // 
            // bottombar_label1
            // 
            this.bottombar_label1.AutoSize = true;
            this.bottombar_label1.Location = new System.Drawing.Point(13, 621);
            this.bottombar_label1.Name = "bottombar_label1";
            this.bottombar_label1.Size = new System.Drawing.Size(91, 13);
            this.bottombar_label1.TabIndex = 20;
            this.bottombar_label1.Text = "refreshing user list";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 583);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(158, 10);
            this.progressBar1.TabIndex = 21;
            // 
            // bg1
            // 
            this.bg1.WorkerReportsProgress = true;
            this.bg1.WorkerSupportsCancellation = true;
            this.bg1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg1_DoWork);
            this.bg1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bg1_ProgressChanged_1);
            this.bg1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bg1_RunWorkerCompleted_1);
            // 
            // bgw_internetstate
            // 
            this.bgw_internetstate.WorkerSupportsCancellation = true;
            this.bgw_internetstate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_internetstate_DoWork);
            this.bgw_internetstate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_internetstate_RunWorkerCompleted);
            // 
            // lStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 637);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.bottombar_label1);
            this.Controls.Add(this.filterUser);
            this.Controls.Add(this.onlineUsers);
            this.Controls.Add(this.countOnline);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tmpLog);
            this.Controls.Add(this.submitSearch);
            this.Controls.Add(this.search);
            this.Controls.Add(this.internetState);
            this.Controls.Add(this.countFilesShared);
            this.Controls.Add(this.rating);
            this.Controls.Add(this.codeLocation);
            this.Controls.Add(this.linkChangeImage);
            this.Controls.Add(this.codeLocationLabel);
            this.Controls.Add(this.ratingLabel);
            this.Controls.Add(this.countFileSharedLabel);
            this.Controls.Add(this.nname);
            this.Controls.Add(this.uname);
            this.Controls.Add(this.profilepic);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.shapeContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximumSize = new System.Drawing.Size(1200, 676);
            this.MinimumSize = new System.Drawing.Size(700, 395);
            this.Name = "lStore";
            this.Text = "lStore: LAN Sharing simplified !";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.profilepic)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox profilepic;
        private System.Windows.Forms.Label uname;
        private System.Windows.Forms.Label nname;
        private System.Windows.Forms.Label countFileSharedLabel;
        private System.Windows.Forms.Label ratingLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fILEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nEWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPENToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eDITToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vIEWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hELPToolStripMenuItem;
        private System.Windows.Forms.Label codeLocationLabel;
        private System.Windows.Forms.LinkLabel linkChangeImage;
        private System.Windows.Forms.Label codeLocation;
        private System.Windows.Forms.Label rating;
        private System.Windows.Forms.Label countFilesShared;
        private System.Windows.Forms.Label internetState;
        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.Button submitSearch;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private System.Windows.Forms.Label tmpLog;
        private System.Windows.Forms.ToolStripMenuItem tOOLSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label countOnline;
        private System.Windows.Forms.ListBox onlineUsers;
        private System.Windows.Forms.TextBox filterUser;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape3;
        private System.Windows.Forms.Label bottombar_label1;
        private System.Windows.Forms.ToolStripMenuItem rESETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutUsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sOCIALToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cHATToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupChatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bROADCASTToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker bg1;
        private System.ComponentModel.BackgroundWorker bgw_internetstate;
    }
}

