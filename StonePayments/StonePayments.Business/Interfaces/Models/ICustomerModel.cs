using StonePayments.Util;
using StonePayments.Util.Attributes;

namespace StonePayments.Business
{
    public interface ICustomerModel: IBaseEntityModel
    {
        [RequiredValue(nameof(StonePaymentResource.NameNotNull))]
        string Name { get; set; }

        [RequiredValue(nameof(StonePaymentResource.CreditLimitNotNull))]
        [MinRequiredValue(0, nameof(StonePaymentResource.CreditLimitMinValueEqualZero))]
        double? CreditLimit { get; set; }
    }
}