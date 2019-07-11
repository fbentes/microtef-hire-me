using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Server.Dal;
using StonePayments.Business;
using LightInject;

namespace StonePayments.Server.Tests.Dal
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
                CardNumber = 1234,
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
