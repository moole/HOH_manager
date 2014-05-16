using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

namespace HOHManager
{
    [Serializable]
    [XmlRoot("HOHDataModel")]
    public class HOHGameModel
    {
        private List<HOHPuzzle> _puzzles;
        private List<HOHTeam> _teams;
        private List<HOHEvent> _events;

        [XmlElement("gameRules")]
        public HOHGameRules gameRules;

        [XmlElement("puzzle")]
        public List<HOHPuzzle> puzzles
        {
            get { return this._puzzles; }
            set { this._puzzles = value; idToPuzzle = new Dictionary<string, HOHPuzzle>(); }
        }

        [XmlElement("team")]
        public List<HOHTeam> teams
        {
            get { return this._teams; }
            set { this._teams = value; idToTeam = new Dictionary<string, HOHTeam>(); }
        }

        [XmlElement("event")]
        public List<HOHEvent> events
        {
            get { return this._events; }
            set { this._events = value; }
        }

        //caches
        private Dictionary<HOHTeam, decimal> pointsForTeam;
        private Dictionary<HOHTeam, int> hintsTakenForTeam;
        private Dictionary<HOHTeam, int> puzzlesTakenForTeam;
        private Dictionary<HOHTeam, int> standingsTakenForTeam;
        private Dictionary<HOHTeam, Dictionary<HOHPuzzle, decimal>> pointsForTeamAtPuzzle;
        private Dictionary<HOHTeam, Dictionary<HOHPuzzle, int>> hintsTakenForTeamAtPuzzle;
        private Dictionary<HOHTeam, Dictionary<HOHPuzzle, int>> placeForTeamAtPuzzle;
        private Dictionary<HOHTeam, Dictionary<HOHPuzzle, int>> correctPlaceForTeamAtPuzzle;
        private Dictionary<string, HOHPuzzle> idToPuzzle;
        private Dictionary<string, HOHTeam> idToTeam;
        private List<HOHTeam> placeForTeam;


        internal HOHPuzzle getPuzzleForPuzzleId(string puzzleId)
        {
            HOHPuzzle result = null;
            if (!idToPuzzle.ContainsKey(puzzleId))
            {
                foreach (HOHPuzzle puzzle in this.puzzles) {
                    if (!idToPuzzle.ContainsKey(puzzle.id))
                        idToPuzzle.Add(puzzle.id, puzzle);
                    if (puzzle.id == puzzleId)
                        result = puzzle;
                }
            }
            else {
                result = idToPuzzle[puzzleId];
            }

            return result;
        }

        internal HOHTeam getTeamForTeamId(string teamId)
        {
            HOHTeam result = null;
            if (!idToTeam.ContainsKey(teamId))
            {
                foreach (HOHTeam team in this.teams)
                {
                    if (!idToTeam.ContainsKey(team.id))
                        idToTeam.Add(team.id, team);
                    if (team.id == teamId)
                        result = team;
                }
            }
            else
            {
                result = idToTeam[teamId];
            }

            return result;
        }

        internal decimal getPointsForTeamAtPuzzle(HOHTeam team, HOHPuzzle puzzle)
        {
            if (!pointsForTeamAtPuzzle.ContainsKey(team))
            {
                pointsForTeamAtPuzzle.Add(team, new Dictionary<HOHPuzzle, decimal>());
            }

            if (!pointsForTeamAtPuzzle[team].ContainsKey(puzzle))
            {
                decimal points = calculatePointsForTeamAtPuzzle(team, puzzle);
                pointsForTeamAtPuzzle[team].Add(puzzle, points);
                return points;
            }
            else
            {
                return pointsForTeamAtPuzzle[team][puzzle];
            }

        }

