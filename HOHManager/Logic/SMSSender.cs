using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsMobile.PocketOutlook;

namespace HOHManager
{
    public class SMSSender
    {
        public static void SmsMessageSend(string phoneNumber, string messageBody)
        {
            SmsMessage smsMessage = new SmsMessage();

            //Set the message body and recipient.
            smsMessage.Body = messageBody;
            smsMessage.To.Add(new Recipient(phoneNumber));

            //Send the SMS message.
            smsMessage.Send();

            return;
        }

    }
}
