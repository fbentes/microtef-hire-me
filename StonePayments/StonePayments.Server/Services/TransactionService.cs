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
            // Caso outro cliente não valide o objeto, o servidor garante a integridade do objeto 
            // antes de enviá-lo para o banco de dados.
            ValidationErrorList errorList = new ValidationErrorList();

            bool isValid = ValidationProperties.IsValid(transactionModel, out errorList);

            if(!isValid)
            {
                throw new SendTransactionException(errorList.ToString());
            }

            return await TransactionRepository.SendTransaction(transactionModel);
        }

        public async Task<List<TransactionModel>> GetTransactions(long? cardNumber = null)
        {
            return await TransactionRepository.GetTransactions(cardNumber);
        }
    }
}