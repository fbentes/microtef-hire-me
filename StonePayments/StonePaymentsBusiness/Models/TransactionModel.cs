using StonePayments.Util;
using StonePayments.Util.Attributes;
using System;

namespace StonePayments.Business
{
    public class TransactionModel: BaseEntity
    {
        [RequiredValue("O campo CardNumber não pode ser vazio !")]
        [RangeLengthValues(12, 19, "O campo CardNumber deve estar entre 12 a 19 dígitos !")]
        public long? CardNumber { get; set; }

        public CardModel Card { get; set; }

        [MinRequiredValue(0.10, "O valor da transação tem que ser no mínimo 10 centavos !")]
        public double? Amount { get; set; }

        public TransactionType Type { get; set; }

        [MinRequiredValue(1, "O número de parcelas deve ser no mínimo igual 1 !")]
        public byte? Number { get; set; }

        [RequiredValue("O campo Senha não pode ser vazio !")]
        [RangeLengthValues(4,6, "A senha deve ter entre 4 e 6 dítigos")]
        public string Senha { get; set; }

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
