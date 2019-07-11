using System;

namespace StonePayments.Business
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
