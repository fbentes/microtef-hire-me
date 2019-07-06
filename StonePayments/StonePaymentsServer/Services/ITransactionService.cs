using StonePaymentsBusiness;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StonePaymentsServer.Services
{
    public interface ITransactionService
    {
        Task SendTransaction(TransactionModel transactionModel);

        Task<List<TransactionModel>> GetTransactions();
    }
}