using StonePayments.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Server.Dal
{
    public interface ITransactionDao
    {
        Task SendTransaction(TransactionModel transactionModel);

        Task<List<TransactionModel>> GetTransactions();
    }
}
