namespace lStore
{
    partial class firstTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(firstTime));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.stepCount = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.label3 = new System.Windows.Forms.Label();
            this.onlinesync = new System.ComponentModel.BackgroundWorker();
            this.localsync = new System.ComponentModel.BackgroundWorker();
            this.localSyncLabel = new System.Windows.Forms.Label();
            this.proxyLabel = new System.Windows.Forms.Label();
            this.infoSender = new System.ComponentModel.BackgroundWorker();
            this.finishbutton = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.closebutton = new System.Windows.Forms.Button();
            this.localsyncprogressbar = new System.Windows.Forms.ProgressBar();
            this.lslabel = new System.Windows.Forms.Label();
            this.finisher = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(963, 201);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Century Schoolbook", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 293);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "Step";
            // 
            // stepCount
            // 
            this.stepCount.AutoSize = true;
            this.stepCount.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.stepCount.Font = new System.Drawing.Font("Lucida Sans Unicode", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepCount.Location = new System.Drawing.Point(14, 338);
            this.stepCount.Name = "stepCount";
            this.stepCount.Size = new System.Drawing.Size(192, 59);
            this.stepCount.TabIndex = 2;
            this.stepCount.Text = "0 of 10";
            // 
            // progress
            // 
            this.progress.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.progress.Location = new System.Drawing.Point(333, 375);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(478, 22);
            this.progress.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(327, 340);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(603, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tip: Always rate users on basis of contents for better performance";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(963, 478);
            this.shapeContainer1.TabIndex = 5;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BackColor = System.Drawing.Color.PeachPuff;
            this.rectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape1.BorderColor = System.Drawing.Color.Transparent;
            this.rectangleShape1.Location = new System.Drawing.Point(0, 449);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(963, 27);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(5, 454);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(548, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Do not close this window, lStore would not work untill you have syncronized data " +
    "with server!!";
            // 
            // onlinesync
            // 
            this.onlinesync.WorkerReportsProgress = true;
            this.onlinesync.WorkerSupportsCancellation = true;
            this.onlinesync.DoWork += new System.ComponentModel.DoWorkEventHandler(this.onlinesync_DoWork);
            this.onlinesync.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.onlinesync_ProgressChanged);
            // 
            // localsync
            // 
            this.localsync.WorkerReportsProgress = true;
            this.localsync.WorkerSupportsCancellation = true;
            this.localsync.DoWork += new System.ComponentModel.DoWorkEventHandler(this.localsync_DoWork);
            this.localsync.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.localsync_ProgressChanged);
            this.localsync.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.localsync_RunWorkerCompleted);
            // 
            // localSyncLabel
            // 
            this.localSyncLabel.AutoSize = true;
            this.localSyncLabel.BackColor = System.Drawing.Color.Transparent;
            this.localSyncLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.localSyncLabel.ForeColor = System.Drawing.Color.DarkCyan;
            this.localSyncLabel.Location = new System.Drawing.Point(696, 452);
            this.localSyncLabel.Name = "localSyncLabel";
            this.localSyncLabel.Size = new System.Drawing.Size(182, 20);
            this.localSyncLabel.TabIndex = 7;
            this.localSyncLabel.Text = "loading data to local files";
            // 
            // proxyLabel
            // 
            this.proxyLabel.AutoSize = true;
            this.proxyLabel.BackColor = System.Drawing.Color.LavenderBlush;
            this.proxyLabel.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.proxyLabel.Location = new System.Drawing.Point(22, 214);
            this.proxyLabel.Name = "proxyLabel";
            this.proxyLabel.Size = new System.Drawing.Size(137, 20);
            this.proxyLabel.TabIndex = 8;
            this.proxyLabel.Text = "PROXY: disabled";
            // 
            // infoSender
            // 
            this.infoSender.WorkerReportsProgress = true;
            this.infoSender.WorkerSupportsCancellation = true;
            this.infoSender.DoWork += new System.ComponentModel.DoWorkEventHandler(this.infoSender_DoWork);
            this.infoSender.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.infoSender_ProgressChanged);
            this.infoSender.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.infoSender_RunWorkerCompleted);
            // 
            // finishbutton
            // 
            this.finishbutton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.finishbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.finishbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finishbutton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.finishbutton.Location = new System.Drawing.Point(736, 403);
            this.finishbutton.Name = "finishbutton";
            this.finishbutton.Size = new System.Drawing.Size(75, 33);
            this.finishbutton.TabIndex = 1;
            this.finishbutton.Text = "finish";
            this.finishbutton.UseVisualStyleBackColor = false;
            this.finishbutton.Visible = false;
            this.finishbutton.Click += new System.EventHandler(this.finishbutton_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonExit.Location = new System.Drawing.Point(775, 403);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(145, 33);
            this.buttonExit.TabIndex = 10;
            this.buttonExit.Text = "Click to Exit";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Visible = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // closebutton
            // 
            this.closebutton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.closebutton.ForeColor = System.Drawing.Color.Red;
            this.closebutton.Location = new System.Drawing.Point(936, 4);
            this.closebutton.Name = "closebutton";
            this.closebutton.Size = new System.Drawing.Size(21, 23);
            this.closebutton.TabIndex = 11;
            this.closebutton.Text = "X";
            this.closebutton.UseVisualStyleBackColor = false;
            this.closebutton.Click += new System.EventHandler(this.closebutton_Click);
            // 
            // localsyncprogressbar
            // 
            this.localsyncprogressbar.Location = new System.Drawing.Point(14, 404);
            this.localsyncprogressbar.Name = "localsyncprogressbar";
            this.localsyncprogressbar.Size = new System.Drawing.Size(135, 10);
            this.localsyncprogressbar.TabIndex = 12;
            this.localsyncprogressbar.Visible = false;
            // 
            // lslabel
            // 
            this.lslabel.AutoSize = true;
            this.lslabel.Location = new System.Drawing.Point(155, 401);
            this.lslabel.Name = "lslabel";
            this.lslabel.Size = new System.Drawing.Size(48, 13);
            this.lslabel.TabIndex = 13;
            this.lslabel.Text = "[ 1 / 10 ]";
            this.lslabel.Visible = false;
            // 
            // finisher
            // 
            this.finisher.WorkerReportsProgress = true;
            this.finisher.WorkerSupportsCancellation = true;
            this.finisher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.finisher_DoWork);
            this.finisher.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.finisher_ProgressChanged);
            this.finisher.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.finisher_RunWorkerCompleted);
            // 
            // firstTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(963, 478);
            this.Controls.Add(this.lslabel);
            this.Controls.Add(this.localsyncprogressbar);
            this.Controls.Add(this.closebutton);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.finishbutton);
            this.Controls.Add(this.proxyLabel);
            this.Controls.Add(this.localSyncLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.stepCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.shapeContainer1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(963, 478);
            this.MinimumSize = new System.Drawing.Size(963, 478);
            this.Name = "firstTime";
            this.Text = "Syncronising with server";
            this.Load += new System.EventHandler(this.firstTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label stepCount;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label label2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker onlinesync;
        private System.ComponentModel.BackgroundWorker localsync;
        private System.Windows.Forms.Label localSyncLabel;
        private System.Windows.Forms.Label proxyLabel;
        private System.ComponentModel.BackgroundWorker infoSender;
        private System.Windows.Forms.Button finishbutton;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button closebutton;
        private System.Windows.Forms.ProgressBar localsyncprogressbar;
        private System.Windows.Forms.Label lslabel;
        private System.ComponentModel.BackgroundWorker finisher;
    }
}