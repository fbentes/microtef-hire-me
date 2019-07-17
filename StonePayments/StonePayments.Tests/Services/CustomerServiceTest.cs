using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Server.Services;
using StonePayments.Server.Tests;
using System.Threading.Tasks;
using LightInject;
using StonePayments.Business;
using System;
using StonePayments.Util.Services;

namespace StonePaymentsServer.Tests.Services
{
    [TestClass]
    public class CustomerServiceTest : BaseTest
    {
        public IBaseCRUDServiceBridge<CustomerModel> CustomerService { get; set; }

        public CustomerServiceTest()
        {
            this.CustomerService = serviceContainer.GetInstance<IBaseCRUDServiceBridge<CustomerModel>>();
        }

        [TestMethod]
        public async Task InsertCustomerSucess()
        {
            var r = new Random();

            var customerModel = new CustomerModel
            {
                Id = Guid.NewGuid(),
                Name = "Ronaldo André Bogado Júnior",
                CreditLimit = Math.Round(r.NextDouble() + r.Next(1, 5000), 2)
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
        public async Task InsertCustomerFailNameIsNull()
        {
            var customerModel = new CustomerModel
            {
                Id = Guid.NewGuid(),
                Name = null,
                CreditLimit = 5000
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

        [TestMethod]
        public async Task InsertCustomerFailCreditLimitIsNegative()
        {
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
