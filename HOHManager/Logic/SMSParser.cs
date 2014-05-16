using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HOHManager
{
    public class SMSParser
    {
        SMSManager smsManager;
        //SMS FORMAT:
        //<team id>[:, ]<puzzleId>:<solution> 
        //<team id>[:, ]<puzzleId> NAPOVEDA
        //<team id>[:, ]PORADI

        private static string puzzleIdVsSolutionSeparator = ":";

        internal List<HOHEvent> parseSMS(SMSMessageProcess smsMessageProces)
        {
            HOHTeam team = null;
            HOHPuzzle puzzle = null;
            string teamName = "", puzzleName = "", remaining = "";
            List<HOHEvent> newEvents = new List<HOHEvent>();
            string smsBody = smsMessageProces.getMessageBody();
            smsBody = " " + smsBody.Trim().ToUpper();
            teamName = smsBody;

            do
            {
                teamName = teamName.Substring(0, teamName.LastIndexOf(" "));
                remaining = smsBody.Substring(teamName.Length);
                team = this.smsManager.dataManager.gameModel.getTeamForTeamId(teamName.Trim().ToUpper());
            } while (team == null && teamName.CompareTo("") != 0);

            if (team == null) return null;
            if (remaining.EndsWith("NAPOVEDA"))
            {
                puzzleName = remaining.Substring(0, remaining.Length - "NAPOVEDA".Length).Trim();
                puzzle = this.smsManager.dataManager.gameModel.getPuzzleForPuzzleId(puzzleName.Trim().ToUpper());
                if (puzzle == null) return null;
                HOHEvent newEvent = new HOHEvent();
                newEvent.eventType = HOHEvent.HOHEventType.HintRequestEventType;
                newEvent.SMSText = smsBody;
                newEvent.phoneNumber = smsMessageProces.getRecipientPhoneNumber();
                newEvent.dateTime = smsMessageProces.getReceivedDate();
                newEvent.team = team;
                newEvent.puzzle = puzzle;
                newEvents.Add(newEvent);
            }
            else
                if (remaining.EndsWith("PORADI"))
                {
                    HOHEvent newEvent = new HOHEvent();
                    newEvent.eventType = HOHEvent.HOHEventType.StandigsRequestType;
                    newEvent.SMSText = smsBody;
                    newEvent.phoneNumber = smsMessageProces.getRecipientPhoneNumber();
                    newEvent.dateTime = smsMessageProces.getReceivedDate();
                    newEvent.team = team;
                    newEvent.puzzle = null;
                    newEvents.Add(newEvent);
                }
            else if (remaining.EndsWith("VZDAVAME"))
            {
                HOHEvent newEvent = new HOHEvent();
                newEvent.eventType = HOHEvent.HOHEventType.GivingUpEventType;
                newEvent.SMSText = smsBody;
                newEvent.phoneNumber = smsMessageProces.getRecipientPhoneNumber();
                newEvent.dateTime = smsMessageProces.getReceivedDate();
                newEvent.team = team;
                newEvent.puzzle = null;
                newEvents.Add(newEvent);
            }
            else
                {
                    String answer = "";
                    if (remaining.LastIndexOf(puzzleIdVsSolutionSeparator) != -1)
                    do
                    {
                        int lastdelim = remaining.LastIndexOf(puzzleIdVsSolutionSeparator);
                        puzzle = null;
                        puzzleName = remaining.Substring(0, lastdelim);
                        answer = remaining.Substring(lastdelim + 1);
                        do
                        {
                            puzzleName = puzzleName.Substring(puzzleName.IndexOf(" ") + 1);
                            puzzle = this.smsManager.dataManager.gameModel.getPuzzleForPuzzleId(puzzleName.Trim().ToUpper());
                        } while (puzzle == null && puzzleName.IndexOf(" ") > -1);
                        remaining = remaining.Substring(0, lastdelim - puzzleName.Length);
                        if (puzzle != null) {
                            HOHEvent newEvent = new HOHEvent();
                            newEvent.eventType = HOHEvent.HOHEventType.SolutionEventType;
                            newEvent.SMSText = smsBody;
                            newEvent.phoneNumber = smsMessageProces.getRecipientPhoneNumber();
                            newEvent.dateTime = smsMessageProces.getReceivedDate();
                            newEvent.team = team;
                            newEvent.puzzle = puzzle;
                            newEvent.solutionText = answer.Trim().ToUpper();
                            newEvents.Add(newEvent);
                        }
                    } while (remaining.LastIndexOf(puzzleIdVsSolutionSeparator) != -1 && puzzle != null);
                    if (remaining.Trim().CompareTo("") != 0 || puzzle == null)
                        return null; 
                }


            return newEvents;
        }

        public SMSParser(SMSManager newSmsManager) {
            this.smsManager = newSmsManager;

        }

    }
}
