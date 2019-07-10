using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Util
{
    public class ErrorList: List<string>
    {
        public override string ToString()
        {
            StringBuilder errorMessages = new StringBuilder(this.Count);

            foreach (var error in this)
            {
                errorMessages.AppendLine(error);
            }

            return errorMessages.ToString();
        }
    }
}
