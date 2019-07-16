using System;

namespace StonePayments.Util.Attributes
{
    /// <summary>
    /// Classe base para todas as classes de validação de propriedades de implementadores de IBaseEntity.
    /// </summary>
    public class BaseValidatorAttribute: Attribute
    {
        public string ErrorMessage { get; set; }

        public BaseValidatorAttribute(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}
