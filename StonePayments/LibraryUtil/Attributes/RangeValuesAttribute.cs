using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Util.Attributes
{
    public class RangeValuesAttribute: BaseValidatorAttribute
    {
        public long MinValue { get; set; }
        public long MaxValue { get; set; }
        public RangeValuesAttribute(long minValue, long maxValue, string errorMessage): base(errorMessage)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }
    }
}
