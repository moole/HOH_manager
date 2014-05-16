using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;


namespace HOHManager
{
    [Serializable]
    [XmlRoot("puzzle")]
    public class HOHPuzzle
    {
        [XmlElement("name")]
        public string name;

        [XmlElement("id")]
        public string id;

        public List<HOHEvent> events;

        [XmlElement("pointMultipliersForPlaces")]
        public string pointMultipliersForPlaces;
        [XmlElement("pointMultiplierForHint")]
        public Decimal pointMultiplierForHint;

        [XmlElement("solution")]
        public string solution;
        [XmlElement("points")]
        public Decimal points;

        [XmlElement("group")]
        public string standpoint;
        [XmlElement("hint")]
        public string hint;

        [XmlElement("sendPlaceOnAnswer")]
        public bool sendPlaceOnAnswer;

        private static decimal defaultPointMultiplierForHint = 0.5M;
        private static decimal defaultPointMultipierForPlace = 1.0M;
        private static char multipleValuesDelimiter = ';';

        public decimal getPointMultipierForPlace(int place)
        {
            place--;
            if (place < 0) place = 0;
            decimal multiplier = defaultPointMultipierForPlace;
            string[] components = this.pointMultipliersForPlaces.Split(multipleValuesDelimiter);
            try
            {
                if (components.Length > place)
                    multiplier = (decimal)Convert.ToDecimal(components[place]);
            }
            catch (Exception) { }
            return multiplier;
        }

        [NonSerialized]
        [XmlIgnore]
        private static string csvHeaderLine = "\"ID stanoviště\";\"ID šifry\";\"Jméno šifry\";\"Správné řešení\";\"Počet bodů za 1. správné řešení\";\"Násobitele za umístění\";\"Násobitel za využitou nápovědu\";\"Nápověda\";\"Posílat info o pořadí\"\r\n";

        [NonSerialized]
        [XmlIgnore]
        private static string csvLine = "\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4:0.00}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\"\r\n";

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

        public string getCSVLine()
        {
            string csvLine = String.Format(HOHPuzzle.csvLine, standpoint, id, name, solution, points, pointMultipliersForPlaces, pointMultiplierForHint, hint, sendPlaceOnAnswer);
            return csvLine;
        }

        public static string getCSVHeaderLine()
        {
            return HOHPuzzle.csvHeaderLine;
        }

        public static HOHPuzzle getPuzzleFromCSVLine(string csvLine)
        {

            if (csvLine.Trim().CompareTo("") == 0)
                return null; 

            HOHPuzzle newPuzzle = null;
            ArrayList alResult = CSVParser.parseCSVLine(csvLine.Trim());

            //is it header line?
            if (string.Join("+", (string[])alResult.ToArray(typeof(string))).CompareTo(string.Join("+", (string[])CSVParser.parseCSVLine(getCSVHeaderLine().Trim()).ToArray(typeof(string)))) == 0)
                return null;

            if (alResult.Count >= (HOHPuzzle.csvLine.Length - HOHPuzzle.csvLine.Replace("{", "").Length))
            {
                try
                {
                    newPuzzle = new HOHPuzzle();
                    newPuzzle.standpoint = alResult[0].ToString().Trim().ToUpper();
                    newPuzzle.id = alResult[1].ToString().Trim().ToUpper();
                    newPuzzle.name = alResult[2].ToString();
                    newPuzzle.solution = alResult[3].ToString().Trim().ToUpper();
                    newPuzzle.points = (decimal)Convert.ToDecimal(alResult[4].ToString().Trim());
                    newPuzzle.pointMultipliersForPlaces = alResult[5].ToString().Trim();
                    newPuzzle.pointMultiplierForHint = (decimal)Convert.ToDecimal(alResult[6].ToString().Trim());
                    newPuzzle.hint = alResult[7].ToString().Trim();
                    newPuzzle.sendPlaceOnAnswer = (Convert.ToDecimal(alResult[8].ToString().Trim()) != 0M);
                }
                catch { return null; }
            }

            return newPuzzle;
        }

        public HOHPuzzle()
            : this("")
        {
        }

        public HOHPuzzle(string newName)
        {
            name = newName;
            events = new List<HOHEvent>();
            pointMultiplierForHint = defaultPointMultiplierForHint;
            sendPlaceOnAnswer = false;
        }
    }
}
