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
            Random r = new Random();

            var transactionModel = new TransactionModel
            {
                CardNumber = 5323467684667,
                Amount = Math.Round(r.NextDouble() + r.Next(1, 100), 2),
                Type = TransactionType.Credit,
                Number = (byte)new Random().Next(1,36),
                Password = "555666"
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
        public async Task TestSendTransactionWithFailCardNumberBetween12_19_Digits()
        {
            var transactionModel = new TransactionModel
            {
                CardNumber = 12346547893,
                Amount = new Random().NextDouble(),
                Type = TransactionType.Credit,
                Number = (byte)new Random().Next(1, 36),
                Password = "132456"  // 123456
            };

            try
            {
                List<TransactionModel> resultList = await TransactionService.SendTransaction(transactionModel);

                Assert.IsTrue(false);
            }
            catch (SendTransactionException E)
            {
                Assert.IsTrue(E.Message.Contains(StonePaymentResource.CardNumberBetween12_19_Digits));
            }
        }

        [TestMethod]
        public async Task TestSendTransactionWithFailCardNumberNotNull()
        {
            var transactionModel = new TransactionModel
            {
                CardNumber = null,
                Amount = new Random().NextDouble(),
                Type = TransactionType.Credit,
                Number = (byte)new Random().Next(1, 36),
                Password = "132456"   // 132456
            };

            try
            {
                List<TransactionModel> resultList = await TransactionService.SendTransaction(transactionModel);

                Assert.IsTrue(false);
            }
            catch (SendTransactionException E)
            {
                Assert.IsTrue(E.Message.Contains(StonePaymentResource.CardNumberNotNull));
            }
        }

        [TestMethod]
        public async Task TestSendTransactionWithFailAmountAtLeastTenCents()
        {
            var transactionModel = new TransactionModel
            {
                CardNumber = 1234654789324,
                Amount = 0.09,
                Type = TransactionType.Credit,
                Number = (byte)new Random().Next(1, 36),
                Password = "132456"  // 123456
            };

            try
            {
                List<TransactionModel> resultList = await TransactionService.SendTransaction(transactionModel);

                Assert.IsTrue(false);
            }
            catch (SendTransactionException E)
            {
                Assert.IsTrue(E.Message.Contains(StonePaymentResource.AmountAtLeastTenCents));
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
