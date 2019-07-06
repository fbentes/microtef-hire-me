using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePaymentsServer.Dal;
using StonePaymentsBusiness;
using LightInject;

namespace StonePaymentsServer.Tests.Dal
{

    [TestClass]
    public class TransactionDaoTest: BaseTest
    {
        private ITransactionDao TransactionDao { get; set; }

        public TransactionDaoTest()
        {
            this.TransactionDao = serviceContainer.GetInstance<ITransactionDao>();
        }

        [TestMethod]
        public async Task TestSendTransaction()
        {
            var transactionModel = new TransactionModel
            {
                Id = Guid.NewGuid(),                
                Card = new CardModel
                {
                    Id = Guid.Parse("f4023d55-c15e-4f70-a8f3-0ac013d16bb6"),        
                    ExpirationDate = DateTime.Now
                },
                Amount = new Random().NextDouble(),
                Number = (byte)new Random().Next(1,36),
                Type = TransactionType.Credit
            };

            try
            {
                await TransactionDao.SendTransaction(transactionModel);

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
