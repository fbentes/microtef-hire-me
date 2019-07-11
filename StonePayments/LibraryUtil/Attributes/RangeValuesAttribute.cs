using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Util.Attributes
{
    public class RangeLengthValuesAttribute: BaseValidatorAttribute
    {
        public long MinLengthValue { get; set; }
        public long MaxLengthValue { get; set; }
        public RangeLengthValuesAttribute(long minLengthValue, long maxLengthValue, string errorMessage): base(errorMessage)
        {
            this.MinLengthValue = minLengthValue;
            this.MaxLengthValue = maxLengthValue;
        }
    }
}
