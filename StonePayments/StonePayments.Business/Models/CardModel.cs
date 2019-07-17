using Newtonsoft.Json;
using StonePayments.Util;
using StonePayments.Util.Attributes;

namespace StonePayments.Business
{
    public class CardModel: BaseEntityModel
    {
        public CustomerModel Customer { get; set; }

        [RequiredValue(nameof(StonePaymentResource.CardNumberNotNull))]
        [RangeLengthValues(12, 19, nameof(StonePaymentResource.CardNumberBetween12_19_Digits))]
        public long? Number { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        [RequiredValue(nameof(StonePaymentResource.ExpirationDateNotNull))]
        public System.DateTime? ExpirationDate { get; set; }

        public CardBrand CardBrand { get; set; }

        [RequiredValue(nameof(StonePaymentResource.PasswordIsNotBeNull))]
        public string Password { get; set; }

        public CardType Type { get; set; }

        [RequiredValue(nameof(StonePaymentResource.HasPasswordNotNull))]
        public bool? HasPassword { get; set; }

        public override string ToString()
        {
            return Number?.ToString();
        }
    }
}