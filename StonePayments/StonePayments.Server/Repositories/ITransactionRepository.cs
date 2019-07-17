using StonePayments.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Server.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<TransactionModel>> SendTransaction(TransactionModel transactionModel);

        Task<List<TransactionModel>> GetTransactions(long? cardNumber = null);
    }
}
