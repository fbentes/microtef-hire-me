/// <summary>
/// Classe de validação para propriedades de IBaseEntity que não podem ser nulas.
/// </summary>
namespace StonePayments.Util.Attributes
{
    public class RequiredValueAttribute: BaseValidatorAttribute
    {
        public RequiredValueAttribute(string errorMessage): base(errorMessage)
        {

        }
    }
}
