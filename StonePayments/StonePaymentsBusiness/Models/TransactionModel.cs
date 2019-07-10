using Library.Util;
using Library.Util.Attributes;
using System;

namespace StonePaymentsBusiness
{
    public class TransactionModel: BaseEntity
    {
        [RequiredValue("O cartão não pode ser nulo !")]
        public CardModel Card { get; set; }

        [RequiredValue("O valor da transação não pode ser 0 !")]
        public double Amount { get; set; }

        public TransactionType Type { get; set; }

        [RequiredValue("O número de parcelas deve ser no mínimo 1 !")]
        public byte Number { get; set; }

        public TransactionModel()
        {
            Card = new CardModel();
        }


        public static TransactionType[] TransactionTypeList
        {
            get
            {
                return (TransactionType[])Enum.GetValues(typeof(TransactionType));
            }
        }
    }
}
