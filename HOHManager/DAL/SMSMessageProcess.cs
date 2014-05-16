using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsMobile.PocketOutlook;

namespace HOHManager
{
    public class SMSMessageProcess
    {
        private SmsMessage smsMessage;
        public bool processed;
        public bool ignore;
        public bool unparsable;
        private string correctedBody;
        private string correctedPhoneNumber;
        private DateTime correctedReceivedDate;
        public bool live;

        public SMSMessageProcess(SmsMessage newSmsMessage) {
            this.smsMessage = newSmsMessage;
            this.processed = false;
            this.ignore = false;
            this.correctedBody = "";
            this.correctedPhoneNumber = "";
            this.correctedReceivedDate = DateTime.MinValue;
            this.live = true;
        }

        public string getMessageBody(){
            if (this.correctedBody.CompareTo("") == 0 && this.smsMessage != null)
                return this.smsMessage.Body;
            else return this.correctedBody;
        }

        public void setMessageBody(string newMessageBody){
            this.correctedBody = newMessageBody;
        }

        public string getRecipientPhoneNumber(){
            if (this.smsMessage == null || this.smsMessage.From == null || this.smsMessage.From.Address == null || this.smsMessage.From.Address.CompareTo("") == 0)
            {
                if (this.correctedPhoneNumber.CompareTo("") == 0)
                {
                    return "<>";
                }
                else
                {
                    return this.correctedPhoneNumber;
                }
            }
            else return this.smsMessage.From.Address;
        }

        public void setRecipientPhoneNumber(string newPhoneNumber){
            this.correctedPhoneNumber = newPhoneNumber;
        }

        public DateTime getReceivedDate(){
            if (this.correctedReceivedDate == DateTime.MinValue && this.smsMessage != null)
                return this.smsMessage.Received;
            else return this.correctedReceivedDate;
        }

        public void setReceivedDate(DateTime newReceivedDate){
            this.correctedReceivedDate = newReceivedDate;
        }

        internal string getOriginalMessageBody()
        {
            if (this.smsMessage != null)
                return this.smsMessage.Body;
            else return this.correctedBody;
        }
    }
}
