using StonePayments.Util;
using StonePayments.Util.Attributes;

namespace StonePayments.Business
{
    public interface ITransactionModel: IBaseEntityModel
    {
        [RequiredValue(nameof(StonePaymentResource.CardNumberNotNull))]
        [RangeLengthValues(12, 19, nameof(StonePaymentResource.CardNumberBetween12_19_Digits))]
        long? CardNumber { get; set; }

        CardModel Card { get; set; }

        [MinRequiredValue(0.10, nameof(StonePaymentResource.AmountAtLeastTenCents))]
        double? Amount { get; set; }

        TransactionType Type { get; set; }

        [MinRequiredValue(1, nameof(StonePaymentResource.NumberMinValueEqualOne))]
        byte? Number { get; set; }

        [RequiredValue(nameof(StonePaymentResource.PasswordIsNotBeNull))]
        [RangeLengthValues(4, 6, nameof(StonePaymentResource.PasswordBetween4_6_Digits))]
        string Password { get; set; }

        /// <summary>
        /// Retorna o tipo de transação (Crédito, Débito) para seleção pelo front-end de envio de 
        /// Transação.
        /// </summary>
        TransactionType[] TransactionTypeList { get; }

        string CustomerName { get; }
    }
}