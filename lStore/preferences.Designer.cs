namespace lStore
{
    partial class preferences
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.statistics = new System.Windows.Forms.TabPage();
            this.stats_internetusage = new System.Windows.Forms.CheckBox();
            this.stats_bugs = new System.Windows.Forms.CheckBox();
            this.stats_usage = new System.Windows.Forms.CheckBox();
            this.stats_search = new System.Windows.Forms.CheckBox();
            this.frequency = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.freq_sync = new System.Windows.Forms.NumericUpDown();
            this.freq_upload = new System.Windows.Forms.NumericUpDown();
            this.freq_crawl = new System.Windows.Forms.NumericUpDown();
            this.addons = new System.Windows.Forms.TabPage();
            this.addon_searchsuggestion = new System.Windows.Forms.CheckBox();
            this.addon_imdb = new System.Windows.Forms.CheckBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_apply = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.check_internet = new System.Windows.Forms.CheckBox();
            this.donelabel = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.input_downloaddirec = new System.Windows.Forms.TextBox();
            this.browsebutton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabs.SuspendLayout();
            this.statistics.SuspendLayout();
            this.frequency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.freq_sync)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.freq_upload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.freq_crawl)).BeginInit();
            this.addons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.statistics);
            this.tabs.Controls.Add(this.frequency);
            this.tabs.Controls.Add(this.addons);
            this.tabs.Location = new System.Drawing.Point(1, 1);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(508, 271);
            this.tabs.TabIndex = 0;
            // 
            // statistics
            // 
            this.statistics.Controls.Add(this.stats_internetusage);
            this.statistics.Controls.Add(this.stats_bugs);
            this.statistics.Controls.Add(this.stats_usage);
            this.statistics.Controls.Add(this.stats_search);
            this.statistics.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statistics.Location = new System.Drawing.Point(4, 22);
            this.statistics.Name = "statistics";
            this.statistics.Padding = new System.Windows.Forms.Padding(3);
            this.statistics.Size = new System.Drawing.Size(500, 245);
            this.statistics.TabIndex = 0;
            this.statistics.Text = "Statistics";
            this.statistics.UseVisualStyleBackColor = true;
            // 
            // stats_internetusage
            // 
            this.stats_internetusage.AutoSize = true;
            this.stats_internetusage.Location = new System.Drawing.Point(17, 103);
            this.stats_internetusage.Name = "stats_internetusage";
            this.stats_internetusage.Size = new System.Drawing.Size(227, 23);
            this.stats_internetusage.TabIndex = 3;
            this.stats_internetusage.Text = "Enable Internet Usage Loggign";
            this.stats_internetusage.UseVisualStyleBackColor = true;
            this.stats_internetusage.CheckedChanged += new System.EventHandler(this.stats_internetusage_CheckedChanged);
            // 
            // stats_bugs
            // 
            this.stats_bugs.AutoSize = true;
            this.stats_bugs.Location = new System.Drawing.Point(17, 74);
            this.stats_bugs.Name = "stats_bugs";
            this.stats_bugs.Size = new System.Drawing.Size(238, 23);
            this.stats_bugs.TabIndex = 2;
            this.stats_bugs.Text = "Enable logging Bugs & Exceptions";
            this.stats_bugs.UseVisualStyleBackColor = true;
            this.stats_bugs.CheckedChanged += new System.EventHandler(this.stats_bugs_CheckedChanged);
            // 
            // stats_usage
            // 
            this.stats_usage.AutoSize = true;
            this.stats_usage.Location = new System.Drawing.Point(17, 45);
            this.stats_usage.Name = "stats_usage";
            this.stats_usage.Size = new System.Drawing.Size(232, 23);
            this.stats_usage.TabIndex = 1;
            this.stats_usage.Text = "Enable logging Usage Statistics";
            this.stats_usage.UseVisualStyleBackColor = true;
            this.stats_usage.CheckedChanged += new System.EventHandler(this.stats_usage_CheckedChanged);
            // 
            // stats_search
            // 
            this.stats_search.AutoSize = true;
            this.stats_search.Location = new System.Drawing.Point(17, 16);
            this.stats_search.Name = "stats_search";
            this.stats_search.Size = new System.Drawing.Size(182, 23);
            this.stats_search.TabIndex = 0;
            this.stats_search.Text = "Enable search statistics";
            this.stats_search.UseVisualStyleBackColor = true;
            this.stats_search.CheckedChanged += new System.EventHandler(this.stats_search_CheckedChanged);
            // 
            // frequency
            // 
            this.frequency.Controls.Add(this.label3);
            this.frequency.Controls.Add(this.label2);
            this.frequency.Controls.Add(this.label1);
            this.frequency.Controls.Add(this.freq_sync);
            this.frequency.Controls.Add(this.freq_upload);
            this.frequency.Controls.Add(this.freq_crawl);
            this.frequency.Location = new System.Drawing.Point(4, 22);
            this.frequency.Name = "frequency";
            this.frequency.Padding = new System.Windows.Forms.Padding(3);
            this.frequency.Size = new System.Drawing.Size(500, 245);
            this.frequency.TabIndex = 1;
            this.frequency.Text = "frequency";
            this.frequency.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(143, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Stats feeding frequency";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(143, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Synchronise frequency";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(143, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Scan frequency";
            // 
            // freq_sync
            // 
            this.freq_sync.Location = new System.Drawing.Point(17, 51);
            this.freq_sync.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.freq_sync.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.freq_sync.Name = "freq_sync";
            this.freq_sync.Size = new System.Drawing.Size(120, 21);
            this.freq_sync.TabIndex = 2;
            this.freq_sync.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.freq_sync.ValueChanged += new System.EventHandler(this.freq_sync_ValueChanged);
            // 
            // freq_upload
            // 
            this.freq_upload.Location = new System.Drawing.Point(17, 78);
            this.freq_upload.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.freq_upload.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.freq_upload.Name = "freq_upload";
            this.freq_upload.Size = new System.Drawing.Size(120, 21);
            this.freq_upload.TabIndex = 1;
            this.freq_upload.Value = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.freq_upload.ValueChanged += new System.EventHandler(this.freq_upload_ValueChanged);
            // 
            // freq_crawl
            // 
            this.freq_crawl.Location = new System.Drawing.Point(17, 24);
            this.freq_crawl.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.freq_crawl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.freq_crawl.Name = "freq_crawl";
            this.freq_crawl.Size = new System.Drawing.Size(120, 21);
            this.freq_crawl.TabIndex = 0;
            this.freq_crawl.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.freq_crawl.ValueChanged += new System.EventHandler(this.freq_crawl_ValueChanged);
            // 
            // addons
            // 
            this.addons.Controls.Add(this.addon_searchsuggestion);
            this.addons.Controls.Add(this.addon_imdb);
            this.addons.Location = new System.Drawing.Point(4, 22);
            this.addons.Name = "addons";
            this.addons.Size = new System.Drawing.Size(500, 245);
            this.addons.TabIndex = 2;
            this.addons.Text = "Addons";
            this.addons.UseVisualStyleBackColor = true;
            // 
            // addon_searchsuggestion
            // 
            this.addon_searchsuggestion.AutoSize = true;
            this.addon_searchsuggestion.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addon_searchsuggestion.Location = new System.Drawing.Point(17, 56);
            this.addon_searchsuggestion.Name = "addon_searchsuggestion";
            this.addon_searchsuggestion.Size = new System.Drawing.Size(224, 23);
            this.addon_searchsuggestion.TabIndex = 2;
            this.addon_searchsuggestion.Text = "Enable Live Search Suggestion";
            this.addon_searchsuggestion.UseVisualStyleBackColor = true;
            this.addon_searchsuggestion.CheckedChanged += new System.EventHandler(this.addon_searchsuggestion_CheckedChanged);
            // 
            // addon_imdb
            // 
            this.addon_imdb.AutoSize = true;
            this.addon_imdb.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addon_imdb.Location = new System.Drawing.Point(17, 27);
            this.addon_imdb.Name = "addon_imdb";
            this.addon_imdb.Size = new System.Drawing.Size(314, 23);
            this.addon_imdb.TabIndex = 1;
            this.addon_imdb.Text = "Enable IMDB  Movie Information Integration";
            this.addon_imdb.UseVisualStyleBackColor = true;
            this.addon_imdb.CheckedChanged += new System.EventHandler(this.addon_imdb_CheckedChanged);
            // 
            // button_save
            // 
            this.button_save.BackColor = System.Drawing.Color.LightGray;
            this.button_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_save.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_save.Location = new System.Drawing.Point(410, 479);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(88, 33);
            this.button_save.TabIndex = 11;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = false;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_apply
            // 
            this.button_apply.BackColor = System.Drawing.Color.LightGray;
            this.button_apply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_apply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_apply.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_apply.Location = new System.Drawing.Point(316, 479);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(88, 33);
            this.button_apply.TabIndex = 12;
            this.button_apply.Text = "Apply";
            this.button_apply.UseVisualStyleBackColor = false;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.BackColor = System.Drawing.Color.LightCoral;
            this.button_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_cancel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button_cancel.Location = new System.Drawing.Point(223, 479);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(88, 33);
            this.button_cancel.TabIndex = 13;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = false;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // check_internet
            // 
            this.check_internet.AutoSize = true;
            this.check_internet.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_internet.Location = new System.Drawing.Point(12, 278);
            this.check_internet.Name = "check_internet";
            this.check_internet.Size = new System.Drawing.Size(264, 23);
            this.check_internet.TabIndex = 4;
            this.check_internet.Text = "Use Internet other than LAN internet";
            this.check_internet.UseVisualStyleBackColor = true;
            this.check_internet.CheckedChanged += new System.EventHandler(this.check_internet_CheckedChanged);
            // 
            // donelabel
            // 
            this.donelabel.AutoSize = true;
            this.donelabel.BackColor = System.Drawing.Color.LightCoral;
            this.donelabel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.donelabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.donelabel.Location = new System.Drawing.Point(8, 483);
            this.donelabel.Name = "donelabel";
            this.donelabel.Size = new System.Drawing.Size(56, 23);
            this.donelabel.TabIndex = 6;
            this.donelabel.Text = "DONE";
            this.donelabel.Visible = false;
            // 
            // input_downloaddirec
            // 
            this.input_downloaddirec.Enabled = false;
            this.input_downloaddirec.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input_downloaddirec.Location = new System.Drawing.Point(12, 306);
            this.input_downloaddirec.Name = "input_downloaddirec";
            this.input_downloaddirec.Size = new System.Drawing.Size(208, 23);
            this.input_downloaddirec.TabIndex = 14;
            // 
            // browsebutton
            // 
            this.browsebutton.BackColor = System.Drawing.Color.LightCoral;
            this.browsebutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.browsebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browsebutton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.browsebutton.Location = new System.Drawing.Point(223, 306);
            this.browsebutton.Name = "browsebutton";
            this.browsebutton.Size = new System.Drawing.Size(65, 25);
            this.browsebutton.TabIndex = 15;
            this.browsebutton.Text = "Browse";
            this.browsebutton.UseVisualStyleBackColor = false;
            this.browsebutton.Click += new System.EventHandler(this.browsebutton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(293, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Download folder";
            // 
            // preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(510, 524);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.browsebutton);
            this.Controls.Add(this.input_downloaddirec);
            this.Controls.Add(this.donelabel);
            this.Controls.Add(this.check_internet);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_apply);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.tabs);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(526, 563);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(526, 563);
            this.Name = "preferences";
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.preferences_Load);
            this.tabs.ResumeLayout(false);
            this.statistics.ResumeLayout(false);
            this.statistics.PerformLayout();
            this.frequency.ResumeLayout(false);
            this.frequency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.freq_sync)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.freq_upload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.freq_crawl)).EndInit();
            this.addons.ResumeLayout(false);
            this.addons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage statistics;
        private System.Windows.Forms.TabPage frequency;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TabPage addons;
        private System.Windows.Forms.CheckBox stats_internetusage;
        private System.Windows.Forms.CheckBox stats_bugs;
        private System.Windows.Forms.CheckBox stats_usage;
        private System.Windows.Forms.CheckBox stats_search;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown freq_sync;
        private System.Windows.Forms.NumericUpDown freq_upload;
        private System.Windows.Forms.NumericUpDown freq_crawl;
        private System.Windows.Forms.CheckBox addon_searchsuggestion;
        private System.Windows.Forms.CheckBox addon_imdb;
        private System.Windows.Forms.CheckBox check_internet;
        private System.Windows.Forms.Label donelabel;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox input_downloaddirec;
        private System.Windows.Forms.Button browsebutton;
        private System.Windows.Forms.Label label4;
    }
}