using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Util.Attributes
{
    public class MinRequiredValueAttribute: BaseValidatorAttribute
    {
        public double MinValue { get; set; }

        public MinRequiredValueAttribute(double minValue, string errorMessage): base(errorMessage)
        {
            this.MinValue = minValue;
        }
    }
}
