using StonePaymentsBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePaymentsServer.Dal
{
    public interface ITransactionDao
    {
        Task SendTransaction(TransactionModel transactionModel);

        Task<List<TransactionModel>> GetTransactions();
    }
}
