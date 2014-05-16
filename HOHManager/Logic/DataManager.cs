using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Windows.Forms;
using CompactFormatter;
using System.Runtime.InteropServices;


//using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization.Formatters;

namespace HOHManager
{
    public interface DataModelEventInterface {
        void dataModelHasChanged();
    }

    public class DataManager
    {
        private SMSManager _smsManager;
        public SMSManager smsManager { get { return this._smsManager; } }

        private HOHGameModel _gameModel;
        public HOHGameModel gameModel { get { return this._gameModel; } }

        public string dataFileName = "state.binary";
        public string dataBackupFileName = "state.backup";
        private List<DataModelEventInterface> dataModelEventListeners;

        private TimeWatcher timeWatcher;
        private Timer saveDataModelTimer;

        public DataManager() {
            _smsManager = new SMSManager(this);
            _gameModel = new HOHGameModel();
            dataModelEventListeners = new List<DataModelEventInterface>();
            timeWatcher = new TimeWatcher(this) ;

            saveDataModelTimer = new Timer();
            saveDataModelTimer.Tick += new EventHandler(saveDataModelTimerTick);
            saveDataModelTimer.Interval = 5 * 1000;
        }

        public void addDataModelEventListener(DataModelEventInterface obj)
        {
            dataModelEventListeners.Add(obj);
        }

        public void removeDataModelEventListener(DataModelEventInterface obj)
        {
            dataModelEventListeners.Remove(obj);
        }

        private void sendDataModelEventNotifications() {
            foreach (DataModelEventInterface obj in dataModelEventListeners)
                obj.dataModelHasChanged();
        }

        private void dataModelHasChanged() {
            sendDataModelEventNotifications();
            saveDataModelTimer.Enabled = false;
            saveDataModelTimer.Enabled = true;
        }

        private void saveDataModelTimerTick(object sender, EventArgs e)
        {
            saveDataModelTimer.Enabled = false;
            saveData();
        }

        public void clearPuzzles()
        {
            _gameModel.puzzles = new List<HOHPuzzle>();
        }

        public void clearTeams()
        {
            _gameModel.teams = new List<HOHTeam>();
        }

        public void clearEvents()
        {
            _gameModel.events = new List<HOHEvent>();
            foreach (HOHPuzzle puzzle in _gameModel.puzzles)
                puzzle.events.Clear();
            foreach (HOHTeam team in _gameModel.teams)
                team.events.Clear();
        }

        private void renameFile(string FilepathOld, string FilepathNew)
        {
            FileInfo fi = new FileInfo(FilepathOld);
            FileInfo fo = new FileInfo(FilepathNew);

            if (fi.Exists)
            {
                if (fo.Exists) {
                    fo.Delete();
                }
                fi.MoveTo(FilepathNew);
            }
        } 

        public int loadData()
        {
            if (loadData(dataFileName) < -1) {
                return loadData(dataBackupFileName);
            }
            return 1;
        }

        public int loadData(String fileName)
        {
            if (!File.Exists(fileName)) return -1;

            int err = 1;
            CompactFormatterPlus CF = new CompactFormatterPlus();
            Stream stream = new FileStream(fileName, FileMode.Open);
            try
            {
//                _gameModel = (HOHGameModel)CF.Deserialize(stream);
//                _gameModel.events = (List<HOHEvent>)CF.Deserialize(stream);
//                _gameModel.puzzles = (List<HOHPuzzle>)CF.Deserialize(stream);
                List<Object> savedObjects = (List<Object>)CF.Deserialize(stream);
                _gameModel.teams = (List<HOHTeam>)savedObjects[0];
                _gameModel.puzzles = (List<HOHPuzzle>)savedObjects[1];
                _gameModel.events = (List<HOHEvent>)savedObjects[2];
            }
            catch (Exception e)
            {
                err = -2;
            }
            stream.Close();

            this.dataModelHasChanged();

            return err;
        }

        public int saveData() {
            this.renameFile(dataFileName, dataBackupFileName);
            return saveData(dataFileName);
        }

        public int saveData(String fileName)
        {
            int err = 1;
            CompactFormatterPlus CF = new CompactFormatterPlus();
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
//                CF.Serialize(stream, _gameModel);
                List<Object> objectsToSave = new List<Object>();
                objectsToSave.Add(_gameModel.teams);
                objectsToSave.Add(_gameModel.puzzles);
                objectsToSave.Add(_gameModel.events);
                CF.Serialize(stream, objectsToSave);
            }
            catch (Exception)
            {
                err = -2;
            }
            stream.Close();

            return err;

            
        }

