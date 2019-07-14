using Newtonsoft.Json;
using StonePayments.Util;

namespace StonePayments.Business
{
    public class CardModel: BaseEntity
    {
        public CustomerModel Customer { get; set; }
        public long? Number { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public System.DateTime? ExpirationDate { get; set; }
        public CardBrand CardBrand { get; set; }
        public string Password { get; set; }
        public CardType Type { get; set; }
        public bool? HasPassword { get; set; }
    }
}