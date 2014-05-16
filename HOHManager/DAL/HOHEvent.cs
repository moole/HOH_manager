using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace HOHManager
{
    [Serializable]
    [XmlRoot("event")]
    public class HOHEvent
    {
        private string _SMSText;

        [XmlElement("smsText")]
        public string SMSText
        {
            get { return this._SMSText; }
            set { this._SMSText = value; }
        }

        public enum HOHEventType { SolutionEventType, HintRequestEventType, HintReplyEventType, StandigsRequestType, StandingsReplyEventType, GivingUpEventType, SendFailedEventType};
        [XmlElement("smsText")]
        public HOHEventType eventType;
        [XmlElement("eventType")]
        public DateTime dateTime;
        [XmlElement("dateTime")]
        public string phoneNumber;
        [XmlElement("puzzleName")]
        public HOHPuzzle puzzle;
        [XmlElement("teamName")]
        public HOHTeam team;
        [XmlElement("solutionText")]
        public string solutionText;

        public HOHEvent()
        {
            this.dateTime = DateTime.Now;
        }

        public HOHEvent(HOHEventType eventType): this()
        {
            this.eventType = eventType;
        }

        internal bool hasCorrectSolution()
        {

            return (this.solutionText != null) && (this.solutionText.CompareTo(this.puzzle.solution) == 0);
        }
    }
}
