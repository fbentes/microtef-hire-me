using System;

namespace StonePayments.Business
{
    public class SendTransactionException : Exception
    {
        public SendTransactionException(string message) : base(message)
        {
        }
    }
}
