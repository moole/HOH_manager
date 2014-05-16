using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.WindowsMobile.PocketOutlook;

namespace HOHManager
{
    public partial class EventsForm : Form, DataModelEventInterface
    {
        private int selectedProblemIndex = -1;
        private DataManager _dataManager;
        public DataManager dataManager
        {
            get { return this._dataManager; }
            set { if (this._dataManager != null) this._dataManager.removeDataModelEventListener(this); this._dataManager = value; this._dataManager.addDataModelEventListener(this); }
        }

        private static string errorDescriptionFormat = "Příchozí SMS z čísla {0} v {1} nebylo možné zpracovat. Opravte text níže:";
        private static string titleLabelTextFormat = "Problém {0}/{1}:";
        private static string titleLabelTextNoProblems = "Žádné problémy.";
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
        
        private void refreshGameRulesTextBox(DataManager dataManager)
        {
            string gameRulesTextBoxText = "";
            gameRulesTextBoxText = dataManager.gameModel.gameRules.description;
            this.gameRulesTextBox.Text = gameRulesTextBoxText;
        }

        public EventsForm(DataManager newDataManager): this()
        {
            dataManager = newDataManager;
            this.refreshAllViews(dataManager);
        }

        private void refreshAllViews(DataManager dataManager)
        {
            refreshEventsView(dataManager);
            refreshProblemsView(dataManager);
            refreshGameRulesTextBox(dataManager);
        }

        private void refreshEventsView(DataManager dataManager)
        {
            eventsListView.BeginUpdate();
            eventsListView.Items.Clear();
            foreach (HOHEvent oneEvent in this.dataManager.gameModel.events) {
                ListViewItem eventListViewItem = new ListViewItem();
                switch (oneEvent.eventType)
                {
                    case HOHEvent.HOHEventType.SolutionEventType:
                        eventListViewItem.Text = oneEvent.dateTime.ToString();
                        if (oneEvent.solutionText.CompareTo(oneEvent.puzzle.solution) == 0)
                        {
                            eventListViewItem.SubItems.Add(String.Format("Spravna odpoved na sifru {0} ({1}) od tymu {2}.", oneEvent.puzzle.name, oneEvent.solutionText, oneEvent.team.name));
                        }
                        else
                        {
                            eventListViewItem.SubItems.Add(String.Format("Spatna odpoved na sifru {0} ({1}, ma byt {2}) od tymu {3}.", oneEvent.puzzle.name, oneEvent.solutionText, oneEvent.puzzle.solution, oneEvent.team.name));
                        }
                        eventListViewItem.SubItems.Add(oneEvent.SMSText);
                        break;
                    case HOHEvent.HOHEventType.HintRequestEventType:
                        eventListViewItem.Text = oneEvent.dateTime.ToString();
                        eventListViewItem.SubItems.Add(String.Format("Zadost o napovedu na sifru {0} od tymu {1}.", oneEvent.puzzle.name, oneEvent.team.name));
                        eventListViewItem.SubItems.Add(oneEvent.SMSText);
                        break;
                    case HOHEvent.HOHEventType.HintReplyEventType:
                        eventListViewItem.Text = oneEvent.dateTime.ToString();
                        eventListViewItem.SubItems.Add(String.Format("Odeslana napoveda k sifre {0} tymu {1} na cislo {2}.", oneEvent.puzzle.name, oneEvent.team.name, oneEvent.phoneNumber));
                        eventListViewItem.SubItems.Add(oneEvent.SMSText);
                        break;
                    case HOHEvent.HOHEventType.StandigsRequestType:
                        eventListViewItem.Text = oneEvent.dateTime.ToString();
                        eventListViewItem.SubItems.Add(String.Format("Zadost o poradi od tymu {0}.", oneEvent.team.name));
                        eventListViewItem.SubItems.Add(oneEvent.SMSText);
                        break;
                    case HOHEvent.HOHEventType.StandingsReplyEventType:
                        eventListViewItem.Text = oneEvent.dateTime.ToString();
                        eventListViewItem.SubItems.Add(String.Format("Odeslano info o poradi tymu {0} na cislo {1}.", oneEvent.team.name, oneEvent.phoneNumber));
                        eventListViewItem.SubItems.Add(oneEvent.SMSText);
                        break;
                    case HOHEvent.HOHEventType.GivingUpEventType:
                        eventListViewItem.Text = oneEvent.dateTime.ToString();
                        eventListViewItem.SubItems.Add(String.Format("Tym {0} vzdal hru.", oneEvent.team.name));
                        eventListViewItem.SubItems.Add(oneEvent.SMSText);
                        break;
                    case HOHEvent.HOHEventType.SendFailedEventType:
                        eventListViewItem.Text = oneEvent.dateTime.ToString();
                        eventListViewItem.SubItems.Add(String.Format("Nepodarilo se odeslat SMS tymu {0} na cislo {1}.", oneEvent.team.name, oneEvent.phoneNumber));
                        eventListViewItem.SubItems.Add(oneEvent.SMSText);
                        break;
                }
                eventsListView.Items.Add(eventListViewItem);
            }
            eventsListView.EndUpdate();
        }

