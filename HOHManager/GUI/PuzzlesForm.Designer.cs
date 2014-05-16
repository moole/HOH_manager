namespace HOHManager
{
    partial class PuzzlesForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.timesTab = new System.Windows.Forms.TabPage();
            this.timesListView = new System.Windows.Forms.ListView();
            this.num = new System.Windows.Forms.ColumnHeader();
            this.teamName = new System.Windows.Forms.ColumnHeader();
            this.arriveTime = new System.Windows.Forms.ColumnHeader();
            this.departTime = new System.Windows.Forms.ColumnHeader();
            this.spending = new System.Windows.Forms.ColumnHeader();
            this.pointsTab = new System.Windows.Forms.TabPage();
            this.pointsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.hintsTab = new System.Windows.Forms.TabPage();
            this.hintsListView = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.descTab = new System.Windows.Forms.TabPage();
            this.descTextBox = new System.Windows.Forms.TextBox();
            this.puzzlesComboBox = new System.Windows.Forms.ComboBox();
            this.formUpdateTimer = new System.Windows.Forms.Timer();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.timesTab.SuspendLayout();
            this.pointsTab.SuspendLayout();
            this.hintsTab.SuspendLayout();
            this.descTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 266);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.timesTab);
            this.tabControl1.Controls.Add(this.pointsTab);
            this.tabControl1.Controls.Add(this.hintsTab);
            this.tabControl1.Controls.Add(this.descTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.None;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 266);
            this.tabControl1.TabIndex = 0;
            // 
            // timesTab
            // 
            this.timesTab.Controls.Add(this.timesListView);
            this.timesTab.Location = new System.Drawing.Point(0, 0);
            this.timesTab.Name = "timesTab";
            this.timesTab.Size = new System.Drawing.Size(240, 243);
            this.timesTab.Text = "Časy";
            // 
            // timesListView
            // 
            this.timesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.timesListView.Columns.Add(this.num);
            this.timesListView.Columns.Add(this.teamName);
            this.timesListView.Columns.Add(this.arriveTime);
            this.timesListView.Columns.Add(this.departTime);
            this.timesListView.Columns.Add(this.spending);
            this.timesListView.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.timesListView.FullRowSelect = true;
            this.timesListView.Location = new System.Drawing.Point(3, 0);
            this.timesListView.Name = "timesListView";
            this.timesListView.Size = new System.Drawing.Size(234, 242);
            this.timesListView.TabIndex = 0;
            this.timesListView.View = System.Windows.Forms.View.Details;
            this.timesListView.ItemActivate += new System.EventHandler(this.timesListView_ItemActivate);
            // 
            // num
            // 
            this.num.Text = "Poř.";
            this.num.Width = 24;
            // 
            // teamName
            // 
            this.teamName.Text = "Tým";
            this.teamName.Width = 63;
            // 
            // arriveTime
            // 
            this.arriveTime.Text = "Příchod";
            this.arriveTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.arriveTime.Width = 48;
            // 
            // departTime
            // 
            this.departTime.Text = "Odchod";
            this.departTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.departTime.Width = 48;
            // 
            // spending
            // 
            this.spending.Text = "Doba";
            this.spending.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.spending.Width = 48;
            // 
            // pointsTab
            // 
            this.pointsTab.Controls.Add(this.pointsListView);
            this.pointsTab.Location = new System.Drawing.Point(0, 0);
            this.pointsTab.Name = "pointsTab";
            this.pointsTab.Size = new System.Drawing.Size(240, 243);
            this.pointsTab.Text = "Body";
            // 
            // pointsListView
            // 
            this.pointsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pointsListView.Columns.Add(this.columnHeader1);
            this.pointsListView.Columns.Add(this.columnHeader2);
            this.pointsListView.Columns.Add(this.columnHeader3);
            this.pointsListView.Columns.Add(this.columnHeader4);
            this.pointsListView.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.pointsListView.FullRowSelect = true;
            this.pointsListView.Location = new System.Drawing.Point(3, 0);
            this.pointsListView.Name = "pointsListView";
            this.pointsListView.Size = new System.Drawing.Size(234, 242);
            this.pointsListView.TabIndex = 1;
            this.pointsListView.View = System.Windows.Forms.View.Details;
            this.pointsListView.ItemActivate += new System.EventHandler(this.pointsListView_ItemActivate);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Poř.";
            this.columnHeader1.Width = 24;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tým";
            this.columnHeader2.Width = 79;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Odp.";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Body";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 48;
            // 
            // hintsTab
            // 
            this.hintsTab.Controls.Add(this.hintsListView);
            this.hintsTab.Location = new System.Drawing.Point(0, 0);
            this.hintsTab.Name = "hintsTab";
            this.hintsTab.Size = new System.Drawing.Size(240, 243);
            this.hintsTab.Text = "Nápovědy";
            // 
            // hintsListView
            // 
            this.hintsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hintsListView.Columns.Add(this.columnHeader5);
            this.hintsListView.Columns.Add(this.columnHeader6);
            this.hintsListView.Columns.Add(this.columnHeader7);
            this.hintsListView.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.hintsListView.FullRowSelect = true;
            this.hintsListView.Location = new System.Drawing.Point(3, 0);
            this.hintsListView.Name = "hintsListView";
            this.hintsListView.Size = new System.Drawing.Size(234, 242);
            this.hintsListView.TabIndex = 1;
            this.hintsListView.View = System.Windows.Forms.View.Details;
            this.hintsListView.ItemActivate += new System.EventHandler(this.hintsListView_ItemActivate);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Poř.";
            this.columnHeader5.Width = 24;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Tým";
            this.columnHeader6.Width = 127;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Čas";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 64;
            // 
            // descTab
            // 
            this.descTab.Controls.Add(this.descTextBox);
            this.descTab.Location = new System.Drawing.Point(0, 0);
            this.descTab.Name = "descTab";
            this.descTab.Size = new System.Drawing.Size(240, 243);
            this.descTab.Text = "Popis";
            // 
            // descTextBox
            // 
            this.descTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.descTextBox.Location = new System.Drawing.Point(3, 0);
            this.descTextBox.Multiline = true;
            this.descTextBox.Name = "descTextBox";
            this.descTextBox.ReadOnly = true;
            this.descTextBox.Size = new System.Drawing.Size(234, 242);
            this.descTextBox.TabIndex = 0;
            // 
            // puzzlesComboBox
            // 
            this.puzzlesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.puzzlesComboBox.Location = new System.Drawing.Point(3, 3);
            this.puzzlesComboBox.Name = "puzzlesComboBox";
            this.puzzlesComboBox.Size = new System.Drawing.Size(234, 22);
            this.puzzlesComboBox.TabIndex = 1;
            this.puzzlesComboBox.SelectedIndexChanged += new System.EventHandler(this.puzzlesComboBox_SelectedIndexChanged_1);
            // 
            // formUpdateTimer
            // 
            this.formUpdateTimer.Interval = 1000;
            this.formUpdateTimer.Tick += new System.EventHandler(this.formUpdateTimer_Tick);
            // 
            // PuzzlesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.puzzlesComboBox);
            this.Controls.Add(this.panel1);
            this.Name = "PuzzlesForm";
            this.Text = "HOH Šifry";
            this.Deactivate += new System.EventHandler(this.PuzzlesForm_Deactivate);
            this.GotFocus += new System.EventHandler(this.PuzzlesForm_GotFocus);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.timesTab.ResumeLayout(false);
            this.pointsTab.ResumeLayout(false);
            this.hintsTab.ResumeLayout(false);
            this.descTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage timesTab;
        private System.Windows.Forms.TabPage pointsTab;
        private System.Windows.Forms.ComboBox puzzlesComboBox;
        private System.Windows.Forms.TabPage descTab;
        private System.Windows.Forms.ListView timesListView;
        private System.Windows.Forms.ColumnHeader num;
        private System.Windows.Forms.ColumnHeader teamName;
        private System.Windows.Forms.ColumnHeader arriveTime;
        private System.Windows.Forms.ColumnHeader departTime;
        private System.Windows.Forms.ColumnHeader spending;
        private System.Windows.Forms.ListView pointsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox descTextBox;
        private System.Windows.Forms.TabPage hintsTab;
        private System.Windows.Forms.ListView hintsListView;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Timer formUpdateTimer;

    }
}