using Library.Util;

namespace StonePaymentsBusiness
{
    public class CustomerModel: BaseEntity
    {
        public string Nome { get; set; }
        public double CreditLimit { get; set; }

    }
}