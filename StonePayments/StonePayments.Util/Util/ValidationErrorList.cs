using System.Collections.Generic;
using System.Text;

namespace StonePayments.Util
{
    /// <summary>
    /// Lista de mensagens invalidadas pela classe ValidationProperties para cada propriedade.
    /// </summary>
    public class ValidationErrorList: List<string>
    {
        /// <summary>
        /// Retorna uma string de todas as mensagens por propriedade do objeto concatenadas 
        /// pronta para ser exibida para o usuário.
        /// </summary>
        /// <returns></returns>
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
