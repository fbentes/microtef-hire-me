using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Business;
using LightInject;
using StonePayments.Server.Services;
using System.Collections.Generic;

namespace StonePayments.Server.Tests.Services
{

    [TestClass]
    public class TransactionServiceTest: BaseTest
    {
        public ITransactionService TransactionService { get; set; }
        public TransactionServiceTest()
        {
            this.TransactionService = serviceContainer.GetInstance<ITransactionService>();
        }

        [TestMethod]
        public async Task TestSendTransactionSucess()
        {
            var transactionModel = new TransactionModel
            {
                CardNumber = 1234654789324,
                Amount = new Random().NextDouble(),
                Type = TransactionType.Credit,
                Number = (byte)new Random().Next(1,36),
                Password = "123456789"
            };

            try
            {
                List<TransactionModel> resultList = await TransactionService.SendTransaction(transactionModel);

                Assert.IsTrue(true);
            }
            catch (SendTransactionException)
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task TestSendTransactionWithFailCardNumber()
        {
            var transactionModel = new TransactionModel
            {
                CardNumber = 123465478932,
                Amount = new Random().NextDouble(),
                Type = TransactionType.Credit,
                Number = (byte)new Random().Next(1, 36),
                Password = "123456789"
            };

            try
            {
                List<TransactionModel> resultList = await TransactionService.SendTransaction(transactionModel);

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
            var result = await TransactionService.GetTransactions();

            Assert.IsNotNull(result);
        }
    }
}
