using System;

namespace StonePaymentsBusiness
{
    public class TransactionModel
    {
        public System.Guid Id { get; set; }
        public CardModel Card { get; set; }
        public double Amount { get; set; }
        public TransactionType Type { get; set; }
        public byte Number { get; set; }
    }
}
