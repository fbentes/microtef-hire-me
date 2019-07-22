using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Server.Repositories;
using StonePayments.Business;
using LightInject;
using System.Collections.Generic;

namespace StonePayments.Server.Tests.Repository
{

    [TestClass]
    public class TransactionRepositoryTest: BaseTest
    {
        private ITransactionRepository TransactionRepository { get; set; }

        public TransactionRepositoryTest()
        {
            this.TransactionRepository = serviceContainer.GetInstance<ITransactionRepository>();
        }

        [TestMethod]
        public async Task TestSendTransactionSucess()
        {
            Random r = new Random();

            var transactionModel = new TransactionModel
            {
                CardNumber = 5323467684667,
                Amount = Math.Round(r.NextDouble() + r.Next(1, 100), 2),
                Number = (byte)r.Next(1, 48),
                Type = TransactionType.Credit,
                Password = "555666"
            };

            try
            {
                List<TransactionModel> resultList = await TransactionRepository.SendTransaction(transactionModel);

                Assert.IsTrue(true);
            }
            catch (SendTransactionException)
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task TestGetAllTransactions()
        {
            var result = await TransactionRepository.GetTransactions();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestGetTransactionsByCardNumber()
        {
            var result = await TransactionRepository.GetTransactions(1234654789324);

            Assert.IsNotNull(result);
        }
    }
}
