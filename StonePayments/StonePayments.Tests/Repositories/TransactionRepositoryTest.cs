using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Server.Repository;
using StonePayments.Business;
using LightInject;
using System.Collections.Generic;

namespace StonePayments.Server.Tests.Repository
{

    [TestClass]
    public class TransactionRepositoryTest: BaseTest
    {
        private ITransactionRepository TransactionDao { get; set; }

        public TransactionRepositoryTest()
        {
            this.TransactionDao = serviceContainer.GetInstance<ITransactionRepository>();
        }

        [TestMethod]
        public async Task TestSendTransactionSucess()
        {
            Random r = new Random();

            var transactionModel = new TransactionModel
            {
                CardNumber = 1234654789324,
                Amount = Math.Round(r.NextDouble() + r.Next(1, 100), 2),
                Number = (byte)r.Next(1, 48),
                Type = TransactionType.Credit,
                Password = "123456"
            };

            try
            {
                List<TransactionModel> resultList = await TransactionDao.SendTransaction(transactionModel);

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
            var result = await TransactionDao.GetTransactions();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestGetTransactionsByCardNumber()
        {
            var result = await TransactionDao.GetTransactions(1234654789324);

            Assert.IsNotNull(result);
        }
    }
}
