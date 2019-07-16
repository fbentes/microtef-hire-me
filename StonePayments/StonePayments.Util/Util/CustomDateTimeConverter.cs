using Newtonsoft.Json.Converters;

namespace StonePayments.Util
{
    /// <summary>
    /// Classe utilizada para anotação de propriedades DateTime de objetos para poderem ser 
    /// deserializados pelo método Newtonsoft.Json.JsonConvert.DeserializeObject.
    /// </summary>
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "dd/MM/yyyy";
        }
    }
}
