using System;
using System.Collections.Generic;
using System.Text;

namespace StonePayments.Util
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
