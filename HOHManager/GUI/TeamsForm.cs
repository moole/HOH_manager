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
    public partial class TeamsForm: Form, DataModelEventInterface
    {
        private DataManager _dataManager;
        public DataManager dataManager
        {
            get { return this._dataManager; }
            set { if (this._dataManager != null) this._dataManager.removeDataModelEventListener(this); this._dataManager = value; this._dataManager.addDataModelEventListener(this); }
        }

        private List<HOHTeam> teamsComboBoxBinding;
        private HOHTeam selectedTeam;
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

        public TeamsForm(DataManager newDataManager) : this()
        {
            dataManager = newDataManager;
            this.refreshAllViews(dataManager);
        }

        private void refreshAllViews(DataManager dataManager)
        {
            this.refreshTeamsCombobox(dataManager);
            this.refreshTeamViews(dataManager);
        }

        private void refreshTeamsCombobox(DataManager dataManager)
        {
            if (this.teamsComboBox.Items.Count == dataManager.gameModel.teams.Count)
                return;

            teamsComboBox.BeginUpdate();
            int newSelectedIndex = 0;
            HOHTeam previouslySelected = null;
            if (this.teamsComboBox.SelectedIndex >= 0 && this.teamsComboBox.SelectedIndex < this.teamsComboBoxBinding.Count)
                previouslySelected = this.teamsComboBoxBinding[this.teamsComboBox.SelectedIndex];
            this.teamsComboBox.Items.Clear();
            this.teamsComboBoxBinding.Clear();
            if (dataManager.gameModel.teams != null)
            {
                foreach (HOHTeam team in dataManager.gameModel.teams)
                {
                    this.teamsComboBox.Items.Add(team.name);
                    this.teamsComboBoxBinding.Add(team);
                    if (team == previouslySelected)
                        newSelectedIndex = this.teamsComboBox.Items.Count - 1;
                }
                if (this.teamsComboBox.Items.Count > newSelectedIndex)
                    this.teamsComboBox.SelectedIndex = newSelectedIndex;
            }
            teamsComboBox.EndUpdate();
        }

        public TeamsForm()
        {
            InitializeComponent();
            this.teamsComboBoxBinding = new List<HOHTeam>();
            timesListView.SetAllowDraggableColumns(true);
            pointsListView.SetAllowDraggableColumns(true);
            hintsListView.SetAllowDraggableColumns(true);
        }

        private void teamsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.teamsComboBox.SelectedIndex >= 0 && this.teamsComboBox.SelectedIndex < this.teamsComboBoxBinding.Count)
                this.selectedTeam = this.teamsComboBoxBinding[this.teamsComboBox.SelectedIndex];
            else
                this.selectedTeam = null;
            this.refreshTeamViews(this.dataManager);
        }

        private void refreshTeamViews(DataManager dataManager)
        {
            this.refreshPointsListView(dataManager);
            this.refreshTimesListView(dataManager);
            this.refreshHintsListView(dataManager);
            this.refreshTimeLabels(dataManager);
            this.refreshPointLabels(dataManager);
            this.refreshDescTextBox(dataManager);
        }

        private void refreshDescTextBox(DataManager dataManager)
        {
            string descTextBoxText = "";
            if (this.selectedTeam != null)
                descTextBoxText = this.selectedTeam.description;
            this.descTextBox.Text = descTextBoxText;
        }

        private void refreshPointLabels(DataManager dataManager)
        {
            string pointsLabelText = "-";
            string hintsLabelText = "-";
            string standingsLabelText = "-";
            if (this.selectedTeam != null)
            {
                pointsLabelText = String.Format("{0} b", this.dataManager.gameModel.getPointsForTeam(selectedTeam));
                hintsLabelText = String.Format("{0} / {1}", this.dataManager.gameModel.getHintsTakenForTeam(selectedTeam), this.dataManager.gameModel.gameRules.maxHintsPerTeam);
                standingsLabelText = String.Format("{0} / {1}", this.dataManager.gameModel.getStandingsTakenForTeam(selectedTeam), this.dataManager.gameModel.gameRules.maxStandingsPerTeam);
            }
            this.pointsLabel.Text = pointsLabelText;
            this.hintsLabel.Text = hintsLabelText;
            this.standingsLabel.Text = standingsLabelText;
        }

        private void refreshTimeLabels(DataManager dataManager)
        {
            string startTimeLabelText = "-";
            string timeToFinishLabelText = "-";
            string finishedTimeLabelText = "-";
            string giveUpTimeLabelText = "-";

            if (this.selectedTeam != null)
            {
                if (this.selectedTeam.hasStarted)
                {
                    startTimeLabelText = String.Format("{0}", this.selectedTeam.startTime);
                    timeToFinishLabelText = String.Format("{0}", this.dataManager.gameModel.getTimeToFinishForTeam(selectedTeam));
                }
                if (this.selectedTeam.hasFinished)
                    finishedTimeLabelText = String.Format("{0}", this.selectedTeam.finishTime);
                if (this.selectedTeam.hasGivenUp)
                    giveUpTimeLabelText = String.Format("{0}", this.selectedTeam.giveUpTime);

                this.finishedTimeLabel.Visible = this.selectedTeam.hasFinished;
                this.giveUpTimeLabel.Visible = this.selectedTeam.hasGivenUp;
                this.label3.Visible = this.selectedTeam.hasFinished;
                this.label4.Visible = this.selectedTeam.hasGivenUp;
            }

            this.startTimeLabel.Text = startTimeLabelText;
            this.timeToFinishLabel.Text = timeToFinishLabelText;
            this.finishedTimeLabel.Text = finishedTimeLabelText;
            this.giveUpTimeLabel.Text = giveUpTimeLabelText;
        }

        private void refreshHintsListView(DataManager dataManager)
        {
            if (selectedTeam == null)
                return;

            hintsListView.BeginUpdate();
            hintsListView.Items.Clear();
            int i = 0;
            foreach (HOHEvent oneEvent in selectedTeam.events){
                if (oneEvent.eventType == HOHEvent.HOHEventType.HintReplyEventType) {
                    i++;
                    ListViewItem newLine = new ListViewItem();
                    newLine.Text = i.ToString();
                    newLine.SubItems.Add(oneEvent.puzzle.name);
                    newLine.SubItems.Add(oneEvent.dateTime.ToString());
                    hintsListView.Items.Add(newLine);
                }
            }
            hintsListView.EndUpdate();
        }

        private void refreshTimesListView(DataManager dataManager)
        {
            if (selectedTeam == null)
                return;

            List<HOHPuzzle> alreadyCounted = new List<HOHPuzzle>();

            timesListView.BeginUpdate();
            timesListView.Items.Clear();
            int i = 0;
            foreach (HOHEvent oneEvent in selectedTeam.events)
            {
                if ((oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType) && !alreadyCounted.Contains(oneEvent.puzzle))
                {
                    alreadyCounted.Add(oneEvent.puzzle);

                    if (oneEvent.hasCorrectSolution())
                        i++;
                    ListViewItem newLine = new ListViewItem();
                    newLine.Text = i.ToString();
                    newLine.SubItems.Add(oneEvent.puzzle.name);
                    newLine.SubItems.Add(dataManager.gameModel.getPlaceForTeamAtPuzzle(selectedTeam, oneEvent.puzzle).ToString());
                    newLine.SubItems.Add("-");
                    newLine.SubItems.Add(oneEvent.dateTime.ToString());
                    newLine.SubItems.Add("-");
                    timesListView.Items.Add(newLine);
                }
            }
            timesListView.EndUpdate();
        }

        private void refreshPointsListView(DataManager dataManager)
        {
            if (selectedTeam == null)
                return;

            List<HOHPuzzle> alreadyCounted = new List<HOHPuzzle>();

            pointsListView.BeginUpdate();
            pointsListView.Items.Clear();
            int i = 0;
            foreach (HOHEvent oneEvent in selectedTeam.events)
            {
                if (oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType && !alreadyCounted.Contains(oneEvent.puzzle))
                {
                    alreadyCounted.Add(oneEvent.puzzle);
                    i++;
                    ListViewItem newLine = new ListViewItem();
                    newLine.Text = i.ToString();
                    newLine.SubItems.Add(oneEvent.puzzle.name);
                    newLine.SubItems.Add(dataManager.gameModel.getPlaceForTeamAtPuzzle(selectedTeam, oneEvent.puzzle).ToString());
                    newLine.SubItems.Add(oneEvent.solutionText);
                    newLine.SubItems.Add(dataManager.gameModel.getPointsForTeamAtPuzzle(selectedTeam, oneEvent.puzzle).ToString());
                    pointsListView.Items.Add(newLine);
                }
            }
            pointsListView.EndUpdate();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            this.refreshTimeLabels(this.dataManager);
        }

        private void teamStartButton_Click(object sender, EventArgs e)
        {
            this.dataManager.startTeam(selectedTeam);
        }

        private void teamFinishedButton_Click(object sender, EventArgs e)
        {
            this.dataManager.finishTeam(selectedTeam);
        }

        private void teamGivenUpButton_Click(object sender, EventArgs e)
        {
            this.dataManager.giveUpTeam(selectedTeam);
        }

        private void hintsListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = hintsListView.Items[hintsListView.SelectedIndices[0]];
            MessageBox.Show(String.Format("Tým {0} si vzal nápovědu na stanovišti {1} v {2}.", selectedTeam.name, selectedItem.SubItems[1].Text, selectedItem.SubItems[2].Text));
        }

        private void pointsListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = pointsListView.Items[pointsListView.SelectedIndices[0]];
            MessageBox.Show(String.Format("Tým {0} odpověděl na stanovišti {1}: {2}, získal {3} bodů.", selectedTeam.name, selectedItem.SubItems[1].Text, selectedItem.SubItems[3].Text, selectedItem.SubItems[4].Text));
        }

        private void timesListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = timesListView.Items[timesListView.SelectedIndices[0]];
            MessageBox.Show(String.Format("Tým {0} odpověděl na {1} v {2}.", selectedTeam.name, selectedItem.SubItems[1].Text, selectedItem.SubItems[4].Text));
        }

        private void TeamsForm_LostFocus(object sender, EventArgs e)
        {

        }

        private void TeamsForm_GotFocus(object sender, EventArgs e)
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

        private void TeamsForm_Deactivate(object sender, EventArgs e)
        {
            this.isVisible = false;

        }

    }
}