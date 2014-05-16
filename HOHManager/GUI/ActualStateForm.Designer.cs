namespace HOHManager
{
    partial class ActualStateForm
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
            this.actualStateList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.formUpdateTimer = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // actualStateList
            // 
            this.actualStateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.actualStateList.Columns.Add(this.columnHeader1);
            this.actualStateList.Columns.Add(this.columnHeader2);
            this.actualStateList.Columns.Add(this.columnHeader3);
            this.actualStateList.Columns.Add(this.columnHeader5);
            this.actualStateList.Columns.Add(this.columnHeader4);
            this.actualStateList.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.actualStateList.FullRowSelect = true;
            this.actualStateList.Location = new System.Drawing.Point(3, 3);
            this.actualStateList.Name = "actualStateList";
            this.actualStateList.Size = new System.Drawing.Size(234, 288);
            this.actualStateList.TabIndex = 1;
            this.actualStateList.View = System.Windows.Forms.View.Details;
            this.actualStateList.ItemActivate += new System.EventHandler(this.actualStateList_ItemActivate);
            this.actualStateList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Poř.";
            this.columnHeader1.Width = 24;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tým";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Stan.";
            this.columnHeader3.Width = 32;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Náp.";
            this.columnHeader5.Width = 32;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Body";
            this.columnHeader4.Width = 48;
            // 
            // formUpdateTimer
            // 
            this.formUpdateTimer.Interval = 1000;
            this.formUpdateTimer.Tick += new System.EventHandler(this.formUpdateTimer_Tick);
            // 
            // ActualStateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.actualStateList);
            this.Name = "ActualStateForm";
            this.Text = "HOH Aktuální stav";
            this.Deactivate += new System.EventHandler(this.ActualStateForm_Deactivate);
            this.GotFocus += new System.EventHandler(this.ActualStateForm_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView actualStateList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Timer formUpdateTimer;
    }
}