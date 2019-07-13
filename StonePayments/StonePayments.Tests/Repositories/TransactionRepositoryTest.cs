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
            var transactionModel = new TransactionModel
            {
                CardNumber = 1234654789324,
                Amount = new Random().NextDouble(),
                Number = (byte)new Random().Next(1, 48),
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
        public async Task TestGetTransactions()
        {
            var result = await TransactionDao.GetTransactions();

            Assert.IsNotNull(result);
        }
    }
}
