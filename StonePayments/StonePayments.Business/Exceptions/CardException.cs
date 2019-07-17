using System;

namespace StonePayments.Business
{
    public class CardException : Exception
    {
        public CardException()
        {

        }

        public CardException(string message) : base(message)
        {
        }
    }
}
