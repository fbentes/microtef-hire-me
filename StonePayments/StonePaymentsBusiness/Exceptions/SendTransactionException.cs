using System;
using System.Collections.Generic;
using System.Text;

namespace StonePaymentsBusiness
{
    public class SendTransactionException : Exception
    {
        public string UserMessage { get; set; }

        public SendTransactionException(string userMessage, string message): base(message)
        {
            UserMessage = userMessage;
        }
    }
}
