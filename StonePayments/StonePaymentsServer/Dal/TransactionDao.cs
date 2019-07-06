using StonePaymentsBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;

namespace StonePaymentsServer.Dal
{
    public class TransactionDao : ITransactionDao
    {
        public async Task SendTransaction(TransactionModel transactionModel)
        {
            using (var context = new StonePaymentsEntities())
            {
                var trans = context.Database.BeginTransaction();

                try
                {
                        var transaction = new Transaction
                        {
                            Id = Guid.NewGuid(),
                            Amount = transactionModel.Amount,
                            Card = transactionModel.Card.Id,
                            Number = transactionModel.Number,
                            Type = transactionModel.Type.ToString()
                        };

                        context.Transactions.Add(transaction);

                        await context.SaveChangesAsync();

                        trans.Commit();
                }
                catch (Exception E)
                {
                    trans.Rollback();

                    throw new SendTransactionException(StonePaymentResource.SendTransactionError, E.Message);
                }
            }
        }

        public async Task<List<TransactionModel>> GetTransactions()
        {
            using (var context = new StonePaymentsEntities())
            {
                var results =  ( from t in 
                                      await (
                                         from t0 in context.Transactions
                                         orderby t0.Card1.Number
                                         select t0
                                       ).ToListAsync<Transaction>()
                                     select new TransactionModel()
                                     {
                                         Id = t.Id,
                                         Amount = t.Amount,
                                         Number = t.Number,
                                         Type = (TransactionType)Enum.Parse(typeof(TransactionType), t.Type, true),
                                         Card = new CardModel
                                         {
                                             Id = t.Card1.Id,
                                             CardBrand = (CardBrand)Enum.Parse(typeof(CardBrand), t.Card1.CardBrand, true),
                                             Customer = new CustomerModel
                                             {
                                                 Id = t.Card1.Customer1.Id,
                                                 Nome = t.Card1.Customer1.Nome,
                                                 CreditLimit = t.Card1.Customer1.CreditLimit
                                             },
                                             ExpirationDate = t.Card1.ExpirationDate,
                                             HasPassword = bool.Parse(t.Card1.HasPassword),
                                             Number = t.Card1.Number,
                                             Password = t.Card1.Password,
                                             Type = (CardType)Enum.Parse(typeof(CardType), t.Card1.Type, true)
                                         },
                                         
                                     }).ToList<TransactionModel>();


                return results;
            }
        }
    }
}