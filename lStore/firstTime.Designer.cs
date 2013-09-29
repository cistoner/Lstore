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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-2, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(951, 194);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Century Schoolbook", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 262);
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
            this.stepCount.Location = new System.Drawing.Point(12, 307);
            this.stepCount.Name = "stepCount";
            this.stepCount.Size = new System.Drawing.Size(192, 59);
            this.stepCount.TabIndex = 2;
            this.stepCount.Text = "0 of 10";
            // 
            // progress
            // 
            this.progress.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.progress.Location = new System.Drawing.Point(331, 344);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(478, 22);
            this.progress.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(325, 309);
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
            this.shapeContainer1.Size = new System.Drawing.Size(947, 439);
            this.shapeContainer1.TabIndex = 5;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BackColor = System.Drawing.Color.PeachPuff;
            this.rectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape1.BorderColor = System.Drawing.Color.Transparent;
            this.rectangleShape1.Location = new System.Drawing.Point(0, 412);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(947, 27);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(5, 417);
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
            this.localSyncLabel.Location = new System.Drawing.Point(760, 415);
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
            this.proxyLabel.Location = new System.Drawing.Point(18, 205);
            this.proxyLabel.Name = "proxyLabel";
            this.proxyLabel.Size = new System.Drawing.Size(137, 20);
            this.proxyLabel.TabIndex = 8;
            this.proxyLabel.Text = "PROXY: disabled";
            // 
            // firstTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(947, 439);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(963, 478);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(963, 478);
            this.Name = "firstTime";
            this.Text = "Syncronising with server";
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
    }
}