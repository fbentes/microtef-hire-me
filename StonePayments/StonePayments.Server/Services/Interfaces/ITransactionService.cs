using StonePayments.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StonePayments.Server.Services
{
    public interface ITransactionService
    {
        Task<List<TransactionModel>> SendTransaction(TransactionModel transactionModel);

        Task<List<TransactionModel>> GetTransactions(long? cardNumber = null);
    }
}