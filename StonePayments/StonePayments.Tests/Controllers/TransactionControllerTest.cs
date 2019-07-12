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
        public async Task TestSendTransaction()
        {

            var transactionModel = new TransactionModel
                {
                    CardNumber = 1234,
                    Amount = new Random().NextDouble(),
                    Number = (byte)new Random().Next(1, 48),
                    Type = TransactionType.Credit
                };

            //var json = JSONHelper.Serialize<TransactionModel>(transactionModel);


            // Act
            IHttpActionResult result = await Controller.SendTransaction(transactionModel) as IHttpActionResult;

            // Assert
            MSTest.Assert.IsNotNull(result);
        }
    }
}