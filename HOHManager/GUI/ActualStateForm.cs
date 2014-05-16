using System;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace HOHManager
{
    public partial class ActualStateForm : Form, DataModelEventInterface
    {
        private DataManager _dataManager;
        public DataManager dataManager {
            get { return this._dataManager; }
            set { if (this._dataManager != null) this._dataManager.removeDataModelEventListener(this); this._dataManager = value; this._dataManager.addDataModelEventListener(this); }
        }
        private bool isVisible = false;
        private DateTime lastUpdate = DateTime.MinValue;

        public void dataModelHasChanged()
        {
            if (this.isVisible) {
                this.formUpdateTimer.Enabled = false;
                this.formUpdateTimer.Enabled = true;
            }
        }


        private void formUpdateTimer_Tick(object sender, EventArgs e)
        {
            this.formUpdateTimer.Enabled = false;
            refreshActualStateList(this.dataManager);
            lastUpdate = DateTime.Now;

        }

        private void refreshActualStateList(DataManager dataManager)
        {

            this.actualStateList.BeginUpdate();
            actualStateList.Items.Clear();
            int i = 0;
            foreach (HOHTeam team in dataManager.gameModel.getTeamPlacesList())
            {   
                i++;
                ListViewItem actualListViewItem = new ListViewItem();
                decimal points = dataManager.gameModel.getPointsForTeam(team);
                string pointsString = points.ToString();
                if (points < -(dataManager.gameModel.gameRules.pointPenaltyForTimeout / 2)) {
                    pointsString = "DNF";
                }
                actualListViewItem.Text = i.ToString();
                actualListViewItem.SubItems.Add(team.name);
                actualListViewItem.SubItems.Add(dataManager.gameModel.getPuzzlesTakenForTeam(team).ToString());
                actualListViewItem.SubItems.Add(dataManager.gameModel.getHintsTakenForTeam(team).ToString());
                actualListViewItem.SubItems.Add(pointsString);
                actualStateList.Items.Add(actualListViewItem);
            }
            this.actualStateList.EndUpdate();

        }

        public ActualStateForm(DataManager newDataManager)
            : this()
        {
            dataManager = newDataManager;
        }
        public ActualStateForm()
        {
            InitializeComponent();
            actualStateList.SetAllowDraggableColumns(true);
        }


        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //listView1.ListViewItemSorter(null);
        }

        private void actualStateList_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = actualStateList.Items[actualStateList.SelectedIndices[0]];
            MessageBox.Show(String.Format("Tým {0} prosel {1} stanovist, vzal {2} napoved a ma {3} bodu.", selectedItem.SubItems[1].Text, selectedItem.SubItems[2].Text, selectedItem.SubItems[3].Text, selectedItem.SubItems[4].Text));
        }

        private void ActualStateForm_Activated(object sender, EventArgs e)
        {
            this.isVisible = true;
            refreshActualStateList(this.dataManager);
            lastUpdate = DateTime.Now;
        }

        private void ActualStateForm_Deactivate(object sender, EventArgs e)
        {
            this.isVisible = false;

        }

    }
}