using System;

namespace StonePayments.Business
{
    public class CustomerException : Exception
    {
        public CustomerException()
        {

        }

        public CustomerException(string message) : base(message)
        {
        }
    }
}
