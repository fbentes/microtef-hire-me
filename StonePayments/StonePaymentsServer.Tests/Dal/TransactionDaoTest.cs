using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePaymentsServer.Dal;
using StonePaymentsBusiness;

namespace StonePaymentsServer.Tests.Dal
{

    [TestClass]
    public class TransactionDaoTest
    {
        private TransactionDao dao;

        public TransactionDaoTest()
        {
            dao = new TransactionDao();
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
                await dao.SendTransaction(transactionModel);

                Assert.IsTrue(true);
            }
            catch (SendTransactionException E)
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task TestGetTransactions()
        {
            var result = await dao.GetTransactions();

            Assert.IsNotNull(result);
        }
    }
}