        private decimal calculatePointsForTeamAtPuzzle(HOHTeam team, HOHPuzzle puzzle)
        {
            HOHPuzzle firstPuzzle = null;
            foreach (HOHEvent oneEvent in team.events)
            {
                if (oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType || oneEvent.eventType == HOHEvent.HOHEventType.HintReplyEventType)
                    if (oneEvent.puzzle.standpoint == puzzle.standpoint) {
                        firstPuzzle = oneEvent.puzzle;
                        break;
                    }
            }
            if (firstPuzzle == null)
                return 0;

            decimal points = 0;
            bool hintHasBeenTaken = false;
            bool answered = false;
            foreach (HOHEvent oneEvent in team.events)
            {
                if (!(oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType) && !(oneEvent.eventType == HOHEvent.HOHEventType.HintReplyEventType))
                    continue;
                if (oneEvent.puzzle != puzzle || oneEvent.puzzle != firstPuzzle)
                    continue;
                /*
                 ODP SPR NAP KOEF
                 1   1   1   0,5
                 1   1   0   BON
                 1   0   1   -1
                 1   0   0   -0,5
                 0   0   1   -0,5
                 0   0   0   0
                 
                 */
                if (oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType)
                {
                    answered = true;
                    if (oneEvent.hasCorrectSolution())
                    {
                        if (hintHasBeenTaken)
                            points += puzzle.points * puzzle.pointMultiplierForHint;
                        else
                            points += puzzle.points * puzzle.getPointMultipierForPlace(getCorrectPlaceForTeamAtPuzzle(team, puzzle));
                    }
                    else
                    {
                        if (hintHasBeenTaken)
                            points -= puzzle.points * 1;
                        else
                            points -= puzzle.points * puzzle.pointMultiplierForHint;
                    }
                    break;
                }
                if (oneEvent.eventType == HOHEvent.HOHEventType.HintReplyEventType)
                    hintHasBeenTaken = true;
            }
            if (hintHasBeenTaken && !answered)
                points -= puzzle.points * puzzle.pointMultiplierForHint;

            return points;
        }

        public int getPlaceForTeamAtPuzzle(HOHTeam team, HOHPuzzle puzzle)
        {
            if (!placeForTeamAtPuzzle.ContainsKey(team))
            {
                placeForTeamAtPuzzle.Add(team, new Dictionary<HOHPuzzle, int>());
            }

            if (!placeForTeamAtPuzzle[team].ContainsKey(puzzle))
            {
                int place = calculatePlaceForTeamAtPuzzle(team, puzzle, false);
                placeForTeamAtPuzzle[team].Add(puzzle, place);
                return place;
            }
            else
            {
                return placeForTeamAtPuzzle[team][puzzle];
            }
        }

        public int getCorrectPlaceForTeamAtPuzzle(HOHTeam team, HOHPuzzle puzzle)
        {
            if (!correctPlaceForTeamAtPuzzle.ContainsKey(team))
            {
                correctPlaceForTeamAtPuzzle.Add(team, new Dictionary<HOHPuzzle, int>());
            }

            if (!correctPlaceForTeamAtPuzzle[team].ContainsKey(puzzle))
            {
                int place = calculatePlaceForTeamAtPuzzle(team, puzzle, true);
                correctPlaceForTeamAtPuzzle[team].Add(puzzle, place);
                return place;
            }
            else
            {
                return correctPlaceForTeamAtPuzzle[team][puzzle];
            }
        }

        private int calculatePlaceForTeamAtPuzzle(HOHTeam team, HOHPuzzle puzzle, bool correctSolutionsOnly)
        {
            Dictionary<HOHTeam, int> arriveTimeAtPuzzle = new Dictionary<HOHTeam, int>();
            foreach (HOHEvent oneEvent in puzzle.events)
            {
                if (oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType && (!correctSolutionsOnly || oneEvent.hasCorrectSolution()) && !arriveTimeAtPuzzle.ContainsKey(oneEvent.team))
                {
                    arriveTimeAtPuzzle.Add(oneEvent.team, (int)oneEvent.dateTime.Subtract(oneEvent.team.startTime).TotalSeconds);
                }
            }

            if (arriveTimeAtPuzzle.ContainsKey(team))
            {

                List<KeyValuePair<HOHTeam, int>> sortedTimes = new List<KeyValuePair<HOHTeam, int>>(arriveTimeAtPuzzle);

                sortedTimes.Sort(
                      delegate(KeyValuePair<HOHTeam, int> firstPair, KeyValuePair<HOHTeam, int> nextPair)
                      {
                          return firstPair.Value.CompareTo(nextPair.Value);
                      }
                );

                return sortedTimes.IndexOf(new KeyValuePair<HOHTeam, int>(team, arriveTimeAtPuzzle[team])) + 1;
            }
            else
            {
                return arriveTimeAtPuzzle.Count + 2;
            }
        }


