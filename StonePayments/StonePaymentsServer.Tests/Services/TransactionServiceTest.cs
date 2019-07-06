using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePaymentsBusiness;
using LightInject;
using StonePaymentsServer.Services;

namespace StonePaymentsServer.Tests.Services
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
                Card = new CardModel
                {
                    Id = Guid.Parse("f4023d55-c15e-4f70-a8f3-0ac013d16bb6")                    
                },
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
