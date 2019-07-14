using LightInject;
using StonePayments.Business;
using StonePayments.Server.Repository;
using StonePayments.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StonePayments.Server.Services
{
    public class TransactionService : ITransactionService
    {
        [Inject]
        public ITransactionRepository TransactionRepository { get; set; }

        public async Task<List<TransactionModel>> SendTransaction(TransactionModel transactionModel)
        {
            ValidationErrorList errorList = new ValidationErrorList();

            bool isValid = ValidationProperties.IsValid(transactionModel, out errorList);

            if(!isValid)
            {
                throw new SendTransactionException(errorList.ToString());
            }

            return await TransactionRepository.SendTransaction(transactionModel);
        }

        public async Task<List<TransactionModel>> GetTransactions()
        {
            return await TransactionRepository.GetTransactions();
        }
    }
}