        public TimeSpan getTimeToFinishForTeam(HOHTeam team)
        {
            return getTimeToFinishForTeam(team, this.gameRules);
        }

        public TimeSpan getTimeToFinishForTeam(HOHTeam team, HOHGameRules gameRules) {
            return gameRules.getTimeToFinishForDateTime(team.startTime);
        }

        internal decimal getTimePenaltyForTeam(HOHTeam team)
        {
            return getTimePenaltyForTeam(team, this.gameRules);
        }

        internal decimal getTimePenaltyForTeam(HOHTeam team, HOHGameRules gameRules)
        {
            if (team.hasGivenUp)
                return gameRules.pointPenaltyForGivingUp;

            DateTime targetTime;
            if (team.hasFinished)
                targetTime = team.finishTime;
            else
                targetTime = DateTime.Now;

            return gameRules.getTimePenaltyForDateTime(team.startTime.AddMinutes(gameRules.gameDurationMinutes), targetTime);
        }

    public HOHGameModel(){

        teams = new List<HOHTeam>();
        puzzles = new List<HOHPuzzle>();
        events = new List<HOHEvent>();
        gameRules = new HOHGameRules();
        pointsForTeam = new Dictionary<HOHTeam, decimal>();
        hintsTakenForTeam = new Dictionary<HOHTeam, int>();
        puzzlesTakenForTeam = new Dictionary<HOHTeam, int>();
        standingsTakenForTeam = new Dictionary<HOHTeam, int>();
        idToPuzzle = new Dictionary<string, HOHPuzzle>();
        idToTeam = new Dictionary<string, HOHTeam>();
        pointsForTeamAtPuzzle = new Dictionary<HOHTeam, Dictionary<HOHPuzzle, decimal>>();
        placeForTeamAtPuzzle = new Dictionary<HOHTeam, Dictionary<HOHPuzzle, int>>();
        correctPlaceForTeamAtPuzzle = new Dictionary<HOHTeam, Dictionary<HOHPuzzle, int>>();
        hintsTakenForTeamAtPuzzle = new Dictionary<HOHTeam, Dictionary<HOHPuzzle, int>>();
        placeForTeam = new List<HOHTeam>();
    }

    public decimal getPointsForTeam(HOHTeam team)
    {
        if (!pointsForTeam.ContainsKey(team))
        {
            decimal pointCount = calculatePointsForTeam(team);
            pointsForTeam.Add(team, pointCount);
            return pointCount;
        }
        else
        {
            return pointsForTeam[team];
        }
    }

    public int getHintsTakenForTeam(HOHTeam team)
    {
        if (!hintsTakenForTeam.ContainsKey(team))
        {
            int hintTakenCount = calculateHintsTakenForTeam(team);
            hintsTakenForTeam.Add(team, hintTakenCount);
            return hintTakenCount;
        }
        else
        {
            return hintsTakenForTeam[team];
        }
    }

    internal int getHintsTakenForTeamAtPuzzle(HOHTeam team, HOHPuzzle puzzle)
    {
        if (!hintsTakenForTeamAtPuzzle.ContainsKey(team))
        {
            hintsTakenForTeamAtPuzzle.Add(team, new Dictionary<HOHPuzzle, int>());
        }

        if (!hintsTakenForTeamAtPuzzle[team].ContainsKey(puzzle))
        {
            int hintCount = calculateHintsTakenForTeamAtPuzzle(team, puzzle);
            hintsTakenForTeamAtPuzzle[team].Add(puzzle, hintCount);
            return hintCount;
        }
        else
        {
            return hintsTakenForTeamAtPuzzle[team][puzzle];
        }

    }

    private int calculateHintsTakenForTeamAtPuzzle(HOHTeam team, HOHPuzzle puzzle)
    {
        foreach (HOHEvent oneEvent in team.events) {
            if (oneEvent.eventType == HOHEvent.HOHEventType.StandingsReplyEventType)
                return 1;
        }
        return 0;
    }


    public int getStandingsTakenForTeam(HOHTeam team)
    {
        if (!standingsTakenForTeam.ContainsKey(team))
        {
            int standingTakenCount = calculateStandingsTakenForTeam(team);
            standingsTakenForTeam.Add(team, standingTakenCount);
            return standingTakenCount;
        }
        else {
            return standingsTakenForTeam[team];
        }
    }

