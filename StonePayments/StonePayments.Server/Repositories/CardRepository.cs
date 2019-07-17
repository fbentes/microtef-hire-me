using StonePayments.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;


namespace StonePayments.Server.Repositories
{
    public class CardRepository : ICardRepository
    {
        public async Task Insert(CardModel cardModel)
        {
            using (var context = new StonePaymentsEntitiesCustom())
            {
                var trans = context.Database.BeginTransaction();

                try
                {
                    var customer = await (from c in context.Customers
                                          where c.Id == cardModel.Customer.Id
                                          select c).FirstOrDefaultAsync<Customer>();

                    if(customer == null)
                    {
                        throw new CustomerException(StonePaymentResource.CustomerNotFound);
                    }

                    var card = new Card
                    {
                        Id = Guid.NewGuid(),
                        Customer = cardModel.Customer.Id,
                        CardBrand = cardModel.CardBrand.ToString(),
                        ExpirationDate = cardModel.ExpirationDate.Value,
                        HasPassword = cardModel.HasPassword.Value.ToString(),
                        Number = cardModel.Number.Value,
                        Password = cardModel.Password,
                        Type = cardModel.Type.ToString()
                    };

                    context.Cards.Add(card);

                    await context.SaveChangesAsync();

                    trans.Commit();
                }
                catch (Exception E)
                {
                    trans.Rollback();

                    throw new CardException(E.Message);
                }
            }
        }

        public async Task Update(CardModel cardModel)
        {
            using (var context = new StonePaymentsEntitiesCustom())
            {
                var trans = context.Database.BeginTransaction();

                try
                {
                    var customer = await (from c in context.Customers
                                          where c.Id == cardModel.Customer.Id
                                          select c).FirstOrDefaultAsync<Customer>();

                    if (customer == null)
                    {
                        throw new CustomerException(StonePaymentResource.CustomerNotFound);
                    }

                    var card = await (from c in context.Cards
                                          where c.Id == cardModel.Id
                                          select c).FirstOrDefaultAsync<Card>();

                    card.Number = cardModel.Number.Value;
                    card.Customer = cardModel.Customer.Id;
                    card.CardBrand = cardModel.CardBrand.ToString();
                    card.ExpirationDate = cardModel.ExpirationDate.Value;
                    card.HasPassword = cardModel.HasPassword.Value.ToString();
                    card.Type = cardModel.Type.ToString();
                    card.Password = cardModel.Password;

                    await context.SaveChangesAsync();

                    trans.Commit();
                }
                catch (Exception E)
                {
                    trans.Rollback();

                    throw new CardException(E.Message);
                }
            }
        }

        public async Task Delete(string id)
        {
            using (var context = new StonePaymentsEntitiesCustom())
            {
                var trans = context.Database.BeginTransaction();

                try
                {
                    var card = await (from c in context.Cards
                                      where c.Id.ToString() == id
                                      select c).FirstOrDefaultAsync<Card>();

                    context.Cards.Remove(card);

                    await context.SaveChangesAsync();

                    trans.Commit();
                }
                catch (Exception E)
                {
                    trans.Rollback();

                    throw new CardException(E.Message);
                }

            }
        }

        public async Task<List<CardModel>> GetCards(string id = "")
        {
            using (var context = new StonePaymentsEntitiesCustom())
            {
                var result = (from c in
                                    await (
                                       from c0 in context.Cards
                                       where id != "" && c0.Id.ToString() == id ||
                                             id == null
                                       orderby c0.Number
                                       select c0
                                     ).ToListAsync<Card>()
                              select new CardModel()
                              {
                                  Id = c.Id,
                                  Number = c.Number,
                                  CardBrand = (CardBrand)Enum.Parse(typeof(CardBrand), c.CardBrand, true),
                                  Customer = new CustomerModel
                                  {
                                      Id = c.Customer1.Id,
                                      Name = c.Customer1.Nome,
                                      CreditLimit = c.Customer1.CreditLimit
                                  },
                                  ExpirationDate = c.ExpirationDate,
                                  HasPassword = bool.Parse(c.HasPassword),
                                  Password = c.Password,
                                  Type = (CardType)Enum.Parse(typeof(CardType), c.Type, true)
                              }).ToList<CardModel>();

                return result;
            }
        }
    }
}
