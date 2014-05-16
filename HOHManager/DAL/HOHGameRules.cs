using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;


namespace HOHManager
{
    [Serializable]
    [XmlRoot("gameRules")]
    public class HOHGameRules
    {
        [XmlElement("gameDurationMinutes")]
        public int gameDurationMinutes;
        [XmlElement("pointPenaltiesForLateness")]
        public string pointPenaltiesForLateness;
        [XmlElement("gameTimeoutMinutes")]
        public int gameTimeoutMinutes;
        [XmlElement("pointPenaltyForTimeout")]
        public decimal pointPenaltyForTimeout;
        [XmlElement("pointPenaltyForGivingUp")]
        public decimal pointPenaltyForGivingUp;
        [XmlElement("maxHintsPerTeam")]
        public int maxHintsPerTeam;
        [XmlElement("maxStandingsPerTeam")]
        public int maxStandingsPerTeam;

        private static int defaultGameDurationMinutes = 12 * 60;
        private static decimal defaultPointPenaltiesForLateness = 10M;
        private static int defaultGameTimeoutMinutes = 30;
        private static int defaultMaxHintsPerTeam = 999;
        private static int defaultMaxStandingsPerTeam = 999;
        private static decimal defaultPointPenaltyForTimeout = 100000M;
        private static decimal defaultPointPenaltyForGivingUp = 100000M;
        private static char multipleValuesDelimiter = ';';

        public string description
        {
            get
            {
                ArrayList headers = CSVParser.parseCSVLine(getCSVHeaderLine().Trim());
                ArrayList values = CSVParser.parseCSVLine(getCSVLine().Trim());
                string outputString = "";
                for (int i = 0; i < headers.Count; i++)
                    outputString += headers[i] + ": " + values[i] + "\r\n";
                return outputString;
            }
        }

        [NonSerialized]
        [XmlIgnore]
        private static string csvHeaderLine = "\"Doba hry v minutách\";\"Penalizace za 10 minut zpoždění\";\"Limit minut pro příchod do cíle\";\"Max. počet nápověd na tým\";\"Max. počet žádostí o pořadí na tým\"\r\n";
        [NonSerialized]
        [XmlIgnore]
        private static string csvLine = "\"{0}\";\"{1:0.00}\";\"{2}\";\"{3}\";\"{4}\"\r\n";

        public string getCSVLine()
        {
            string csvLine = String.Format(HOHGameRules.csvLine, gameDurationMinutes, pointPenaltiesForLateness, gameTimeoutMinutes, maxHintsPerTeam, maxStandingsPerTeam);
            return csvLine;
        }

        public static string getCSVHeaderLine()
        {
            return HOHGameRules.csvHeaderLine;
        }

        public static HOHGameRules getGameRulesFromCSVLine(string csvLine)
        {

            if (csvLine.Trim().CompareTo("") == 0)
                return null;

            HOHGameRules newGameRules = null;
            ArrayList alResult = CSVParser.parseCSVLine(csvLine);

            if (string.Join("+", (string[])alResult.ToArray(typeof(string))).CompareTo(string.Join("+", (string[])CSVParser.parseCSVLine(getCSVHeaderLine().Trim()).ToArray(typeof(string)))) == 0)
                return null;

            if (alResult.Count >= (HOHGameRules.csvLine.Length - HOHGameRules.csvLine.Replace("{", "").Length))
            {
                try
                {
                    newGameRules = new HOHGameRules();
                    newGameRules.gameDurationMinutes = Convert.ToInt32(alResult[0]);
                    newGameRules.pointPenaltiesForLateness = alResult[1].ToString();
                    newGameRules.gameTimeoutMinutes = Convert.ToInt32(alResult[2]);
                    newGameRules.maxHintsPerTeam = Convert.ToInt32(alResult[3]);
                    newGameRules.maxStandingsPerTeam = Convert.ToInt32(alResult[4]);
                }
                catch { return null; }
            }

            return newGameRules;
        }


        public HOHGameRules() {
            this.gameDurationMinutes = defaultGameDurationMinutes;
            this.gameTimeoutMinutes = defaultGameTimeoutMinutes;
            this.pointPenaltyForGivingUp = defaultPointPenaltyForGivingUp;
            this.pointPenaltyForTimeout = defaultPointPenaltyForTimeout;
            this.maxHintsPerTeam = defaultMaxHintsPerTeam;
            this.maxStandingsPerTeam = defaultMaxStandingsPerTeam;
        }

        internal decimal getTimePenaltyForDateTime(DateTime gameEndTime, DateTime teamTime)
        {
            int minutesLate = (int)teamTime.Subtract(gameEndTime).TotalMinutes;
            if (minutesLate > 0)
                if (minutesLate <= this.gameTimeoutMinutes)
                    return this.getPointPenaltyForMinutesLate(minutesLate);
                else
                    return this.pointPenaltyForTimeout;
            else
                return 0;
        }


        public decimal getPointPenaltyForMinutesLate(int minutes)
        {
            decimal sum = 0;
            for (int i = 1; i < minutes; i++)
                sum += this.getPointPenaltyForLateness(i);
            return sum;
        }

        public decimal getPointPenaltyForLateness(int minutes)
        {
            decimal penalty = defaultPointPenaltiesForLateness;
            try
            {
                string[] components = this.pointPenaltiesForLateness.Split(multipleValuesDelimiter);
                if (components.Length > minutes / 10)
                    penalty = Convert.ToDecimal(components[minutes / 10]);
            }
            catch (Exception) { }
            return penalty;
        }

        public TimeSpan getTimeToFinishForDateTime(DateTime targetTime)
        {

            return TimeSpan.FromSeconds(this.gameDurationMinutes * 60 - (DateTime.Now.Subtract(targetTime).TotalSeconds));
        }
    }
}