    public int getPuzzlesTakenForTeam(HOHTeam team)
    {
        if (!puzzlesTakenForTeam.ContainsKey(team)){
            int puzzleTakenCount = calculatePuzzlesTakenForTeam(team);
            puzzlesTakenForTeam.Add(team, puzzleTakenCount);
            return puzzleTakenCount;
        } else {
            return puzzlesTakenForTeam[team];
        }
    }


    private decimal calculatePointsForTeam(HOHTeam team)
    {
        return calculatePointsForTeam(team, this.gameRules);
    }

    private decimal calculatePointsForTeam(HOHTeam team, HOHGameRules gameRules)
    {
        decimal points = 0;

        foreach (HOHPuzzle puzzle in this.puzzles)
            points += getPointsForTeamAtPuzzle(team, puzzle);

        points -= this.getTimePenaltyForTeam(team);

        return points;
    }

    private int calculateHintsTakenForTeam(HOHTeam team)
    {
        return calculateHintsTakenForTeam(team, this.gameRules);
    }

    private int calculateHintsTakenForTeam(HOHTeam team, HOHGameRules gameRules)
    {
        int hintsTaken = 0;

        List<HOHPuzzle> puzzlesWithHint = new List<HOHPuzzle>();

        foreach (HOHEvent oneEvent in team.events)
        {
            if (oneEvent.eventType == HOHEvent.HOHEventType.HintReplyEventType && !puzzlesWithHint.Contains(oneEvent.puzzle))
            {
                puzzlesWithHint.Add(oneEvent.puzzle);
                hintsTaken++;
            }
        }

        return hintsTaken;
    }

    private int calculateStandingsTakenForTeam(HOHTeam team)
    {
        return calculateStandingsTakenForTeam(team, this.gameRules);
    }

    private int calculateStandingsTakenForTeam(HOHTeam team, HOHGameRules gameRules)
    {
        int standingsTaken = 0;

        foreach (HOHEvent oneEvent in team.events)
        {
            if (oneEvent.eventType == HOHEvent.HOHEventType.StandingsReplyEventType)
            {
                standingsTaken++;
            }
        }

        return standingsTaken;
    }

    private int calculatePuzzlesTakenForTeam(HOHTeam team)
    {
        return calculatePuzzlesTakenForTeam(team, this.gameRules);
    }

    private int calculatePuzzlesTakenForTeam(HOHTeam team, HOHGameRules gameRules)
    {
        int puzzlesTakenCount = 0;

        List<HOHPuzzle> puzzlesTaken = new List<HOHPuzzle>();

        foreach (HOHEvent oneEvent in team.events)
        {
            if (oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType && !puzzlesTaken.Contains(oneEvent.puzzle))
            {
                puzzlesTaken.Add(oneEvent.puzzle);
                puzzlesTakenCount++;
            }
        }

        return puzzlesTakenCount;
    }

    private int calculatePuzzlesCorrectForTeam(HOHTeam team, HOHGameRules gameRules)
    {
        int puzzlesTakenCount = 0;

        List<HOHPuzzle> puzzlesTaken = new List<HOHPuzzle>();

        foreach (HOHEvent oneEvent in team.events)
        {
            if (oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType && !puzzlesTaken.Contains(oneEvent.puzzle))
            {
                puzzlesTaken.Add(oneEvent.puzzle);
                if (oneEvent.hasCorrectSolution())
                    puzzlesTakenCount++;
            }
        }

        return puzzlesTakenCount;
    }

    internal bool isHintAvailableForTeam(HOHTeam team)
    {
        return getHintsTakenForTeam(team) < gameRules.maxHintsPerTeam;
    }

    internal bool isStandingsAvailableForTeam(HOHTeam team)
    {
        return getStandingsTakenForTeam(team) < gameRules.maxStandingsPerTeam;
    }


