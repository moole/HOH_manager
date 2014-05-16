using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;
using Microsoft.WindowsMobile.PocketOutlook;

namespace HOHManager
{
    public class SMSFetcher : IDisposable
    {

        private SMSManager smsManager;
        private Dictionary<string, MessageInterceptor> _smsInterceptors = null;

        public void addSmsInterceptorForPhoneNumber(string phoneNumber)
        {
            MessageInterceptor smsInterceptor = new MessageInterceptor(InterceptionAction.Notify, true);
            if (phoneNumber.CompareTo("") != 0)
                smsInterceptor.MessageCondition = new MessageCondition(MessageProperty.Sender, phoneNumber);
            else
                phoneNumber = "any";
            smsInterceptor.MessageReceived += SmsInterceptor_MessageReceived;
            _smsInterceptors.Add(phoneNumber, smsInterceptor);
        }

        public void removeSmsInterceptorForPhoneNumber(string phoneNumber){
            if (phoneNumber.CompareTo("") == 0)
                phoneNumber = "any";
            if (_smsInterceptors.ContainsKey(phoneNumber))
            {
                MessageInterceptor smsInterceptor = (_smsInterceptors[phoneNumber] as MessageInterceptor);
                smsInterceptor.MessageReceived -= SmsInterceptor_MessageReceived;
                smsInterceptor.Dispose();
                _smsInterceptors.Remove(phoneNumber);
            }
        }

        public void removeAllSmsInterceptors() {
            foreach (KeyValuePair<string, MessageInterceptor> kvp in _smsInterceptors)
            {
                MessageInterceptor smsInterceptor = (kvp.Value as MessageInterceptor);
                smsInterceptor.MessageReceived -= SmsInterceptor_MessageReceived;
                smsInterceptor.Dispose();
            }
            _smsInterceptors.Clear();
        }

        void SmsInterceptor_MessageReceived(object sender, MessageInterceptorEventArgs e)
        {
            SmsMessage newMessage = e.Message as SmsMessage;
            
            smsManager.newIncomingMessage(newMessage);
        }

        public SMSFetcher(SMSManager newSmsManager)
        {
            smsManager = newSmsManager;
            _smsInterceptors = new Dictionary<string, MessageInterceptor>();
        }

        ~SMSFetcher()
        {
            Dispose(false);
        }

        #region IDisposable Members

        public void Dispose()
        { 
            Dispose(true); 
            GC.SuppressFinalize(this); 
        } 
     
        protected virtual void Dispose(bool disposing) 
        { 
            if (disposing) 
            { 
                // get rid of managed resources 
                
            }    
            // get rid of unmanaged resources 
            this.removeAllSmsInterceptors();
        }

        #endregion
    }
}
