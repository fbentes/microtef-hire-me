using StonePayments.Util;

namespace StonePayments.Business
{
    public class CustomerModel: BaseEntity
    {
        public string Nome { get; set; }
        public double CreditLimit { get; set; }

    }
}