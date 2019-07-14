using System;
using System.Collections.Generic;
using System.Text;

namespace StonePayments.Util
{
    /// <summary>
    /// Lista com o par <Propriedade, Mensagem> de mensagens invalidadas pela classe ValidationProperties para 
    /// cada propriedade.
    /// </summary>
    public class ValidationErrorList: List<string>
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