        private void refreshProblemsView(DataManager dataManager)
        {
            if (selectedProblemIndex < 0 && dataManager.smsManager.unparsableSmsList.Count > 0)
                selectedProblemIndex = dataManager.smsManager.unparsableSmsList.Count - 1;
            if (selectedProblemIndex > dataManager.smsManager.unparsableSmsList.Count -1 )
                selectedProblemIndex = dataManager.smsManager.unparsableSmsList.Count - 1;
            if (selectedProblemIndex >= 0)
            {
                SMSMessageProcess problemSMS = dataManager.smsManager.unparsableSmsList[selectedProblemIndex];
                //if (problemSMS.correctedBody.CompareTo("") != 0)
                //    smsTextTextBox.Text = problemSMS.correctedBody;
                //else
                //    smsTextTextBox.Text = problemSMS.smsMessage.Body;
                smsTextTextBox.Text = problemSMS.getMessageBody();
                smsTextTextBox.ReadOnly = false;
                if (problemSMS.getRecipientPhoneNumber().CompareTo("<>") == 0)
                    errorDescriptionLabel.Text = String.Format(errorDescriptionFormat, "<testovací>", problemSMS.getReceivedDate());
                else
                    errorDescriptionLabel.Text = String.Format(errorDescriptionFormat, problemSMS.getRecipientPhoneNumber(), problemSMS.getReceivedDate());
                titleLabel.Text = String.Format(titleLabelTextFormat, (selectedProblemIndex + 1).ToString(), dataManager.smsManager.unparsableSmsList.Count);
            }
            else { 
                smsTextTextBox.Text = "";
                smsTextTextBox.ReadOnly = true;
                errorDescriptionLabel.Text = "";
                titleLabel.Text = titleLabelTextNoProblems;
            }
            refreshProblemsButtonsState(dataManager);
        }

        private void refreshProblemsButtonsState(DataManager dataManager) {
            previousProblembutton.Enabled = (!(selectedProblemIndex == previousProblemIndex(dataManager, selectedProblemIndex)));
            nextProblemButton.Enabled = (!(selectedProblemIndex == nextProblemIndex(dataManager, selectedProblemIndex)));
            problemCorrectedButton.Enabled = (selectedProblemIndex != -1);
        }

        public EventsForm()
        {
            InitializeComponent();
            eventsListView.SetAllowDraggableColumns(true);

        }

        private void problemCorrectedButton_Click(object sender, EventArgs e)
        {
            this.dataManager.smsManager.correctUnparsableSms(selectedProblemIndex, smsTextTextBox.Text);
        }

        private void previousProblembutton_Click(object sender, EventArgs e)
        {
            selectedProblemIndex = previousProblemIndex(this.dataManager, selectedProblemIndex);
            refreshProblemsView(this.dataManager);
        }

        private int previousProblemIndex(DataManager dataManager, int newSelectedProblemIndex)
        {
            if (newSelectedProblemIndex > 0) newSelectedProblemIndex--;
            if (newSelectedProblemIndex > dataManager.smsManager.unparsableSmsList.Count)
                newSelectedProblemIndex = dataManager.smsManager.unparsableSmsList.Count - 1;
            return newSelectedProblemIndex;
        }

        private int nextProblemIndex(DataManager dataManager, int newSelectedProblemIndex)
        {
            newSelectedProblemIndex++;
            if (newSelectedProblemIndex > dataManager.smsManager.unparsableSmsList.Count - 1)
                newSelectedProblemIndex = dataManager.smsManager.unparsableSmsList.Count - 1;
            return newSelectedProblemIndex;
        }

        private void nextProblemButton_Click(object sender, EventArgs e)
        {
            selectedProblemIndex = nextProblemIndex(this.dataManager, selectedProblemIndex);
            refreshProblemsView(this.dataManager);
        }

        private void eventsListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = eventsListView.Items[eventsListView.SelectedIndices[0]];
            MessageBox.Show(String.Format("{0}: {1}\n{2}", selectedItem.SubItems[0].Text, selectedItem.SubItems[1].Text, selectedItem.SubItems[2].Text));
        }

        [DllImport("aygshell.dll")]
        public static extern bool SHFullScreen(IntPtr hwndRequester, uint dwState);

        [DllImport("Coredll.dll")]
        public static extern uint GetForegroundWindow();
        [DllImport("Coredll.dll")]
        public static extern void  SetForegroundWindow(IntPtr hwnd);
        [DllImport("Coredll.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("coredll.dll")]
        public static extern IntPtr MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);

        public void HideSIP()
        {
      /*      const uint SHFS_HIDESIPBUTTON = 0x08;
            IntPtr hWnd = FindWindow("MS_SIPBUTTON", null);
            GetWindowRect(hWndTaskBar, ref rtSipButton);
            //MoveWindow(hWnd, 0, 100, 50, 50, true);
             hWnd = FindWindow("SipWndClass", null);
             GetWindowRect(hWndTaskBar, ref rtSipWnd);
            MoveWindow(hWnd, 0, 100, 50, 50, true);
            SetForegroundWindow(hWnd);
            SHFullScreen(hWnd, SHFS_HIDESIPBUTTON);
       */ }

        private void smsTextTextBox_GotFocus(object sender, EventArgs e)
        {
            HideSIP();
            inputPanel1.Enabled = true;
            HideSIP();
        }

        private void smsTextTextBox_LostFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = false;
        }

        private void dummySmsButton_Click(object sender, EventArgs e)
        {
            SmsMessage msg = new SmsMessage();
            msg.Body = "Text zpravy";
            //            msg.From.Add( new Recipient("+420608807237"));
            //            msg.Received = DateTime.Now;
            //            msg.LastModified = DateTime.Now;
            dataManager.smsManager.newIncomingMessage(msg);
        }

        private void EventsForm_GotFocus(object sender, EventArgs e)
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

        private void EventsForm_Deactivate(object sender, EventArgs e)
        {
            this.isVisible = false;
        }
    }
}