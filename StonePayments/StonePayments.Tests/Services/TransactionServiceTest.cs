using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Business;
using LightInject;
using StonePayments.Server.Services;

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
        public async Task TestSendTransaction()
        {
            var transactionModel = new TransactionModel
            {
                Id = Guid.NewGuid(),                
                CardNumber = 1234,
                Amount = new Random().NextDouble(),
                Number = (byte)new Random().Next(1,36),
                Type = TransactionType.Credit
            };

            try
            {
                await TransactionService.SendTransaction(transactionModel);

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
