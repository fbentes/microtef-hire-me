using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Util
{
    public class DateTimeFormatAttribute : Attribute
    {
        public string StringFormat { set; get; }

        public DateTimeFormatAttribute(string stringFormat)
        {
            this.StringFormat = stringFormat;
        }
    }
}
