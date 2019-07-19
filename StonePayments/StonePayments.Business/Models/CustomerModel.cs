using StonePayments.Util;
using StonePayments.Util.Attributes;

namespace StonePayments.Business
{
    public class CustomerModel: BaseEntityModel, ICustomerModel
    {
        [RequiredValue(nameof(StonePaymentResource.NameNotNull))]
        public string Name { get; set; }

        [RequiredValue(nameof(StonePaymentResource.CreditLimitNotNull))]
        [MinRequiredValue(0, nameof(StonePaymentResource.CreditLimitMinValueEqualZero))]
        public double? CreditLimit { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}