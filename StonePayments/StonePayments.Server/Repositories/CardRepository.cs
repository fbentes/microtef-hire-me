using StonePayments.Business;
using StonePayments.Util.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StonePayments.Server.Repositories
{
    public class CardRepository : 
        AbstractCRUDRepository<CardModel, CardException, StonePaymentsEntitiesCustom>
    {
        protected override void PrepareEntityToInsert(
            CardModel entityModel, 
            StonePaymentsEntitiesCustom context)
        {
            var Card = new Card
            {
                Id = entityModel.Id,
                CardBrand = entityModel.CardBrand.ToString(),
                Customer = entityModel.Customer.Id,
                Number = entityModel.Number.Value,
                ExpirationDate = entityModel.ExpirationDate.Value,
                HasPassword = entityModel.HasPassword.Value.ToString(),
                Password = entityModel.Password,
                Type = entityModel.Type.ToString()
            };

            context.Cards.Add(Card);
        }

        protected override void PrepareEntityToUpdate(
            CardModel entityModel, 
            StonePaymentsEntitiesCustom context)
        {
            var card = (from c in context.Cards
                        where c.Id == entityModel.Id
                        select c).FirstOrDefaultAsync<Card>().GetAwaiter().GetResult();

            card.CardBrand = entityModel.CardBrand.ToString();
            card.Customer = entityModel.Customer.Id;
            card.Number = entityModel.Number.Value;
            card.ExpirationDate = entityModel.ExpirationDate.Value;
            card.HasPassword = entityModel.HasPassword.Value.ToString();
            card.Password = entityModel.Password;
            card.Type = entityModel.Type.ToString();
        }

        protected override void PrepareEntityToDelete(
            CardModel entityModel, 
            StonePaymentsEntitiesCustom context)
        {
            var card = (from c in context.Cards
                        where c.Id == entityModel.Id
                        select c).FirstOrDefaultAsync<Card>().GetAwaiter().GetResult();

            context.Cards.Remove(card);
        }

        protected override async Task<List<CardModel>> PrepareEntityToSelect(
            CardModel entityModel,
            StonePaymentsEntitiesCustom context)
        {
            return (from c in
                        await (
                            from c0 in context.Cards
                            where entityModel.Id != Guid.Empty && c0.Id == entityModel.Id ||
                                    entityModel.Id == Guid.Empty
                            orderby c0.Number
                            select c0
                            ).ToListAsync<Card>()
                    select new CardModel()
                    {
                        Id = c.Id,
                        CardBrand = (CardBrand)Enum.Parse(typeof(CardBrand), c.CardBrand, true),
                        Customer = new CustomerModel
                        {
                            Id = c.Customer1.Id,
                            Name = c.Customer1.Nome,
                            CreditLimit = c.Customer1.CreditLimit
                        },
                        ExpirationDate = c.ExpirationDate,
                        HasPassword = bool.Parse(c.HasPassword),
                        Number = c.Number,
                        Password = c.Password,
                        Type = (CardType)Enum.Parse(typeof(CardType), c.Type, true)

                    }).ToList<CardModel>();
        }
    }
}