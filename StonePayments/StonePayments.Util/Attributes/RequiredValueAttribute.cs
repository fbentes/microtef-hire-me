using System;
/// <summary>
/// Classe de validação para propriedades de IBaseEntity que não podem ser nulas.
/// </summary>
namespace StonePayments.Util.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredValueAttribute: BaseValidatorAttribute
    {
        public RequiredValueAttribute(string errorMessage): base(errorMessage)
        {

        }
    }
}
