using System;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Util;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePaymentsBusiness;
using StonePaymentsServer.Controllers;
using LightInject;
using Xunit;

namespace StonePaymentsServer.Tests.Controllers
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
                Id = Guid.NewGuid(),
                Card = new CardModel
                {
                    Id = Guid.Parse("f4023d55-c15e-4f70-a8f3-0ac013d16bb6")
                },
                Amount = new Random().NextDouble(),
                Number = (byte)new Random().Next(1, 36),
                Type = TransactionType.Credit
            };

            var json = JSONHelper.Serialize<TransactionModel>(transactionModel);


            // Act
            IHttpActionResult result = await Controller.SendTransaction(json) as IHttpActionResult;

            // Assert
            MSTest.Assert.IsNotNull(result);
        }
    }
}