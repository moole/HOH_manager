namespace HOHManager
{
    partial class MainForm
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
            this.Dispose2();
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.actualStateButton = new System.Windows.Forms.Button();
            this.eventsButton = new System.Windows.Forms.Button();
            this.teamsButton = new System.Windows.Forms.Button();
            this.puzzlesButton = new System.Windows.Forms.Button();
            this.importExportButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // actualStateButton
            // 
            this.actualStateButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.actualStateButton.Location = new System.Drawing.Point(25, 57);
            this.actualStateButton.Name = "actualStateButton";
            this.actualStateButton.Size = new System.Drawing.Size(190, 33);
            this.actualStateButton.TabIndex = 0;
            this.actualStateButton.Text = "Aktuální pořadí";
            this.actualStateButton.Click += new System.EventHandler(this.actualStateButton_Click);
            // 
            // eventsButton
            // 
            this.eventsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.eventsButton.Location = new System.Drawing.Point(25, 174);
            this.eventsButton.Name = "eventsButton";
            this.eventsButton.Size = new System.Drawing.Size(190, 33);
            this.eventsButton.TabIndex = 3;
            this.eventsButton.Text = "Události a problémy";
            this.eventsButton.Click += new System.EventHandler(this.eventsButton_Click);
            // 
            // teamsButton
            // 
            this.teamsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.teamsButton.Location = new System.Drawing.Point(25, 135);
            this.teamsButton.Name = "teamsButton";
            this.teamsButton.Size = new System.Drawing.Size(190, 33);
            this.teamsButton.TabIndex = 2;
            this.teamsButton.Text = "Týmy";
            this.teamsButton.Click += new System.EventHandler(this.teamsButton_Click);
            // 
            // puzzlesButton
            // 
            this.puzzlesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.puzzlesButton.Location = new System.Drawing.Point(25, 96);
            this.puzzlesButton.Name = "puzzlesButton";
            this.puzzlesButton.Size = new System.Drawing.Size(190, 33);
            this.puzzlesButton.TabIndex = 1;
            this.puzzlesButton.Text = "Šifry";
            this.puzzlesButton.Click += new System.EventHandler(this.puzzlesButton_Click);
            // 
            // importExportButton
            // 
            this.importExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.importExportButton.Location = new System.Drawing.Point(25, 213);
            this.importExportButton.Name = "importExportButton";
            this.importExportButton.Size = new System.Drawing.Size(190, 33);
            this.importExportButton.TabIndex = 4;
            this.importExportButton.Text = "Export a Import dat";
            this.importExportButton.Click += new System.EventHandler(this.importExportButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(234, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.importExportButton);
            this.Controls.Add(this.puzzlesButton);
            this.Controls.Add(this.teamsButton);
            this.Controls.Add(this.eventsButton);
            this.Controls.Add(this.actualStateButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MainForm";
            this.Text = "HOH";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button actualStateButton;
        private System.Windows.Forms.Button eventsButton;
        private System.Windows.Forms.Button teamsButton;
        private System.Windows.Forms.Button puzzlesButton;
        private System.Windows.Forms.Button importExportButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

