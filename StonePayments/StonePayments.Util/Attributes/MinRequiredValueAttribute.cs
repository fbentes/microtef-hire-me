
namespace StonePayments.Util.Attributes
{
    /// <summary>
    /// Classe de validação para propriedades de IBaseEntity que tenham um valor mínimo de acordo
    /// com a propriedade MinValue.
    /// </summary>
    public class MinRequiredValueAttribute: BaseValidatorAttribute
    {
        public double MinValue { get; set; }

        public MinRequiredValueAttribute(double minValue, string errorMessage): base(errorMessage)
        {
            this.MinValue = minValue;
        }
    }
}
