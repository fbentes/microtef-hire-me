using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Server.Services;
using StonePayments.Server.Tests;
using System.Threading.Tasks;
using LightInject;
using StonePayments.Business;
using System;

namespace StonePaymentsServer.Tests.Services
{
    [TestClass]
    public class CustomerServiceTest : BaseTest
    {
        public ICustomerService CustomerService { get; set; }

        public CustomerServiceTest()
        {
            this.CustomerService = serviceContainer.GetInstance<ICustomerService>();
        }

        [TestMethod]
        public async Task InsertCustomerSucess()
        {
            var r = new Random();

            var customerModel = new CustomerModel
            {
                Id = Guid.NewGuid(),
                Name = "Ronaldo Bogado",
                CreditLimit = Math.Round(r.NextDouble() + r.Next(1, 100), 2)
            };

            try
            {
                await CustomerService.Insert(customerModel);

                Assert.IsTrue(true);
            }
            catch (CustomerException)
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task InsertCustomerSomeFail()
        {
            var r = new Random();

            var customerModel = new CustomerModel
            {
                Id = Guid.NewGuid(),
                Name = null,
                CreditLimit = -1
            };

            try
            {
                await CustomerService.Insert(customerModel);

                Assert.IsTrue(false);
            }
            catch (CustomerException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
