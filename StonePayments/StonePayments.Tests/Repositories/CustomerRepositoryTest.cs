using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Server.Repositories;
using StonePayments.Business;
using LightInject;
using System.Collections.Generic;
using StonePayments.Util.Repositories;

namespace StonePayments.Server.Tests.Repository
{

    [TestClass]
    public class CustomerRepositoryTest : BaseTest
    {
        private IBaseCRUDRepository<CustomerModel> CustomerRepository { get; set; }

        public CustomerRepositoryTest()
        {
            this.CustomerRepository = serviceContainer.GetInstance<IBaseCRUDRepository<CustomerModel>>();
        }

        [TestMethod]
        public async Task TestInsertCustomer()
        {
            var r = new Random();

            var customerModel = new CustomerModel
            {
                Id = Guid.NewGuid(),
                Name = "Fulano de Tal",
                CreditLimit = Math.Round(r.NextDouble() + r.Next(1, 6000), 2)
            };

            try
            {
                await CustomerRepository.Insert(customerModel);

                Assert.IsTrue(true);
            }
            catch (CustomerException)
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task TestUpdateCustomer()
        {
            Random r = new Random();

            var customerModel = new CustomerModel
            {
                Id = Guid.Parse("86A8E8BC-1E31-4A69-A50B-770E2979200B"),
                Name = "Fulano de Tal",
                CreditLimit = Math.Round(r.NextDouble() + r.Next(1, 5000), 2)
            };

            try
            {
                await CustomerRepository.Update(customerModel);

                Assert.IsTrue(true);
            }
            catch (CustomerException)
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task TestDeleteCustomer()
        {
            Random r = new Random();

            var customerModel = new CustomerModel
            {
                Id = Guid.Parse("A0FD7EBD-88B7-4A33-ACAA-E0593276D05C"),
                Name = "Ronaldo Bogado",
                CreditLimit = Math.Round(r.NextDouble() + r.Next(1, 5000), 2)
            };

            try
            {
                await CustomerRepository.Delete(customerModel);

                Assert.IsTrue(true);
            }
            catch (CustomerException)
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task TestSelectCustomer()
        {
            Random r = new Random();

            var customerModel = new CustomerModel
            {
                Id = Guid.Parse("2E17B5F9-9857-40D7-A142-E6EAC31D252B"),
                Name = "Ronaldo Bogado",
                CreditLimit = Math.Round(r.NextDouble() + r.Next(1, 2000), 2)
            };

            try
            {
                List<CustomerModel> list = await CustomerRepository.Select(customerModel);

                Assert.IsTrue(true);
            }
            catch (CustomerException)
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task TestSelectAllCustomers()
        {
            try
            {
                List<CustomerModel> list = await CustomerRepository.Select();

                Assert.IsTrue(true);
            }
            catch (CustomerException)
            {
                Assert.IsFalse(true);
            }
        }
    }
}
