using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;
using System.Windows.Forms;


namespace HOHManager
{
    [Serializable]
    [XmlRoot("team")]
    public class HOHTeam
    {
        [XmlElement("name")]
        public string name;
        [XmlElement("id")]
        public string id;
        [XmlElement("startTime")]
        public DateTime startTime;
        [XmlElement("finishTime")]
        public DateTime finishTime;
        [XmlElement("giveUpTime")]
        public DateTime giveUpTime;
        [XmlElement("hasFinished")]
        public bool hasFinished;
        [XmlElement("hasStarted")]
        public bool hasStarted;
        [XmlElement("hasGivenUp")]
        public bool hasGivenUp;

        public List<HOHEvent> events;

        public string description
        {
            get {
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
        private static string csvHeaderLine = "\"Jméno týmu\";\"ID týmu\";\"Čas startu\"\r\n";

        [NonSerialized]
        [XmlIgnore]
        private static string csvLine = "\"{0}\";\"{1}\";\"{2}\"\r\n";

        public string getCSVLine() {
            string csvLine = String.Format(HOHTeam.csvLine, name, id, startTime);
            return csvLine;
        }

        public static string getCSVHeaderLine() {
            return HOHTeam.csvHeaderLine;
        }

        public static HOHTeam getTeamFromCSVLine(string csvLine){

            if (csvLine.Trim().CompareTo("") == 0)
                return null;
            HOHTeam newTeam = null;
            ArrayList alResult = CSVParser.parseCSVLine(csvLine);

            if (string.Join("+", (string[])alResult.ToArray(typeof(string))).CompareTo(string.Join("+", (string[])CSVParser.parseCSVLine(getCSVHeaderLine().Trim()).ToArray(typeof(string)))) == 0)
                return null;

            if (alResult.Count >= (HOHTeam.csvLine.Length - HOHTeam.csvLine.Replace("{", "").Length))
            {
                try
                {
                    newTeam = new HOHTeam();
                    newTeam.name = alResult[0].ToString();
                    newTeam.id = alResult[1].ToString().ToUpper();
                    try
                    {
                        newTeam.startTime = DateTime.Parse(alResult[2].ToString());
                    }
                    catch {
                        newTeam.startTime = DateTime.Now;
                    }
                }
                catch { return null; }
            }

            if (newTeam.id.CompareTo("") == 0)
                newTeam.id = newTeam.name;
            if (newTeam.startTime.CompareTo(DateTime.Now) <= 0)
                newTeam.hasStarted = true;

            return newTeam;
        }

        public HOHTeam() : this ("")
        {
        }

        public HOHTeam(string newName)
        {
            name = newName;
            events = new List<HOHEvent>();

            hasGivenUp = false;
            hasStarted = false;
            hasFinished = false;
        }
    }
}
