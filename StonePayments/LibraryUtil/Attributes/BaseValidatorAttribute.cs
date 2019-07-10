using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Util.Attributes
{
    public class BaseValidatorAttribute: Attribute
    {
        public string ErrorMessage { get; set; }

        public BaseValidatorAttribute(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}
