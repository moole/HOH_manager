using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsMobile.PocketOutlook;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HOHManager
{
    public class SMSManager
    {
        private List<SMSMessageProcess> smsList;
        public List<SMSMessageProcess> unparsableSmsList;
        public SMSFetcher smsFetcher;
        public SMSParser smsParser;
        public DataManager dataManager;
        private Timer checkUnprocessedMessagesTimer;
        private static int fetchBackUpToHours = 4;


        [DllImport("MAPIHelper.dll")]
        static extern int SaveSmsMessages(StringBuilder outputString, Int32 cchBuffer);

        public int importSMSFromPocketOutlook(bool liveImport)
        {
            List<string> smsTexts = new List<string>();

            foreach (HOHEvent oneEvent in this.dataManager.gameModel.events)
            {
                string item = String.Format("{0}{1}{2}", oneEvent.dateTime, oneEvent.phoneNumber.Trim(), oneEvent.SMSText.Trim());
                if (!smsTexts.Contains(item))
                    smsTexts.Add(item);
            }
            lock (smsList)
            {
                foreach (SMSMessageProcess oneSMS in this.smsList)
                {
                    string item = String.Format("{0}{1}{2}{3}", oneSMS.getReceivedDate().ToShortDateString(), oneSMS.getReceivedDate().ToShortTimeString(), oneSMS.getRecipientPhoneNumber().Trim(), oneSMS.getOriginalMessageBody().Trim()).ToUpper();
                    if (!smsTexts.Contains(item))
                        smsTexts.Add(item);
                }
            }
            lock (unparsableSmsList)
            {
                foreach (SMSMessageProcess oneSMS in this.unparsableSmsList)
                {
                    string item = String.Format("{0}{1}{2}{3}", oneSMS.getReceivedDate().ToShortDateString(), oneSMS.getReceivedDate().ToShortTimeString(), oneSMS.getRecipientPhoneNumber().Trim(), oneSMS.getOriginalMessageBody().Trim()).ToUpper();
                    if (!smsTexts.Contains(item))
                        smsTexts.Add(item);
                }
            }
            lock (dataManager.gameModel.events)
            {
                foreach (HOHEvent oneEvent in dataManager.gameModel.events)
                {
                    string item = String.Format("{0}{1}{2}{3}", oneEvent.dateTime.ToShortDateString(), oneEvent.dateTime.ToShortTimeString(), oneEvent.phoneNumber.Trim(), oneEvent.SMSText.Trim()).ToUpper();
                    if (!smsTexts.Contains(item))
                        smsTexts.Add(item);
                }
            }

            int importedSmsCount = 0;
        //TODO: Idea: to import missed SMS from Mobile Outlook up to fetchBackUpToHours back
            StringBuilder smsBlock = new StringBuilder();
            smsBlock.Capacity = 1024 * 1024;
            SaveSmsMessages(smsBlock, smsBlock.Capacity);
            string[] componentList = smsBlock.ToString().Split('`');
            int i = 1;
            while (i <= componentList.Length - 3) {
                    SMSMessageProcess smsMessage = new SMSMessageProcess(null);
                    smsMessage.live = liveImport;
                    string[] ft = componentList[i].Split(':');
                    Int64 h = Int64.Parse(ft[0]);
                    Int64 l = Int64.Parse(ft[1]);
                    long hFT2 = (((long)h) << 32) + l;

                    DateTime dt = DateTime.FromFileTimeUtc(hFT2);
                    smsMessage.setReceivedDate(dt);
//                    smsMessage.setReceivedDate(DateTime.Now);
                    smsMessage.setRecipientPhoneNumber(componentList[i + 1]);
                    smsMessage.setMessageBody(componentList[i + 2]);

                    string item = String.Format("{0}{1}{2}{3}", smsMessage.getReceivedDate().ToShortDateString(), smsMessage.getReceivedDate().ToShortTimeString(), smsMessage.getRecipientPhoneNumber().Trim(), smsMessage.getMessageBody().Trim()).ToUpper();
                    if (!smsTexts.Contains(item)) {
                        lock (smsList)
                        {
                            int j = 0;
                            while (j < smsList.Count && smsList[j].getReceivedDate() < smsMessage.getReceivedDate())
                                j++;
                            smsList.Insert(j, smsMessage);
                        }
                        importedSmsCount++;
                    }
                    i += 3;
            }
            return importedSmsCount;
        }

        public int processPendingMessages(){
            int processedMessagesCount = 0;
            lock (smsList) {
                foreach (SMSMessageProcess smsMessageProces in smsList)
                {
                    if (!smsMessageProces.ignore && !smsMessageProces.processed)
                    {
                        List<HOHEvent> newEvents = smsParser.parseSMS(smsMessageProces);
                            if (newEvents != null)
                            {
                                dataManager.addEvents(newEvents, smsMessageProces.live);
                            }
                            else
                                markSmsUnparsable(smsMessageProces);

                            smsMessageProces.processed = true;
                            processedMessagesCount++;
                            //                        MessageBox.Show(smsMessageProces.smsMessage.From.Address + ": " + smsMessageProces.smsMessage.Body);
                    }
                }
            }
            if (processedMessagesCount > 0)
                dataManager.invokeDataModelChangedEvent();

            return processedMessagesCount;
        }

        private void markSmsUnparsable(SMSMessageProcess smsMessageProces)
        {
            smsMessageProces.unparsable = true;
            unparsableSmsList.Add(smsMessageProces);

        }

        public SMSManager(DataManager newDataManager) {
            dataManager = newDataManager;
            smsFetcher = new SMSFetcher(this);
            smsFetcher.addSmsInterceptorForPhoneNumber("");
            smsParser = new SMSParser(this);
            smsList = new List<SMSMessageProcess>();
            unparsableSmsList = new List<SMSMessageProcess>();
            checkUnprocessedMessagesTimer = new Timer();
            checkUnprocessedMessagesTimer.Interval = 5000;
            checkUnprocessedMessagesTimer.Tick += new EventHandler(checkUnprocessedMessagesTimer_Tick);
            checkUnprocessedMessagesTimer.Enabled = true;
        }

        public void checkUnprocessedMessagesTimer_Tick(object sender,EventArgs eArgs)
        {
            processPendingMessages();
        }

        internal void newIncomingMessage(SmsMessage newMessage)
        {
            lock (smsList)
            {
                SMSMessageProcess smsMessageProcess = new SMSMessageProcess(newMessage);
                smsList.Add(smsMessageProcess);
            }
        }

        internal void correctUnparsableSms(int selectedProblemIndex, string newCorrectedBody)
        {
            SMSMessageProcess unparsableSMS = unparsableSmsList[selectedProblemIndex];
            //unparsableSMS.correctedBody = newCorrectedBody;
            unparsableSMS.setMessageBody(newCorrectedBody);
            if (newCorrectedBody.CompareTo("") == 0)
                unparsableSMS.ignore = true;
            unparsableSMS.unparsable = false;
            unparsableSMS.processed = false;
            unparsableSmsList.Remove(unparsableSMS);
            dataManager.invokeDataModelChangedEvent();
        }

        internal void sendSmsHintForTeamAndPuzzle(HOHTeam team, HOHPuzzle puzzle, string phoneNumber, bool live) {

            if (puzzle == null) return;

            if (puzzle.hint.Equals("")) return;

            string smsText = String.Format("Napoveda pro {0}: {1}", puzzle.name, puzzle.hint);

            HOHEvent newEvent = new HOHEvent(HOHEvent.HOHEventType.HintReplyEventType);
            newEvent.team = team;
            newEvent.puzzle = puzzle;
            newEvent.phoneNumber = phoneNumber;
            newEvent.SMSText = smsText; 
            
            SmsMessage msg = new SmsMessage();
            msg.Body = smsText;
            msg.To.Add(new Recipient(phoneNumber));

            if (live && !sendSms(msg, newEvent))
            { 
                newEvent.eventType = HOHEvent.HOHEventType.SendFailedEventType;
            }
            dataManager.addEvents(new List<HOHEvent>(new HOHEvent[] { newEvent }), live);

        }

        internal void sendSmsStandingsForTeamAndPuzzle(HOHTeam team, HOHPuzzle puzzle, string phoneNumber, bool live)
        {

            if (puzzle == null || team == null) return;

            int place = this.dataManager.gameModel.getPlaceForTeamAtPuzzle(team, puzzle);

            string smsText = String.Format("Vase poradi na sifre {0}: {1}.", puzzle.name, place);
            /* TODO
            if (place > 0)
                smsText += String.Format(" Pred vami: {0}", dataManager.gameModel.);
            if (place > 1)
                smsText += String.Format(" Prvni na sifre: {0}", dataManager.gameModel.);
            */

            HOHEvent newEvent = new HOHEvent(HOHEvent.HOHEventType.StandingsReplyEventType);
            newEvent.team = team;
            newEvent.puzzle = puzzle;
            newEvent.phoneNumber = phoneNumber;
            newEvent.SMSText = smsText;

            SmsMessage msg = new SmsMessage();
            msg.Body = smsText;
            msg.To.Add(new Recipient(phoneNumber));

            if (live && !sendSms(msg, newEvent))
            {
                newEvent.eventType = HOHEvent.HOHEventType.SendFailedEventType;
            }
            dataManager.addEvents(new List<HOHEvent>(new HOHEvent[] { newEvent }), live);
        }

        internal void sendSmsStandingsForTeam(HOHTeam team, string phoneNumber, bool live)
        {
            if (team == null) return;

            int place = dataManager.gameModel.getTeamPlacesList().IndexOf(team);

            string smsText = String.Format("Vase poradi ve hre: {0}. ({1} b)", place + 1, dataManager.gameModel.getPointsForTeam(team));
            if (place > 0)
            {
                smsText += String.Format(" Pred vami: {0} ({1} b)", dataManager.gameModel.getTeamPlacesList()[place - 1].name, dataManager.gameModel.getPointsForTeam(dataManager.gameModel.getTeamPlacesList()[place - 1]));
            }
            if (place < dataManager.gameModel.getTeamPlacesList().Count() - 1)
            {
                smsText += String.Format(" Za vami: {0} ({1} b)", dataManager.gameModel.getTeamPlacesList()[place + 1].name, dataManager.gameModel.getPointsForTeam(dataManager.gameModel.getTeamPlacesList()[place + 1]));
            }
            if (place > 0)
            {
                smsText += String.Format(" Prvni misto: {0} ({1} b)", dataManager.gameModel.getTeamPlacesList()[0].name, dataManager.gameModel.getPointsForTeam(dataManager.gameModel.getTeamPlacesList()[0]));
            }

            HOHEvent newEvent = new HOHEvent(HOHEvent.HOHEventType.StandingsReplyEventType);
            newEvent.team = team;
            newEvent.phoneNumber = phoneNumber;
            newEvent.SMSText = smsText;

            SmsMessage msg = new SmsMessage();
            msg.Body = smsText;
            msg.To.Add(new Recipient(phoneNumber));

            if (live && !sendSms(msg, newEvent))
            {
                newEvent.eventType = HOHEvent.HOHEventType.SendFailedEventType;
            }
            dataManager.addEvents(new List<HOHEvent>(new HOHEvent[] { newEvent }), live);
        }

        private bool sendSms(SmsMessage msg, HOHEvent newEvent)
        {
            try
            {
                if (msg.To.Count == 0 || msg.To[0] == null || msg.To[0].Address.CompareTo("<>") == 0)
                    throw new Exception();

                msg.Send();
            }
            catch
            {
                return false;
            }
            return true;
        }

        internal void clearSMSMessages()
        {
            lock (smsList)
            {
                smsList.Clear();
            }

            lock (unparsableSmsList)
            {
                unparsableSmsList.Clear();
            }
        }
    }
}
