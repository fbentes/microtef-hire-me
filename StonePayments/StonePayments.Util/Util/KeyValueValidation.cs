using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Util
{
    public class KeyValueValidation : Dictionary<string, string>
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
