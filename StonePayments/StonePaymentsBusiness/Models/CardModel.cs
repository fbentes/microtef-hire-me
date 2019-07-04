﻿namespace StonePaymentsBusiness
{
    public class CardModel
    {
        public System.Guid Id { get; set; }
        public CustomerModel Customer { get; set; }
        public long Number { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public CardBrand CardBrand { get; set; }
        public string Password { get; set; }
        public CardType Type { get; set; }
        public bool HasPassword { get; set; }
    }
}