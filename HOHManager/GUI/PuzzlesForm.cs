using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HOHManager
{
    public partial class PuzzlesForm : Form, DataModelEventInterface
    {
        private DataManager _dataManager;
        public DataManager dataManager
        {
            get { return this._dataManager; }
            set { if (this._dataManager != null) this._dataManager.removeDataModelEventListener(this); this._dataManager = value; this._dataManager.addDataModelEventListener(this); }
        }

        private List<HOHPuzzle> puzzlesComboBoxBinding;
        private HOHPuzzle selectedPuzzle;
        private bool isVisible = false;
        private DateTime lastUpdate = DateTime.MinValue;

        public void dataModelHasChanged()
        {
            if (this.isVisible)
            {
                this.formUpdateTimer.Enabled = false;
                this.formUpdateTimer.Enabled = true;
            }
        }
        
        public PuzzlesForm(DataManager newDataManager) : this()
        {
            dataManager = newDataManager;
            this.refreshAllViews(dataManager);
        }

        private void refreshAllViews(DataManager dataManager)
        {
            this.refreshPuzzlesCombobox(dataManager);
            this.refreshPuzzlesViews(dataManager);
        }

        private void refreshPuzzlesCombobox(DataManager dataManager)
        {
            if (this.puzzlesComboBox.Items.Count == dataManager.gameModel.puzzles.Count)
                return;

            this.puzzlesComboBox.BeginUpdate();
            int newSelectedIndex = 0;
            HOHPuzzle previouslySelected = null;
            if (this.puzzlesComboBox.SelectedIndex >= 0 && this.puzzlesComboBox.SelectedIndex < this.puzzlesComboBoxBinding.Count)
                previouslySelected = this.puzzlesComboBoxBinding[this.puzzlesComboBox.SelectedIndex];
            this.puzzlesComboBox.Items.Clear();
            this.puzzlesComboBoxBinding.Clear();
            if (dataManager.gameModel.puzzles != null)
            {
                foreach (HOHPuzzle puzzle in dataManager.gameModel.puzzles)
                {
                    this.puzzlesComboBox.Items.Add(puzzle.name);
                    this.puzzlesComboBoxBinding.Add(puzzle);
                    if (puzzle == previouslySelected)
                        newSelectedIndex = this.puzzlesComboBox.Items.Count - 1;
                }
                if (this.puzzlesComboBox.Items.Count > newSelectedIndex)
                    this.puzzlesComboBox.SelectedIndex = newSelectedIndex;
            }
            this.puzzlesComboBox.EndUpdate();
        }

        public PuzzlesForm()
        {
            InitializeComponent();
            this.puzzlesComboBoxBinding = new List<HOHPuzzle>();
            timesListView.SetAllowDraggableColumns(true);
            pointsListView.SetAllowDraggableColumns(true);
            hintsListView.SetAllowDraggableColumns(true);
        }

        private void puzzlesComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.puzzlesComboBox.SelectedIndex >= 0 && this.puzzlesComboBox.SelectedIndex < this.puzzlesComboBoxBinding.Count)
                this.selectedPuzzle = this.puzzlesComboBoxBinding[this.puzzlesComboBox.SelectedIndex];
            else
                this.selectedPuzzle = null;
            this.refreshPuzzlesViews(this.dataManager);
        }

        private void refreshPuzzlesViews(DataManager dataManager)
        {
            this.refreshPointsListView(dataManager);
            this.refreshTimesListView(dataManager);
            this.refreshHintsListView(dataManager);
            this.refreshDescTextBox(dataManager);
        }

        private void refreshPointsListView(DataManager dataManager)
        {
            if (selectedPuzzle == null)
                return;

            this.pointsListView.BeginUpdate();
            List<HOHTeam> alreadyCounted = new List<HOHTeam>();

            pointsListView.Items.Clear();
            int i = 0;
            foreach (HOHEvent oneEvent in selectedPuzzle.events)
            {
                if (oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType && !alreadyCounted.Contains(oneEvent.team))
                {
                    alreadyCounted.Add(oneEvent.team);
                    ListViewItem newLine = new ListViewItem();
                    if (oneEvent.hasCorrectSolution())
                    {
                        i++;
                        newLine.Text = i.ToString();
                    }
                    else {
                        newLine.Text = "x";
                    }
                    newLine.SubItems.Add(oneEvent.team.name);
                    newLine.SubItems.Add(oneEvent.solutionText);
                    newLine.SubItems.Add(this.dataManager.gameModel.getPointsForTeamAtPuzzle(oneEvent.team, selectedPuzzle).ToString());
                    pointsListView.Items.Add(newLine);
                }
            }
            this.pointsListView.EndUpdate();
        }

        private void refreshTimesListView(DataManager dataManager)
        {
            if (selectedPuzzle == null)
                return;

            this.timesListView.BeginUpdate();

            List<HOHTeam> alreadyCounted = new List<HOHTeam>();

            timesListView.Items.Clear();
            int i = 0;
            foreach (HOHEvent oneEvent in selectedPuzzle.events)
            {
                if (oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType && !alreadyCounted.Contains(oneEvent.team))
                {
                    alreadyCounted.Add(oneEvent.team);
                    i++;
                    ListViewItem newLine = new ListViewItem();
                    newLine.Text = i.ToString();
                    newLine.SubItems.Add(oneEvent.team.name);
                    newLine.SubItems.Add("-");
                    newLine.SubItems.Add(oneEvent.dateTime.ToString());
                    newLine.SubItems.Add("-");
                    timesListView.Items.Add(newLine);
                }
            }
            this.timesListView.EndUpdate();
        }

        private void refreshHintsListView(DataManager dataManager)
        {
            if (selectedPuzzle == null)
                return;

            this.hintsListView.BeginUpdate();
            hintsListView.Items.Clear();
            int i = 0;
            foreach (HOHEvent oneEvent in selectedPuzzle.events)
            {
                if (oneEvent.eventType == HOHEvent.HOHEventType.HintReplyEventType)
                {
                    i++;
                    ListViewItem newLine = new ListViewItem();
                    newLine.Text = i.ToString();
                    newLine.SubItems.Add(oneEvent.team.name);
                    newLine.SubItems.Add(oneEvent.dateTime.ToString());
                    hintsListView.Items.Add(newLine);
                }
            }
            this.hintsListView.EndUpdate();
        }

        private void refreshDescTextBox(DataManager dataManager)
        {
            string descTextBoxText = "";
            if (this.selectedPuzzle != null)
                descTextBoxText = this.selectedPuzzle.description;
            this.descTextBox.Text = descTextBoxText;
        }

        private void hintsListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = hintsListView.Items[hintsListView.SelectedIndices[0]];
            MessageBox.Show(String.Format("Tým {0} si vzal nápovědu na stanovišti {1} v {2}.", selectedItem.SubItems[1].Text, selectedPuzzle.name, selectedItem.SubItems[2].Text));
        }

        private void pointsListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = pointsListView.Items[pointsListView.SelectedIndices[0]];
            MessageBox.Show(String.Format("Tým {0} odpověděl na {1}: {2}, získal {3} bodů.", selectedItem.SubItems[1].Text, selectedPuzzle.name, selectedItem.SubItems[2].Text, selectedItem.SubItems[3].Text));
        }

        private void timesListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = timesListView.Items[timesListView.SelectedIndices[0]];
            MessageBox.Show(String.Format("Tým {0} odpověděl na {1} v {2}.", selectedItem.SubItems[1].Text, selectedPuzzle.name, selectedItem.SubItems[3].Text));
        }

        private void PuzzlesForm_GotFocus(object sender, EventArgs e)
        {
            this.isVisible = true;
            refreshAllViews(this.dataManager);
            lastUpdate = DateTime.Now;
        }

        private void formUpdateTimer_Tick(object sender, EventArgs e)
        {
            this.formUpdateTimer.Enabled = false;
            refreshAllViews(this.dataManager);
            lastUpdate = DateTime.Now;
        }

        private void PuzzlesForm_Deactivate(object sender, EventArgs e)
        {
            this.isVisible = false;

        }

    }
}