    internal void addEvent(HOHEvent oneEvent)
    {

        if (oneEvent.team != null)
        {
            if (oneEvent.eventType == HOHEvent.HOHEventType.SolutionEventType)
            {
                try { pointsForTeam.Clear(); } catch {}
                try { puzzlesTakenForTeam.Remove(oneEvent.team); } catch {}
                try { pointsForTeamAtPuzzle.Clear(); } catch {}
                try { placeForTeamAtPuzzle.Clear(); }
                catch { }
                try { correctPlaceForTeamAtPuzzle.Clear(); }
                catch { }
                placeForTeam.Clear();
            }
            if (oneEvent.eventType == HOHEvent.HOHEventType.HintReplyEventType)
            {
                int hintTakenCount = getHintsTakenForTeam(oneEvent.team);
                this.hintsTakenForTeam[oneEvent.team] = hintTakenCount + 1;
                int hintTakenCountAtPuzzle = getHintsTakenForTeamAtPuzzle(oneEvent.team, oneEvent.puzzle);
                try { hintsTakenForTeamAtPuzzle[oneEvent.team][oneEvent.puzzle] = hintTakenCountAtPuzzle + 1; } catch {}
                try { pointsForTeam.Remove(oneEvent.team); } catch {}
                try { pointsForTeamAtPuzzle[oneEvent.team].Remove(oneEvent.puzzle); } catch {}
                placeForTeam.Clear();
            }
            if (oneEvent.eventType == HOHEvent.HOHEventType.StandingsReplyEventType)
            {
                int standingsTakenCount = getStandingsTakenForTeam(oneEvent.team);
                this.standingsTakenForTeam[oneEvent.team] = standingsTakenCount + 1;
            }
            if (oneEvent.eventType == HOHEvent.HOHEventType.GivingUpEventType)
            {
                try { pointsForTeam.Remove(oneEvent.team); } catch {}
                placeForTeam.Clear();
            }
        }

        this.events.Add(oneEvent);

        if (oneEvent.team != null)
        {
            oneEvent.team.events.Add(oneEvent);
        }

        if (oneEvent.puzzle != null)
        {
            oneEvent.puzzle.events.Add(oneEvent);
        }
    }

    internal void startTeam(HOHTeam selectedTeam, DateTime startTime)
    {
        if (selectedTeam == null) return;
        selectedTeam.startTime = startTime;
        selectedTeam.hasStarted = true;
        selectedTeam.hasGivenUp = false;
        selectedTeam.hasFinished = false;

        try { pointsForTeam.Remove(selectedTeam); } catch {}
        placeForTeam.Clear();
    }

    internal void finishTeam(HOHTeam selectedTeam, DateTime finishTime)
    {
        if (selectedTeam == null) return;
        selectedTeam.finishTime = finishTime;
        selectedTeam.hasGivenUp = false;
        selectedTeam.hasFinished = true;

        try { pointsForTeam.Remove(selectedTeam); } catch {}
        placeForTeam.Clear();
    }

    internal void giveUpTeam(HOHTeam selectedTeam, DateTime giveUpTime)
    {
        if (selectedTeam == null) return;
        selectedTeam.giveUpTime = giveUpTime;
        selectedTeam.hasFinished = false;
        selectedTeam.hasGivenUp = true;

        try { pointsForTeam.Remove(selectedTeam); }
        catch { }
        placeForTeam.Clear();
    }

    internal List<HOHTeam> getTeamPlacesList()
    {
        if (placeForTeam == null || placeForTeam.Count() == 0)
        {
            placeForTeam = calculateTeamPlacesList();
        }
        return placeForTeam;
    }

    private List<HOHTeam> calculateTeamPlacesList()
    {
        Dictionary<HOHTeam, decimal> points = new Dictionary<HOHTeam, decimal>();

        foreach (HOHTeam team in teams)
        {
            points.Add(team, getPointsForTeam(team));
        }

        List<KeyValuePair<HOHTeam, decimal>> sortedTeams = new List<KeyValuePair<HOHTeam, decimal>>(points);

        sortedTeams.Sort(
              delegate(KeyValuePair<HOHTeam, decimal> firstPair, KeyValuePair<HOHTeam, decimal> nextPair)
              {
                  return nextPair.Value.CompareTo(firstPair.Value);
              }
        );

        List<HOHTeam> actualStateList = new List<HOHTeam>();
        foreach (KeyValuePair<HOHTeam, decimal> KP in sortedTeams)
        {
            actualStateList.Add(KP.Key);
        }

        return actualStateList;
    }

    internal void invalidatePointsForTeam(HOHTeam team)
    {
        //nyni toto pouziva jen timer co kontroluje zpozdeni
        try { pointsForTeam.Remove(team); }
        catch { }
        placeForTeam.Clear();
    }
    }
}
