using StonePayments.Util;
using StonePayments.Util.Attributes;
using System;

namespace StonePayments.Business
{
    public class TransactionModel: BaseEntityModel, ITransactionModel
    {
        [RequiredValue(nameof(StonePaymentResource.CardNumberNotNull))]
        [RangeLengthValues(12, 19, nameof(StonePaymentResource.CardNumberBetween12_19_Digits))]
        public long? CardNumber { get; set; }

        public CardModel Card { get; set; }  // Somente usado para retorno de lista de transações.

        [MinRequiredValue(0.10, nameof(StonePaymentResource.AmountAtLeastTenCents))]
        public double? Amount { get; set; }

        public TransactionType Type { get; set; }

        [MinRequiredValue(1, nameof(StonePaymentResource.NumberMinValueEqualOne))]
        public byte? Number { get; set; }

        [RequiredValue(nameof(StonePaymentResource.PasswordIsNotBeNull))]
        [RangeLengthValues(4,6, nameof(StonePaymentResource.PasswordBetween4_6_Digits))]
        public string Password { get; set; } // Usado para envio de transação como senha do cartão para validação.

        public string CustomerName  // Somente usado no front-end para exibição quando solicitado a lista de transação.
        {
            get
            {
                return Card?.Customer?.Name;
            }
        }

        public TransactionModel()
        {
            Card = new CardModel();
        }

        /// <summary>
        /// Retorna o tipo de transação (Crédito, Débito) para seleção pelo front-end de envio de 
        /// Transação.
        /// </summary>
        public TransactionType[] TransactionTypeList
        {
            get
            {
                return (TransactionType[])Enum.GetValues(typeof(TransactionType));
            }
        }

    }
}
