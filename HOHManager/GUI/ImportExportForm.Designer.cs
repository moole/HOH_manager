namespace HOHManager
{
    partial class ImportExportForm
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
            this.exportStateButton = new System.Windows.Forms.Button();
            this.importTeamsButton = new System.Windows.Forms.Button();
            this.exportSMSButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.exportTeamsButton = new System.Windows.Forms.Button();
            this.exportPuzzlesButton = new System.Windows.Forms.Button();
            this.importPuzzlesButton = new System.Windows.Forms.Button();
            this.exportGameRulesButton = new System.Windows.Forms.Button();
            this.importGamesRulesButton = new System.Windows.Forms.Button();
            this.clearGameAfterImport = new System.Windows.Forms.CheckBox();
            this.importSMSButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exportStateButton
            // 
            this.exportStateButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.exportStateButton.Location = new System.Drawing.Point(3, 160);
            this.exportStateButton.Name = "exportStateButton";
            this.exportStateButton.Size = new System.Drawing.Size(234, 33);
            this.exportStateButton.TabIndex = 9;
            this.exportStateButton.Text = "Exportovat aktuální stav";
            this.exportStateButton.Click += new System.EventHandler(this.exportStateButton_Click);
            // 
            // importTeamsButton
            // 
            this.importTeamsButton.Location = new System.Drawing.Point(3, 4);
            this.importTeamsButton.Name = "importTeamsButton";
            this.importTeamsButton.Size = new System.Drawing.Size(114, 33);
            this.importTeamsButton.TabIndex = 1;
            this.importTeamsButton.Text = "Import týmů";
            this.importTeamsButton.Click += new System.EventHandler(this.importTeamsButton_Click);
            // 
            // exportSMSButton
            // 
            this.exportSMSButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportSMSButton.Location = new System.Drawing.Point(123, 121);
            this.exportSMSButton.Name = "exportSMSButton";
            this.exportSMSButton.Size = new System.Drawing.Size(114, 33);
            this.exportSMSButton.TabIndex = 8;
            this.exportSMSButton.Text = "Export SMS";
            this.exportSMSButton.Click += new System.EventHandler(this.exportSMSButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // exportTeamsButton
            // 
            this.exportTeamsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportTeamsButton.Location = new System.Drawing.Point(123, 4);
            this.exportTeamsButton.Name = "exportTeamsButton";
            this.exportTeamsButton.Size = new System.Drawing.Size(114, 33);
            this.exportTeamsButton.TabIndex = 2;
            this.exportTeamsButton.Text = "Export týmů";
            this.exportTeamsButton.Click += new System.EventHandler(this.exportTeamsButton_Click);
            // 
            // exportPuzzlesButton
            // 
            this.exportPuzzlesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportPuzzlesButton.Location = new System.Drawing.Point(123, 43);
            this.exportPuzzlesButton.Name = "exportPuzzlesButton";
            this.exportPuzzlesButton.Size = new System.Drawing.Size(114, 33);
            this.exportPuzzlesButton.TabIndex = 4;
            this.exportPuzzlesButton.Text = "Export šifer";
            this.exportPuzzlesButton.Click += new System.EventHandler(this.exportPuzzlesButton_Click);
            // 
            // importPuzzlesButton
            // 
            this.importPuzzlesButton.Location = new System.Drawing.Point(3, 43);
            this.importPuzzlesButton.Name = "importPuzzlesButton";
            this.importPuzzlesButton.Size = new System.Drawing.Size(114, 33);
            this.importPuzzlesButton.TabIndex = 3;
            this.importPuzzlesButton.Text = "Import šifer";
            this.importPuzzlesButton.Click += new System.EventHandler(this.importPuzzlesButton_Click);
            // 
            // exportGameRulesButton
            // 
            this.exportGameRulesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportGameRulesButton.Location = new System.Drawing.Point(123, 82);
            this.exportGameRulesButton.Name = "exportGameRulesButton";
            this.exportGameRulesButton.Size = new System.Drawing.Size(114, 33);
            this.exportGameRulesButton.TabIndex = 6;
            this.exportGameRulesButton.Text = "Export pravidel";
            this.exportGameRulesButton.Click += new System.EventHandler(this.exportGameRulesButton_Click);
            // 
            // importGamesRulesButton
            // 
            this.importGamesRulesButton.Location = new System.Drawing.Point(3, 82);
            this.importGamesRulesButton.Name = "importGamesRulesButton";
            this.importGamesRulesButton.Size = new System.Drawing.Size(114, 33);
            this.importGamesRulesButton.TabIndex = 5;
            this.importGamesRulesButton.Text = "Import pravidel";
            this.importGamesRulesButton.Click += new System.EventHandler(this.importGamesRulesButton_Click);
            // 
            // clearGameAfterImport
            // 
            this.clearGameAfterImport.Checked = true;
            this.clearGameAfterImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clearGameAfterImport.Enabled = false;
            this.clearGameAfterImport.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.clearGameAfterImport.Location = new System.Drawing.Point(3, 199);
            this.clearGameAfterImport.Name = "clearGameAfterImport";
            this.clearGameAfterImport.Size = new System.Drawing.Size(234, 19);
            this.clearGameAfterImport.TabIndex = 10;
            this.clearGameAfterImport.Text = "Zahodit stav hry při importu nových dat";
            // 
            // importSMSButton
            // 
            this.importSMSButton.Location = new System.Drawing.Point(3, 121);
            this.importSMSButton.Name = "importSMSButton";
            this.importSMSButton.Size = new System.Drawing.Size(114, 33);
            this.importSMSButton.TabIndex = 7;
            this.importSMSButton.Text = "Import SMS";
            this.importSMSButton.Click += new System.EventHandler(this.importSMSButton_Click);
            // 
            // ImportExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.importSMSButton);
            this.Controls.Add(this.clearGameAfterImport);
            this.Controls.Add(this.exportGameRulesButton);
            this.Controls.Add(this.importGamesRulesButton);
            this.Controls.Add(this.exportPuzzlesButton);
            this.Controls.Add(this.importPuzzlesButton);
            this.Controls.Add(this.exportTeamsButton);
            this.Controls.Add(this.exportSMSButton);
            this.Controls.Add(this.exportStateButton);
            this.Controls.Add(this.importTeamsButton);
            this.Name = "ImportExportForm";
            this.Text = "HOH Import Export";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exportStateButton;
        private System.Windows.Forms.Button importTeamsButton;
        private System.Windows.Forms.Button exportSMSButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button exportTeamsButton;
        private System.Windows.Forms.Button exportPuzzlesButton;
        private System.Windows.Forms.Button importPuzzlesButton;
        private System.Windows.Forms.Button exportGameRulesButton;
        private System.Windows.Forms.Button importGamesRulesButton;
        private System.Windows.Forms.CheckBox clearGameAfterImport;
        private System.Windows.Forms.Button importSMSButton;
    }
}