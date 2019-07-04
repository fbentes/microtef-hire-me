using StonePaymentsBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;

namespace StonePaymentsServer.Dal
{
    public class TransactionDao
    {
        public async Task sendTransaction(TransactionModel transactionModel)
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
                            Type = transactionModel.Type.ToString().Substring(1, 1)
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
                var results = await (from t in context.Transactions
                                     orderby t.Card1.Number
                                     select new TransactionModel()
                                     {
                                         Id = t.Id,
                                         Amount = t.Amount,
                                         Card = new CardModel
                                         {
                                             Id = t.Card1.Id,
                                             
                                         }

                                     }).ToListAsync<TransactionModel>();

                return results;
            }
        }
    }
}