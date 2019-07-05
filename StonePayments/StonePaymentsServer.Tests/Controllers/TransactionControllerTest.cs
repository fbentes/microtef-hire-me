using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePaymentsServer;
using StonePaymentsServer.Controllers;

namespace StonePaymentsServer.Tests.Controllers
{
    [TestClass]
    public class TransactionControllerTest
    {
        [TestMethod]
        public async Task TestGetTransactions()
        {
            // Arrange
            TransactionController controller = new TransactionController();

            // Act
            IHttpActionResult result = await controller.GetTransactions() as IHttpActionResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
