namespace HOHManager
{
    partial class TeamsForm
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
            this.puzzleName = new System.Windows.Forms.ColumnHeader();
            this.arriveNum = new System.Windows.Forms.ColumnHeader();
            this.arriveTime = new System.Windows.Forms.ColumnHeader();
            this.departTime = new System.Windows.Forms.ColumnHeader();
            this.spending = new System.Windows.Forms.ColumnHeader();
            this.pointsTab = new System.Windows.Forms.TabPage();
            this.pointsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.hintsTab = new System.Windows.Forms.TabPage();
            this.hintsListView = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.infoPage = new System.Windows.Forms.TabPage();
            this.standingsLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.giveUpTimeLabel = new System.Windows.Forms.Label();
            this.finishedTimeLabel = new System.Windows.Forms.Label();
            this.hintsLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pointsLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timeToFinishLabel = new System.Windows.Forms.Label();
            this.startTimeLabel = new System.Windows.Forms.Label();
            this.teamStartButton = new System.Windows.Forms.Button();
            this.teamFinishedButton = new System.Windows.Forms.Button();
            this.teamGivenUpButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.descTab = new System.Windows.Forms.TabPage();
            this.descTextBox = new System.Windows.Forms.TextBox();
            this.teamsComboBox = new System.Windows.Forms.ComboBox();
            this.refreshTimer = new System.Windows.Forms.Timer();
            this.formUpdateTimer = new System.Windows.Forms.Timer();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.timesTab.SuspendLayout();
            this.pointsTab.SuspendLayout();
            this.hintsTab.SuspendLayout();
            this.infoPage.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.infoPage);
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
            this.timesTab.Size = new System.Drawing.Size(232, 240);
            this.timesTab.Text = "Časy";
            // 
            // timesListView
            // 
            this.timesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.timesListView.Columns.Add(this.num);
            this.timesListView.Columns.Add(this.puzzleName);
            this.timesListView.Columns.Add(this.arriveNum);
            this.timesListView.Columns.Add(this.arriveTime);
            this.timesListView.Columns.Add(this.departTime);
            this.timesListView.Columns.Add(this.spending);
            this.timesListView.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.timesListView.FullRowSelect = true;
            this.timesListView.Location = new System.Drawing.Point(3, 0);
            this.timesListView.Name = "timesListView";
            this.timesListView.Size = new System.Drawing.Size(226, 239);
            this.timesListView.TabIndex = 0;
            this.timesListView.View = System.Windows.Forms.View.Details;
            this.timesListView.ItemActivate += new System.EventHandler(this.timesListView_ItemActivate);
            // 
            // num
            // 
            this.num.Text = "Poř.";
            this.num.Width = 24;
            // 
            // puzzleName
            // 
            this.puzzleName.Text = "Stan.";
            this.puzzleName.Width = 39;
            // 
            // arriveNum
            // 
            this.arriveNum.Text = "Poř.";
            this.arriveNum.Width = 24;
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
            this.pointsTab.Size = new System.Drawing.Size(232, 240);
            this.pointsTab.Text = "Body";
            // 
            // pointsListView
            // 
            this.pointsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pointsListView.Columns.Add(this.columnHeader1);
            this.pointsListView.Columns.Add(this.columnHeader2);
            this.pointsListView.Columns.Add(this.columnHeader5);
            this.pointsListView.Columns.Add(this.columnHeader3);
            this.pointsListView.Columns.Add(this.columnHeader4);
            this.pointsListView.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.pointsListView.FullRowSelect = true;
            this.pointsListView.Location = new System.Drawing.Point(3, 0);
            this.pointsListView.Name = "pointsListView";
            this.pointsListView.Size = new System.Drawing.Size(226, 239);
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
            this.columnHeader2.Text = "Stan.";
            this.columnHeader2.Width = 39;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Poř.";
            this.columnHeader5.Width = 24;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Odp.";
            this.columnHeader3.Width = 96;
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
            this.hintsTab.Size = new System.Drawing.Size(232, 240);
            this.hintsTab.Text = "Nápovědy";
            // 
            // hintsListView
            // 
            this.hintsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hintsListView.Columns.Add(this.columnHeader6);
            this.hintsListView.Columns.Add(this.columnHeader7);
            this.hintsListView.Columns.Add(this.columnHeader8);
            this.hintsListView.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.hintsListView.FullRowSelect = true;
            this.hintsListView.Location = new System.Drawing.Point(3, 0);
            this.hintsListView.Name = "hintsListView";
            this.hintsListView.Size = new System.Drawing.Size(226, 239);
            this.hintsListView.TabIndex = 2;
            this.hintsListView.View = System.Windows.Forms.View.Details;
            this.hintsListView.ItemActivate += new System.EventHandler(this.hintsListView_ItemActivate);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Poř.";
            this.columnHeader6.Width = 24;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Tým";
            this.columnHeader7.Width = 127;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Čas";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 64;
            // 
            // infoPage
            // 
            this.infoPage.Controls.Add(this.standingsLabel);
            this.infoPage.Controls.Add(this.label5);
            this.infoPage.Controls.Add(this.label3);
            this.infoPage.Controls.Add(this.label4);
            this.infoPage.Controls.Add(this.label2);
            this.infoPage.Controls.Add(this.giveUpTimeLabel);
            this.infoPage.Controls.Add(this.finishedTimeLabel);
            this.infoPage.Controls.Add(this.hintsLabel);
            this.infoPage.Controls.Add(this.label10);
            this.infoPage.Controls.Add(this.pointsLabel);
            this.infoPage.Controls.Add(this.label7);
            this.infoPage.Controls.Add(this.timeToFinishLabel);
            this.infoPage.Controls.Add(this.startTimeLabel);
            this.infoPage.Controls.Add(this.teamStartButton);
            this.infoPage.Controls.Add(this.teamFinishedButton);
            this.infoPage.Controls.Add(this.teamGivenUpButton);
            this.infoPage.Controls.Add(this.label1);
            this.infoPage.Location = new System.Drawing.Point(0, 0);
            this.infoPage.Name = "infoPage";
            this.infoPage.Size = new System.Drawing.Size(240, 243);
            this.infoPage.Text = "Info";
            // 
            // standingsLabel
            // 
            this.standingsLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.standingsLabel.Location = new System.Drawing.Point(133, 96);
            this.standingsLabel.Name = "standingsLabel";
            this.standingsLabel.Size = new System.Drawing.Size(100, 20);
            this.standingsLabel.Text = "0";
            this.standingsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.Text = "Žádosti o pořadí";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.Text = "Čas cíle";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.Text = "Čas vzdání";
            this.label4.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.Text = "Zbývá do cíle";
            // 
            // giveUpTimeLabel
            // 
            this.giveUpTimeLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.giveUpTimeLabel.Location = new System.Drawing.Point(95, 24);
            this.giveUpTimeLabel.Name = "giveUpTimeLabel";
            this.giveUpTimeLabel.Size = new System.Drawing.Size(138, 20);
            this.giveUpTimeLabel.Text = "0:00:00";
            this.giveUpTimeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.giveUpTimeLabel.Visible = false;
            // 
            // finishedTimeLabel
            // 
            this.finishedTimeLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.finishedTimeLabel.Location = new System.Drawing.Point(95, 24);
            this.finishedTimeLabel.Name = "finishedTimeLabel";
            this.finishedTimeLabel.Size = new System.Drawing.Size(138, 20);
            this.finishedTimeLabel.Text = "0:00:00";
            this.finishedTimeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.finishedTimeLabel.Visible = false;
            // 
            // hintsLabel
            // 
            this.hintsLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.hintsLabel.Location = new System.Drawing.Point(133, 76);
            this.hintsLabel.Name = "hintsLabel";
            this.hintsLabel.Size = new System.Drawing.Size(100, 20);
            this.hintsLabel.Text = "0";
            this.hintsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(7, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 20);
            this.label10.Text = "Nápovědy";
            // 
            // pointsLabel
            // 
            this.pointsLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.pointsLabel.Location = new System.Drawing.Point(133, 56);
            this.pointsLabel.Name = "pointsLabel";
            this.pointsLabel.Size = new System.Drawing.Size(100, 20);
            this.pointsLabel.Text = "0";
            this.pointsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.Text = "Body";
            // 
            // timeToFinishLabel
            // 
            this.timeToFinishLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.timeToFinishLabel.Location = new System.Drawing.Point(95, 24);
            this.timeToFinishLabel.Name = "timeToFinishLabel";
            this.timeToFinishLabel.Size = new System.Drawing.Size(138, 20);
            this.timeToFinishLabel.Text = "0:00:00";
            this.timeToFinishLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // startTimeLabel
            // 
            this.startTimeLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.startTimeLabel.Location = new System.Drawing.Point(95, 4);
            this.startTimeLabel.Name = "startTimeLabel";
            this.startTimeLabel.Size = new System.Drawing.Size(138, 20);
            this.startTimeLabel.Text = "0:00:00";
            this.startTimeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // teamStartButton
            // 
            this.teamStartButton.Location = new System.Drawing.Point(7, 129);
            this.teamStartButton.Name = "teamStartButton";
            this.teamStartButton.Size = new System.Drawing.Size(226, 33);
            this.teamStartButton.TabIndex = 5;
            this.teamStartButton.Text = "Start týmu";
            this.teamStartButton.Click += new System.EventHandler(this.teamStartButton_Click);
            // 
            // teamFinishedButton
            // 
            this.teamFinishedButton.Location = new System.Drawing.Point(7, 168);
            this.teamFinishedButton.Name = "teamFinishedButton";
            this.teamFinishedButton.Size = new System.Drawing.Size(226, 33);
            this.teamFinishedButton.TabIndex = 6;
            this.teamFinishedButton.Text = "Cíl týmu";
            this.teamFinishedButton.Click += new System.EventHandler(this.teamFinishedButton_Click);
            // 
            // teamGivenUpButton
            // 
            this.teamGivenUpButton.Location = new System.Drawing.Point(7, 207);
            this.teamGivenUpButton.Name = "teamGivenUpButton";
            this.teamGivenUpButton.Size = new System.Drawing.Size(226, 33);
            this.teamGivenUpButton.TabIndex = 7;
            this.teamGivenUpButton.Text = "Vzdání týmu";
            this.teamGivenUpButton.Click += new System.EventHandler(this.teamGivenUpButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.Text = "Čas startu";
            // 
            // descTab
            // 
            this.descTab.Controls.Add(this.descTextBox);
            this.descTab.Location = new System.Drawing.Point(0, 0);
            this.descTab.Name = "descTab";
            this.descTab.Size = new System.Drawing.Size(232, 240);
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
            this.descTextBox.Size = new System.Drawing.Size(226, 239);
            this.descTextBox.TabIndex = 0;
            // 
            // teamsComboBox
            // 
            this.teamsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.teamsComboBox.Location = new System.Drawing.Point(3, 3);
            this.teamsComboBox.Name = "teamsComboBox";
            this.teamsComboBox.Size = new System.Drawing.Size(234, 22);
            this.teamsComboBox.TabIndex = 1;
            this.teamsComboBox.SelectedIndexChanged += new System.EventHandler(this.teamsComboBox_SelectedIndexChanged);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 1000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // formUpdateTimer
            // 
            this.formUpdateTimer.Interval = 1000;
            this.formUpdateTimer.Tick += new System.EventHandler(this.formUpdateTimer_Tick);
            // 
            // TeamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.teamsComboBox);
            this.Controls.Add(this.panel1);
            this.Name = "TeamsForm";
            this.Text = "HOH Týmy";
            this.Deactivate += new System.EventHandler(this.TeamsForm_Deactivate);
            this.GotFocus += new System.EventHandler(this.TeamsForm_GotFocus);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.timesTab.ResumeLayout(false);
            this.pointsTab.ResumeLayout(false);
            this.hintsTab.ResumeLayout(false);
            this.infoPage.ResumeLayout(false);
            this.descTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage timesTab;
        private System.Windows.Forms.TabPage pointsTab;
        private System.Windows.Forms.ComboBox teamsComboBox;
        private System.Windows.Forms.TabPage descTab;
        private System.Windows.Forms.ListView timesListView;
        private System.Windows.Forms.ColumnHeader num;
        private System.Windows.Forms.ColumnHeader puzzleName;
        private System.Windows.Forms.ColumnHeader arriveTime;
        private System.Windows.Forms.ColumnHeader departTime;
        private System.Windows.Forms.ColumnHeader spending;
        private System.Windows.Forms.ListView pointsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox descTextBox;
        private System.Windows.Forms.ColumnHeader arriveNum;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TabPage hintsTab;
        private System.Windows.Forms.ListView hintsListView;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.TabPage infoPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timeToFinishLabel;
        private System.Windows.Forms.Label startTimeLabel;
        private System.Windows.Forms.Button teamStartButton;
        private System.Windows.Forms.Button teamFinishedButton;
        private System.Windows.Forms.Button teamGivenUpButton;
        private System.Windows.Forms.Label hintsLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label pointsLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label giveUpTimeLabel;
        private System.Windows.Forms.Label finishedTimeLabel;
        private System.Windows.Forms.Timer formUpdateTimer;
        private System.Windows.Forms.Label standingsLabel;
        private System.Windows.Forms.Label label5;

    }
}