        public int saveTeamsToCSV(string fileName){

            int err = 1;
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    sw.Write(HOHTeam.getCSVHeaderLine());
                    foreach (HOHTeam team in _gameModel.teams)
                    {
                        sw.Write(team.getCSVLine());
                    }
                }
            }
            catch (Exception )
            {
                err = -1;
            }			
            return err;
        }

        public int loadTeamsFromCSV(string fileName){
            int err = 1;
            int line = 0;

            try
            {
                // Create an instance of StreamReader to read from a file.
                using (StreamReader objReader = new StreamReader(fileName, Encoding.UTF8))
                {
                    string strLineText;
                    while ((strLineText = objReader.ReadLine()) != null)
                    {
                        line++;
                        HOHTeam newTeam = HOHTeam.getTeamFromCSVLine(strLineText);
                        if (newTeam != null) {
                            this.addTeam(newTeam);
                        } else {
                            if ((strLineText.Trim().CompareTo("") != 0) && (line != 1)) {
                                err = -line;
                                break;
                            }
                        }                     
                    }
                }
            }
            catch (Exception e)
            {
                err = 0;
            }

            return err;
        }

        private void addTeam(HOHTeam newTeam)
        {
            _gameModel.teams.Add(newTeam);
           // _smsManager.smsFetcher.addSmsInterceptorForPhoneNumber(newTeam.phoneNumber);
        }


        public int savePuzzlesToCSV(string fileName)
        {
            int err = 1;
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    sw.Write(HOHPuzzle.getCSVHeaderLine());
                    foreach (HOHPuzzle puzzle in _gameModel.puzzles)
                    {
                        sw.Write(puzzle.getCSVLine());
                    }
                }
            }
            catch (Exception )
            {
                err = -1;
            }
            return err;
        }

        public int loadPuzzlesFromCSV(string fileName)
        {
            int err = 1;
            int line = 0;

            try
            {
                // Create an instance of StreamReader to read from a file.
                using (StreamReader objReader = new StreamReader(fileName, Encoding.UTF8))
                {
                    string strLineText;
                    while ((strLineText = objReader.ReadLine()) != null)
                    {
                        line++;
                        HOHPuzzle newPuzzle = HOHPuzzle.getPuzzleFromCSVLine(strLineText);
                        if (newPuzzle != null)
                        {
                            _gameModel.puzzles.Add(newPuzzle);
                        }
                        else
                        {
                            if ((strLineText.Trim().CompareTo("") != 0) && (line != 1)) {
                                err = -line;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception )
            {
                err = 0;
            }

            return err;
        }

        public int saveSMSToCSV(string fileName)
        {
            int err = 1;
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    sw.Write("\"Datum a čas\";\"Telefonní číslo\";\"Text SMS\"\r\n");
                    foreach (HOHEvent oneEvent in gameModel.events)
                    {
                        sw.Write(String.Format("\"{0}\";\"{1}\";\"{2}\"\r\n", oneEvent.dateTime.ToString(), oneEvent.phoneNumber, oneEvent.SMSText.Trim()));
                    }
                }
            }
            catch (Exception)
            {
                err = -1;
            }
            return err;
        }

        public int saveStateToCSV(string fileName)
        {
            int err = 1;
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    sw.Write("\"Tým\"");
                    foreach (HOHPuzzle puzzle in gameModel.puzzles)
                        sw.Write(String.Format(";\"{0}\"", puzzle.id));
                    sw.Write(";\"Příchod\"");
                    sw.Write(";\"Součet\"");
                    sw.Write(";\"\"");
                    sw.Write(";\"Start\"");
                    sw.Write(";\"Cíl\"");
                    sw.Write(";\"Příchod\"");
                    sw.Write(";\"Nápověd\"");
                    sw.Write(";\"Žádostí\"");
                    sw.Write("\r\n");

                    sw.Write("\"\"");
                    foreach (HOHPuzzle puzzle in gameModel.puzzles)
                        sw.Write(String.Format(";\"{0}\"", puzzle.points));
                    sw.Write("\r\n");

                    foreach (HOHTeam team in gameModel.teams) {
                        sw.Write("\"{0}\"", team.name);
                        foreach (HOHPuzzle puzzle in gameModel.puzzles) {
                            decimal pointCoef = 0;
                            if (puzzle.points > 0)
                                pointCoef = (gameModel.getPointsForTeamAtPuzzle(team, puzzle) / puzzle.points);
                            sw.Write(String.Format(";\"{0:0.00}\"", pointCoef));
                        }
                        sw.Write(String.Format(";\"{0}\"", -gameModel.getTimePenaltyForTeam(team)));
                        sw.Write(String.Format(";\"{0}\"", gameModel.getPointsForTeam(team)));
                        sw.Write(";\"\"");
                        sw.Write(String.Format(";\"{0}\"", team.startTime.ToString()));
                        sw.Write(String.Format(";\"{0}\"", team.startTime.AddMinutes(gameModel.gameRules.gameDurationMinutes).ToString()));
                        if (team.hasFinished)
                        {
                            sw.Write(String.Format(";\"{0}\"", team.finishTime.ToString()));
                        }
                        else if (team.hasGivenUp)
                        {
                            sw.Write(String.Format(";\"VZDAL\""));
                        }
                        else if (!team.hasStarted)
                        {
                            sw.Write(String.Format(";\"NEODSTARTOVAL\""));
                        }
                        else if (gameModel.gameRules.getTimeToFinishForDateTime(team.startTime).TotalMinutes > -gameModel.gameRules.gameTimeoutMinutes)
                        {
                            sw.Write(String.Format(";\"VE HŘE\""));
                        }
                        else 
                        {
                            sw.Write(String.Format(";\"TIMEOUT\""));
                        }

                            
                            sw.Write(String.Format(";\"{0}\"", gameModel.getHintsTakenForTeam(team)));
                        sw.Write(String.Format(";\"{0}\"", gameModel.getStandingsTakenForTeam(team)));
                        sw.Write("\r\n");
                    }

                    sw.Write("\r\n");
                    sw.Write("Nápovědy\r\n");
                    foreach (HOHEvent oneEvent in gameModel.events)
                    {
                        if (oneEvent.eventType == HOHEvent.HOHEventType.HintReplyEventType) {
                            sw.Write(String.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\"", oneEvent.team.name, oneEvent.puzzle.id, oneEvent.dateTime.ToString(), oneEvent.phoneNumber, oneEvent.SMSText));
                            sw.Write("\r\n");
                        }
                    }

                    sw.Write("\r\n");
                    sw.Write("Vzdali\r\n");
                    foreach (HOHEvent oneEvent in gameModel.events)
                    {
                        if (oneEvent.eventType == HOHEvent.HOHEventType.GivingUpEventType)
                        {
                            sw.Write(String.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"", oneEvent.team.name, oneEvent.dateTime.ToString(), oneEvent.phoneNumber, oneEvent.SMSText));
                            sw.Write("\r\n");
                        }
                    }

                    sw.Write("\r\n");
                    sw.Write("Odpovědi\r\n");
                    foreach (HOHEvent oneEvent in gameModel.events)
                    {
                        if (oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType)
                        {
                            sw.Write(String.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\"", oneEvent.team.name, oneEvent.puzzle.id, oneEvent.solutionText, oneEvent.dateTime.ToString(), oneEvent.phoneNumber, oneEvent.SMSText));
                            sw.Write("\r\n");
                        }
                    }

                    sw.Write("\r\n");
                    sw.Write("Žádosti o pořadí\r\n");
                    foreach (HOHEvent oneEvent in gameModel.events)
                    {
                        if (oneEvent.eventType == HOHEvent.HOHEventType.StandigsRequestType)
                        {
                            sw.Write(String.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"", oneEvent.team.name, oneEvent.dateTime.ToString(), oneEvent.phoneNumber, oneEvent.SMSText));
                            sw.Write("\r\n");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                err = -1;
            }
            return err;
        }

        public void startTeam(HOHTeam selectedTeam)
        {
            startTeam(selectedTeam, DateTime.Now);
        }
        public void startTeam(HOHTeam selectedTeam, DateTime startTime)
        {
            this.gameModel.startTeam(selectedTeam, startTime);
            this.dataModelHasChanged();
        }

        public void finishTeam(HOHTeam selectedTeam)
        {
            finishTeam(selectedTeam, DateTime.Now);
        }

        public void finishTeam(HOHTeam selectedTeam, DateTime finishTime)
        {
            this.gameModel.finishTeam(selectedTeam, finishTime);
            this.dataModelHasChanged();
        }

        public void giveUpTeam(HOHTeam selectedTeam)
        {
            giveUpTeam(selectedTeam, DateTime.Now);
        }

        public void giveUpTeam(HOHTeam selectedTeam, DateTime giveUpTime)
        {
            this.gameModel.giveUpTeam(selectedTeam, giveUpTime);
            this.dataModelHasChanged();
        }

        public int exportPuzzlesToCSV(string fileName)
        {
            return savePuzzlesToCSV(fileName);
        }

        public int importPuzzlesFromCSV(string fileName)
        {
            return importPuzzlesFromCSV(fileName, true);
        }

        public int importPuzzlesFromCSV(string fileName, bool clearGameAfterImport)
        {
            if (clearGameAfterImport)
            {
                this.clearEvents();
                this.smsManager.clearSMSMessages();
            }
            this.clearPuzzles();
            int result = this.loadPuzzlesFromCSV(fileName);
            this.dataModelHasChanged();
            return result;
        }

        public int importTeamsFromCSV(string fileName)
        {
            return importTeamsFromCSV(fileName, true);
        }

        public int importTeamsFromCSV(string fileName, bool clearGameAfterImport)
        {
            if (clearGameAfterImport)
            {
                this.clearEvents();
                this.smsManager.clearSMSMessages();
            }
            this.clearTeams();
            int result = loadTeamsFromCSV(fileName);
            this.dataModelHasChanged();
            return result;
        }

        public int importGameRulesFromCSV(string fileName)
        {
            return importGameRulesFromCSV(fileName, true);
        }

        public int importGameRulesFromCSV(string fileName, bool clearGameAfterImport)
        {
            if (clearGameAfterImport) 
            {
                this.clearEvents();
                this.smsManager.clearSMSMessages();
            }
            int result = loadGameRulesFromCSV(fileName);
            this.dataModelHasChanged();
            return result;
        }

        public int exportSMSToCSV(string fileName)
        {
            return saveSMSToCSV(fileName);
        }

        public int exportStateToCSV(string fileName)
        {
            return saveStateToCSV(fileName);
        }

        public int exportTeamsToCSV(string fileName)
        {
            return saveTeamsToCSV(fileName);
        }


        private int loadGameRulesFromCSV(string fileName)
        {
            int err = 1;
            int line = 0;

            try
            {
                // Create an instance of StreamReader to read from a file.
                using (StreamReader objReader = new StreamReader(fileName, Encoding.UTF8))
                {
                    string strLineText;
                    while ((strLineText = objReader.ReadLine()) != null)
                    {
                        line++;
                        HOHGameRules newGameRules = HOHGameRules.getGameRulesFromCSVLine(strLineText);
                        if (newGameRules != null)
                        {
                            _gameModel.gameRules = newGameRules;
                        }
                        else
                        {
                            if ((strLineText.Trim().CompareTo("") != 0) && (line != 1))
                            {
                                err = -line;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                err = 0;
            }

            return err;
        }

        public int exportGameRulesToCSV(string fileName)
        {
            return saveGameRulesToCSV(fileName);
        }

        private int saveGameRulesToCSV(string fileName)
        {
            int err = 1;
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    sw.Write(HOHGameRules.getCSVHeaderLine());           
                        sw.Write(_gameModel.gameRules.getCSVLine());
                }
            }
            catch (Exception)
            {
                err = -1;
            }
            return err;
        }

        internal void invokeDataModelChangedEvent()
        {
            this.sendDataModelEventNotifications();
        }

        internal void addEvents(List<HOHEvent> newEvents, bool live)
        {
            foreach (HOHEvent oneEvent in newEvents) {
                if (oneEvent.team.hasStarted && !oneEvent.team.hasFinished && !oneEvent.team.hasGivenUp && oneEvent.dateTime < oneEvent.team.startTime.AddMinutes(this.gameModel.gameRules.gameDurationMinutes + this.gameModel.gameRules.gameTimeoutMinutes)){
                this.gameModel.addEvent(oneEvent);
                if (oneEvent.team != null)
                {
                  if (oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType && oneEvent.puzzle.sendPlaceOnAnswer) {
                      smsManager.sendSmsStandingsForTeamAndPuzzle(oneEvent.team, oneEvent.puzzle, oneEvent.phoneNumber, live);
                  }
                  if (oneEvent.eventType == HOHEvent.HOHEventType.HintRequestEventType && this.gameModel.isHintAvailableForTeam(oneEvent.team))
                      {
                          smsManager.sendSmsHintForTeamAndPuzzle(oneEvent.team, oneEvent.puzzle, oneEvent.phoneNumber, live);
                      }
                  if (oneEvent.eventType == HOHEvent.HOHEventType.StandigsRequestType && this.gameModel.isStandingsAvailableForTeam(oneEvent.team))
                      {
                          smsManager.sendSmsStandingsForTeam(oneEvent.team, oneEvent.phoneNumber, live);
                      }
                  if (oneEvent.eventType == HOHEvent.HOHEventType.GivingUpEventType)
                  {
                      this.giveUpTeam(oneEvent.team);
                  }
                }
              }
            }
            
            this.dataModelHasChanged();
        }


        internal void invalidatePointsForTeam(HOHTeam team)
        {
            this.gameModel.invalidatePointsForTeam(team);
            this.dataModelHasChanged();
        }


        internal int importSMSFromPocketOutlook(bool liveImport)
        {
            return smsManager.importSMSFromPocketOutlook(liveImport);
        }
    }
}
