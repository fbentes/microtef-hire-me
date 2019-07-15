using StonePayments.Util;

namespace StonePayments.Business
{
    public class CustomerModel: BaseEntity
    {
        public string Name { get; set; }
        public double CreditLimit { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}