using LightInject;
using StonePayments.Business;
using StonePayments.Server.Dal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StonePayments.Server.Services
{
    public class TransactionService : ITransactionService
    {
        [Inject]
        public ITransactionDao TransactionDao { get; set; }

        public async Task SendTransaction(TransactionModel transactionModel)
        {
            await TransactionDao.SendTransaction(transactionModel);
        }

        public async Task<List<TransactionModel>> GetTransactions()
        {
            return await TransactionDao.GetTransactions();
        }
    }
}