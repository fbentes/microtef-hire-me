using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Util.Attributes
{
    public class RequiredValueAttribute: BaseValidatorAttribute
    {
        public RequiredValueAttribute(string errorMessage): base(errorMessage)
        {

        }
    }
}
