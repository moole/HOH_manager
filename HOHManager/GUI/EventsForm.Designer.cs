namespace HOHManager
{
    partial class EventsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.eventsTab = new System.Windows.Forms.TabPage();
            this.eventsListView = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.problemsTab = new System.Windows.Forms.TabPage();
            this.dummySmsButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.problemCorrectedButton = new System.Windows.Forms.Button();
            this.nextProblemButton = new System.Windows.Forms.Button();
            this.previousProblembutton = new System.Windows.Forms.Button();
            this.smsTextTextBox = new System.Windows.Forms.TextBox();
            this.errorDescriptionLabel = new System.Windows.Forms.Label();
            this.gameRulesTab = new System.Windows.Forms.TabPage();
            this.gameRulesTextBox = new System.Windows.Forms.TextBox();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.formUpdateTimer = new System.Windows.Forms.Timer();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.eventsTab.SuspendLayout();
            this.problemsTab.SuspendLayout();
            this.gameRulesTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 296);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.problemsTab);
            this.tabControl1.Controls.Add(this.eventsTab);
            this.tabControl1.Controls.Add(this.gameRulesTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.None;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 296);
            this.tabControl1.TabIndex = 0;
            // 
            // eventsTab
            // 
            this.eventsTab.Controls.Add(this.eventsListView);
            this.eventsTab.Location = new System.Drawing.Point(0, 0);
            this.eventsTab.Name = "eventsTab";
            this.eventsTab.Size = new System.Drawing.Size(232, 270);
            this.eventsTab.Text = "Události";
            // 
            // eventsListView
            // 
            this.eventsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.eventsListView.Columns.Add(this.columnHeader6);
            this.eventsListView.Columns.Add(this.columnHeader7);
            this.eventsListView.Columns.Add(this.columnHeader8);
            this.eventsListView.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.eventsListView.FullRowSelect = true;
            this.eventsListView.Location = new System.Drawing.Point(3, 3);
            this.eventsListView.Name = "eventsListView";
            this.eventsListView.Size = new System.Drawing.Size(226, 265);
            this.eventsListView.TabIndex = 0;
            this.eventsListView.View = System.Windows.Forms.View.Details;
            this.eventsListView.ItemActivate += new System.EventHandler(this.eventsListView_ItemActivate);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Čas";
            this.columnHeader6.Width = 56;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Událost";
            this.columnHeader7.Width = 95;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Text SMS";
            this.columnHeader8.Width = 76;
            // 
            // problemsTab
            // 
            this.problemsTab.Controls.Add(this.dummySmsButton);
            this.problemsTab.Controls.Add(this.titleLabel);
            this.problemsTab.Controls.Add(this.problemCorrectedButton);
            this.problemsTab.Controls.Add(this.nextProblemButton);
            this.problemsTab.Controls.Add(this.previousProblembutton);
            this.problemsTab.Controls.Add(this.smsTextTextBox);
            this.problemsTab.Controls.Add(this.errorDescriptionLabel);
            this.problemsTab.Location = new System.Drawing.Point(0, 0);
            this.problemsTab.Name = "problemsTab";
            this.problemsTab.Size = new System.Drawing.Size(240, 273);
            this.problemsTab.Text = "Problémy";
            // 
            // dummySmsButton
            // 
            this.dummySmsButton.Location = new System.Drawing.Point(164, 3);
            this.dummySmsButton.Name = "dummySmsButton";
            this.dummySmsButton.Size = new System.Drawing.Size(72, 16);
            this.dummySmsButton.TabIndex = 6;
            this.dummySmsButton.Text = "Gen.SMS";
            this.dummySmsButton.Click += new System.EventHandler(this.dummySmsButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(7, 4);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(226, 20);
            this.titleLabel.Text = "Problém 1/1:";
            // 
            // problemCorrectedButton
            // 
            this.problemCorrectedButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.problemCorrectedButton.Location = new System.Drawing.Point(83, 250);
            this.problemCorrectedButton.Name = "problemCorrectedButton";
            this.problemCorrectedButton.Size = new System.Drawing.Size(72, 20);
            this.problemCorrectedButton.TabIndex = 3;
            this.problemCorrectedButton.Text = "Potvrdit";
            this.problemCorrectedButton.Click += new System.EventHandler(this.problemCorrectedButton_Click);
            // 
            // nextProblemButton
            // 
            this.nextProblemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextProblemButton.Location = new System.Drawing.Point(161, 250);
            this.nextProblemButton.Name = "nextProblemButton";
            this.nextProblemButton.Size = new System.Drawing.Size(72, 20);
            this.nextProblemButton.TabIndex = 4;
            this.nextProblemButton.Text = ">>";
            this.nextProblemButton.Click += new System.EventHandler(this.nextProblemButton_Click);
            // 
            // previousProblembutton
            // 
            this.previousProblembutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.previousProblembutton.Location = new System.Drawing.Point(7, 250);
            this.previousProblembutton.Name = "previousProblembutton";
            this.previousProblembutton.Size = new System.Drawing.Size(72, 20);
            this.previousProblembutton.TabIndex = 2;
            this.previousProblembutton.Text = "<<";
            this.previousProblembutton.Click += new System.EventHandler(this.previousProblembutton_Click);
            // 
            // smsTextTextBox
            // 
            this.smsTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.smsTextTextBox.Location = new System.Drawing.Point(7, 85);
            this.smsTextTextBox.Multiline = true;
            this.smsTextTextBox.Name = "smsTextTextBox";
            this.smsTextTextBox.Size = new System.Drawing.Size(226, 159);
            this.smsTextTextBox.TabIndex = 1;
            this.smsTextTextBox.Text = "TEAM blamksdfgsd;gz; f;uzf ;ugh;vfhj;";
            this.smsTextTextBox.GotFocus += new System.EventHandler(this.smsTextTextBox_GotFocus);
            this.smsTextTextBox.LostFocus += new System.EventHandler(this.smsTextTextBox_LostFocus);
            // 
            // errorDescriptionLabel
            // 
            this.errorDescriptionLabel.Location = new System.Drawing.Point(7, 24);
            this.errorDescriptionLabel.Name = "errorDescriptionLabel";
            this.errorDescriptionLabel.Size = new System.Drawing.Size(226, 41);
            this.errorDescriptionLabel.Text = "Příchozí SMS v .... nebyla zpracována, opravte tvar:";
            // 
            // gameRulesTab
            // 
            this.gameRulesTab.Controls.Add(this.gameRulesTextBox);
            this.gameRulesTab.Location = new System.Drawing.Point(0, 0);
            this.gameRulesTab.Name = "gameRulesTab";
            this.gameRulesTab.Size = new System.Drawing.Size(232, 270);
            this.gameRulesTab.Text = "Pravidla";
            // 
            // gameRulesTextBox
            // 
            this.gameRulesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gameRulesTextBox.Location = new System.Drawing.Point(3, 3);
            this.gameRulesTextBox.Multiline = true;
            this.gameRulesTextBox.Name = "gameRulesTextBox";
            this.gameRulesTextBox.ReadOnly = true;
            this.gameRulesTextBox.Size = new System.Drawing.Size(226, 265);
            this.gameRulesTextBox.TabIndex = 0;
            // 
            // formUpdateTimer
            // 
            this.formUpdateTimer.Interval = 1000;
            this.formUpdateTimer.Tick += new System.EventHandler(this.formUpdateTimer_Tick);
            // 
            // EventsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.panel1);
            this.Name = "EventsForm";
            this.Text = "HOH Události";
            this.Deactivate += new System.EventHandler(this.EventsForm_Deactivate);
            this.GotFocus += new System.EventHandler(this.EventsForm_GotFocus);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.eventsTab.ResumeLayout(false);
            this.problemsTab.ResumeLayout(false);
            this.gameRulesTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage eventsTab;
        private System.Windows.Forms.TabPage problemsTab;
        private System.Windows.Forms.ListView eventsListView;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button problemCorrectedButton;
        private System.Windows.Forms.Button nextProblemButton;
        private System.Windows.Forms.Button previousProblembutton;
        private System.Windows.Forms.TextBox smsTextTextBox;
        private System.Windows.Forms.Label errorDescriptionLabel;
        private System.Windows.Forms.TabPage gameRulesTab;
        private System.Windows.Forms.TextBox gameRulesTextBox;
        private System.Windows.Forms.Label titleLabel;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.Button dummySmsButton;
        private System.Windows.Forms.Timer formUpdateTimer;

    }
}