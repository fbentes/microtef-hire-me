using System;

namespace StonePayments.Util
{

    /// <summary>
    /// Classe para anotar propriedades que requeiram validação de formato de data de acordo com 
    /// a propriedade StringFormat.
    /// </summary>

    [AttributeUsage(AttributeTargets.Property)]
    public class DateTimeFormatAttribute : Attribute
    {
        public string StringFormat { set; get; }

        public DateTimeFormatAttribute(string stringFormat)
        {
            this.StringFormat = stringFormat;
        }
    }
}
