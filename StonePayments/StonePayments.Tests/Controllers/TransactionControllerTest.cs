using System;
using System.Threading.Tasks;
using System.Web.Http;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Business;
using StonePayments.Server.Controllers;
using LightInject;

namespace StonePayments.Server.Tests.Controllers
{
    [MSTest.TestClass]
    public class TransactionControllerTest: BaseTest
    {
        public ITransactionController Controller { set; get; }

        public TransactionControllerTest()
        {
            Controller = serviceContainer.GetInstance<ITransactionController>();
        }

        [MSTest.TestMethod]
        public async Task TestGetTransactions()
        {
            // Act
            IHttpActionResult result = await Controller.GetTransactions() as IHttpActionResult;

            // Assert
            MSTest.Assert.IsNotNull(result);
        }

        [MSTest.TestMethod]
        public async Task TestSendTransactionSucess()
        {
            Random r = new Random();

            var transactionModel = new TransactionModel
                {                    
                    CardNumber = 1234654789324,
                    Amount = Math.Round(r.NextDouble() + r.Next(1, 100), 2),
                    Number = (byte)r.Next(1, 48),
                    Type = TransactionType.Credit,
                    Password = "132456"
            };

            // Act
            IHttpActionResult result = await Controller.SendTransaction(transactionModel) as IHttpActionResult;

            // Assert
            MSTest.Assert.IsNotNull(result);
        }
    }
}